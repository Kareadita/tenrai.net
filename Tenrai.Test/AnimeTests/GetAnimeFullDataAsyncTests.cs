using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Tenrai;
using Xunit;

namespace Tenrai.Tests.AnimeTests
{
    [Collection("TenraiTests")]
    public class GetAnimeFullDataAsyncTests
    {
        private readonly ITenrai _tenrai;

        public GetAnimeFullDataAsyncTests(TenraiFixture tenraiFixture)
        {
            _tenrai = tenraiFixture.TenraiClient;
        }

        [Theory]
        [InlineData(long.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetAnimeFullDataAsync_InvalidId_ShouldThrowValidationException(long malId)
        {
            // When
            var func = _tenrai.Awaiting(x => x.GetAnimeFullDataAsync(malId));

            // Then
            await func.Should().ThrowExactlyAsync<TenraiValidationException>();
        }


        [Fact]
        public async Task GetAnimeFullDataAsync_BebopId_ShouldParseCowboyBebop()
        {
            // When
            var bebopAnime = await _tenrai.GetAnimeFullDataAsync(1);

            // Then
            using var _ = new AssertionScope();
            bebopAnime.Data.Title.Should().Be("Cowboy Bebop");
            bebopAnime.Data.ExternalLinks.Should().Contain(x =>
                x.Name.Equals("Wikipedia") && x.Url.Equals("http://en.wikipedia.org/wiki/Cowboy_Bebop"));
            bebopAnime.Data.MusicThemes.Openings.Should().ContainSingle().Which
                .Equals("\"Tank!\" by The Seatbelts (eps 1-25)");
            bebopAnime.Data.MusicThemes.Endings.Should().HaveCount(3);
            bebopAnime.Data.Relations.Should().HaveCount(3);
            bebopAnime.Data.Relations.Should()
                .ContainSingle(x => x.Relation.Equals("Adaptation") && x.Entry.Count == 2);
            bebopAnime.Data.Relations.Should()
                .ContainSingle(x => x.Relation.Equals("Side story") && x.Entry.Count == 2);
            bebopAnime.Data.Relations.Should().ContainSingle(x => x.Relation.Equals("Summary") && x.Entry.Count == 1);
            bebopAnime.Data.StreamingLinks.Should().Contain(x => x.Name.Equals("Crunchyroll") && x.Url.Equals("http://www.crunchyroll.com/series-271225"));
            bebopAnime.Data.StreamingLinks.Should().Contain(x => x.Name.Equals("Netflix") && x.Url.Equals("https://www.netflix.com/title/80001305"));
        }
    }
}