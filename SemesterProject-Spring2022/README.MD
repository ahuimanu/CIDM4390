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

# Required Global Dotnet Tooling - DO THESE FIRST
1. Identity Framework Core: `dotnet tool install --global dotnet-ef`

# First steps
1. Make sure you are using a 6.x version of the sdk: `dotnet --list-sdks`
1. [Uninstall all older SDKs](https://docs.microsoft.com/en-us/dotnet/core/install/remove-runtime-sdk-versions?pivots=os-windows)
1. Create a project directory: `mkdir SemsterProject-Spring2022`
1. Move into that directory: `cd SemsterProject-Spring2022`
1. Create a globaljson file: `dotnet new globaljson`
1. Create a new gitignore file: `dotnet new gitignore`
1. Create a solution file: `dotnet new sln`

# Create the Razor Pages App with Idenity

1. Create razor pages web app with Identity using SQLite: `dotnet new webapp --auth Individual -o webapp`
    1. Migrations are not necessary when using SQLite
1. Add razor pages web app to solution: `dotnet sln add ./webapp/webapp.csproj`
1. Consider using [DB Browser for SQLite](https://sqlitebrowser.org/) to verify transactions.

## Adding Identity Services Configuraiton

Some of these options are deemed commonly useful in [the tutorial](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-6.0&tabs=netcore-cli#configure-identity-services):

```C#
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});
```
## Scaffold Register, Login, LogOut, and RegisterConfirmation

1. Obtain and register scaffolding tools: `dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design`
1. When using SQLite, append `--useSqLite` or `-sqlite`: `dotnet aspnet-codegenerator identity -dc webapp.Data.ApplicationDbContext --files "Account.Register;Account.Login;Account.Logout;Account.RegisterConfirmation" --useSqLite`
    1. CRITICAL NOTE: With the default templates, the user is redirected to the `Account.RegisterConfirmation` where they can select a link to have the account confirmed. The default `Account.RegisterConfirmation` is used only for testing, automatic account verification should be disabled in a production app

## Account confirmation and password recovery in ASP.NET Core

The following steps assume that you will use SendGrid to send authentication emails. This is from the [Configure an email provider](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?view=aspnetcore-6.0&tabs=netcore-cli#configure-an-email-provider) portion of Microsoft's tutorial.  Although you do need to sign up with sendgrid, [you do not have to pay](https://sendgrid.com/free/).  Further, there are many services you can tap into, for free or with free credits, when you sign up for the FREE [GitHub Student Developer Pack](https://education.github.com/pack).

**DO NOT EVER ENTER A CREDIT CARD NUMBER, WHATSOEVER, IN ORDER TO COMPLETE ANY ASPECT OF THESE INSTRUCTIONS.**

## Creating a LibraryService for Email

1. Make a new directory for an email class library:
    1. from the same folder where the SLN file is located: 
        1. `mkdir services`
        1. `cd services`
        1. `mkdir emaillib`
        1. `cd emaillib`
1. create a new Class Library: `dotnet new classlib`
    1. rename `Class1.cs` to `EmailLib.cs`
    1. inside this file, rename `Class1` to `EmailLib`

## SendGrip API Key Options

There are two main options (further elaborated in [Safe storage of app secrets in development in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows)):

* *Environment Variables*: I prefer this approach [dotenv.net](https://github.com/bolorundurowb/dotenv.net) and would normally use this for my own implementation
* *Dotnet Secret Manager*: However, below I will use this tool, which is described in this [Secrets Management Article](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows#secret-manager)

What the tutorial says:

1. First, initialize the user-secrets tool for the webapp project: `dotnet user-secrets init`
1. Then, to use secrets to store your SendGrid API Key: `dotnet user-secrets set SendGridKey <key>`
1. Next, add the SendGrid package to the project: `dotnet add package SendGrid`
1. over in webapp, add the services:

```C#
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
```
1. The tutorial suggests that SendGrid will required that you create a Sender Persona and this is absolutely correct. [Adding a Sender at SendGrid](https://docs.sendgrid.com/ui/sending-email/senders).

# Acme Airline Scheduling Examples

I will be using some of the libraries necessary to demonstrate aspects of testing and architecture from this case.

## Testing and Libraries

The `services\noaalib` project contains several testable components that are demonstrated here.

### Notable aspects of using xUnit.

1. Most of our [xUnit tests](https://xunit.net/docs/getting-started/netcore/cmdline) will work with these types of tests:
    1. Test methods annotated with `[Fact]`: Facts are tests which are always true. They test invariant conditions.
    1. Test methods annotated with `[Theory]`: Theories are tests which are only true for a particular set of data.
        1. Theories use the `[InlineData()]` annotation to specify test data
1. Running tests uding the [dotnet test](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-test) command.
    .1 [Running Selective Tests](https://docs.microsoft.com/en-us/dotnet/core/testing/selective-unit-tests?pivots=mstest)
1. [Sharing Resources Accross Tests](https://xunit.net/docs/shared-context) - so you don't have to recreate the same object repeatedly within the tests themselves.
1. [The .NET Core Test Explorer](https://marketplace.visualstudio.com/items?itemName=formulahendry.dotnet-test-explorer) provides a nicer GUI experience for running tests in VS Code.

### Notable C#/.NET Capabilities Demonstrated
1. Q:"How can I download data from the Internet/Web?": 
    1. A: [HttpClient](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-6.0)
1. Q: "How can XML data from the web?":
    1. A: [https://docs.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/general/read-xml-data-from-url](How to read XML data from a URL)
1. Q: "How can I create a valid URL from variables?"
    1. A: [Uri](https://docs.microsoft.com/en-us/dotnet/api/system.uri?view=net-6.0) and [UriBuilder](https://docs.microsoft.com/en-us/dotnet/api/system.uribuilder.query?view=net-6.0#system-uribuilder-query)
    1. A: [QueryHelper](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.webutilities.queryhelpers?view=aspnetcore-5.0) - An [article](https://rehansaeed.com/asp-net-core-hidden-gem-queryhelpers/) demonstrating the use of QueryHelper
