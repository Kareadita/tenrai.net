namespace Tenrai;

/// <summary>
/// A person's or character's name split into parts, in the shape AniList uses.
/// MyAnimeList sends one string in "Family, Given" order ("Kurosaki, Ichigo"); single names ("Megumin") have no family part.
/// </summary>
public class EntityName
{
	/// <summary>
	/// Given name ("Ichigo"), or the whole name when it has no family part ("Megumin").
	/// </summary>
	public string FirstName { get; }

	/// <summary>
	/// Family name ("Kurosaki"), or null when the name has no family part.
	/// </summary>
	public string LastName { get; }

	/// <summary>
	/// Given name then family name ("Ichigo Kurosaki").
	/// </summary>
	public string FullName { get; }

	private EntityName(string firstName, string lastName)
	{
		FirstName = firstName;
		LastName = lastName;
		FullName = lastName == null ? firstName : firstName + " " + lastName;
	}

	internal static EntityName Parse(string name)
	{
		if (string.IsNullOrWhiteSpace(name)) return new EntityName(null, null);

		var trimmed = name.Trim();
		var separator = trimmed.IndexOf(", ", System.StringComparison.Ordinal);
		if (separator < 0) return new EntityName(trimmed, null);

		var family = trimmed.Substring(0, separator).Trim();
		var given = trimmed.Substring(separator + 2).Trim();
		if (given.Length == 0 || family.Length == 0) return new EntityName(trimmed, null);

		return new EntityName(given, family);
	}
}
