namespace Services.CaptivePortalProfileJobService;

using System.Threading.Tasks.Dataflow;

using Microsoft.EntityFrameworkCore;

using Services.CaptivePortalDataService;
using Services.CaptivePortalEmailService;
using Services.CaptivePortalProfileJobService;
using Services.CaptivePortalProfileService;

public record GuestProfileJob
{
    public int ID { get; set; }
    public DateTime Date { get; set; }
    public string? Email { get; set; }
}

public record GuestProfileJobResult
{
    public int ID { get; set; }
    public DateTime Date { get; set; }
    public string? Email { get; set; }
    public int GuestProfileID { get; set; }

}


/// <summary>
/// A Factory Class uses the Factory pattern where we pass the
/// Items used to create a class instance to a static method. This
/// provides the opportunity to check and audit the values to correct errors
/// or abort the instance creation.
/// </summary>
public class GuestProfileJobFactory
{
    public static GuestProfileJob? CreateGuestProfileJob(
        string? email,
        DateTime date
    )
    {
        var guestjob = new GuestProfileJob
        {
            Email = email,
            Date = date,
        };
        return guestjob;
    }
}
/// <summary>
/// Manages the scheduling of jobs to create guest profiles.
/// </summary>
public class GuestProfileJobScheduler
{

    /// <summary>
    /// Take job profile job to create profile
    /// </summary>
    /// <param name="job">GuestProfile job</param>
    /// <returns>Completed Guest Profile</returns>
    public static async Task<GuestProfile> DoGuestProfileJobAsync(GuestProfileJob job)
    {
        //read email from job
        string email = job.Email!;

        GuestProfile? profile = null;

        // check email
        EmailValidatorService emailValidator = new CaptivePortalEmailValidatorService();

        // validate email
        bool isEmailValid = emailValidator.IsValidEmail(email);

        // call external service for validation
        // TODO

        // check to see if the profile exists
        if (await CaptivePortalDbContext.CheckEmailExistsAsync(email) != email)
        {
            Console.WriteLine($"email found is: {email}");

            // create profile
            profile = new GuestProfile
            {
                Email = email
            };

            // add the profile to the database
            await CaptivePortalDbContext.AddGuestProfileAsync(profile);
        }

        // call external service to grant access
        // TODO

        //create the job result
        GuestProfileJobResult? outcome = new()
        {
            Email = email,
            Date = DateTime.Now,
            GuestProfileID = profile!.ID
        };

        //log the job result to the database
        await CaptivePortalDbContext.AddGuestProfileJobResultAsync(outcome);

        return profile;

    }
}