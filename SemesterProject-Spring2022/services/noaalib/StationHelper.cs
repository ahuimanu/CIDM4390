using System.Net.Http;
using System.Xml;

using Microsoft.AspNetCore.WebUtilities;

namespace noaalib;

public abstract class StationHelper
{
    // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract
    public abstract Station GetStationFromStationId(string stationId);

    public abstract Station GetStationFromLatLon(float latitude, float longitude);
}

public class NOAAADDSStationHelper : StationHelper
{

    static readonly HttpClient client = new HttpClient();

    private string NOAA_ADDS_URL = 
        "https://aviationweather-bldr.ncep.noaa.gov/adds/dataserver_current/httpparam";
    
    private string NOAA_ADDS_FORMAT = "xml";

    public override Station GetStationFromStationId(string stationId)
    {
        // 1. Create proper URL
        // 2. get XML from NOAA
        // 3. Parse XML to create Station Object
        return new Station();
    }

    public override Station GetStationFromLatLon(float latitude, float longitude)
    {
        return new Station();
    }

    /// <summary>
    /// Parse NOAA XML into Station Object
    /// </summary>
    /// <param name="xml">xml string from NOAA</param>
    /// <returns>Station Object</returns>
    public string ParseNOAAXML(string xml)
    {

        XmlTextReader reader = new XmlTextReader(xml);

        return "";
    }

    /// <summary>
    /// Builds a valid NOAA URL
    /// </summary>
    /// <param name="stationId">Station ID</param>
    /// <returns>URL string</returns>
    public string CreateNOAARequestUri(string stationId)
    {
        // prepare URL

        // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.webutilities.queryhelpers?view=aspnetcore-5.0
        var queryArguments = new Dictionary<string, string>();

        // https://rehansaeed.com/asp-net-core-hidden-gem-queryhelpers/
        // "dataSource": "stations",        
        queryArguments.Add("dataSource", "stations");
        // "requestType": "retrieve",        
        queryArguments.Add("requestType", "retrieve");
        // "format": format,        
        queryArguments.Add("format", $"{NOAA_ADDS_FORMAT}");
        // "stationString": station_id.strip(),
        queryArguments.Add("stationString", $"{stationId.Trim()}");

        // https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=net-6.0
        UriBuilder builder = new UriBuilder(
            QueryHelpers.AddQueryString(NOAA_ADDS_URL, queryArguments)
        );        

        return builder.ToString();
    }

    /// <summary>
    /// Sends request to the NOAA ADDS server to obtain needed data
    /// </summary>
    /// <param name="stationId">NOAA Station ID</param>
    /// <returns>XML Station String</returns>
    public async Task<string> RequestNOAAXML(string stationId)
    {

        Uri stationUri = new UriBuilder(CreateNOAARequestUri(stationId)).Uri;

        string xmlResponse = "";        

        // Call asynchronous network methods in a try/catch block to handle exceptions.
        try	
        {
            xmlResponse = await client.GetStringAsync(stationUri);
        }
        catch(HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");	
            Console.WriteLine("Message :{0} ",e.Message);
        }

        return xmlResponse;

    }
}