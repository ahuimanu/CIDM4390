using SendGrid;
using SendGrid.Helpers.Mail;

namespace emaillib;

public class EmailSenderAdapter
{

    //Options.SendGridKey, subject, message, toEmail, fromEmail
    public async Task SendEmailAsync(string SendGridKey, 
                                     string subject, 
                                     string message, 
                                     string toEmail, 
                                     string fromEmail)
    {
        if (string.IsNullOrEmpty(SendGridKey))
        {
            throw new Exception("Null SendGridKey");
        }
        await Execute(SendGridKey, subject, message, toEmail, fromEmail);
    }

    public async Task Execute(string apiKey, 
                              string subject, 
                              string message, 
                              string toEmail, 
                              string fromEmail)
    {
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(fromEmail, "Password Recovery"),
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