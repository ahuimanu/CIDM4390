namespace Services.CaptivePortalProfileJobService;

using Services.CaptivePortalDataService;
using Services.CaptivePortalEmailService;
using Services.CaptivePortalProfileService;
using Services.CaptivePortalProfileJobService;


public record GuestProfileJob
{
    public int ID { get; set; }
    public DateTime Date { get; set; }
    public string? Email { get; set; }
    public bool IsEmailValid { get; set; }
    public int JobFrequencyInMinutes { get; set; }
    public DateTime JobScheduledAt { get; set; }

}

public record GuestProfileJobResult
{
    public int ID { get; set; }
    public DateTime Date { get; set; }
    public string? Email { get; set; }
    public bool IsEmailValid { get; set; }
    public bool IsEmailSent { get; set; }
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

    // public int ID { get; set; }
    // public DateTime Date { get; set; }
    // public string? Email { get; set; }
    // public bool IsEmailValid { get; set; }

        int id,
        DateTime date,
        string? email,
        bool isEmailValid,
        DateTime jobScheduledAT
    )
    {
        return new GuestProfileJob
        {
            ID = id,
            Date = date,
            Email = email,
            IsEmailValid = isEmailValid,
            JobScheduledAt = jobScheduledAT,
        };
    }
}
/// <summary>
/// Class with static methods that manage weather report jobs.
/// </summary>
public class GuestProfileJobScheduler
{

    /// <summary>
    /// Called from a worker backgrund process to execute a scheduled job.
    /// </summary>
    /// <param name="job">WeatherReportJob to run</param>
    /// <returns>Completed WeatherReportJob</returns>
    public static async Task DoScheduledJobAsync(GuestProfileJob job)
    {

        //read job

        //create guest profile
        GuestProfile? profile = null;

        // call external service for validation
        // call external service to grant access

        //create the job result
        GuestProfileJobResult? outcome = null;

        //update job run timestamp

        //log the job result to the database
        await GuestProfileJobScheduler.LogGuestProfileJobResultAsync(outcome);

    }

    /// <summary>
    /// Get all jobs that are due to be run
    /// </summary>
    /// <returns>List of jobs to run</returns>
    public async static Task<List<GuestProfileJob>> GetScheduledJobsToRunAsync()
    {
        List<GuestProfileJob> currentJobs = new List<GuestProfileJob>();
        using (var db = new CaptivePortalDbContext())
        {
            currentJobs = await db.GetWeatherReportJobsDueAsync();
        }

        return currentJobs;
    }

    /// <summary>
    /// Gets all Weather Report Jobs
    /// </summary>
    /// <returns>List of Weather Report Jobs</returns>
    public async static Task<List<GuestProfileJob>> GetGuestProfileJobsAsync()
    {
        var jobs = new List<GuestProfileJob>();
        using (var db = new CaptivePortalDbContext())
        {
            jobs = await db.GetGuestProfileJobsAsync();
        }
        return jobs;
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

    /// <summary>
    /// Runs the Scheduled Jobs
    /// </summary>
    /// <returns>Task</returns>
    public async static Task RunScheduledJobs()
    {

        List<GuestProfileJob> currentJobs = await GetScheduledJobsToRunAsync();

        foreach (GuestProfileJob job in currentJobs)
        {
            await GuestProfileJobScheduler.DoScheduledJobAsync(job);
        }
    }


    /// <summary>
    /// Add job to the schedule
    /// </summary>
    /// <param name="job"></param>
    /// <returns>scheduled job</returns>
    public async static Task<GuestProfileJob> ScheduleGuestProfileJobAsync(GuestProfileJob job)
    {
        using (var db = new CaptivePortalDbContext())
        {
            await db.AddGuestProfileJobAsync(job);
        }
        return job;
    }

}