# CIDM4390 Spring 2022

This project synthesizes the design and technical aspects of your project initiative for CIDM4390.

## Technologies

-   [ASP.NET Core Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-6.0&tabs=visual-studio-code)
    -   [Create a Razor Pages web app with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/razor-pages-start?view=aspnetcore-6.0&tabs=visual-studio-code)
-   [ASP.NET Core Web API](https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-6.0)
    -   [Create a web API with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio)
-   [ASP.NET Core Security and Identity](https://docs.microsoft.com/en-us/aspnet/core/security/?view=aspnetcore-6.0)
    -   [Create a Web app with authentication](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-6.0&tabs=netcore-cli#create-a-web-app-with-authentication)
-   [Entity Framework](https://docs.microsoft.com/en-us/ef/core/)

# First steps
1. Make sure you are using a 6.x version of the sdk: `dotnet --list-sdks`
1. [Uninstall all older SDKs](https://docs.microsoft.com/en-us/dotnet/core/install/remove-runtime-sdk-versions?pivots=os-windows)
1. Create a project directory: `mkdir SemsterProject-Spring2022`
1. Move into that directory: `cd SemsterProject-Spring2022`
1. Create a globaljson file: `dotnet new globaljson`
1. Create a new gitignore file: `dotnet new gitignore`
1. Create a solution file: `dotnet new sln`

# Create the Razor Pages App with Idenity

1. Create razor pages web app with Identity: `dotnet new webapp --auth Individual -uld -o webapp`
1. Add razor pages web app to solution: `dotnet sln add ./webapp/webapp.csproj`
1. 