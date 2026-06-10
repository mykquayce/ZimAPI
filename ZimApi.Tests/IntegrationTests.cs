using Microsoft.AspNetCore.Mvc.Testing;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace ZimApi.Tests;

public class IntegrationTests(WebApplicationFactory<Program> factory)
	: IClassFixture<WebApplicationFactory<Program>>
{
	private static readonly WebApplicationFactoryClientOptions _options = new() { AllowAutoRedirect = false, };
	private readonly HttpClient _httpClient = factory.CreateClient(_options);

	[Theory]
	[InlineData("/healthz")]
	public async Task HealthCheckTests(string url)
	{
		// Arrange
		var cts = new CancellationTokenSource(millisecondsDelay: 1_000);

		// Act
		var response = await _httpClient.GetAsync(url, cts.Token);

		// Assert
		var statusCode = response.StatusCode;
		var contentType = response.Content.Headers.ContentType?.ToString();
		var content = await response.Content.ReadAsStringAsync(cts.Token);

		Assert.Equal(HttpStatusCode.OK, statusCode);
		Assert.Equal("text/plain", contentType, StringComparer.Ordinal);
		Assert.Equal("Healthy", content, StringComparer.Ordinal);
	}

	[SuppressMessage("Usage", "xUnit1004:Test methods should not be skipped", Justification = "requires third-party")]
	[Theory(Skip = "requires third-party")]
	[InlineData("/library")]
	public async Task LibraryTests(string url)
	{
		// Arrange
		var cts = new CancellationTokenSource(millisecondsDelay: 10_000);

		// Act
		var response = await _httpClient.GetAsync(url, cts.Token);

		// Assert
		var statusCode = response.StatusCode;
		var contentType = response.Content.Headers.ContentType?.ToString();
		var content = await response.Content.ReadAsStringAsync(cts.Token);

		Assert.Equal(HttpStatusCode.OK, statusCode);
		Assert.Equal("application/json; charset=utf-8", contentType, StringComparer.Ordinal);
		Assert.InRange(content.Length, 200_000, 2_000_000);
		Assert.StartsWith("[", content);
	}

	[SuppressMessage("Usage", "xUnit1004:Test methods should not be skipped", Justification = "requires third-party")]
	[Theory(Skip = "requires third-party")]
	[InlineData("/MetaData?uri=https%3A%2F%2Flbo.download.kiwix.org%2Fzim%2Fwikipedia%2Fwikipedia_en_all_maxi_2026-02.zim.meta4")]
	public async Task MetaDataTests(string url)
	{
		// Arrange
		var cts = new CancellationTokenSource(millisecondsDelay: 10_000);

		// Act
		var response = await _httpClient.GetAsync(url, cts.Token);

		// Assert
		var statusCode = response.StatusCode;
		var contentType = response.Content.Headers.ContentType?.ToString();
		var content = await response.Content.ReadAsStringAsync(cts.Token);

		Assert.Equal(HttpStatusCode.OK, statusCode);
		Assert.Equal("application/json; charset=utf-8", contentType, StringComparer.Ordinal);
		Assert.InRange(content.Length, 500, 5_000);
		Assert.StartsWith("[", content);
	}
}
