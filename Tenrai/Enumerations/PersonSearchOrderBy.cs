using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Properties by which person search results can be ordered.
/// </summary>
public enum PersonSearchOrderBy
{
	/// <summary>
	/// Does not order results.
	/// </summary>
	[Description("")]
	NoSorting,

	/// <summary>
	/// Orders results by MAL id.
	/// </summary>
	[Description("mal_id")]
	MalId,

	/// <summary>
	/// Orders results by name.
	/// </summary>
	[Description("name")]
	Name,

	/// <summary>
	/// Orders results by birthday.
	/// </summary>
	[Description("birthday")]
	Birthday,

	/// <summary>
	/// Orders results by favorites.
	/// </summary>
	[Description("favorites")]
	Favorites,

	/// <summary>
	/// Orders results by the number of anime the person is credited in, as staff or as a voice actor.
	/// </summary>
	[Description("anime_credits")]
	AnimeCredits,

	/// <summary>
	/// Orders results by the number of manga the person is credited as an author of.
	/// </summary>
	[Description("manga_credits")]
	MangaCredits,

	/// <summary>
	/// Orders results by the number of characters the person voiced.
	/// </summary>
	[Description("voice_roles")]
	VoiceRoles,

	/// <summary>
	/// Orders results by the start year of the person's earliest credited anime or manga. People without one come last.
	/// </summary>
	[Description("debut")]
	Debut,

	/// <summary>
	/// Orders results by the start year of the person's latest credited anime or manga. People without one come last.
	/// </summary>
	[Description("last_credit")]
	LastCredit,
}