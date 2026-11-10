# Migrating from 3.x to 4.0

4.0 brings Tenrai.Net up to the Tenrai OpenAPI spec 1.0.30. Most code keeps compiling; the changes below are the ones that do not, or that compile but return different data. The [changelog](../Changelog.md) lists everything that was added.

## Safe-for-work filtering

**This is the change most likely to affect you: it compiles, but some calls return less.**

In 3.x the search configs had `Sfw = true` by default, while many plain calls never filtered. In 4.0 filtering is one client setting, `TenraiClientConfiguration.Sfw`, and it is **on by default**. Every call to an endpoint that takes `sfw` now leaves out adult entries, including calls that returned them in 3.x. Every config's `Sfw` is `bool?` and overrides the client setting for that call only.

| Call | 3.x | 4.0 with the default client |
|------|-----|-----------------------------|
| `SearchAnimeAsync("berserk")` | Left out adult entries | Same |
| `GetMangaRecommendationsAsync(id)` | Returned adult recommendations | Leaves them out |
| `GetMangaFullDataAsync(id)`, `GetAnimeFullDataAsync(id)` | Returned adult entries | Leaves them out |
| Relations, a character's or person's anime, manga and voice roles, genres | Returned adult entries | Leave them out |
| `GetMangaReviewsAsync(id)`, `GetAnimeReviewsAsync(id)` | Not filtered | Same (these endpoints take no `sfw`) |

**If your app needs adult entries**, which 3.x returned from those plain calls, turn the setting off where you create the client:

```csharp
var tenrai = new TenraiClient(new TenraiClientConfiguration { Sfw = false });

// With dependency injection
services.AddSingleton<ITenrai>(sp => new TenraiClient(
    new TenraiClientConfiguration { Sfw = false },
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("Tenrai")));
```

One call can still differ from the client setting:

```csharp
var all = await tenrai.SearchAnimeAsync(new AnimeSearchConfig { Query = "berserk", Sfw = false });
```

