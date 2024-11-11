namespace Tests;

using Xunit;
using Xunit.Abstractions;

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
    public void TestA()
    {
        //arrange and act
        string answer = "the meaning of life is: 42";

        // output.WriteLine($"the meaning of life is: {answer}");

        //assert
        Assert.NotNull(answer);
    }
}