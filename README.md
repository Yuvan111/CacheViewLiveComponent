# CacheView Live Component SSR Sample

A focused .NET 11 Blazor static SSR sample for validating how `CacheView` replays cached markup while designated live components rerender in place and a streaming child completes asynchronously.

## Repository description

.NET 11 Blazor static SSR sample that validates per-user `CacheView` output, live component rerendering, captured parameters, stable component ordering, and streaming rendering on cache hits.

## Prerequisites

- .NET SDK `11.0.100-rc.1.26431.118`
- A browser with a normal and private window, or two separate browser profiles

The required preview SDK is pinned in `global.json`. Confirm the selected SDK before running the sample:

```bash
dotnet --version
```

## Run the sample

```bash
dotnet restore
dotnet run
```

Open `http://localhost:5280/cache-view`. Unauthenticated requests redirect to `/sign-in`, where the sample offers two test identities: Alice and Bob.

> The sign-in endpoints are deliberately simplified for local testing. They are not suitable for production authentication.
