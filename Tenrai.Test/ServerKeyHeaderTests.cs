using System.Threading.Tasks;
using FluentAssertions;
using Tenrai.Config;
using Tenrai.Tests.Helpers;
using Xunit;

namespace Tenrai.Tests;

public class ServerKeyHeaderTests
{
	[Fact]
	public async Task SuppliedHttpClient_WithServerKey_SendsServerKeyHeader()
	{
		var handler = new StubHttpMessageHandler();
		var client = handler.CreateClient(Configuration("my-key"));

		await client.SearchAnimeAsync("berserk");

		handler.LastServerKeys.Should().Equal("my-key");
	}

	[Fact]
	public async Task SuppliedHttpClient_AlreadyCarryingServerKey_KeepsTheCallersKey()
	{
		var handler = new StubHttpMessageHandler();
		var httpClient = handler.CreateHttpClient();
		httpClient.DefaultRequestHeaders.Add("X-Server-Key", "callers-key");
		var client = new TenraiClient(Configuration("config-key"), httpClient);

		await client.SearchAnimeAsync("berserk");

		handler.LastServerKeys.Should().Equal("callers-key");
	}

	[Fact]
	public async Task SuppliedHttpClient_WithoutServerKey_SendsNoServerKeyHeader()
	{
		var handler = new StubHttpMessageHandler();
		var client = handler.CreateClient(Configuration(null));

		await client.SearchAnimeAsync("berserk");

		handler.LastServerKeys.Should().BeEmpty();
	}

	private static TenraiClientConfiguration Configuration(string serverKey) =>
		new() { ServerKey = serverKey, LimiterConfigurations = TaskLimiterConfiguration.None };
}
