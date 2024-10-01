using System.Net.Http.Json;

namespace Pharmacies.PostIt.Client;

public class PostItClient(HttpClient httpClient, string key) : IPostItClient
{
	public async Task<string> GetPostCode(string address, CancellationToken cancellationToken)
	{
		if (string.IsNullOrWhiteSpace(address))
		{
			return string.Empty;
		}

		var requestUri = $"?term={address.Replace(' ', '+')}&key={key}";
		var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

		try
		{
			var response = await httpClient.SendAsync(request, cancellationToken);

			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadFromJsonAsync<PostItClientResponse>(cancellationToken: cancellationToken);

			if (content != null && !string.IsNullOrWhiteSpace(content.Data.FirstOrDefault()?.PostCode))
			{
				return content.Data.FirstOrDefault()?.PostCode ?? string.Empty;
			}

			return string.Empty;
		}
		catch
		{
			return string.Empty;
		}
	}
}

