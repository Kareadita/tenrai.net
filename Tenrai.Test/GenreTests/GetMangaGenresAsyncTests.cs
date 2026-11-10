using FluentAssertions;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.GenreTests;

[Collection("TenraiTests")]
[Trait("Category", "Integration")]
public class GetMangaGenresAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetMangaGenresAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Fact]
	public async Task GetMangaGenresAsync_NoParameters_ShouldParseAllAvailableGenres()
	{
		// Given
		const int expectedGenreCount = 77;

		// When
		var result = await _tenrai.GetMangaGenresAsync();

		// Then
		result.Data.Should().HaveCount(expectedGenreCount);
	}

	[Theory]
	[InlineData(GenresFilter.Genres, 18)]
	[InlineData(GenresFilter.ExplicitGenres, 1)]
	[InlineData(GenresFilter.Themes, 53)]
	[InlineData(GenresFilter.Demographics, 5)]
	public async Task GetMangaGenresAsync_WithFilter_ShouldParseFilteredGenres(GenresFilter filter, int expectedGenreCount)
	{
		// When
		var result = await _tenrai.GetMangaGenresAsync(filter);

		// Then
		result.Data.Should().HaveCount(expectedGenreCount);
	}

	[Theory]
	[InlineData((GenresFilter)int.MaxValue)]
	[InlineData((GenresFilter)int.MinValue)]
	public async Task GetMangaGenresAsync_InvalidFilter_ShouldThrowValidationException(GenresFilter filter)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetMangaGenresAsync(filter));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}
}