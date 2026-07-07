using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using Xunit;

namespace JikanDotNet.Tests.IdsTests
{
	[Collection("JikanTests")]
	public class GetIdsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetIdsAsyncTests(JikanFixture jikanFixture)
		{
			_jikan = jikanFixture.Jikan;
		}

		[Fact]
		public void IdsResponse_ShouldDeserializeDataArrayIntoCollectionOfIds()
		{
			// Given
			const string json = "{\"data\":[1,5,6,7,8,15,16]}";

			// When
			var response = JsonSerializer.Deserialize<BaseJikanResponse<ICollection<long>>>(json);

			// Then
			using (new AssertionScope())
			{
				response.Data.Should().HaveCount(7);
				response.Data.Should().ContainInOrder(1L, 5L, 6L, 7L, 8L, 15L, 16L);
			}
		}

		[Fact]
		public async Task GetAnimeIdsAsync_WithoutServerKey_ShouldRequireAuthorization()
		{
			// When (public tier has no Server Key, so the endpoint is gated)
			var func = _jikan.Awaiting(x => x.GetAnimeIdsAsync());

			// Then
			var exceptionAssertion = await func.Should().ThrowExactlyAsync<JikanRequestException>();
			using (new AssertionScope())
			{
				var apiError = exceptionAssertion.Which.ApiError;
				apiError.Should().NotBeNull();
				apiError.Status.Should().Be(HttpStatusCode.Unauthorized);
				apiError.Path.Should().Contain("/anime/ids");
			}
		}
	}
}
