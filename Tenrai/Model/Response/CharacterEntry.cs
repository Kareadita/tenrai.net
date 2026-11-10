using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Character listed on an anime or manga, in a person's voice roles, or in a user's favorites.
/// </summary>
public class CharacterEntry
{
	/// <summary>
	/// ID associated with MyAnimeList.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long MalId { get; set; }

	/// <summary>
	/// Character's name.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; }

	/// <summary>
	/// <see cref="Name"/> split into given and family name.
	/// </summary>
	[JsonIgnore]
	public EntityName ParsedName => EntityName.Parse(Name);

	/// <summary>
	/// Url to character's main page.
	/// </summary>
	[JsonPropertyName("url")]
	public string Url { get; set; }

	/// <summary>
	/// Character's set of images
	/// </summary>
	[JsonPropertyName("images")]
	public ImagesSet Images { get; set; }

	/// <summary>
	/// Overriden ToString method.
	/// </summary>
	/// <returns>Name if not null, base method elsewhere.</returns>
	public override string ToString()
	{
		return Name ?? base.ToString();
	}
}