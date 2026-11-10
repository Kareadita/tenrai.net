using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Tenrai.Tests.ModelTests;

public class EntityNameTests
{
	[Theory]
	[InlineData("Kurosaki, Ichigo", "Ichigo", "Kurosaki", "Ichigo Kurosaki")]
	[InlineData("Monkey D., Luffy", "Luffy", "Monkey D.", "Luffy Monkey D.")]
	[InlineData("Lupin VIII, Arsène", "Arsène", "Lupin VIII", "Arsène Lupin VIII")]
	[InlineData("Megumin", "Megumin", null, "Megumin")]
	[InlineData("Kana, ", "Kana,", null, "Kana,")]
	[InlineData("", null, null, null)]
	[InlineData(null, null, null, null)]
	public void ParsedName_FamilyGivenName_SplitsOnFirstComma(string name, string first, string last, string full)
	{
		var parsed = new Character { Name = name }.ParsedName;

		using var _ = new AssertionScope();
		parsed.FirstName.Should().Be(first);
		parsed.LastName.Should().Be(last);
		parsed.FullName.Should().Be(full);
	}

	[Fact]
	public void ParsedName_EveryPersonAndCharacterType_HasIt()
	{
		const string name = "Watanabe, Shinichirou";

		using var _ = new AssertionScope();
		new Person { Name = name }.ParsedName.FirstName.Should().Be("Shinichirou");
		new PersonEntry { Name = name }.ParsedName.FirstName.Should().Be("Shinichirou");
		new CharacterEntry { Name = name }.ParsedName.FirstName.Should().Be("Shinichirou");
		new AuthorEntry { Name = name }.ParsedName.FirstName.Should().Be("Shinichirou");
		new PersonUrl { Name = name }.ParsedName.FirstName.Should().Be("Shinichirou");
		new CharacterUrl { Name = name }.ParsedName.FirstName.Should().Be("Shinichirou");
	}
}
