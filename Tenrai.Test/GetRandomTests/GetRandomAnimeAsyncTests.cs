using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.GetRandomTests
{
	[Collection("TenraiTests")]
	public class GetRandomAnimeAsyncTests
	{
		private readonly ITenrai _tenrai;

		public GetRandomAnimeAsyncTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Fact]
		public async Task GetRandomAnimeAsync_ShouldReturnNotNullAnime()
		{
			// When
			var anime = await _tenrai.GetRandomAnimeAsync();

			// Then
			anime.Data.Should().NotBeNull();
		}
	}
}