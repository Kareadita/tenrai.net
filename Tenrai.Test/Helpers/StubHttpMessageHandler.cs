using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tenrai.Config;

namespace Tenrai.Tests.Helpers;

/// <summary>
/// Answers every request with a canned response and records what was asked, so client tests run without the network.
/// </summary>
public sealed class StubHttpMessageHandler : HttpMessageHandler
{
	public const string EmptyPage = "{\"pagination\":{\"last_visible_page\":1,\"has_next_page\":false},\"data\":[]}";

	public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

	public string Body { get; set; } = EmptyPage;

	public string LastQuery { get; private set; }

	public int RequestCount { get; private set; }

	public string[] LastServerKeys { get; private set; }

	public TenraiClient CreateClient(TenraiClientConfiguration configuration) => new(configuration, CreateHttpClient());

	public HttpClient CreateHttpClient() => new(this) { BaseAddress = new Uri("https://api.tenrai.org/v1/") };

	protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		RequestCount++;
		LastQuery = request.RequestUri.Query;
		LastServerKeys = request.Headers.TryGetValues("X-Server-Key", out var keys) ? keys.ToArray() : Array.Empty<string>();
		return Task.FromResult(new HttpResponseMessage(StatusCode) { Content = new StringContent(Body, Encoding.UTF8, "application/json") });
	}
}
