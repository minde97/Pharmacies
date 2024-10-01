using MediatR;
using Microsoft.EntityFrameworkCore;
using Pharmacies.API.Database;
using Pharmacies.PostIt.Client;

namespace Pharmacies.API.Features.Pharmacies.UpdatePostCodes;

public class UpdatePostCodesHandler(PharmaciesDbContext pharmaciesDbContext, IPostItClient postItClient) : IRequestHandler<UpdatePostCodesRequest>
{
	private readonly PharmaciesDbContext pharmaciesDbContext = pharmaciesDbContext;
	private readonly IPostItClient postItClient = postItClient;

	public async Task Handle(UpdatePostCodesRequest request, CancellationToken cancellationToken)
	{
		await this.UpdatePharmacyPostCode(cancellationToken);
	}

	private async Task UpdatePharmacyPostCode(CancellationToken cancellationToken)
	{
		var pharmacies = await this.pharmaciesDbContext.Pharmacies.ToListAsync(cancellationToken);

		foreach (var pharmacy in pharmacies.Where(pharmacy => !string.IsNullOrEmpty(pharmacy.Address)))
		{
			var newPostCode = await this.postItClient.GetPostCode(pharmacy.Address, cancellationToken);

			if (!string.IsNullOrEmpty(newPostCode))
			{
				pharmacy.PostCode = newPostCode;
			}
		}

		await this.pharmaciesDbContext.SaveChangesAsync(cancellationToken);
	}
}
