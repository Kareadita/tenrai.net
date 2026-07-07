# API Reference

All methods are asynchronous and return `Task<T>`. Response models wrap data in a `Data` property. Paginated endpoints return `PaginatedTenraiResponse<T>` with pagination metadata. See [Models](Models.md) for response model details.

> **Unsupported endpoints:** Tenrai does not implement user, club, watch, forum-topic, or anime/manga user-update endpoints (plus random user and user/club search). Those methods are retained on `ITenrai` for source compatibility but are marked `[Obsolete]` and throw `NotSupportedException`; they are not listed below. For user-specific data, use the [official MyAnimeList API](https://myanimelist.net/apiconfig/references/api/v2).

> **Server Key:** The bulk `Get*IdsAsync` methods require a Tenrai Server Key (set `TenraiClientConfiguration.ServerKey`); without one the API returns 401. See [Rate Limiting](RateLimiting.md).

## Anime

### GetAnimeAsync

Returns anime with given MAL id.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<Anime>`

**Example:**

```csharp
var anime = await tenrai.GetAnimeAsync(1);
Console.WriteLine(anime.Data.Title);
Console.WriteLine(anime.Data.Type);
```

**Model:** [Anime](Models.md#anime)

### GetAnimeCharactersAsync

Returns collection of characters of anime.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<ICollection<AnimeCharacter>>`

**Example:**

```csharp
var characters = await tenrai.GetAnimeCharactersAsync(1);
foreach (var c in characters.Data)
    Console.WriteLine($"{c.Character.Name} - {c.Role}");
```

**Model:** [AnimeCharacter](Models.md#animecharacter)

### GetAnimeStaffAsync

Returns collection of staff of anime.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<ICollection<AnimeStaffPosition>>`

**Example:**

```csharp
var staff = await tenrai.GetAnimeStaffAsync(1);
foreach (var member in staff.Data)
    Console.WriteLine($"{member.Person.Name} ({string.Join(", ", member.Position)})");
```

**Model:** [AnimeStaffPosition](Models.md#animestaffposition)

### GetAnimeEpisodesAsync

Returns list of episodes. Overload: `(long id, int page)` for pagination.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<AnimeEpisode>>`

**Example:**

```csharp
var episodes = await tenrai.GetAnimeEpisodesAsync(1);
foreach (var ep in episodes.Data)
    Console.WriteLine($"Ep {ep.MalId}: {ep.Title}");
```

**Model:** [AnimeEpisode](Models.md#animeepisode)

### GetAnimeEpisodeAsync

Returns details about specific episode.

| Parameter | Type | Description |
|-----------|------|-------------|
| animeId | long | MAL id of anime |
| episodeId | int | Id of episode |

**Returns:** `BaseTenraiResponse<AnimeEpisode>`

**Example:**

```csharp
var episode = await tenrai.GetAnimeEpisodeAsync(1, 1);
Console.WriteLine(episode.Data.Synopsis);
```

**Model:** [AnimeEpisode](Models.md#animeepisode)

### GetAnimeNewsAsync

Returns news related to anime. Overload: `(long id, int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<News>>`

**Example:**

```csharp
var news = await tenrai.GetAnimeNewsAsync(1);
foreach (var n in news.Data)
    Console.WriteLine(n.Title);
```

### GetAnimeVideosAsync

Returns videos (PV, episodes) related to anime.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<AnimeVideos>`

**Example:**

```csharp
var videos = await tenrai.GetAnimeVideosAsync(1);
// videos.Data contains Promo, Episodes, MusicVideos
```

### GetAnimeVideosEpisodesAsync

Returns video episodes. Overload: `(long id, int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<EpisodeVideo>>`

**Example:**

```csharp
var episodes = await tenrai.GetAnimeVideosEpisodesAsync(1);
foreach (var ep in episodes.Data)
    Console.WriteLine(ep.Title);
```

### GetAnimePicturesAsync

Returns links to pictures.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<ICollection<ImagesSet>>`

**Example:**

```csharp
var pictures = await tenrai.GetAnimePicturesAsync(1);
foreach (var img in pictures.Data)
    Console.WriteLine(img.Jpg?.ImageUrl);
```

### GetAnimeStatisticsAsync

Returns statistics.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<AnimeStatistics>`

**Example:**

```csharp
var stats = await tenrai.GetAnimeStatisticsAsync(1);
Console.WriteLine($"Score: {stats.Data.Score?.Mean}");
```

### GetAnimeMoreInfoAsync

Returns additional information.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<MoreInfo>`

**Example:**

```csharp
var info = await tenrai.GetAnimeMoreInfoAsync(1);
Console.WriteLine(info.Data.MoreInfo);
```

### GetAnimeRecommendationsAsync

Returns anime recommendations.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<ICollection<Recommendation>>`

**Example:**

```csharp
var recs = await tenrai.GetAnimeRecommendationsAsync(1);
foreach (var r in recs.Data)
    Console.WriteLine(r.Content);
```

**Model:** [Recommendation](Models.md#recommendation)

### GetAnimeReviewsAsync

Returns reviews. A simple `long id` overload returns the first page; a second overload takes a [`ReviewsSearchConfig`](#reviewssearchconfig) for paging, sorting, and filtering.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |
| searchConfig | ReviewsSearchConfig | (overload) Page, sort, preliminary/spoiler filters, sentiment |

**Returns:** `PaginatedTenraiResponse<ICollection<Review>>`

**Example:**

```csharp
// Simple: first page
var reviews = await tenrai.GetAnimeReviewsAsync(1);

// Filtered: newest first, recommended sentiment, spoilers excluded
var filtered = await tenrai.GetAnimeReviewsAsync(1, new ReviewsSearchConfig
{
    Sort = ReviewSortOrder.Newest,
    Sentiment = ReviewSentiment.Recommended,
    Spoilers = ReviewFilter.Exclude
});
foreach (var r in filtered.Data)
    Console.WriteLine($"{r.User?.Username}: {r.Score} [{string.Join(", ", r.Tags)}]");
```

**Model:** [Review](Models.md#review) · **Config:** [ReviewsSearchConfig](#reviewssearchconfig)

### GetAnimeRelationsAsync

Returns related entries.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `PaginatedTenraiResponse<ICollection<RelatedEntry>>`

**Example:**

```csharp
var relations = await tenrai.GetAnimeRelationsAsync(1);
foreach (var r in relations.Data)
    Console.WriteLine($"{r.Relation}: {r.Entry?.FirstOrDefault()?.Name}");
```

**Model:** [RelatedEntry](Models.md#relatedentry)

### GetAnimeThemesAsync

Returns openings and endings.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<AnimeThemes>`

**Example:**

```csharp
var themes = await tenrai.GetAnimeThemesAsync(1);
// themes.Data contains Openings, Endings
```

### GetAnimeExternalLinksAsync

Returns external services links.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<ICollection<ExternalLink>>`

**Example:**

```csharp
var links = await tenrai.GetAnimeExternalLinksAsync(1);
foreach (var l in links.Data)
    Console.WriteLine($"{l.Name}: {l.Url}");
```

**Model:** [ExternalLink](Models.md#externallink)

### GetAnimeStreamingLinksAsync

Returns streaming services links.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<ICollection<ExternalLink>>`

**Example:**

```csharp
var links = await tenrai.GetAnimeStreamingLinksAsync(1);
foreach (var l in links.Data)
    Console.WriteLine($"{l.Name}: {l.Url}");
```

### GetAnimeFullDataAsync

Returns anime with all additional data.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of anime |

**Returns:** `BaseTenraiResponse<AnimeFull>`

**Example:**

```csharp
var full = await tenrai.GetAnimeFullDataAsync(1);
// full.Data contains anime plus relations, news, etc.
```

---

## Manga

### GetMangaAsync

Returns manga with given MAL id.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseTenraiResponse<Manga>`

**Example:**

```csharp
var manga = await tenrai.GetMangaAsync(2);
Console.WriteLine(manga.Data.Title);
Console.WriteLine(manga.Data.Status);
```

**Model:** [Manga](Models.md#manga)

### GetMangaCharactersAsync

Returns characters appearing in manga.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseTenraiResponse<ICollection<MangaCharacter>>`

**Example:**

```csharp
var characters = await tenrai.GetMangaCharactersAsync(2);
foreach (var c in characters.Data)
    Console.WriteLine($"{c.Character.Name} - {c.Role}");
```

**Model:** [MangaCharacter](Models.md#mangacharacter)

### GetMangaNewsAsync

Returns news. Overload: `(long id, int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<News>>`

**Example:**

```csharp
var news = await tenrai.GetMangaNewsAsync(2);
foreach (var n in news.Data)
    Console.WriteLine(n.Title);
```

### GetMangaPicturesAsync

Returns links to pictures.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseTenraiResponse<ICollection<ImagesSet>>`

**Example:**

```csharp
var pictures = await tenrai.GetMangaPicturesAsync(2);
```

### GetMangaStatisticsAsync

Returns statistics.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseTenraiResponse<MangaStatistics>`

**Example:**

```csharp
var stats = await tenrai.GetMangaStatisticsAsync(2);
Console.WriteLine($"Score: {stats.Data.Score?.Mean}");
```

### GetMangaMoreInfoAsync

Returns additional information.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseTenraiResponse<MoreInfo>`

**Example:**

```csharp
var info = await tenrai.GetMangaMoreInfoAsync(2);
```

### GetMangaRecommendationsAsync

Returns manga recommendations.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseTenraiResponse<ICollection<Recommendation>>`

**Example:**

```csharp
var recs = await tenrai.GetMangaRecommendationsAsync(2);
```

### GetMangaReviewsAsync

Returns reviews. A simple `long id` overload returns the first page; a second overload takes a [`ReviewsSearchConfig`](#reviewssearchconfig) for paging, sorting, and filtering.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |
| searchConfig | ReviewsSearchConfig | (overload) Page, sort, preliminary/spoiler filters, sentiment |

**Returns:** `PaginatedTenraiResponse<ICollection<Review>>`

**Example:**

```csharp
var reviews = await tenrai.GetMangaReviewsAsync(2);

// Only spoiler reviews, oldest first
var spoilers = await tenrai.GetMangaReviewsAsync(2, new ReviewsSearchConfig
{
    Spoilers = ReviewFilter.Only,
    Sort = ReviewSortOrder.Oldest
});
```

**Model:** [Review](Models.md#review) · **Config:** [ReviewsSearchConfig](#reviewssearchconfig)

### GetMangaRelationsAsync

Returns related entries.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `PaginatedTenraiResponse<ICollection<RelatedEntry>>`

**Example:**

```csharp
var relations = await tenrai.GetMangaRelationsAsync(2);
```

### GetMangaExternalLinksAsync

Returns external services links.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseTenraiResponse<ICollection<ExternalLink>>`

**Example:**

```csharp
var links = await tenrai.GetMangaExternalLinksAsync(2);
```

### GetMangaFullDataAsync

Returns manga with all additional data.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of manga |

**Returns:** `BaseTenraiResponse<MangaFull>`

**Example:**

```csharp
var full = await tenrai.GetMangaFullDataAsync(2);
```

---

## Character

### GetCharacterAsync

Returns character with given MAL id.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseTenraiResponse<Character>`

**Example:**

```csharp
var character = await tenrai.GetCharacterAsync(2219);
Console.WriteLine(character.Data.Name);
Console.WriteLine(character.Data.About);
```

**Model:** [Character](Models.md#character)

### GetCharacterAnimeAsync

Returns animeography (anime where character appeared).

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseTenraiResponse<ICollection<CharacterAnimeographyEntry>>`

**Example:**

```csharp
var anime = await tenrai.GetCharacterAnimeAsync(2219);
foreach (var entry in anime.Data)
    Console.WriteLine($"{entry.Anime?.Title} - {entry.Role}");
```

### GetCharacterMangaAsync

Returns mangaography (manga where character appeared).

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseTenraiResponse<ICollection<CharacterMangaographyEntry>>`

**Example:**

```csharp
var manga = await tenrai.GetCharacterMangaAsync(2219);
foreach (var entry in manga.Data)
    Console.WriteLine(entry.Manga?.Title);
```

### GetCharacterVoiceActorsAsync

Returns voice actors.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseTenraiResponse<ICollection<VoiceActorEntry>>`

**Example:**

```csharp
var va = await tenrai.GetCharacterVoiceActorsAsync(2219);
foreach (var entry in va.Data)
    Console.WriteLine($"{entry.Person.Name} ({entry.Language})");
```

### GetCharacterPicturesAsync

Returns links to pictures.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseTenraiResponse<ICollection<ImagesSet>>`

**Example:**

```csharp
var pictures = await tenrai.GetCharacterPicturesAsync(2219);
```

### GetCharacterFullDataAsync

Returns character with all additional data.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of character |

**Returns:** `BaseTenraiResponse<CharacterFull>`

**Example:**

```csharp
var full = await tenrai.GetCharacterFullDataAsync(2219);
```

---

## Person

### GetPersonAsync

Returns person with given MAL id.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseTenraiResponse<Person>`

**Example:**

```csharp
var person = await tenrai.GetPersonAsync(1870);
Console.WriteLine(person.Data.FamilyName);
Console.WriteLine(person.Data.GivenName);
```

**Model:** [Person](Models.md#person)

### GetPersonAnimeAsync

Returns animeography (anime the person collaborated on).

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseTenraiResponse<ICollection<PersonAnimeographyEntry>>`

**Example:**

```csharp
var anime = await tenrai.GetPersonAnimeAsync(1870);
foreach (var entry in anime.Data)
    Console.WriteLine(entry.Anime?.Title);
```

### GetPersonMangaAsync

Returns mangaography (manga the person worked on).

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseTenraiResponse<ICollection<PersonMangaographyEntry>>`

**Example:**

```csharp
var manga = await tenrai.GetPersonMangaAsync(1870);
```

### GetPersonVoiceActingRolesAsync

Returns voice acting roles.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseTenraiResponse<ICollection<VoiceActingRole>>`

**Example:**

```csharp
var roles = await tenrai.GetPersonVoiceActingRolesAsync(1870);
foreach (var r in roles.Data)
    Console.WriteLine($"{r.Character?.Name} in {r.Anime?.Title}");
```

### GetPersonPicturesAsync

Returns links to pictures.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseTenraiResponse<ICollection<ImagesSet>>`

**Example:**

```csharp
var pictures = await tenrai.GetPersonPicturesAsync(1870);
```

### GetPersonFullDataAsync

Returns person with all additional data.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of person |

**Returns:** `BaseTenraiResponse<PersonFull>`

**Example:**

```csharp
var full = await tenrai.GetPersonFullDataAsync(1870);
```

---

## Season

### GetSeasonAsync

Returns season preview. Overload: `(int year, Season season, int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| year | int | Year of season |
| season | Season | Season (Winter, Spring, Summer, Fall) |
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<Anime>>`

**Example:**

```csharp
var season = await tenrai.GetSeasonAsync(2024, Season.Spring);
foreach (var anime in season.Data)
    Console.WriteLine(anime.Title);
```

### GetSeasonArchiveAsync

Returns list of available seasons.

**Returns:** `PaginatedTenraiResponse<ICollection<SeasonArchive>>`

**Example:**

```csharp
var archive = await tenrai.GetSeasonArchiveAsync();
foreach (var s in archive.Data)
    Console.WriteLine($"{s.Year} {s.Season}");
```

### GetCurrentSeasonAsync

Returns current airing season. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<Anime>>`

**Example:**

```csharp
var current = await tenrai.GetCurrentSeasonAsync();
```

### GetUpcomingSeasonAsync

Returns anime with undefined airing (marked "Later" on MAL). Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<Anime>>`

**Example:**

```csharp
var upcoming = await tenrai.GetUpcomingSeasonAsync();
```

---

## Schedule

### GetScheduleAsync

Returns current season schedule. Overloads: `(int page)`, `(ScheduledDay scheduledDay)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |
| scheduledDay | ScheduledDay | (optional) Filter by day |

**Returns:** `PaginatedTenraiResponse<ICollection<Anime>>`

**Example:**

```csharp
var schedule = await tenrai.GetScheduleAsync(ScheduledDay.Monday);
foreach (var anime in schedule.Data)
    Console.WriteLine(anime.Title);
```

---

## Top

### GetTopAnimeAsync

Returns top anime. Overloads: `(int page)`, `(TopAnimeFilter filter)`, `(TopAnimeFilter filter, int page)`, `(AnimeTopSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |
| filter | TopAnimeFilter | (optional) Airing, Upcoming, Popularity, Favorite |
| searchConfig | AnimeTopSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedTenraiResponse<ICollection<Anime>>`

**Example:**

```csharp
var top = await tenrai.GetTopAnimeAsync();
foreach (var anime in top.Data)
    Console.WriteLine($"{anime.Rank}. {anime.Title}");
```

### GetTopMangaAsync

Returns top manga. Overloads: `(int page)`, `(MangaTopSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |
| searchConfig | MangaTopSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedTenraiResponse<ICollection<Manga>>`

**Example:**

```csharp
var top = await tenrai.GetTopMangaAsync();
```

### GetTopPeopleAsync

Returns most popular people. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<Person>>`

**Example:**

```csharp
var top = await tenrai.GetTopPeopleAsync();
```

### GetTopCharactersAsync

Returns most popular characters. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<Character>>`

**Example:**

```csharp
var top = await tenrai.GetTopCharactersAsync();
```

### GetTopReviewsAsync

Returns most popular reviews. A parameterless overload returns the first page; a second overload takes a [`ReviewsSearchConfig`](#reviewssearchconfig) (use `Type` to restrict to anime or manga, plus `Page`/`Limit` and preliminary/spoiler filters).

| Parameter | Type | Description |
|-----------|------|-------------|
| searchConfig | ReviewsSearchConfig | (overload) Type, page, limit, preliminary/spoiler filters |

**Returns:** `PaginatedTenraiResponse<ICollection<Review>>`

**Example:**

```csharp
var top = await tenrai.GetTopReviewsAsync();

// Top manga reviews only
var topManga = await tenrai.GetTopReviewsAsync(new ReviewsSearchConfig
{
    Type = ReviewTargetType.Manga
});
```

**Model:** [Review](Models.md#review) · **Config:** [ReviewsSearchConfig](#reviewssearchconfig)

---

## Genre

### GetAnimeGenresAsync

Returns anime genres. Overload: `(GenresFilter filter)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| filter | GenresFilter | (optional) Filter for genre types |

**Returns:** `BaseTenraiResponse<ICollection<Genre>>`

**Example:**

```csharp
var genres = await tenrai.GetAnimeGenresAsync();
foreach (var g in genres.Data)
    Console.WriteLine(g.Name);
```

### GetMangaGenresAsync

Returns manga genres. Overload: `(GenresFilter filter)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| filter | GenresFilter | (optional) Filter for genre types |

**Returns:** `BaseTenraiResponse<ICollection<Genre>>`

**Example:**

```csharp
var genres = await tenrai.GetMangaGenresAsync();
```

---

## Producer

### GetProducersAsync

Returns producers list. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<Producer>>`

**Example:**

```csharp
var producers = await tenrai.GetProducersAsync();
```

### GetProducerAsync

Returns producer by id.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of producer |

**Returns:** `BaseTenraiResponse<Producer>`

**Example:**

```csharp
var producer = await tenrai.GetProducerAsync(1);
```

### GetProducerExternalLinksAsync

Returns external links related to producer.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of producer |

**Returns:** `BaseTenraiResponse<ICollection<ExternalLink>>`

**Example:**

```csharp
var links = await tenrai.GetProducerExternalLinksAsync(1);
```

### GetProducerFullDataAsync

Returns full producer data.

| Parameter | Type | Description |
|-----------|------|-------------|
| id | long | MAL id of producer |

**Returns:** `BaseTenraiResponse<ProducerFull>`

**Example:**

```csharp
var full = await tenrai.GetProducerFullDataAsync(1);
```

---

## Magazine

### GetMagazinesAsync

Returns magazines list. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<Magazine>>`

**Example:**

```csharp
var magazines = await tenrai.GetMagazinesAsync();
```

---

## Random

### GetRandomAnimeAsync

Gets random anime.

**Returns:** `BaseTenraiResponse<Anime>`

**Example:**

```csharp
var anime = await tenrai.GetRandomAnimeAsync();
Console.WriteLine(anime.Data.Title);
```

### GetRandomMangaAsync

Gets random manga.

**Returns:** `BaseTenraiResponse<Manga>`

**Example:**

```csharp
var manga = await tenrai.GetRandomMangaAsync();
```

### GetRandomCharacterAsync

Gets random character.

**Returns:** `BaseTenraiResponse<Character>`

**Example:**

```csharp
var character = await tenrai.GetRandomCharacterAsync();
```

### GetRandomPersonAsync

Gets random person.

**Returns:** `BaseTenraiResponse<Person>`

**Example:**

```csharp
var person = await tenrai.GetRandomPersonAsync();
```

## Recommendations

### GetRecentAnimeRecommendationsAsync

Returns recently added anime recommendations. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<UserRecommendation>>`

**Example:**

```csharp
var recs = await tenrai.GetRecentAnimeRecommendationsAsync();
```

### GetRecentMangaRecommendationsAsync

Returns recently added manga recommendations. Overload: `(int page)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| page | int | (optional) Page index |

**Returns:** `PaginatedTenraiResponse<ICollection<UserRecommendation>>`

**Example:**

```csharp
var recs = await tenrai.GetRecentMangaRecommendationsAsync();
```

---

## Reviews

### GetRecentAnimeReviewsAsync

Returns recently added anime reviews. A parameterless overload returns the first page; a second overload takes a [`ReviewsSearchConfig`](#reviewssearchconfig) (page, limit, sort, preliminary/spoiler filters, sentiment).

| Parameter | Type | Description |
|-----------|------|-------------|
| searchConfig | ReviewsSearchConfig | (overload) Page, limit, sort, filters, sentiment |

**Returns:** `PaginatedTenraiResponse<ICollection<Review>>`

**Example:**

```csharp
var reviews = await tenrai.GetRecentAnimeReviewsAsync();
```

### GetRecentMangaReviewsAsync

Returns recently added manga reviews. A parameterless overload returns the first page; a second overload takes a [`ReviewsSearchConfig`](#reviewssearchconfig) (page, limit, sort, preliminary/spoiler filters, sentiment).

| Parameter | Type | Description |
|-----------|------|-------------|
| searchConfig | ReviewsSearchConfig | (overload) Page, limit, sort, filters, sentiment |

**Returns:** `PaginatedTenraiResponse<ICollection<Review>>`

**Example:**

```csharp
var reviews = await tenrai.GetRecentMangaReviewsAsync();
```

---

## Search

### SearchAnimeAsync

Returns anime search results. Overload: `(AnimeSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| query | string | Search query |
| searchConfig | AnimeSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedTenraiResponse<ICollection<Anime>>`

**Example:**

```csharp
var results = await tenrai.SearchAnimeAsync("cowboy bebop");
foreach (var anime in results.Data)
    Console.WriteLine(anime.Title);
```

### SearchMangaAsync

Returns manga search results. Overload: `(MangaSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| query | string | Search query |
| searchConfig | MangaSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedTenraiResponse<ICollection<Manga>>`

**Example:**

```csharp
var results = await tenrai.SearchMangaAsync("berserk");
```

### SearchPersonAsync

Returns person search results. Overload: `(PersonSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| query | string | Search query |
| searchConfig | PersonSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedTenraiResponse<ICollection<Person>>`

**Example:**

```csharp
var results = await tenrai.SearchPersonAsync("miyazaki");
```

### SearchCharacterAsync

Returns character search results. Overload: `(CharacterSearchConfig searchConfig)`.

| Parameter | Type | Description |
|-----------|------|-------------|
| query | string | Search query |
| searchConfig | CharacterSearchConfig | (optional) Advanced filters |

**Returns:** `PaginatedTenraiResponse<ICollection<Character>>`

**Example:**

```csharp
var results = await tenrai.SearchCharacterAsync("lain");
```

---

## Bulk IDs

> These endpoints require a Tenrai Server Key (set `TenraiClientConfiguration.ServerKey`); without one the API returns 401.

### GetAnimeIdsAsync / GetMangaIdsAsync / GetCharacterIdsAsync / GetPersonIdsAsync / GetProducerIdsAsync

Returns the complete collection of MAL IDs for the given resource.

**Returns:** `BaseTenraiResponse<ICollection<long>>`

**Example:**

```csharp
var tenrai = new TenraiClient(new TenraiClientConfiguration { ServerKey = "your-server-key" });
var animeIds = await tenrai.GetAnimeIdsAsync();
Console.WriteLine(animeIds.Data.Count);
```

---

## Reviews Configuration

### ReviewsSearchConfig

Passed to the review methods (`GetAnimeReviewsAsync`, `GetMangaReviewsAsync`, `GetTopReviewsAsync`, `GetRecentAnimeReviewsAsync`, `GetRecentMangaReviewsAsync`) to page, sort, and filter reviews.

| Property | Type | Applies to | Description |
|----------|------|-----------|-------------|
| Page | int? | all | Page index |
| Limit | int? | recent, top | Entries per page |
| Sort | ReviewSortOrder? | per-entity, recent | `MostHelpful` (default), `Newest`, `Oldest` |
| Preliminary | ReviewFilter? | all | `Include`, `Exclude`, or `Only` preliminary reviews |
| Spoilers | ReviewFilter? | all | `Include`, `Exclude`, or `Only` spoiler reviews |
| Sentiment | ReviewSentiment? | per-entity, recent | `Recommended`, `MixedFeelings`, `NotRecommended` |
| Type | ReviewTargetType? | top only | `Anime` or `Manga` |

**Enums:** `ReviewSortOrder`, `ReviewFilter`, `ReviewSentiment`, `ReviewTargetType`.

**Example:**

```csharp
var config = new ReviewsSearchConfig
{
    Page = 1,
    Sort = ReviewSortOrder.Newest,
    Preliminary = ReviewFilter.Exclude,
    Spoilers = ReviewFilter.Only,
    Sentiment = ReviewSentiment.NotRecommended
};
var reviews = await tenrai.GetMangaReviewsAsync(2, config);
```

