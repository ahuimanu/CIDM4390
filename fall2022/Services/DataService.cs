namespace Services.DataService;

using Microsoft.EntityFrameworkCore;

record class Airport
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}


class ApiDb : DbContext
{
    public ApiDb(DbContextOptions<ApiDb> options)
        : base(options) { }

    public DbSet<Airport> Airports => Set<Airport>();
}

record class CloudLayer
{
    public Dictionary<string, string>? CloudBase {get; set;}
    public string? Amount {get; set}
}

/// <summary>
/// Models the schema for a NOAA API Web Service Weather Station Observation
/// WMO Unit Documentation: http://codes.wmo.int/common/unit
/// Quality Control documentation: https://madis.ncep.noaa.gov/madis_sfc_qc_notes.shtml
/// </summary>
public record class WeatherStationObservation 
{
    public string? Geometry {get; set;}
    public Dictionary<string, string>? Elevation {get; set;}
    public string? Station {get; set;}
    public string? Timestamp {get; set;}
    public string? RawMessage {get; set;}
    public string? TextDescription {get; set;}
    public string? Icon {get; set;}
    public string[]? PresentWeather {get; set;}
    public Dictionary<string, string>? Temperature {get; set;}
    public Dictionary<string, string>? Dewpoint {get; set;}
    public Dictionary<string, string>? WindDirection {get; set;}
    public Dictionary<string, string>? WindSpeed {get; set;}
    public Dictionary<string, string>? WindGust {get; set;}
    public Dictionary<string, string>? BarometricPressure {get; set;}
    public Dictionary<string, string>? SeaLevelPressure {get; set;}
    public Dictionary<string, string>? Visibility {get; set;}
    public Dictionary<string, string>? MaxTemperatureLast24Hours {get; set;}
    public Dictionary<string, string>? MinTemperatureLast24Hours {get; set;}
    public Dictionary<string, string>? PrecipitationLastHour {get; set;}
    public Dictionary<string, string>? PrecipitationLast3Hours {get; set;}
    public Dictionary<string, string>? PrecipitationLast6Hours {get; set;}
    public Dictionary<string, string>? RelativeHumidity {get; set;}
    public Dictionary<string, string>? WindChill {get; set;}
    public Dictionary<string, string>? HeatIndex {get; set;}
    public CloudLayer[]? CloudLayers {get; set;}
}
