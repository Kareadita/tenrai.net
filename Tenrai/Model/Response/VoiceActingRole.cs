using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Voice acting role model class for person's class.
/// </summary>
public class VoiceActingRole
{
	/// <summary>
	/// Anime associated with voice acting role.
	/// </summary>
	[JsonPropertyName("anime")]
	public MalImageSubItem Anime { get; set; }

	/// <summary>
	/// Character associated with voice acting role.
	/// </summary>
	[JsonPropertyName("character")]
	public CharacterEntry Character { get; set; }

	/// <summary>
	/// Status of the role: Main/Supporting.
	/// </summary>
	[JsonPropertyName("role")]
	public string Role { get; set; }
}