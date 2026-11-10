using System.Threading.Tasks;
using FluentAssertions;
using Tenrai.Config;
using Tenrai.Tests.Helpers;
using Xunit;

namespace Tenrai.Tests.SearchConfigTests;

public class ClientSfwTests
{
	[Fact]
	public async Task DefaultConfiguration_PlainSearch_SendsSfw()
	{
		var handler = new StubHttpMessageHandler();
		var client = handler.CreateClient(new TenraiClientConfiguration { LimiterConfigurations = TaskLimiterConfiguration.None });

		await client.SearchAnimeAsync("berserk");

		handler.LastQuery.Should().Be("?q=berserk&sfw=true");
	}

	[Fact]
	public async Task ClientSfwOn_PlainSeasonCall_SendsSfw()
	{
		var handler = new StubHttpMessageHandler();
		var client = handler.CreateClient(Configuration(sfw: true));

		await client.GetCurrentSeasonAsync();

		handler.LastQuery.Should().Be("?sfw=true");
	}

	[Fact]
	public async Task ClientSfwOn_ConfigTurnsItOff_SendsNoSfw()
	{
		var handler = new StubHttpMessageHandler();
		var client = handler.CreateClient(Configuration(sfw: true));

		await client.SearchAnimeAsync(new AnimeSearchConfig { Query = "berserk", Sfw = false });

		handler.LastQuery.Should().Be("?q=berserk");
	}

	[Fact]
	public async Task ClientSfwOff_ConfigTurnsItOn_SendsSfw()
	{
		var handler = new StubHttpMessageHandler();
		var client = handler.CreateClient(Configuration(sfw: false));

		await client.SearchAnimeAsync(new AnimeSearchConfig { Query = "berserk", Sfw = true });

		handler.LastQuery.Should().Be("?q=berserk&sfw=true");
	}

	[Fact]
	public async Task ClientSfwOff_PlainCall_SendsNoSfw()
	{
		var handler = new StubHttpMessageHandler();
		var client = handler.CreateClient(Configuration(sfw: false));

		await client.SearchAnimeAsync("berserk");

		handler.LastQuery.Should().Be("?q=berserk");
	}

	[Fact]
	public async Task ClientSfwOn_ConfigWithoutSfwParam_SendsNoSfw()
	{
		var handler = new StubHttpMessageHandler();
		var client = handler.CreateClient(Configuration(sfw: true));

		await client.GetAnimeForumTopicsAsync(1, new AnimeForumSearchConfig { Page = 2 });

		handler.LastQuery.Should().Be("?page=2");
	}

	[Fact]
	public async Task ClientSfwOn_PlainSubList_SendsSfw()
	{
		var handler = new StubHttpMessageHandler();
		var client = handler.CreateClient(Configuration(sfw: true));

		await client.GetPersonVoiceActingRolesAsync(1);

		handler.LastQuery.Should().Be("?sfw=true");
	}

	[Fact]
	public async Task ClientSfwOn_SubListConfigTurnsItOff_SendsNoSfw()
	{
		var handler = new StubHttpMessageHandler();
		var client = handler.CreateClient(Configuration(sfw: true));

		await client.GetPersonVoiceActingRolesAsync(1, new SfwConfig { Sfw = false });

		handler.LastQuery.Should().BeEmpty();
	}

	[Fact]
	public async Task ClientSfwOn_PlainInterestStack_SendsSfw()
	{
		var handler = new StubHttpMessageHandler();
		var client = handler.CreateClient(Configuration(sfw: true));
		handler.Body = "{\"data\":null}";

		await client.GetInterestStackAsync(1);

		handler.LastQuery.Should().Be("?sfw=true");
	}

	[Fact]
	public async Task ClientSfwOn_AnimeIdEntries_SendsSfwAndInclude()
	{
		var handler = new StubHttpMessageHandler();
		var client = handler.CreateClient(Configuration(sfw: true));

		await client.GetAnimeIdEntriesAsync(new AnimeIdsConfig { Types = { AnimeType.Movie } });

		handler.LastQuery.Should().Be("?type=Movie&sfw=true&include=title,type");
	}

	private static TenraiClientConfiguration Configuration(bool sfw)
		=> new() { Sfw = sfw, LimiterConfigurations = TaskLimiterConfiguration.None };
}
