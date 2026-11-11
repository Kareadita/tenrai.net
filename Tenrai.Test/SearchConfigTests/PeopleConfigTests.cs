using FluentAssertions;
using Xunit;

namespace Tenrai.Tests.SearchConfigTests;

public class PeopleConfigTests
{
	[Fact]
	public void CharacterSearchConfig_BebopMainCastByFavorites_BuildsQuery()
	{
		var config = new CharacterSearchConfig
		{
			AnimeIds = { 1 },
			Role = CharacterRole.Main,
			OrderBy = CharacterSearchOrderBy.Favorites,
			SortDirection = SortDirection.Descending
		};

		config.ConfigToString().Should().Be("?anime=1&role=main&order_by=favorites&sort=desc");
	}

	[Fact]
	public void CharacterSearchConfig_UpcomingFall2026_BuildsQuery()
	{
		var config = new CharacterSearchConfig
		{
			Upcoming = true,
			Publishing = false,
			Seasons = { Season.Fall },
			SeasonYears = { 2026 }
		};

		config.ConfigToString().Should().Be("?upcoming=true&publishing=false&season=fall&season_year=2026");
	}

	[Fact]
	public void PersonSearchConfig_BerserkStoryAuthorsByMangaCredits_BuildsQuery()
	{
		var config = new PersonSearchConfig
		{
			MangaIds = { 2 },
			Credits = { PersonCredit.Author },
			AuthorRole = AuthorRole.Story,
			Positions = { StaffPosition.SecondKeyAnimation },
			GenreIds = { 41 },
			MinimumMangaCredits = 1,
			LastCreditBefore = 2021,
			Sfw = false,
			OrderBy = PersonSearchOrderBy.MangaCredits,
			SortDirection = SortDirection.Descending
		};

		config.ConfigToString().Should().Be("?manga=2&credit=author&position=2nd_key_animation&author_role=story&genres=41&last_credit_before=2021&min_manga_credits=1&order_by=manga_credits&sort=desc");
	}

	[Fact]
	public void ProducerSearchConfig_StudiosActiveSince2020ByCount_BuildsQuery()
	{
		var config = new ProducerSearchConfig
		{
			Roles = { ProducerRole.Studio },
			ActiveAfter = 2020,
			OrderBy = ProducerSearchOrderBy.Count,
			SortDirection = SortDirection.Descending
		};

		config.ConfigToString().Should().Be("?role=studio&active_after=2020&order_by=count&sort=desc");
	}
}
