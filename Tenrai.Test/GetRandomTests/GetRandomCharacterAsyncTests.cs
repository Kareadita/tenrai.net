using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.GetRandomTests
{
	[Collection("TenraiTests")]
	public class GetRandomCharacterAsyncTests
	{
		private readonly ITenrai _tenrai;

		public GetRandomCharacterAsyncTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Fact]
		public async Task GetRandomCharacterAsync_ShouldReturnNotNullCharacter()
		{
			// When
			var character = await _tenrai.GetRandomCharacterAsync();

			// Then
			character.Data.Should().NotBeNull();
		}
	}
}