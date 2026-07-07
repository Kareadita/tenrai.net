using System;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

// The endpoints exercised here are intentionally obsolete: Tenrai v1 does not implement them.
#pragma warning disable CS0618

namespace JikanDotNet.Tests
{
	/// <summary>
	/// Verifies that endpoint families Tenrai does not implement (users, clubs, watch, forum topics,
	/// user updates) are marked <see cref="ObsoleteAttribute"/> and throw <see cref="NotSupportedException"/>
	/// at runtime instead of issuing a request that would 404. The calls throw synchronously, so they are
	/// asserted as actions rather than awaited.
	/// </summary>
	public class UnsupportedEndpointsTests
	{
		private readonly IJikan _jikan = new Jikan();

		[Fact]
		public void UserEndpoints_ShouldThrowNotSupported()
		{
			using (new AssertionScope())
			{
				FluentActions.Invoking(() => { _ = _jikan.GetUserProfileAsync("Ervelan"); }).Should().Throw<NotSupportedException>();
				FluentActions.Invoking(() => { _ = _jikan.GetUserByIdAsync(1); }).Should().Throw<NotSupportedException>();
				FluentActions.Invoking(() => { _ = _jikan.GetUserReviewsAsync("Ervelan"); }).Should().Throw<NotSupportedException>();
				FluentActions.Invoking(() => { _ = _jikan.GetUserFullDataAsync("Ervelan"); }).Should().Throw<NotSupportedException>();
				FluentActions.Invoking(() => { _ = _jikan.SearchUserAsync("Ervelan"); }).Should().Throw<NotSupportedException>();
				FluentActions.Invoking(() => { _ = _jikan.GetRandomUserAsync(); }).Should().Throw<NotSupportedException>();
			}
		}

		[Fact]
		public void ClubEndpoints_ShouldThrowNotSupported()
		{
			using (new AssertionScope())
			{
				FluentActions.Invoking(() => { _ = _jikan.GetClubAsync(1); }).Should().Throw<NotSupportedException>();
				FluentActions.Invoking(() => { _ = _jikan.GetClubMembersAsync(1); }).Should().Throw<NotSupportedException>();
				FluentActions.Invoking(() => { _ = _jikan.SearchClubAsync("test"); }).Should().Throw<NotSupportedException>();
			}
		}

		[Fact]
		public void WatchEndpoints_ShouldThrowNotSupported()
		{
			using (new AssertionScope())
			{
				FluentActions.Invoking(() => { _ = _jikan.GetWatchRecentEpisodesAsync(); }).Should().Throw<NotSupportedException>();
				FluentActions.Invoking(() => { _ = _jikan.GetWatchPopularEpisodesAsync(); }).Should().Throw<NotSupportedException>();
				FluentActions.Invoking(() => { _ = _jikan.GetWatchRecentPromosAsync(); }).Should().Throw<NotSupportedException>();
				FluentActions.Invoking(() => { _ = _jikan.GetWatchPopularPromosAsync(); }).Should().Throw<NotSupportedException>();
			}
		}

		[Fact]
		public void ForumAndUserUpdateEndpoints_ShouldThrowNotSupported()
		{
			using (new AssertionScope())
			{
				FluentActions.Invoking(() => { _ = _jikan.GetAnimeForumTopicsAsync(1); }).Should().Throw<NotSupportedException>();
				FluentActions.Invoking(() => { _ = _jikan.GetMangaForumTopicsAsync(1); }).Should().Throw<NotSupportedException>();
				FluentActions.Invoking(() => { _ = _jikan.GetAnimeUserUpdatesAsync(1); }).Should().Throw<NotSupportedException>();
				FluentActions.Invoking(() => { _ = _jikan.GetMangaUserUpdatesAsync(1); }).Should().Throw<NotSupportedException>();
			}
		}
	}
}

#pragma warning restore CS0618
