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
        if (string.IsNullOrEmpty(Options.SendGridKey))
        {
            throw new Exception("Null SendGridKey");
        }
        await SenderAdapter.SendEmailAsync(Options.SendGridKey, subject, message, toEmail);
    }
}