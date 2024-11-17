namespace Services.CaptivePortalProfileJobService;

using Services.CaptivePortalDataService;
using Services.CaptivePortalEmailService;
using Services.CaptivePortalProfileService;
using Services.CaptivePortalProfileJobService;
using System.Threading.Tasks.Dataflow;

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
            Date = date,
            Email = email,
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
    /// Schedules a job to create a guest profile.
    /// </summary>
    /// <param name="job"></param>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="job"></param>
    /// <returns></returns>
    public static async Task<GuestProfile> DoGuestProfileJobAsync(GuestProfileJob job)
    {

        //read job
        string email = job.Email!;

        // check email
        EmailValidatorService emailValidator = new CaptivePortalEmailValidatorService();

        // validate email
        bool isEmailValid = emailValidator.IsValidEmail(email);

        // create profile
        GuestProfile? profile = new GuestProfile();
        profile.Email = email;

        // call external service for validation
        // TODO
        // call external service to grant access
        // TODO

        // add the profile to the database
        await GuestProfileJobScheduler.CreateGuestProfileAsync(profile);

        //create the job result
        GuestProfileJobResult? outcome = new GuestProfileJobResult();
        outcome.Email = email;
        outcome.Date = DateTime.Now;
        outcome.GuestProfileID = profile.ID;

        //log the job result to the database
        await GuestProfileJobScheduler.LogGuestProfileJobResultAsync(outcome);

        return profile;

    }

    /// <summary>
    /// Add a new guest profile
    /// </summary>
    /// <param name="profile"></param>
    /// <returns></returns>
    public async static Task<GuestProfile> CreateGuestProfileAsync(GuestProfile profile)
    {
        using (var db = new CaptivePortalDbContext())
        {
            await db.AddGuestProfileAsync(profile);
        }
        return profile;
    }


    /// <summary>
    /// Logs results of the completed job.
    /// </summary>
    /// <param name="result">WeatherReportJobResult</param></param>
    /// <returns>Completed WeatherReportJob</returns>
    public async static Task<GuestProfileJobResult> LogGuestProfileJobResultAsync(GuestProfileJobResult result)
    {
        using (var db = new CaptivePortalDbContext())
        {
            await db.AddGuestProfileJobResultAsync(result);
        }
        return result;
    }
}