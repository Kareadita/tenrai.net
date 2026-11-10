using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Character linked from another entry, such as the characters an article covers.
/// </summary>
public class CharacterUrl : MalUrl
{
	/// <summary>
	/// <see cref="MalUrl.Name"/> split into given and family name.
	/// </summary>
	[JsonIgnore]
	public EntityName ParsedName => EntityName.Parse(Name);
}
