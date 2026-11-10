using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.MangaTests;

[Collection("TenraiTests")]
[Trait("Category", "Integration")]
public class GetMangaRelationsAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetMangaRelationsAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetMangaRelationsAsync_InvalidId_ShouldThrowValidationException(long id)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetMangaRelationsAsync(id));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetMangaRelationsAsync_MonsterId_ShouldParseMonsterRelations()
	{
		// When
		var monster = await _tenrai.GetMangaRelationsAsync(1, new SfwConfig { Sfw = false });

		// Then
		using var _ = new AssertionScope();
		monster.Data.Should().HaveCount(2);
		monster.Data.Should().ContainSingle(x => x.Relation.Equals("Adaptation") && x.Entry.Count == 1);
		monster.Data.Should().ContainSingle(x => x.Relation.Equals("Side Story") && x.Entry.Count == 1);
	}

	[Fact]
	public async Task GetMangaRelationsAsync_MonsterIdDefaultClient_ShouldLeaveOutAdultAdaptation()
	{
		// When
		var monster = await _tenrai.GetMangaRelationsAsync(1);

		// Then
		monster.Data.Should().ContainSingle().Which.Relation.Should().Be("Side Story");
	}
}