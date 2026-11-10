using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.AnimeTests;

[Collection("TenraiTests")]
[Trait("Category", "Integration")]
public class GetAnimeStaffAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetAnimeStaffAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetAnimeStaffAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetAnimeStaffAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetAnimeStaffAsync_BebopId_ShouldParseCowboyBebopStaff()
	{
		// When
		var bebop = await _tenrai.GetAnimeStaffAsync(1);

		// Then
		bebop.Data.Should().Contain(x => x.Person.Name.Equals("Watanabe, Shinichirou"));
	}

	[Fact]
	public async Task GetAnimeStaffAsync_BebopId_ShouldParseShinichiroWatanabeDetails()
	{
		// When
		var bebop = await _tenrai.GetAnimeStaffAsync(1);

		// Then
		var shinichiroWatanabe = bebop.Data.First(x => x.Person.Name.Equals("Watanabe, Shinichirou"));
		using (new AssertionScope())
		{
			shinichiroWatanabe.Position.Should().HaveCount(4);
			shinichiroWatanabe.Position.Should().Contain("Director");
			shinichiroWatanabe.Position.Should().Contain(x => x.StartsWith("Script"));
			shinichiroWatanabe.Person.ParsedName.FirstName.Should().Be("Shinichirou");
			shinichiroWatanabe.Person.ParsedName.LastName.Should().Be("Watanabe");
			shinichiroWatanabe.Person.MalId.Should().Be(2009);
		}
	}

	[Fact]
	public async Task GetAnimeStaffAsync_BebopId_ShouldParseShinichiroWatanabePictures()
	{
		// When
		var bebop = await _tenrai.GetAnimeStaffAsync(1);

		// Then
		var shinichiroWatanabe = bebop.Data.First(x => x.Person.MalId == 2009);
		shinichiroWatanabe.Person.Images.JPG.ImageUrl.Should().NotBeNullOrEmpty();
	}
}