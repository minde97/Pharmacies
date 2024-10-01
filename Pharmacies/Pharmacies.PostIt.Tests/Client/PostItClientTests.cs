using System.Net;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace Pharmacies.PostItClient.Tests.Client;

[TestClass]
public class PostItClientTests
{
	private HttpClient httpClient;

	[TestMethod]
	public async Task GetPostCode_ReturnsEmptyString_WhenAddressIsEmpty()
	{
		// Arrange
		var address = string.Empty;
		var key = "someKey";
		var postItClient = new PostIt.Client.PostItClient(this.httpClient, key);
		var expectedResult = string.Empty;

		// Act
		var actualResult = await postItClient.GetPostCode(address, default);

		// Assert
		actualResult.Should().Be(expectedResult);
	}

	[TestMethod]
	public async Task GetPostCode_RetursPostCode_WhenAddressIsProvided()
	{
		// Arrange
		var address = "Savanorių pr.12, Vilniaus";
		var key = "someKey";
		this.MockHttpClient();
		var postItClient = new PostIt.Client.PostItClient(this.httpClient, key);
		var expectedResult = "03116";

		// Act
		var actualResult = await postItClient.GetPostCode(address, default);

		// Assert
		actualResult.Should().Be(expectedResult);
	}

	private void MockHttpClient()
	{
		var mockMessageHandler = new Mock<HttpMessageHandler>();
		var responseContent = "{\"status\":\"success\",\"success\":true,\"message\":\"\",\"message_code\":0,"
		                      + "\"total\":1,\"data\":[{\"post_code\":\"03116\",\"address\":\"Savanorių pr.12\",\"street\":\"Savanorių pr.\","
		                      + "\"number\":\"12\",\"only_number\":\"12\",\"housing\":\"\",\"city\":\"Vilnius\",\"eldership\":\"\","
		                      + "\"municipality\":\"Vilniaus m.sav.\",\"post\":\"Vilniaus 59-asis paštas\",\"mailbox\":\"\"}]}";

		mockMessageHandler.Protected()
			.Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
			.ReturnsAsync(new HttpResponseMessage
			{
				StatusCode = HttpStatusCode.OK, Content = new StringContent(responseContent)
			});

		httpClient = new HttpClient(mockMessageHandler.Object)
		{
			BaseAddress = new Uri("http://localhost:5000"),
		};

	}
}
