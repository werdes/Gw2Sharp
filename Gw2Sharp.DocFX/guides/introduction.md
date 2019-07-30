---
uid: Guides.Introduction
title: Gw2Sharp Introduction
---

# Introduction
The library is written to be as close as possible with the API endpoints.
This means that the basic functionality is easy to understand.

You start with creating a `Connection`:
```cs
// The constructor also accepts overloads with an access token and locale
var connection = new Gw2Sharp.Connection();
```

Then you'll need to create the `Gw2Client`:
```cs
var client = new Gw2Sharp.Gw2Client(connection);
var renderClient = client.WebApi.Render;
var webApiClient = client.WebApi.V2;
```

Now you're ready!  
Keep in mind that all API calls are done asynchronously.
Depending on the API endpoint, you'll find that the following methods are available:
- `IdsAsync()` - Gets the full list of ids
- `GetAsync()` - Gets a single item or an API blob object
- `ManyAsync()` - Gets multiple items at once (bulk)
- `PageAsync()` - Gets a page
- `AllAsync()` - Gets all items at once (bulk all)

Check the [services](xref:Guides.Services) for the full list of supported services.

If an endpoint has sub-endpoints, you can access them as a property.

For example, you can do the following:

```cs
// Get all item ids
var ids = await client.WebApi.V2.Items.IdsAsync();

// Get a single achievement
var item = await client.WebApi.V2.Achievements.GetAsync(123);

// Get multiple gliders
var items = await client.WebApi.V2.Gliders.ManyAsync(new[] { 123, 456 });

// Get itemstats by page
var page = await client.WebApi.V2.Itemstats.PageAsync(5);

// Get all colors
var all = await client.WebApi.V2.Colors.AllAsync();

// Access account sub-endpoints (e.g. account/home/cats that contains blob data)
var accountCats = await client.WebApi.V2.Account.Home.Cats.GetAsync();
```