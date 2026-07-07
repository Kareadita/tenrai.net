using System.ComponentModel;

namespace Tenrai
{
	/// <summary>
	/// Tri-state filter applied to preliminary and spoiler reviews.
	/// </summary>
	public enum ReviewFilter
	{
		/// <summary>
		/// Include reviews of this kind alongside the others (<c>true</c>).
		/// </summary>
		[Description("true")]
		Include,

		/// <summary>
		/// Exclude reviews of this kind from the results (<c>false</c>).
		/// </summary>
		[Description("false")]
		Exclude,

		/// <summary>
		/// Return only reviews of this kind (<c>only</c>).
		/// </summary>
		[Description("only")]
		Only
	}
}
