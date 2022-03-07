using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace sendgrid_test;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Sending an Email using the SendGrid v3 API");

        var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
        string apiKey = config.GetSection("SENDGRID_API_KEY").Value;
        string fromAddress = config.GetSection("EMAIL_FROM_ADDRESS").Value;
        string toAddress = config.GetSection("EMAIL_TO_ADDRESS").Value;        

        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(fromAddress, "SendGrid Test From Address");
        var subject = "This is a SendGrid Test";
        var to = new EmailAddress(toAddress, "SendGrid Test From Address");
        var plainTextContent = "Completed using C#";
        var htmlContent = "<strong>Completed using C#</strong>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

        Console.WriteLine($"{response.StatusCode}");        
    }
}
