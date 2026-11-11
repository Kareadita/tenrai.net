namespace Tenrai;

/// <summary>
/// The Tenrai API spec this release of the library is built against.
/// </summary>
public static class TenraiSpec
{
	/// <summary>
	/// The Tenrai OpenAPI spec version (<c>info.version</c>) this library covers, such as "1.0.31".
	/// </summary>
	// Not const: a const is copied into the caller's build and would report a stale version
	public static string SupportedVersion { get; } = "1.0.31";

	/// <summary>
	/// Tenrai's changelog, listing what changed in the API after <see cref="SupportedVersion"/>.
	/// </summary>
	public static string ChangelogUrl { get; } = "https://api.tenrai.org/changelog";
}
