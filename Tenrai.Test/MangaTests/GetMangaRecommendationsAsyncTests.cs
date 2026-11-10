using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.MangaTests;

[Collection("TenraiTests")]
public class GetMangaRecommendationsAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetMangaRecommendationsAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetMangaRecommendationsAsync_InvalidId_ShouldThrowValidationException(long id)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetMangaRecommendationsAsync(id));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetMangaRecommendationsAsync_BerserkId_ShouldParseBerserkRecommendations()
	{
		// When
		var berserk = await _tenrai.GetMangaRecommendationsAsync(2);

		// Then
		using (new AssertionScope())
		{
			berserk.Data.Should().BeInDescendingOrder(x => x.Votes);
			berserk.Data.Should().Contain(x => x.Entry.MalId == 656);
			berserk.Data.First().Votes.Should().BeGreaterThan(25);
			berserk.Data.Count.Should().BeGreaterThan(90);
		}
	}
}