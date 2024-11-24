namespace Services.CaptivePortalEmailService;

using Microsoft.EntityFrameworkCore;

using Services.CaptivePortalDataService;
using EmailValidation;

public abstract class EmailValidatorService
{
    public abstract bool IsValidEmail(string email);
}

public class CaptivePortalEmailValidatorService : EmailValidatorService
{
    public override bool IsValidEmail(string email)
    {
        email = email.Trim();
        return EmailValidator.Validate(email);
    }
}

