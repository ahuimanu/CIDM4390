namespace Services.WeatherReportService;


// public record CloudLayerOld
// {
//     public Dictionary<string, string>? CloudBase {get; set;}
//     public string? Amount {get; set;}
// }

// public record WeatherStationObservationOld
// {
//     public string? Geometry {get; set;}
//     public Dictionary<string, string>? Elevation {get; set;}
//     public string? Station {get; set;}
//     public string? Timestamp {get; set;}
//     public string? RawMessage {get; set;}
//     public string? TextDescription {get; set;}
//     public string? Icon {get; set;}
//     public string[]? PresentWeather {get; set;}
//     public Dictionary<string, string>? Temperature {get; set;}
//     public Dictionary<string, string>? Dewpoint {get; set;}
//     public Dictionary<string, string>? WindDirection {get; set;}
//     public Dictionary<string, string>? WindSpeed {get; set;}
//     public Dictionary<string, string>? WindGust {get; set;}
//     public Dictionary<string, string>? BarometricPressure {get; set;}
//     public Dictionary<string, string>? SeaLevelPressure {get; set;}
//     public Dictionary<string, string>? Visibility {get; set;}
//     public Dictionary<string, string>? MaxTemperatureLast24Hours {get; set;}
//     public Dictionary<string, string>? MinTemperatureLast24Hours {get; set;}
//     public Dictionary<string, string>? PrecipitationLastHour {get; set;}
//     public Dictionary<string, string>? PrecipitationLast3Hours {get; set;}
//     public Dictionary<string, string>? PrecipitationLast6Hours {get; set;}
//     public Dictionary<string, string>? RelativeHumidity {get; set;}
//     public Dictionary<string, string>? WindChill {get; set;}
//     public Dictionary<string, string>? HeatIndex {get; set;}
//     public CloudLayerOld[]? CloudLayers {get; set;}
// }


public class WeatherReportReconciler
{
    
}

/// <summary>
/// Models the schema for a NOAA API Web Service Weather Station Observation
/// WMO Unit Documentation: http://codes.wmo.int/common/unit
/// Quality Control documentation: https://madis.ncep.noaa.gov/madis_sfc_qc_notes.shtml
/// </summary>
public class WeatherStationObservation
{
    public Context? Context { get; set; }
    public string? Id { get; set; }
    public string? Type { get; set; }
    public string? Geometry { get; set; }
    public Elevation? Elevation { get; set; }
    public string? Station { get; set; }
    public DateTime? Timestamp { get; set; }
    public string? RawMessage { get; set; }
    public string? TextDescription { get; set; }
    public string? Icon { get; set; }
    public object[]? PresentWeather { get; set; }
    public Temperature? Temperature { get; set; }
    public Dewpoint? Dewpoint { get; set; }
    public Winddirection? WindDirection { get; set; }
    public Windspeed? WindSpeed { get; set; }
    public Windgust? WindGust { get; set; }
    public Barometricpressure? BarometricPressure { get; set; }
    public Sealevelpressure? SeaLevelPressure { get; set; }
    public Visibility? Visibility { get; set; }
    public Maxtemperaturelast24hours? MaxTemperatureLast24Hours { get; set; }
    public Mintemperaturelast24hours? MinTemperatureLast24Hours { get; set; }
    public Precipitationlasthour? PrecipitationLastHour { get; set; }
    public Precipitationlast3hours? PrecipitationLast3Hours { get; set; }
    public Precipitationlast6hours? PrecipitationLast6Hours { get; set; }
    public Relativehumidity? RelativeHumidity { get; set; }
    public Windchill? WindChill { get; set; }
    public Heatindex? HeatIndex { get; set; }
    public Cloudlayer[]? CloudLayers { get; set; }
}

public class Context
{
    public string? Version { get; set; }
    public string? Wx { get; set; }
    public string? S { get; set; }
    public string? Geo { get; set; }
    public string? Unit { get; set; }
    public string? Vocab { get; set; }
    public Geometry? Geometry { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public Distance? Distance { get; set; }
    public Bearing? Bearing { get; set; }
    public Value? Value { get; set; }
    public Unitcode? UnitCode { get; set; }
    public Forecastoffice? ForecastOffice { get; set; }
    public Forecastgriddata? ForecastGridData { get; set; }
    public Publiczone? PublicZone { get; set; }
    public County? County { get; set; }
}

public class Geometry
{
    public string? Id { get; set; }
    public string? Type { get; set; }
}

public class Distance
{
    public string? Id { get; set; }
    public string? Type { get; set; }
}

public class Bearing
{
    public string? Type { get; set; }
}

public class Value
{
    public string? Id { get; set; }
}

public class Unitcode
{
    public string? Id { get; set; }
    public string? Type { get; set; }
}

public class Forecastoffice
{
    public string? Type { get; set; }
}

public class Forecastgriddata
{
    public string? Type { get; set; }
}

public class Publiczone
{
    public string? Type { get; set; }
}

public class County
{
    public string? Type { get; set; }
}

public class Elevation
{
    public string? UnitCode { get; set; }
    public int Value { get; set; }
}

public class Temperature
{
    public string? UnitCode { get; set; }
    public float Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Dewpoint
{
    public string? UnitCode { get; set; }
    public float Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Winddirection
{
    public string? UnitCode { get; set; }
    public float Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Windspeed
{
    public string? UnitCode { get; set; }
    public float Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Windgust
{
    public string? UnitCode { get; set; }
    public object? Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Barometricpressure
{
    public string? UnitCode { get; set; }
    public float Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Sealevelpressure
{
    public string? UnitCode { get; set; }
    public float Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Visibility
{
    public string? UnitCode { get; set; }
    public float Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Maxtemperaturelast24hours
{
    public string? UnitCode { get; set; }
    public object? Value { get; set; }
}

public class Mintemperaturelast24hours
{
    public string? UnitCode { get; set; }
    public object? Value { get; set; }
}

public class Precipitationlasthour
{
    public string? UnitCode { get; set; }
    public object? Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Precipitationlast3hours
{
    public string? UnitCode { get; set; }
    public object? Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Precipitationlast6hours
{
    public string? UnitCode { get; set; }
    public object? Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Relativehumidity
{
    public string? UnitCode { get; set; }
    public float Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Windchill
{
    public string? UnitCode { get; set; }
    public object? Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Heatindex
{    
    public string? UnitCode { get; set; }
    public object? Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Cloudlayer
{
    public Base? _base { get; set; }
    public string? amount { get; set; }
}

public class Base
{
    public string? UnitCode { get; set; }
    public object? Value { get; set; }
}