`SfwStrict = true` also leaves out ecchi entries; it is off by default. See [Safe-for-work filtering](GettingStarted.md#safe-for-work-filtering).

## Search configs

| 3.x | 4.0 |
|-----|-----|
| `PageSize = 10` | `Limit = 10` (maximum 100) |
| `Type = AnimeType.TV` | `Types = { AnimeType.TV }` |
| `Status = AiringStatus.Airing` | `Statuses = { AiringStatus.Airing }` |
| `Rating = AnimeAgeRating.PG13` | `Ratings = { AnimeAgeRating.PG13 }` |
| `MinimumScore = 7` (`int?`) | `MinimumScore = 7.5` (`double?`); same for `MaximumScore` |
| `Type = AnimeType.EveryType` | Leave `Types` empty |

The same applies to `MangaSearchConfig` (`Types` of `MangaType`, `Statuses` of `PublishingStatus`). Each list also has an excluded twin, for example `ExcludedTypes`.

```csharp
// 3.x
new AnimeSearchConfig { Query = "one", Type = AnimeType.TV, PageSize = 10 };

// 4.0
new AnimeSearchConfig { Query = "one", Types = { AnimeType.TV }, Limit = 10 };
```

### Removed and renamed values

| 3.x | 4.0 |
|-----|-----|
| `AnimeType.EveryType`, `MangaType.EveryType`, `AnimeAgeRating.EveryRating`, `AiringStatus.EveryStatus`, `PublishingStatus.EveryStatus` | Removed. Leave a list empty, or a single value `null` |
| `InterestStackType.EveryType` | Removed. `InterestStackSearchConfig.Type` is `InterestStackType?`; leave it `null` |
| `AnimeSearchOrderBy.Type`, `AnimeSearchOrderBy.Rating`, `MangaSearchOrderBy.Type` | Removed. The API answered these with a 400 |
| `MangaGenreSearch.Doujinshi` | Removed. No longer a MyAnimeList genre |
| `MangaGenreSearch.GenderBender` | `MangaGenreSearch.Crossdressing` |
| `ReviewFilter` | `InclusionFilter` (same values: `Include`, `Exclude`, `Only`) |

### Novels

`MangaType.Novel` sent `lightnovel` in 3.x. In 4.0 it sends `novel`, and light novels are `MangaType.LightNovel`. Code that used `Novel` for light novels still compiles but now finds a different, much smaller set.

```csharp
// 3.x: light novels
new MangaSearchConfig { Type = MangaType.Novel };

// 4.0: light novels
new MangaSearchConfig { Types = { MangaType.LightNovel } };
```

## Top anime and manga

| 3.x | 4.0 |
|-----|-----|
| `AnimeTopSearchConfig.Type`, `.Rating` | `Types`, `Ratings` (lists) |
| `MangaTopSearchConfig.Type` | `Types` (list) |
| `Filter = TopAnimeFilter.NoFilter` (or `TopMangaFilter.NoFilter`) | Leave `Filter` `null` for the overall ranking |

## Reviews

Each review endpoint now has its own config, since they take different filters.

| 3.x | 4.0 |
|-----|-----|
| `GetTopReviewsAsync(ReviewsSearchConfig)` | `GetTopReviewsAsync(TopReviewsConfig)`. `Preliminary` and `Spoilers` are `bool?`: the API answers 400 to "only" on top reviews |
| `GetRecentAnimeReviewsAsync(ReviewsSearchConfig)`, `GetRecentMangaReviewsAsync(ReviewsSearchConfig)` | Take a `RecentReviewsConfig` |
| `ReviewsSearchConfig.Limit`, `.Type` | Removed. Per-title reviews never took them |

```csharp
// 3.x
await tenrai.GetTopReviewsAsync(new ReviewsSearchConfig { Spoilers = ReviewFilter.Exclude });

// 4.0
await tenrai.GetTopReviewsAsync(new TopReviewsConfig { Spoilers = false });
```

## Forum topics

The forum methods threw `NotSupportedException` in 3.x. They work again and return pages.

| 3.x | 4.0 |
|-----|-----|
| `GetAnimeForumTopicsAsync(id)` returned `BaseTenraiResponse<ICollection<ForumTopic>>` (and threw) | Returns `PaginatedTenraiResponse<ICollection<ForumTopic>>`, 30 episode threads per page |
| `GetAnimeForumTopicsAsync(id, ForumTopicType.Episode)` | `GetAnimeForumTopicsAsync(id, new AnimeForumSearchConfig { Page = 2 })` |
| `ForumTopicType` | Removed. Tenrai serves only episode (anime) and chapter (manga) threads |

The same applies to `GetMangaForumTopicsAsync` with `MangaForumSearchConfig`.

## Response types

Most of these keep compiling, because the new type extends the old one.

| 3.x | 4.0 | Breaks when |
|-----|-----|-------------|
| `Manga.Authors`: `ICollection<MalUrl>` | `ICollection<AuthorEntry>` (adds `Role`, e.g. "Story & Art") | You assign the collection, or declare it as `ICollection<MalUrl>` |
| `RelatedEntry.Entry`: `ICollection<MalUrl>` | `ICollection<RelationEntry>` (adds `MediaType`, `Images`) | Same |
| `VoiceActorEntry.Person`: `MalImageSubItem` | `PersonEntry` (adds `Favorites`) | Same |
| `GetAnimeIdsAsync()` and the other bulk id calls: `BaseTenraiResponse<ICollection<long>>` | `IdsTenraiResponse` (adds `Total`) | You name the old type |
| `GetAnimeFullDataAsync(id)`, `GetMangaFullDataAsync(id)`: `BaseTenraiResponse<T>` | `FullTenraiResponse<T>`, the same type as the batch overloads | Never: it extends `BaseTenraiResponse<T>` |
| `UserRecommendation.Date`: `DateTime` | `DateTime?` (the API marks it nullable) | You use it as a non-null `DateTime` |

```csharp
// 3.x
DateTime when = recommendation.Date;

// 4.0
DateTime? when = recommendation.Date;
```

## No longer obsolete

`Title`, `TitleEnglish`, `TitleJapanese` and `TitleSynonyms` on `Anime` and `Manga` lost their `[Obsolete]` attribute: Tenrai still returns and documents them. A `#pragma warning disable CS0618` around them can go.
