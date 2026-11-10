using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Config;
using Tenrai.Exceptions;
using Tenrai.Tests.Helpers;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests;

public class ExceptionsTests
{
	private const string MangaNotFoundBody = "{\"status\":404,\"type\":\"ResourceNotFoundException\",\"message\":\"Manga with ID 999999999 not found.\",\"error\":\"The requested resource does not exist in our database or the ID is invalid.\",\"path\":\"/v1/manga/999999999\"}";

	[Fact]
	public async Task GetMangaAsync_NonExistentId_ShouldDeserializeTenraiErrorEnvelope()
	{
		// Given
		var tenrai = NotFoundClient(suppressException: false);

		// When
		var func = tenrai.Awaiting(x => x.GetMangaAsync(999999999));

		// Then
		var exceptionAssertion = await func.Should().ThrowExactlyAsync<TenraiRequestException>();
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
	public async Task GetAnimeAsync_WrongIdDoNotSuppressExceptions_ShouldThrowTenraiRequestException()
	{
		var func = NotFoundClient(suppressException: false).Awaiting(x => x.GetAnimeAsync(2));

		await func.Should().ThrowExactlyAsync<TenraiRequestException>();
	}

	[Fact]
	public async Task GetMangaAsync_WrongIdDoNotSuppressExceptions_ShouldThrowTenraiRequestException()
	{
		var func = NotFoundClient(suppressException: false).Awaiting(x => x.GetMangaAsync(5));

		await func.Should().ThrowExactlyAsync<TenraiRequestException>();
	}

	[Fact]
	public async Task GetPersonAsync_WrongIdDoNotSuppressExceptions_ShouldThrowTenraiRequestException()
	{
		var func = NotFoundClient(suppressException: false).Awaiting(x => x.GetPersonAsync(13308));

		await func.Should().ThrowExactlyAsync<TenraiRequestException>();
	}

	[Fact]
	public async Task GetMangaAsync_NonExistentId_ShouldPutApiMessageInExceptionMessage()
	{
		var func = NotFoundClient(suppressException: false).Awaiting(x => x.GetMangaAsync(999999999));

		var exceptionAssertion = await func.Should().ThrowExactlyAsync<TenraiRequestException>();
		exceptionAssertion.Which.Message.Should().Be("GET request failed. Status code: NotFound Inner message: Manga with ID 999999999 not found.");
	}

	[Fact]
	public async Task GetMangaAsync_NonJsonErrorBody_ShouldKeepStatusAndBody()
	{
		var handler = new StubHttpMessageHandler { StatusCode = HttpStatusCode.BadGateway, Body = "<html>502 Bad Gateway</html>" };
		var tenrai = handler.CreateClient(new TenraiClientConfiguration { LimiterConfigurations = TaskLimiterConfiguration.None });

		var func = tenrai.Awaiting(x => x.GetMangaAsync(1));

		var exceptionAssertion = await func.Should().ThrowExactlyAsync<TenraiRequestException>();
		using (new AssertionScope())
		{
			exceptionAssertion.Which.Message.Should().Be("GET request failed. Status code: BadGateway Inner message: <html>502 Bad Gateway</html>");
			exceptionAssertion.Which.ApiError.Should().BeNull();
		}
	}

	[Fact]
	public async Task GetMangaAsync_WrongIdSuppressExceptions_ShouldReturnNull()
	{
		var result = await NotFoundClient(suppressException: true).GetMangaAsync(999999999);

		result.Should().BeNull();
	}

	private static ITenrai NotFoundClient(bool suppressException)
	{
		var handler = new StubHttpMessageHandler { StatusCode = HttpStatusCode.NotFound, Body = MangaNotFoundBody };
		return handler.CreateClient(new TenraiClientConfiguration { SuppressException = suppressException, LimiterConfigurations = TaskLimiterConfiguration.None });
	}
}
