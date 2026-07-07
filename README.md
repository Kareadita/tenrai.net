![build status](https://img.shields.io/nuget/v/JikanDotNet.svg) [![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT) [![GitHub issues open](https://img.shields.io/github/issues/Ervie/jikan.net.svg?maxAge=2592000)]() 

# tenrai.net

Tenrai.net is a .NET wrapper for [Tenrai](https://tenrai.org) RESTful API for parsing data from [MyAnimeList](https://myanimelist.com). Main objective of the wrapper is to simplify utilization of Tenrai API, as strongly typed languages are not-so-easy to use with elastic json (sure we can go use dynamics in .NET, but let's think about performance).

Massive thanks to @Ervie who wrote the Jikan.NET library this is based from (https://github.com/Ervie/jikan.net).


### Differences to Jikan.NET
- Note all APIs are supported by Tenrai. I have left the methods but they will throw a NotImplemented exception
- Introduced the ability to attach a server key for enhanced rate

### Main attributes

* Written in to work with .NET Standard 2.0, compatible with .NET Framework (4.6.1 or newer) and .NET (6.0 or newer).
* Fully asynchronous request fetching (can be forced to synchromous if needed).
* Light on dependencies 
    * No dependencies if you are using .NET 6.0+
    * Single dependency for .NET Framework (System.Text.Json).
* Usable with Dependency Injection.

# List of features

- Anime
    - Basic information
    - Characters 
    - Staff
    - Episode
    - News
    - Videos/PV/Episodes
    - Pictures
    - Statistics
    - Forum Topics
    - More Info
    - Reviews
    - Recommendations
    - User Updates
    - Related entries
    - Themes
    - External links
    - Full information
- Manga
    - Basic information
    - Characters 
    - News
    - Pictures
    - Stats
    - Forum Topics
    - More Info
    - Reviews
    - Recommendations
    - User Updates
    - Related entries
    - External links
    - Full information
- People
    - Basic information
    - Related anime
    - Related manga
    - Voice acting roles
    - Pictures
    - Full information
- Characters
    - Basic information
    - Related anime
    - Related manga
    - Voice actors
    - Pictures
    - Full information
- Search 
    - Anime
    - Manga
    - People
    - Characters
    - Users
    - Clubs
- Seasonal Anime
    - Current
    - Upcoming
    - Archival
- Anime Scheduling (for current season)
- Top
    - Anime
    - Manga
    - People
    - Characters
    - Reviews
- Genre
    - Anime genres
    - Manga genres
- Producer
    - Basic information
    - External links
    - Full data
- Magazine
- User
    - Profile
    - Friends
    - History
    - Statistics
    - Favorites
    - About
    - Reviews
    - Recommendations
    - Clubs
    - Full data
- Clubs
    - Profile
    - Member list
    - Staff
    - Relations
# Installation

### Package manager

```
PM> Install-Package JikanDotNet
```

### .NET CLI

```
>dotnet add package JikanDotNet
```

Then restore dependencies:
```
>dotnet restore
```

# Changelog

## 25.04.2026 - Version 2.10.4

- Fix `TimePeriod.From`/`TimePeriod.To` losing UTC offset during deserialization (affects `Anime.Aired` and `Manga.Published`). Types changed from `DateTime?` to `DateTimeOffset?` - **breaking change**

## 22.04.2026 - Version 2.10.3

- Add `StartDate`/`EndDate` (`DateTime?`) to `AnimeSearchConfig` and `MangaSearchConfig`
- Fix excluded genres parameter in search config for `SearchAnimeAsync` and `SearchMangaAsync`

## 08.03.2026 - Version 2.10.2

- Add missing API method overloads (manga forum topic filter, video episodes pagination, watch promos pagination)
- Fix `SearchClubAsync` using incorrect endpoint
- Fix `GetAnimeVideosEpisodesAsync` return type to `PaginatedJikanResponse`

**[Read More](https://github.com/Ervie/jikan.net/blob/master/Changelog.md)**

# Documentation & Usage example

See [documentation](docs/README.md) for quick start, API reference, and guides.
