using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class ExceptionsTests
	{
		private readonly IJikan _jikan;

		public ExceptionsTests()
		{
			_jikan = new Jikan(new Config.JikanClientConfiguration { SuppressException = true });
		}

		[Fact]
		public async Task GetMangaAsync_NonExistentId_ShouldDeserializeTenraiErrorEnvelope()
		{
			// Given
			var jikan = new Jikan();

			// When
			var func = jikan.Awaiting(x => x.GetMangaAsync(999999999));

			// Then
			var exceptionAssertion = await func.Should().ThrowExactlyAsync<JikanRequestException>();
			using (new AssertionScope())
			{
				var apiError = exceptionAssertion.Which.ApiError;
				apiError.Should().NotBeNull();
				apiError.Status.Should().Be(HttpStatusCode.NotFound);
				apiError.Type.Should().Be("ResourceNotFoundException");
				apiError.Path.Should().Contain("/manga/999999999");
			}
		}

		[Fact]
		public void GetAnimeAsync_WrongIdDoNotSurpressExceptions_ShouldThrowJikanRequestExceptionGetAnime()
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeAsync(2));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public void GetMangaAsync_WrongIdDoNotSurpressExceptions_ShouldThrowJikanRequestExceptionGetManga()
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaAsync(5));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public void GetPerson_WrongIdDoNotSurpressExceptions_ShouldThrowJikanRequestExceptionGetPerson()
		{
			// When
			var func = _jikan.Awaiting(x => x.GetPersonAsync(13308));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}
	}
}