# Using Azure Search with Auth0 and ASP.NET Core

This repository acts as sample in a scenario where you want to use Azure Search and integrate Auth0's custom database to create a rich search experience.

You can read the complete guide and use case in [Auth0's blog](https://auth0.com/blog/azure-search-with-aspnetcore/).

It includes an ASP.NET Core Web application with a [appsettings.json](https://github.com/ealsur/auth0search/blob/master/appsettings.json) file you need to configure with your account settings:

```javascript
{
 "Auth0": {
    "Domain": "{YOUR_AUTH0_DOMAIN}",
    "ClientId": "{YOUR_AUTH0_CLIENT_ID}",
    "ClientSecret": "{YOUR_AUTH0_CLIENT_SECRET}",
    "CallbackUrl": "{YOUR_CALLBACK_URL}"
  },
  "Search":{
    "QueryKey":"{YOUR_AZURE_SEARCH_QUERY_KEY}",
    "AccountName":"{YOUR_AZURE_SEARCH_ACCOUNT_NAME}"
  }
}
```
