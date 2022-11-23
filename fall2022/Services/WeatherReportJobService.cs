namespace Services.WeatherReportJobService;

using WeatherDataService;
using WeatherReportService;
using WeatherService;

/// <summary>
/// Indicates the type of job check, I'll just be doing the "store raw metar."
/// </summary>
public enum WeatherJobActionType
{
    CHECK_TEMPERATURE_QUALITY,
    CHECK_WIND_QUALITY,
}

/// <summary>
/// Indicates the type of job check, I'll just be doing the "store raw metar."
/// </summary>
public enum WeatherJobActionResult
{
    FAULT,
    OK,
}

/// <summary>
/// This would presumably be some indication of what to do for reconciliation.
/// TODO
/// </summary>
public enum WeatherJobReconciliationAction
{
    WRITE_TO_DB,
    WRITE_TO_LOG,
    WRITE_TO_EMAIL,
}

public record WeatherReportJob
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ICAOStationId { get; set; }
    public WeatherJobActionType JobActionType { get; set; }
    public long JobFrequencyInMinutes { get; set; }
    public DateTime JobScheduledAt { get; set; }
}

public record WeatherReportJobResult
{
    public int ID { get; set; }
    public int JobNumber; //job's id
    public string? Observation { get; set; }
    public WeatherJobActionType JobActionType { get; set; }
    public string? Status { get; set; }
}

/// <summary>
/// A Factory Class uses the Factory pattern where we pass the
/// Items used to create a class instance to a static method. This
/// provides the opportunity to check and audit the values to correct errors
/// or abort the instance creation.
/// </summary>
public class WeatherReportJobFactory
{
    public static WeatherReportJob? CreateWeatherReportJob(
        string name,
        string description,
        string icaoStationId,
        WeatherJobActionType jobActionType,
        long jobFrequencyInMinutes,
        DateTime jobScheduledAT
    )
    {
        return new WeatherReportJob
        {
            Name = name,
            Description = description,
            ICAOStationId = icaoStationId,
            JobActionType = jobActionType,
            JobFrequencyInMinutes = jobFrequencyInMinutes,
            JobScheduledAt = jobScheduledAT,
        };
    }
}

/// <summary>
/// Class with static methods that manage weather report jobs.
/// </summary>
public class WeatherReportJobScheduler
{
    public async static Task<WeatherReportJob> ScheduleWeatherReportJobAsync(WeatherReportJob job)
    {
        using (var db = new WeatherDbContext())
        {
            await db.AddWeatherReportJobAsync(job);
        }
        return job;
    }

    /// <summary>
    /// Called from a worker backgrund process to execute a scheduled job.
    /// </summary>
    /// <param name="job">WeatherReportJob to run</param>
    /// <returns>Completed WeatherReportJob</returns>
    public static async Task DoScheduledJobAsync(WeatherReportJob job)
    {
        // switch (job.JobActionType)
        // {
        //     case WeatherJobActionType.CHECK_TEMPERATURE_QUALITY:
        //         break;

        //     case WeatherJobActionType.CHECK_WIND_QUALITY:
        //         break;

        //     default:
        //         await Console.Error.WriteLineAsync("JOB TYPE INVALID");
        //         break;
        // }


        // return job;

        WeatherStationObservation obs =
            await WeatherDotGovAPI.GetLastestObservationAsync(job.ICAOStationId);

        string status = WeatherReportReconciler.WeatherReportJobCheck(obs, job.JobActionType);

        // public int JobNumber; //job's id
        // public string? Observation { get; set; }
        // public WeatherJobActionType JobActionType { get; set; }
        // public string? Status { get; set; }
        WeatherReportJobResult outcome = new WeatherReportJobResult()
        {
            JobNumber = job.ID,
            Observation = obs.RawMessage,

        };

    }

    /// <summary>
    /// Logs results of the completed job.
    /// </summary>
    /// <param name="result">WeatherReportJobResult</param></param>
    /// <returns>Completed WeatherReportJob</returns>
    public async static Task<WeatherReportJobResult> LogWeatherReportJobResultAsync(WeatherReportJobResult result)
    {
        using (var db = new WeatherDbContext())
        {
            await db.AddWeatherReportJobResultAsync(result);
        }
        return result;
    }

    /// <summary>
    /// Gets all Weather Report Jobs
    /// </summary>
    /// <returns>List of Weather Report Jobs</returns>
    public async static Task<List<WeatherReportJob>> GetWeatherReportJobsAsync()
    {
        var jobs = new List<WeatherReportJob>();
        using (var db = new WeatherDbContext())
        {
            jobs = await db.GetWeatherReportJobsAsync();
        }
        return jobs;
    }


    /// <summary>
    /// Get all jobs that are due to be run
    /// </summary>
    /// <returns>List of jobs to run</returns>
    public async static Task<List<WeatherReportJob>> GetScheduledJobsToRunAsync()
    {
        List<WeatherReportJob> currentJobs = new List<WeatherReportJob>();
        using (var db = new WeatherDbContext())
        {
            currentJobs = await db.GetWeatherReportJobsDueAsync();
        }

        return currentJobs;
    }
}
