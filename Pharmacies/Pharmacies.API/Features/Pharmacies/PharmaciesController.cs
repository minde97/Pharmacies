using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pharmacies.API.Features.Pharmacies.GetPharmacies;
using Pharmacies.API.Features.Pharmacies.ImportPharmacies;
using Pharmacies.API.Features.Pharmacies.UpdatePostCodes;

namespace Pharmacies.API.Features.Pharmacies;

[ApiController]
[Route("api/[controller]")]
public class PharmaciesController(IMediator mediator) : ControllerBase
{
	[HttpGet]
	public async Task<GetPharmaciesResponse> GetPharmacies(CancellationToken cancellationToken)
	{
		return await mediator.Send(new GetPharmaciesRequest(), cancellationToken);
	}

	[HttpPost("import")]
	public async Task<ImportPharmaciesResponse> ImportPharmacies(IFormFile file, CancellationToken cancellationToken)
	{
		return await mediator.Send(new ImportPharmaciesRequest() { File = file }, cancellationToken);
	}

	[HttpPost("update-post-codes")]
	public async Task UpdatePostCodes(CancellationToken cancellationToken)
	{
		await mediator.Send(new UpdatePostCodesRequest(), cancellationToken);
	}
}
