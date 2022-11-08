using Services.WeatherService;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("DA API");

string answer = await WeatherDotGovAPI.GetLastestObservationForStationAsync("KAMA");

Console.WriteLine($"answer is: {answer}");
