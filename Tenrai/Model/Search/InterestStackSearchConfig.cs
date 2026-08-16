using System.Text;
using Tenrai.Consts;
using Tenrai.Extensions;
using Tenrai.Helpers;
using Tenrai.Interfaces;

namespace Tenrai
{
	/// <summary>
	/// Model class of search configuration for interest stack search.
	/// </summary>
	public class InterestStackSearchConfig : ISearchConfig
	{
		/// <summary>
		/// 1-based page index (e.g. 1 returns the first page, 2 the second).
		/// </summary>
		public int? Page { get; set; }

		/// <summary>
		/// Size of the page (25 is the max).
		/// </summary>
		public int? PageSize { get; set; }

		/// <summary>
		/// Search query.
		/// </summary>
		public string Query { get; set; }

		/// <summary>
		/// Type of entries the stack holds.
		/// </summary>
		public InterestStackType Type { get; set; } = InterestStackType.EveryType;

		/// <summary>
		/// Select order property.
		/// </summary>
		public InterestStackSearchOrderBy OrderBy { get; set; }

		/// <summary>
		/// Define sort direction for <see cref="OrderBy">OrderBy</see> property.
		/// </summary>
		public SortDirection SortDirection { get; set; }

		/// <summary>
		/// Should only search for sfw stacks. Filter out adult entries.
		/// </summary>
		public bool Sfw { get; set; } = true;

		/// <summary>
		/// Create query from current parameters for search request.
		/// </summary>
		/// <returns>Query from current parameters for search request</returns>
		public string ConfigToString()
		{
			var builder = new StringBuilder().Append('?');

			if (Page.HasValue)
			{
				Guard.IsGreaterThanZero(Page.Value, nameof(Page.Value));
				builder.Append($"page={Page.Value}&");
			}

			if (PageSize.HasValue)
			{
				Guard.IsGreaterThanZero(PageSize.Value, nameof(PageSize.Value));
				Guard.IsLesserOrEqualThan(PageSize.Value, ParameterConsts.MaximumPageSize, nameof(PageSize.Value));
				builder.Append($"limit={PageSize.Value}&");
			}

			if (!string.IsNullOrWhiteSpace(Query))
			{
				builder.Append($"q={Query}&");
			}

			if (Type != InterestStackType.EveryType)
			{
				Guard.IsValidEnum(Type, nameof(Type));
				builder.Append($"stack_type={Type.GetDescription()}&");
			}

			if (OrderBy != InterestStackSearchOrderBy.NoSorting)
			{
				Guard.IsValidEnum(OrderBy, nameof(OrderBy));
				Guard.IsValidEnum(SortDirection, nameof(SortDirection));
				builder.Append($"order_by={OrderBy.GetDescription()}&");
				builder.Append($"sort={SortDirection.GetDescription()}&");
			}

			if (Sfw)
			{
				builder.Append("sfw");
			}

			return builder.ToString().Trim('&');
		}
	}
}
