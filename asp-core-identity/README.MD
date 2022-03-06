# Demonstrating ASP.NET Core

NOTE: Refactored for DOTNET 6.0 - Spring 2022

This is a walkthrough using the [Introduction to ASP.NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-5.0&tabs=netcore-cli) Tutorial.

This was written in Markdown, if you don't know what that is, look [here](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet).

## Removing HTTPS while developing

1. Comment out HTTPS redirects in `Startup.cs`: in the `Configure()` method, comment this out: 
```cs
//app.UseHttpsRedirection();
```
2. Change the applicationUrl property in launchSeetings.json: see below

```json
// "applicationUrl": "https://localhost:5001;http://localhost:5000",
"applicationUrl": "http://localhost:5000",
```

## Updating Tools

If you've had prior versions of the ASP.NET Core SDK installed, it may be necessary to [update the aspnet-codegenerator tool](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/tools/dotnet-aspnet-codegenerator?view=aspnetcore-5.0). 

