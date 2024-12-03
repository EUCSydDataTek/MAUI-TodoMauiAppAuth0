# TodoApi

A simple Minimal API with ASP.NET Core, build around this tutorial: [Tutorial: Create a minimal API with ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/test/http-files?view=aspnetcore-8.0).

Database: SQLite

NuGet packages:
- Microsoft.EntityFrameworkCore.Sqlite
- Microsoft.EntityFrameworkCore.Tools 
- Microsoft.AspNetCore.Authentication.JwtBearer

&nbsp;


## Migrations

In the PMB terminal, run the following commands:

```add-migration InitialCreate```

```update-database```

Database is wiped and recreated with every run of the application (can be changed in `Program.cs`)

&nbsp;

## Security

JWT according to this article: [Authentication and authorization in minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/security).

