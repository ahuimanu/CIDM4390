namespace Tests;

using Xunit;
using Xunit.Abstractions;

using Services.WeatherService;

public class WeatherServicesUnitTests
{

    // capturing output
    // https://xunit.net/docs/capturing-output
    private readonly ITestOutputHelper output;

    public WeatherServicesUnitTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public async Task TestGetLastestObservationForStationAsync()
    {
        //arrange and act
        string answer = await WeatherDotGovAPI.GetLastestObservationForStationAsync("KAMA");

        // output.WriteLine($"the meaning of life is: {answer}");

        //assert
        Assert.NotNull(answer);
    }
}