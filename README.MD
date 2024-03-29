# CIDM 4390

A few examples of some software engineering concepts.

# Minimum Required Tools
* VS Code
* Most Recent Dotnet SDK (8.x)
* Git 

## SPRING 2024

There are some project examples I'll commonly refer to:

-   One project example is in the [fall2022](fall2022) folder. This example is somewhat specific to one of the pojects for this course, but many archictural and design principles are present here
-   There will be more pfojects to come.

## Outdated Nuget Packages

There is a tool extension we can install that helps to find and update outdated packages that were installed using the dotnet cli.

[dotnet-outdated](https://github.com/dotnet-outdated/dotnet-outdated)

We update nuget packages with this command:

`dotnet tool install --global dotnet-outdated-tool`

if you have already installed this tool, you can update it this way:

`dotnet tool update --global dotnet-outdated-tool`

It is then possible to use the `dotnet-outdated` tool to see which packages in your project can be updated.  Take care that there maybe changes in new versions as you adopt them.

You can be walked through an interactive updating session with the following command: `dotnet-outdated -u:prompt`

## Tools

We can also see what tools are installed and what versions they are at: `dotnet tool list --global`
You can update a specific tool like this: `dotnet tool update dotnet-ef --global`

Most tools are installed globally, which makes that additional switch important.

The EntityFramework tooling is installed globally: `dotnet tool install --global dotnet-ef --version 8.0.3`

Don't forget that version numbers change all the time.

## Web API

**NOTE**: the specific version below are not important and simply making sure that everyone sharing your project is using the SAME versions.

* Find out what version of the dotnet runtime you are using: `dotnet --version`
* Find out what version of the dotnet sdk are installed: `dotnet --list-sdks`
* Find out what version of Entity Framework you are using: `dotnet ef --version`

1. From the root solution folder: `dotnet new globaljson --sdk-version 8.0.203`
2. `mkdir webapi` and then `cd webapi`
3. from within this folder: `dotnet new webapi --no-https`
4. also in webapi: `dotnet add package Microsoft.EntityFrameworkCore --version 8.0.3`
5. also in webapi (necessary for the scaffolder to work): `dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0.4`
6. also in webapi: `dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 7.0.4`
7. also in webapi: `dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 7.0.4`

### WebApi Controller Scaffolding

Scaffolding is a simple way to generate a controller for API Endpoints.

We have a bunch of adding to do:

```
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 8.0.2
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.3
dotnet tool install --global dotnet-aspnet-codegenerator --version 8.0.3
dotnet aspnet-codegenerator controller -name NOAAStationsController -async -api -m NOAAtation -dc WebApiDbContext -outDir Controllers
dotnet aspnet-codegenerator controller -name VatsimMETARController -async -api -m VatsimMETAR -dc WebApiDbContext -outDir Controllers
```

We'll test against this api.

## Architecture

SOLID principles

-   [unit of work](https://pradeeploganathan.com/architecture/repository-and-unit-of-work-pattern-asp-net-core-3-1/)
-   [my demo from spring 2020 that implements unit of work](https://github.com/ahuimanu/wizarddemo)
-   [older asp.net mvc documentation regarding repository and unit of work](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)

## Testing

XUnit for unit testing.

Projects:

-   webapi
-   tests
