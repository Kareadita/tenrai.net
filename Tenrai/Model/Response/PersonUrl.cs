using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Person linked from another entry, such as the people an article covers.
/// </summary>
public class PersonUrl : MalUrl
{
	/// <summary>
	/// <see cref="MalUrl.Name"/> split into given and family name.
	/// </summary>
	[JsonIgnore]
	public EntityName ParsedName => EntityName.Parse(Name);
}
