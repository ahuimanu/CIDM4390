// database services
using Services.DataService;
using Services.WeatherService;
using Services.WeatherReportService;
using Services.WeatherReportJobService;

// SETUP
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var stations = new[]{
    ""
};

// ENDPOINTS
// GET /
app.MapGet("/job/all", async () => {
    List<WeatherReportJob> jobs = await WeatherReportJobScheduler.GetWeatherReportJobsAsync();
    return jobs;
});

// GET /obs/{station}
// app.MapGet("/obs/{id}", (string id) => $"Dude, this is your airport {id}");
app.MapGet(
    "/obs/{id}", 
    async (string id) => {
        var output = await WeatherDotGovAPI.GetLastestObservationForStationAsync(id);
        WeatherStationObservation? obs = WeatherDotGovAPI.DeserializeWeatherStationObservationFromJSON(output);
        // write to db here (call wep service to write to db)
        Console.WriteLine($"Returned value: {obs?.RawMessage}");
        return obs?.RawMessage;
    }
); 

app.MapPost(
    "/job/create",
    // do stuff here
    async (WeatherReportJob? job) => {
        var output = await WeatherReportJobScheduler.ScheduleWeatherReportJobAsync(job);
        return output;
    }
);



// EXECUTE
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0#working-with-ports
app.Run("https://localhost:3000");
