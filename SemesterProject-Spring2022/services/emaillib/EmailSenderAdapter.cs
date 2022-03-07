using SendGrid;
using SendGrid.Helpers.Mail;

namespace emaillib;

public class EmailSenderAdapter
{

    public async Task SendEmailAsync(string toEmail, string subject, string message, string SendGridKey)
    {
        if (string.IsNullOrEmpty(SendGridKey))
        {
            throw new Exception("Null SendGridKey");
        }
        await Execute(SendGridKey, subject, message, toEmail);
    }

    public async Task Execute(string apiKey, string subject, string message, string toEmail)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress("ahuimanu@gmail.com", "Password Recovery"),
            Subject = subject,
            PlainTextContent = message,
            HtmlContent = message
        };
        msg.AddTo(new EmailAddress(toEmail));

        // Disable click tracking.
        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
        msg.SetClickTracking(false, false);
        var response = await client.SendEmailAsync(msg);
    }
}