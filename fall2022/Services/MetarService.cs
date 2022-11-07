namespace Services.MetarService;


using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

public class WeatherGovAPI {

    static readonly string BaseUrl = @"https://api.weather.gov/";
    static readonly HttpClient client = new HttpClient();

    static WeatherGovAPI(){
        //make sure we accept JSON+LD
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/ld+json"));
    }

    public async Task<string> GetLastestObservationForStationAsync(string ICAOStationId)
    {
        string responseBody = "";
        try{
            responseBody = await client.GetStringAsync($"{BaseUrl}stations/{ICAOStationId}/observations/latest");
            //good resource for success codes: https://www.airport-data.com/api/doc.php#status

        }catch (HttpRequestException exp)
        {
            Console.WriteLine(exp.Message);
        }
        return responseBody;
    }
}