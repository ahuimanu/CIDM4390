namespace Tests;

using Xunit;
using Xunit.Abstractions;

using Services.CaptivePortalEmailService;

public class GuestProfileServicesUnitTests
{

    // capturing output
    // https://xunit.net/docs/capturing-output
    private readonly ITestOutputHelper output;

    public GuestProfileServicesUnitTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Fact]
    public void TestGuestProfileEmailIsValid()
    {

        //arrange
        string? email = "jane@browsers.org";

        Console.WriteLine($"Email: {email}");

        //act
        EmailValidatorService emailValidator = new CaptivePortalEmailValidatorService();

        // validate email
        bool isEmailValid = emailValidator.IsValidEmail(email);

        //assert
        Assert.True(isEmailValid);
    }
}