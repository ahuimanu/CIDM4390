// database services
using Services.DataService;
using Services.WeatherService;

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
app.MapGet("/", () => @"This is the Example API");

// GET /obs/{station}
// app.MapGet("/obs/{id}", (string id) => $"Dude, this is your airport {id}");
app.MapGet(
    "/obs/{id}", 
    async (string id) => {
        var output = await WeatherDotGovAPI.GetLastestObservationForStationAsync(id);
        WeatherStationObservation? obs = WeatherDotGovAPI.DeserializeWeatherStationObservationFromJSON(output);
        Console.WriteLine($"Returned value: {obs?.RawMessage}");
        return obs?.RawMessage;
    }
); 

// EXECUTE
app.Run();
