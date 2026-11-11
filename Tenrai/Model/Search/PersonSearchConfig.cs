using System.Collections.Generic;
using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai;

/// <summary>
/// Model class of search configuration for advanced person search.
/// </summary>
public class PersonSearchConfig: ISearchConfig, ISfwConfig
{
	/// <summary>
	/// Index of page folding 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)
	/// </summary>
	public int? Page { get; set; }

	/// <summary>
	/// Size of the page (100 is the max).
	/// </summary>
	public int? Limit { get; set; }

	/// <summary>
	/// Search query.
	/// </summary>
	public string Query { get; set; }

	/// <summary>
	/// Return entries starting with the given letter.
	/// </summary>
	public char? Letter { get; set; }

	/// <summary>
	/// Whole given name, ignoring case. MyAnimeList stores it in Japanese for Japanese people: Kentarou Miura's is "建太郎", so "Kentarou" finds no one.
	/// </summary>
	public string GivenName { get; set; }

	/// <summary>
	/// Whole family name, ignoring case. MyAnimeList stores it in Japanese for Japanese people: Kentarou Miura's is "三浦", so "Miura" finds no one.
	/// </summary>
	public string FamilyName { get; set; }

	/// <summary>
	/// Whether the person has a birthday.
	/// </summary>
	public bool? HasBirthday { get; set; }

	/// <summary>
	/// Whether the person has a picture. MyAnimeList's question-mark placeholder counts as none.
	/// </summary>
	public bool? HasImage { get; set; }

	/// <summary>
	/// Whether the person has an about text.
	/// </summary>
	public bool? HasAbout { get; set; }

	/// <summary>
	/// Whether the person has a website.
	/// </summary>
	public bool? HasWebsite { get; set; }

	/// <summary>
	/// Whether the person has at least one alternate name.
	/// </summary>
	public bool? HasAlternateNames { get; set; }

	/// <summary>
	/// Whether the person has a family name.
	/// </summary>
	public bool? HasFamilyName { get; set; }

	/// <summary>
	/// People credited in any of these anime, as staff or as a voice actor.
	/// </summary>
	public ICollection<long> AnimeIds { get; set; } = new List<long>();

	/// <summary>
	/// People credited as an author of any of these manga.
	/// </summary>
	public ICollection<long> MangaIds { get; set; } = new List<long>();

	/// <summary>
	/// People who voiced any of these characters.
	/// </summary>
	public ICollection<long> CharacterIds { get; set; } = new List<long>();

	/// <summary>
	/// People with any of these kinds of credit.
	/// </summary>
	public ICollection<PersonCredit> Credits { get; set; } = new List<PersonCredit>();

	/// <summary>
	/// People with any of these kinds of credit are left out.
	/// </summary>
	public ICollection<PersonCredit> ExcludedCredits { get; set; } = new List<PersonCredit>();

	/// <summary>
	/// People with any of these staff jobs: in <see cref="AnimeIds"/> when set, otherwise in any anime.
	/// </summary>
	public ICollection<StaffPosition> Positions { get; set; } = new List<StaffPosition>();

	/// <summary>
	/// People with any of these staff jobs are left out: in <see cref="AnimeIds"/> when set, otherwise in any anime.
	/// </summary>
	public ICollection<StaffPosition> ExcludedPositions { get; set; } = new List<StaffPosition>();

	/// <summary>
	/// Authors credited for the story or the art: in <see cref="MangaIds"/> when set, otherwise in any manga.
	/// </summary>
	public AuthorRole? AuthorRole { get; set; }

	/// <summary>
	/// People credited in titles with every one of these genre, theme or demographic ids, all in their anime or all in their manga.
	/// Ids, not <see cref="AnimeGenreSearch"/>/<see cref="MangaGenreSearch"/>: the same genre has a different id per kind (Seinen is 42 on anime, 41 on manga).
	/// </summary>
	public ICollection<long> GenreIds { get; set; } = new List<long>();

	/// <summary>
	/// People credited in any anime or manga with any of these genre, theme or demographic ids are left out.
	/// </summary>
	public ICollection<long> ExcludedGenreIds { get; set; } = new List<long>();

	/// <summary>
	/// People credited in an anime of any of these studios or producers, as staff or as a voice actor.
	/// </summary>
	public ICollection<long> ProducerIds { get; set; } = new List<long>();

