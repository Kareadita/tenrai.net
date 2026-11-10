using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Manga author with the part they had in the work.
/// </summary>
public class AuthorEntry : MalUrl
{
	/// <summary>
	/// <see cref="MalUrl.Name"/> split into given and family name.
	/// </summary>
	[JsonIgnore]
	public EntityName ParsedName => EntityName.Parse(Name);

	/// <summary>
	/// Part the author had, e.g. "Story &amp; Art", "Story" or "Art".
	/// </summary>
	[JsonPropertyName("role")]
	public string Role { get; set; }
}
