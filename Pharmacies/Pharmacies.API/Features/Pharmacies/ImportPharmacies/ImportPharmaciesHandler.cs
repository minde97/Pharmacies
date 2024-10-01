using System.Text.Json;
using MediatR;
using Pharmacies.API.Features.Pharmacies.GetPharmacies;
using Pharmacies.Database.Entities;
using PharmaciesDbContext = Pharmacies.API.Database.PharmaciesDbContext;

namespace Pharmacies.API.Features.Pharmacies.ImportPharmacies;

public class ImportPharmaciesHandler(PharmaciesDbContext dbContext) : IRequestHandler<ImportPharmaciesRequest, ImportPharmaciesResponse>
{
	private readonly PharmaciesDbContext dbContext = dbContext;

	public async Task<ImportPharmaciesResponse> Handle(ImportPharmaciesRequest request, CancellationToken cancellationToken)
	{
		return await this.ImportPharmaciesFromFile(request, cancellationToken);
	}

	private async Task<ImportPharmaciesResponse> ImportPharmaciesFromFile(ImportPharmaciesRequest request, CancellationToken cancellationToken)
	{
		var pharmaciesResponse = new ImportPharmaciesResponse();
		using var streamReader = new StreamReader(request.File.OpenReadStream());
		var fileContents = await streamReader.ReadToEndAsync(cancellationToken);

		var pharmaciesFromFile = JsonSerializer.Deserialize<List<PharmacyListItem>>(fileContents);

		if (pharmaciesFromFile == null || pharmaciesFromFile.Count == 0)
		{
			return new ImportPharmaciesResponse();
		}

		var pharmacyNames = this.dbContext.Pharmacies.Select(x => x.Name).ToList();
		var uniquePharmaciesFromFile = pharmaciesFromFile.GroupBy(x => x.Name).Select(x => x.First()).ToList();

		var newPharmacies = uniquePharmaciesFromFile.Where(x => !pharmacyNames.Contains(x.Name))
			.Select(x => new Pharmacy
			{
				Name = x.Name,
				Address = x.Address,
				PostCode = x.PostCode,
			}).ToList();

		await this.dbContext.AddRangeAsync(newPharmacies, cancellationToken);
		await this.dbContext.SaveChangesAsync(cancellationToken);

		return new ImportPharmaciesResponse() { PharmaciesUploaded = newPharmacies.Count };
	}
}
