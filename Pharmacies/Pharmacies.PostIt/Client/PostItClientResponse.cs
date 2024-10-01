using System.Text.Json.Serialization;

namespace Pharmacies.PostIt.Client;

public class PostItClientResponse
{
	public string Status { get; set; }

	public bool Success { get; set; }

	public string Message { get; set; }

	[JsonPropertyName("message_code")]
	public int MessageCode { get; set; }

	public int Total { get; set; }

	public List<AddressData> Data { get; set; }
}
