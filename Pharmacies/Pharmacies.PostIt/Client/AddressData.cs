using System.Text.Json.Serialization;

namespace Pharmacies.PostIt.Client;

public class AddressData
{
	[JsonPropertyName("post_code")]
	public string PostCode { get; set; }

	public string Address { get; set; }

	public string Street { get; set; }

	public int Number { get; set; }

	[JsonPropertyName("only_number")]
	public int OnlyNumber { get; set; }

	public string Housing { get; set; }

	public string City { get; set; }

	public string Eldership { get; set; }

	public string Municipality { get; set; }

	public string Post { get; set; }

	public string Mailbox { get; set; }
}
