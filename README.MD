# CIDM 4390

IMPORTANT NOTE!  This repository uses a branching strategy to reflect the current semester of use.

## The Projects 

* *asp-code-identity* - a separate project that implements ASP.NET Core Identity according to the [Documentation - Introduction to Identity on ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-6.0&tabs=netcore-cli)
    * *Special Note:* You will want to ensure you also implement [Account confirmation and password recovery in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?view=aspnetcore-6.0&tabs=netcore-cli)
* *SemesterProject-Spring2021* - A rebuild of the *Testing* project to target [ASP.NET 6.0](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-6.0)
* *sendgrid-test* - A console app that uses the SendGrid API as well as `dotnet user-secrets`
* *Testing* - A project Implemented in ASP.NET 5.0 that provides a full example architecture that focuses on weather data from NOAA. This is provided as an example project architecture.  This is being re-written for ASP.NET 6.0.

## Git and Github Documentation

* The [GITHUB.MD](GITHUB.MD) file provides guidance on how to get the most out of Git and Github for your development process.

## Spring 2022
There is a [Git and Github](GITHUB.MD) Markdown document that is worth reviewing.

The files here contain a few examples of some of the software engineering concepts introduced in CIDM4390.

## Outdated Nuget Packages

There is a tool extension we can install that helps to find and update outdated packages that were installed using the dotnet cli.

[dotnet-outdated](https://github.com/dotnet-outdated/dotnet-outdated)

We update nuget packages with this command:

`dotnet tool install --global dotnet-outdated-tool`

It is then possible to use the -

## Web API

1. From the root solution folder: `dotnet new globaljson --sdk-version 5.1.200`
2. `mkdir webapi` and then `cd webapi`
3. from within this folder: `dotnet new webapi --no-https`
4. also in webapi: `dotnet add package Microsoft.EntityFrameworkCore --version 3.1.10`
5. also in webapi (necessary for the scaffolder to work): `dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 3.1.10`
5. also in webapi: `dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 3.1.10`
6. also in webapi: `dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 3.1.10`

### WebApi Controller Scaffolding

Scaffolding is a simple way to generate a controller for API Endpoints.

We have a bunch of adding to do:

```
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 3.1.4
dotnet add package Microsoft.EntityFrameworkCore.Design --version 3.1.10
dotnet tool install --global dotnet-aspnet-codegenerator --version 3.1.4
dotnet aspnet-codegenerator controller -name NOAAStationsController -async -api -m NOAAtation -dc WebApiDbContext -outDir Controllers
dotnet aspnet-codegenerator controller -name VatsimMETARController -async -api -m VatsimMETAR -dc WebApiDbContext -outDir Controllers
```

We'll test against this api.

## Architecture

SOLID principles

* [unit of work](https://pradeeploganathan.com/architecture/repository-and-unit-of-work-pattern-asp-net-core-3-1/)
* [my demo from spring 2020 that implements unit of work](https://github.com/ahuimanu/wizarddemo)
* [older asp.net mvc documentation regarding repository and unit of work](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)

## Testing

XUnit for unit testing.

Projects:
* webapi
* tests