	/// <summary>
	/// Authors of a manga serialized in any of these magazines.
	/// </summary>
	public ICollection<long> MagazineIds { get; set; } = new List<long>();

	/// <summary>
	/// People credited in an anime of any of these types.
	/// </summary>
	public ICollection<AnimeType> AnimeTypes { get; set; } = new List<AnimeType>();

	/// <summary>
	/// Authors of a manga of any of these types.
	/// </summary>
	public ICollection<MangaType> MangaTypes { get; set; } = new List<MangaType>();

	/// <summary>
	/// People credited in an anime adapted from any of these sources.
	/// </summary>
	public ICollection<AnimeSource> Sources { get; set; } = new List<AnimeSource>();

	/// <summary>
	/// People credited in an anime of any of these MyAnimeList debut seasons. With <see cref="SeasonYears"/>, the season and year must match the same anime.
	/// </summary>
	public ICollection<Season> Seasons { get; set; } = new List<Season>();

	/// <summary>
	/// People credited in an anime of any of these MyAnimeList debut season years. Can differ from the calendar start year.
	/// </summary>
	public ICollection<int> SeasonYears { get; set; } = new List<int>();

	/// <summary>
	/// True for people credited in at least one anime airing now, false for people credited in none.
	/// </summary>
	public bool? Airing { get; set; }

	/// <summary>
	/// True for people credited in at least one anime that has not aired yet, false for people credited in none.
	/// </summary>
	public bool? Upcoming { get; set; }

	/// <summary>
	/// True for authors of at least one manga publishing now, false for people who author none.
	/// </summary>
	public bool? Publishing { get; set; }

	/// <summary>
	/// People credited as a voice actor in any of these languages.
	/// </summary>
	public ICollection<VoiceLanguage> VoiceLanguages { get; set; } = new List<VoiceLanguage>();

	/// <summary>
	/// People credited as a voice actor in any of these languages are left out.
	/// </summary>
	public ICollection<VoiceLanguage> ExcludedVoiceLanguages { get; set; } = new List<VoiceLanguage>();

	/// <summary>
	/// People credited in an anime or manga that started in this year or later. With <see cref="ActiveBefore"/>, the same anime or manga must meet both.
	/// </summary>
	public int? ActiveAfter { get; set; }

	/// <summary>
	/// People credited in an anime or manga that started in this year or earlier.
	/// </summary>
	public int? ActiveBefore { get; set; }

	/// <summary>
	/// People whose earliest credited anime or manga started in this year or later.
	/// </summary>
	public int? DebutAfter { get; set; }

	/// <summary>
	/// People whose latest credited anime or manga started in this year or earlier. Titles not started yet count with their planned year.
	/// </summary>
	public int? LastCreditBefore { get; set; }

	/// <summary>
	/// Lowest number of favorites.
	/// </summary>
	public int? MinimumFavorites { get; set; }

	/// <summary>
	/// Highest number of favorites.
	/// </summary>
	public int? MaximumFavorites { get; set; }

	/// <summary>
	/// Lowest number of anime the person is credited in, as staff or as a voice actor.
	/// </summary>
	public int? MinimumAnimeCredits { get; set; }

	/// <summary>
	/// Highest number of anime the person is credited in, as staff or as a voice actor.
	/// </summary>
	public int? MaximumAnimeCredits { get; set; }

	/// <summary>
	/// Lowest number of manga the person is credited as an author of.
	/// </summary>
	public int? MinimumMangaCredits { get; set; }

	/// <summary>
	/// Highest number of manga the person is credited as an author of.
	/// </summary>
	public int? MaximumMangaCredits { get; set; }

	/// <summary>
	/// Lowest number of characters the person voiced.
	/// </summary>
	public int? MinimumVoiceRoles { get; set; }

	/// <summary>
	/// Highest number of characters the person voiced.
	/// </summary>
	public int? MaximumVoiceRoles { get; set; }

	/// <summary>
	/// Lowest number of main characters the person voiced.
	/// </summary>
	public int? MinimumMainVoiceRoles { get; set; }

	/// <summary>
	/// Highest number of main characters the person voiced.
	/// </summary>
	public int? MaximumMainVoiceRoles { get; set; }

	/// <summary>
	/// Lowest number of supporting characters the person voiced.
	/// </summary>
	public int? MinimumSupportingVoiceRoles { get; set; }

