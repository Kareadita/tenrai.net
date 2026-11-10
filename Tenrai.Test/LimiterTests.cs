using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using FluentAssertions;
using Tenrai.Config;
using Tenrai.Tests.Helpers;
using Xunit;

namespace Tenrai.Tests;

public class LimiterTests
{
	// The stub answers instantly, so elapsed time is only the limiter's waits; 10% under each wait absorbs timer resolution
	private const double Tolerance = 0.9;

	[Fact]
	public async Task GetAnimeAsync_DefaultLimiterWithTwoCalls_ShouldWait()
	{
		var elapsed = await TimeCallsAsync(TaskLimiterConfiguration.Default, calls: 2);

		elapsed.Should().BeGreaterThan((long)(300 * Tolerance));
	}

	[Fact]
	public async Task GetAnimeAsync_NoneLimiter_ShouldNotWait()
	{
		var elapsed = await TimeCallsAsync(TaskLimiterConfiguration.None, calls: 2);

		elapsed.Should().BeLessThan(100);
	}

	[Fact]
	public async Task GetAnimeAsync_OneCallPerSecond_ShouldWait()
	{
		var elapsed = await TimeCallsAsync(new List<TaskLimiterConfiguration> { new(1, TimeSpan.FromMilliseconds(1000)) }, calls: 2);

		elapsed.Should().BeGreaterThan((long)(1000 * Tolerance));
	}

	[Fact]
	public async Task GetAnimeAsync_OneCallThreeSeconds_ShouldWait()
	{
		var elapsed = await TimeCallsAsync(new List<TaskLimiterConfiguration> { new(1, TimeSpan.FromMilliseconds(3000)) }, calls: 2);

		elapsed.Should().BeGreaterThan((long)(3000 * Tolerance));
	}

	[Fact]
	public async Task GetAnimeAsync_OneCallPerSecondAndTwoPerFiveSeconds_ShouldWaitForLonger()
	{
		var limits = new List<TaskLimiterConfiguration>
		{
			new(1, TimeSpan.FromMilliseconds(1000)),
			new(2, TimeSpan.FromMilliseconds(5000))
		};

		var elapsed = await TimeCallsAsync(limits, calls: 3);

		elapsed.Should().BeGreaterThan((long)(5000 * Tolerance));
	}

	private static async Task<long> TimeCallsAsync(List<TaskLimiterConfiguration> limits, int calls)
	{
		var handler = new StubHttpMessageHandler { Body = "{\"data\":null}" };
		var client = handler.CreateClient(new TenraiClientConfiguration { LimiterConfigurations = limits });

		var watch = Stopwatch.StartNew();
		for (var i = 0; i < calls; i++)
		{
			await client.GetAnimeAsync(1);
		}
		watch.Stop();

		handler.RequestCount.Should().Be(calls);
		return watch.ElapsedMilliseconds;
	}
}
