namespace Pharmacies.Database.Entities;

public class Pharmacy
{
	public int Id { get; set; }

	public string Name { get; set; }

	public string Address { get; set; }

	public string? PostCode { get; set; }
}
