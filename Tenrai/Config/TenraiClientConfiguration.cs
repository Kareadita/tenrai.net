using System.Collections.Generic;

namespace Tenrai.Config
{
	/// <summary>
	/// Object containing information of client configuration.
	/// </summary>
	public class TenraiClientConfiguration
	{
		/// <summary>
		/// Should exception be thrown in case of failed request.
		/// </summary>
		public bool SuppressException { get; set; }

		/// <summary>
		/// Optional Tenrai Server Key. When set, it is sent as the <c>X-Server-Key</c> header to raise
		/// the rate limits (300/min, 5/sec, unlimited daily). If <see cref="LimiterConfigurations"/> is
		/// left at its default, the client automatically switches to the Server Key limiter tier.
		/// </summary>
		public string ServerKey { get; set; }

		/// <summary>
		/// Configuration of the API limiter
		/// </summary>
		public List<TaskLimiterConfiguration> LimiterConfigurations { get; set; }

		/// <summary>
		/// Initializes a new instance of <see cref="TenraiClientConfiguration"/> with default settings:
		/// exceptions are not suppressed and the default task limiter configuration is applied.
		/// </summary>
		public TenraiClientConfiguration()
		{
			SuppressException = false;
			LimiterConfigurations = TaskLimiterConfiguration.Default;
		}
	}
}