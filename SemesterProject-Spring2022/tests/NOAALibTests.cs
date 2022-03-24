using System;
using Xunit;
using Xunit.Abstractions;

using noaalib;

namespace tests;


// https://xunit.net/docs/shared-context#class-fixture
public  class NOAAHelperFixture : IDisposable
{
    public NOAAADDSStationHelper StationHelper { get; set; } = new NOAAADDSStationHelper();

    public void Dispose(){}
}

public class UnitTestNOAALib : IClassFixture<NOAAHelperFixture>
{

    private readonly NOAAHelperFixture fixture;

    /* Capturing Output in Xunit
     * https://xunit.net/docs/capturing-output
     */
    private readonly ITestOutputHelper output;

    public UnitTestNOAALib(ITestOutputHelper output, NOAAHelperFixture fixture)
    {
        //this uses dependency injection in Xunit
        this.output = output;
        this.fixture = fixture;
    }

    [Fact(DisplayName = "Can Create NOAA URL")]
    public void CanCreateNOAAURL()
    {
        // dotnet test --filter FullyQualifiedName~CanCreateNOAAURL --logger "console;verbosity=detailed"

        // Arrange
        // Act
        string url = fixture.StationHelper.CreateNOAARequestUri("KAMA");
        output.WriteLine(url);

        // Assert
        Assert.True(url.Length > 0);
    }

    [Fact(DisplayName = "Can Request NOAA XML")]
    public async void CanRequestNOAAXML()
    {

        // FullyQualifiedName~CanRequestNOAAXML
        // dotnet test --filter FullyQualifiedName~CanRequestNOAAXML --logger "console;verbosity=detailed"

        // Arrange
        // Act
        string xml = await fixture.StationHelper.RequestNOAAXML("KAMA");

        // If running tests via dotnet test, specify --logger "console"
        output.WriteLine(xml);

        // Assert
        Assert.True(xml.Length > 0);
        Assert.True(xml.Length > 10);
    }
}