	/// <summary>
	/// Highest number of supporting characters the person voiced.
	/// </summary>
	public int? MaximumSupportingVoiceRoles { get; set; }

	/// <summary>
	/// Lowest number of languages the person is credited as a voice actor in.
	/// </summary>
	public int? MinimumVoiceLanguages { get; set; }

	/// <summary>
	/// Highest number of languages the person is credited as a voice actor in.
	/// </summary>
	public int? MaximumVoiceLanguages { get; set; }

	/// <summary>
	/// Filter out people only credited in adult entries. Null uses the client setting (<see cref="Config.TenraiClientConfiguration.Sfw"/>).
	/// </summary>
	public bool? Sfw { get; set; }

	/// <summary>
	/// Also filter out people only credited in ecchi entries, on top of <see cref="Sfw"/>. Null uses the client setting.
	/// </summary>
	public bool? SfwStrict { get; set; }

	/// <summary>
	/// Select order by property.
	/// </summary>
	public PersonSearchOrderBy OrderBy { get; set; }

	/// <summary>
	/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
	/// </summary>
	public SortDirection SortDirection { get; set; }

	/// <summary>
	/// Create query from current parameters for search request.
	/// </summary>
	/// <returns>Query from current parameters for search request</returns>
	public string ConfigToString()
	{
		if (Letter.HasValue)
		{
			Guard.IsLetter(Letter.Value, nameof(Letter));
		}

		var query = new QueryBuilder()
			.Page(Page)
			.Limit(Limit)
			.Add("q", Query)
			.Add("letter", Letter?.ToString())
			.Add("given_name", GivenName)
			.Add("family_name", FamilyName)
			.Add("has_birthday", HasBirthday)
			.Add("has_image", HasImage)
			.Add("has_about", HasAbout)
			.Add("has_website", HasWebsite)
			.Add("has_alternate_names", HasAlternateNames)
			.Add("has_family_name", HasFamilyName)
			.AddList("anime", AnimeIds)
			.AddList("manga", MangaIds)
			.AddList("characters", CharacterIds)
			.AddList("credit", Credits)
			.AddList("credit_exclude", ExcludedCredits)
			.AddList("position", Positions)
			.AddList("position_exclude", ExcludedPositions)
			.Add("author_role", AuthorRole)
			.AddList("genres", GenreIds)
			.AddList("genres_exclude", ExcludedGenreIds)
			.AddList("producers", ProducerIds)
			.AddList("magazines", MagazineIds)
			.AddList("anime_type", AnimeTypes)
			.AddList("manga_type", MangaTypes)
			.AddList("source", Sources)
			.AddList("season", Seasons)
			.AddList("season_year", SeasonYears)
			.Add("airing", Airing)
			.Add("upcoming", Upcoming)
			.Add("publishing", Publishing)
			.AddList("voice_language", VoiceLanguages)
			.AddList("voice_language_exclude", ExcludedVoiceLanguages)
			.Add("active_after", ActiveAfter)
			.Add("active_before", ActiveBefore)
			.Add("debut_after", DebutAfter)
			.Add("last_credit_before", LastCreditBefore)
			.Add("min_favorites", MinimumFavorites)
			.Add("max_favorites", MaximumFavorites)
			.Add("min_anime_credits", MinimumAnimeCredits)
			.Add("max_anime_credits", MaximumAnimeCredits)
			.Add("min_manga_credits", MinimumMangaCredits)
			.Add("max_manga_credits", MaximumMangaCredits)
			.Add("min_voice_roles", MinimumVoiceRoles)
			.Add("max_voice_roles", MaximumVoiceRoles)
			.Add("min_main_voice_roles", MinimumMainVoiceRoles)
			.Add("max_main_voice_roles", MaximumMainVoiceRoles)
			.Add("min_supporting_voice_roles", MinimumSupportingVoiceRoles)
			.Add("max_supporting_voice_roles", MaximumSupportingVoiceRoles)
			.Add("min_voice_languages", MinimumVoiceLanguages)
			.Add("max_voice_languages", MaximumVoiceLanguages)
			.Sfw(Sfw, SfwStrict);

		if (OrderBy != PersonSearchOrderBy.NoSorting)
		{
			query.Add("order_by", OrderBy).Add("sort", SortDirection);
		}

		return query.ToString();
	}
}
