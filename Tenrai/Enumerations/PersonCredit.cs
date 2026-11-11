using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Kind of credit a person has on MyAnimeList.
/// </summary>
public enum PersonCredit
{
	/// <summary>
	/// Voice actor.
	/// </summary>
	[Description("voice")]
	Voice,

	/// <summary>
	/// Anime staff.
	/// </summary>
	[Description("staff")]
	Staff,

	/// <summary>
	/// Manga author.
	/// </summary>
	[Description("author")]
	Author
}
