using System.ComponentModel.DataAnnotations;

namespace noaalib;

/// <summary>
/// https://aviationweather.gov/dataserver/fields?datatype=station
/// The station type, which can be a combination of the following:
/// METAR | rawinsonde | TAF | NEXRAD | wind_profiler | WFO_office | SYNOPS
/// Useful information on enums: https://betterprogramming.pub/enumerations-in-python-b01a1fb479de
/// </summary>
public enum StationType
{
    METAR,
    RAWINDSONDE,
    TAF,
    NEXTRAD,
    WIND_PROFILER,
    WFO_OFFICE,
    SYNOPS
}

/// <summary>
/// https://aviationweather.gov/dataserver/fields?datatype=station
/// </summary>
public class Station
{
    [Key]
    public string? StationId { get; set; }
    public string? WmoId { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public float ElevationInMeters { get; set; }
    public string? Site { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public StationType SiteType { get; set; }

}


