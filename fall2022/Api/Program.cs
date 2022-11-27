// database services
using Services.WeatherService;
using Services.WeatherReportService;
using Services.WeatherReportJobService;

// background worker
using JobWorker;

// SETUP
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var stations = new[]{""};

// ENDPOINTS
// GET /job/all
app.MapGet("/jobs/all", async () => {
    List<WeatherReportJob> jobs = await WeatherReportJobScheduler.GetWeatherReportJobsAsync();
    return jobs;
});

app.MapGet("/jobs/due", async () => {
    List<WeatherReportJob> jobs = await WeatherReportJobScheduler.GetScheduledJobsToRunAsync();
    return jobs;
});

// POST /job/create 
app.MapPost(
    "/jobs/create",
    // do stuff here
    async (WeatherReportJob? job) => {
        var output = await WeatherReportJobScheduler.ScheduleWeatherReportJobAsync(job!);
        return output;
    }
);

// GET /obs/{station}/raw
app.MapGet(
    "/obs/{id}/raw", 
    async (string id) => {

        WeatherStationObservation? obs = await WeatherDotGovAPI.GetLastestObservationAsync(id);
        // write to db here (call wep service to write to db)
        Console.WriteLine($"Returned value: {obs?.RawMessage}");
        return obs?.RawMessage;
    }
);

// EXECUTE
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0#working-with-ports
app.Run("https://localhost:3000");
