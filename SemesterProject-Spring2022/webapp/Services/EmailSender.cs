using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

using emaillib;


namespace webapp.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger _logger;
    private  EmailSenderAdapter SenderAdapter {get; } = new EmailSenderAdapter();

    public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                       ILogger<EmailSender> logger)
    {
        Options = optionsAccessor.Value;
        _logger = logger;
    }

    public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        if (string.IsNullOrEmpty(Options.SENDGRID_API_KEY))
        {
            throw new Exception("Null SendGridKey");
        }
        _logger.LogInformation("SENDGRID_API_KEY OK");

        if (string.IsNullOrEmpty(Options.EMAIL_FROM_ADDRESS))
        {
            throw new Exception("Null SendGridKey");
        }        
        _logger.LogInformation("EMAIL_FROM_ADDRESS OK");

        // this calls our seperate service
        await SenderAdapter.SendEmailAsync(
            Options.SENDGRID_API_KEY, 
            subject, 
            message, 
            toEmail, 
            Options.EMAIL_FROM_ADDRESS);
    }
}