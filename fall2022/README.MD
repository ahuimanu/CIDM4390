# APIs - Production and Consumption

CIDM4390 - Fall 2022

## Projects
* Api (WebAPI simple) - A web service that handles requests for work (includes library calling and db work)
* App - RazorPages front-end for client requests
* Services - Class Libraries that determine models and business logic actions
* Tests - Unit tests using xUnit
* Worker - A background worker that handles tasks on an interval

NOTE: Ignore the "Runner" project, it is for simple testing and won't be kept.

## IMPORTANT USE NOTES
1. I would git clone this or use "Code -> Download Zip" from the main repo page
1. Since the migrations are in place in the Services project, you should be good to go.
    1. just in case, you can go to the Service project and do `dotnet ef database update`
1. **IMPORTANT**!!! You must run Api and App in their own terminals (e.g. `dotnet run`)
    1. in VS Code, use the PLUS button in the terminal
    1. in VS Code, you can right click on your second terminal and select "Split Terminal"
    1. it is possible to have these start together and I might make that improvement later

1. Remember that the SQLite database will be created in the Api project `bin` directory
1. Use the **SQLite** VS Code Extension to help view the contents of a SQLite database file to check results.
1. I don't have every last thing checked here, but all of the pieces do fit together


## External APIs
* METAR - [Weather.gov | API Web Service](https://www.weather.gov/documentation/services-web-api)
* XPlane - [Scenery Gateway API Documentation](https://gateway.x-plane.com/api)
* Airports - 
    * [Wikipedia Page Scraping](https://en.wikipedia.org/wiki/Dallas_Fort_Worth_International_Airport)
    * [Airport Data API](https://www.airport-data.com/api/doc.php)
    * Additional Airport Information - [NavData](https://developer.x-plane.com/article/navdata-in-x-plane-11/)

## References
* [Tutorial: Create a minimal web API with ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio-code)
* [Test APIs with HttpRepl](https://learn.microsoft.com/en-us/aspnet/core/web-api/http-repl/?view=aspnetcore-6.0&tabs=windows)
* [JSON Serialize and Deserialize](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview?pivots=dotnet-6-0)
* [Background tasks with hosted services in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-6.0&tabs=netcore-cli)
* [Working with nullable reference types in C#](https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references)


### JSON Structure of a JOb
``` json
{
    "name":"Test Job 4",
    "description": "Test Job Description 4",
    "icaoStationId": "KAUS",
    "jobActionType": 1,
    "jobFrequencyInMinutes": "5",
    "jobScheduledAT" : "2022-11-23T21:45:37.3083145Z"
}
```