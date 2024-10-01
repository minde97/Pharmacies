using System.ComponentModel.DataAnnotations;

namespace Pharmacies.API.Features.Pharmacies.GetPharmacies;

public class GetPharmaciesResponse(List<PharmacyListItem> pharmacies)
{
	[Required]
	public List<PharmacyListItem> Pharmacies { get; set; } = pharmacies;
}
