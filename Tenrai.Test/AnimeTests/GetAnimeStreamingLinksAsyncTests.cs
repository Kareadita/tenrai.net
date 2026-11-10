using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.AnimeTests;

[Collection("TenraiTests")]
[Trait("Category", "Integration")]
public class GetAnimeStreamingLinksAsyncTests
{

	private readonly ITenrai _tenrai;

	public GetAnimeStreamingLinksAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetAnimeStreamingLinksAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetAnimeStreamingLinksAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetAnimeStreamingLinksAsync_BebopId_ShouldReturnBebopLinks()
	{
		// When
		var links = await _tenrai.GetAnimeStreamingLinksAsync(1);

		// Then
		using var _ = new AssertionScope();
		links.Data.Should().HaveCountGreaterOrEqualTo(2);
		links.Data.Should().Contain(x => x.Name.Equals("Crunchyroll") && x.Url.Contains("crunchyroll.com"));
		links.Data.Should().Contain(x => x.Name.Equals("Netflix") && x.Url.Contains("netflix.com%2Ftitle%2F80001305"));
	}
}