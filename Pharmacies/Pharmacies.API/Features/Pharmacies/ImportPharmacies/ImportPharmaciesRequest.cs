using MediatR;

namespace Pharmacies.API.Features.Pharmacies.ImportPharmacies;

public class ImportPharmaciesRequest : IRequest<ImportPharmaciesResponse>
{
	public IFormFile File { get; set; }
}
