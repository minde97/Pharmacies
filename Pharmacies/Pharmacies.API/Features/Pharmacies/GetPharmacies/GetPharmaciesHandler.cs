using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PharmaciesDbContext = Pharmacies.API.Database.PharmaciesDbContext;

namespace Pharmacies.API.Features.Pharmacies.GetPharmacies;

public class GetPharmaciesHandler(PharmaciesDbContext dbContext) : IRequestHandler<GetPharmaciesRequest, GetPharmaciesResponse>
{

	public Task<GetPharmaciesResponse> Handle(GetPharmaciesRequest request, CancellationToken cancellationToken)
	{
		return this.GetClients();
	}

	private async Task<GetPharmaciesResponse> GetClients()
	{
		var pharmacies = await dbContext.Pharmacies.ToListAsync();

		var clients = pharmacies.Select(p => new PharmacyListItem
			{
				Id = p.Id, Name = p.Name, Address = p.Address, PostCode = p.PostCode
			})
			.ToList();

		return new GetPharmaciesResponse(clients);
	}
}
