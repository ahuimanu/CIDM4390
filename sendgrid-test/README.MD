# SendGrid Console Test

This project was created as a part of the [Debugging Process recommended by Microsoft](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?view=aspnetcore-6.0&tabs=netcore-cli#debug-email) in the use of SendGrid as an Email provider for your use of ASP.NET Identity Core.

This is a quick test to see if SendGrid works. [Documentation from Twillio/SendGrid](https://github.com/sendgrid/sendgrid-csharp).

SendGrid provides this code example: [v3 API C# Code Example](https://docs.sendgrid.com/for-developers/sending-email/v3-csharp-code-example).

Since we are in development, we can use the dotnet [Secret Manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=linux).

## Secrets Manager

1. initialize in a project: `dotnet user-secrets init`
1. set the SENDGRID_API_KEY: `dotnet user-secrets set "<key>" "<value>"`
1. I do the same thing for environment variables to contain my to and from email addresses.
1. This is a good example for using the secrets in development in a console app: [Managing Secrets in .NET Console Apps](https://swharden.com/blog/2021-10-09-console-secrets/)

## Steps for  Console Test

1. install: `dotnet add package SendGrid`
1. use the [Hello Email Quick Guide](https://github.com/sendgrid/sendgrid-csharp#hello-email) from SendGrid.
