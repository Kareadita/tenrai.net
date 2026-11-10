using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Person listed as a voice actor or staff member, or in a user's favorites.
/// </summary>
public class PersonEntry : MalImageSubItem
{
	/// <summary>
	/// <see cref="MalImageSubItem.Name"/> split into given and family name.
	/// </summary>
	[JsonIgnore]
	public EntityName ParsedName => EntityName.Parse(Name);

	/// <summary>
	/// Number of users who favorited the person. Set on anime character lists, null on character voice lists.
	/// </summary>
	[JsonPropertyName("favorites")]
	public int? Favorites { get; set; }
}
