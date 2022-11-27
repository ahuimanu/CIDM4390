namespace Services.WeatherReportService;

using WeatherReportJobService;

public class WeatherReportReconciler
{

    /// <summary>
    /// Checks for the job action status code and applies fixed rules.
    /// TODO: create a rules object so that rules can be adjusted.
    /// </summary>
    /// <param name="obs">Weather Station Observation</param>
    /// <param name="jobActionType">Job Check Action Type</param>
    /// <returns></returns>
    public static string WeatherReportJobCheck(
        WeatherStationObservation obs, 
        WeatherJobActionType jobActionType)
    {
        string status = "";

        decimal gust = 0;
        decimal windspeed = 0;

        // make windspeed a decimal
        if (obs.WindSpeed!.Value != null){
            windspeed = (decimal)obs.WindSpeed.Value;
        }

        // gust value isn't aways present
        if (obs.WindGust!.Value == null) {
            gust = 0;
        }

        switch(jobActionType)
        {
            case WeatherJobActionType.CHECK_TEMPERATURE_QUALITY:
                if(obs.Temperature!.Value <= 10)
                {
                    status = "ICING CONDITIONS";
                } 
                else if(obs.Temperature!.Value >= 40)
                {
                    status = "CHECK DENSITY ALTITUDE";
                }
                else{
                    status = "NORMAL";
                }
                break;
            
            case WeatherJobActionType.CHECK_WIND_QUALITY:
                if(obs.WindSpeed!.Value < 0)
                {
                    status = "NEGATIVE WIND SPEED NOT POSSIBLE";
                }
                else if(obs.WindSpeed!.Value >= 35)
                {
                    status = "HIGH WIND CONDITIONS";
                }
                else if(Math.Abs(windspeed - gust) >= 15)
                {
                    status = "WIND SHEAR ALERT";
                }
                break;

            default:
                status = "INVALID JOB ACTION TYPE";
                break;                
        }

        return status;

    }
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
    public int? Value { get; set; }
}

public class Temperature
{
    public string? UnitCode { get; set; }
    public float? Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Dewpoint
{
    public string? UnitCode { get; set; }
    public float? Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Winddirection
{
    public string? UnitCode { get; set; }
    public float? Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Windspeed
{
    public string? UnitCode { get; set; }
    public float? Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Windgust
{
    public string? UnitCode { get; set; }
    public float? Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Barometricpressure
{
    public string? UnitCode { get; set; }
    public float? Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Sealevelpressure
{
    public string? UnitCode { get; set; }
    public float? Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Visibility
{
    public string? UnitCode { get; set; }
    public float? Value { get; set; }
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
    public float? Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Windchill
{
    public string? UnitCode { get; set; }
    public float? Value { get; set; }
    public string? QualityControl { get; set; }
}

public class Heatindex
{    
    public string? UnitCode { get; set; }
    public float? Value { get; set; }
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
    public float? Value { get; set; }
}