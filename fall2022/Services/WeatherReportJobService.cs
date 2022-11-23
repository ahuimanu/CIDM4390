namespace Services.WeatherReportJobService;

using WeatherDataService;
using WeatherReportService;

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
    public WeatherJobActionType JobActionType { get; set; }

    //expressed in minutes
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

public class WeatherReportJobFactory
{
    public static WeatherReportJob? CreateWeatherReportJob(
        string name,
        string description,
        WeatherJobActionType jobActionType,
        long jobFrequencyInMinutes,
        DateTime jobScheduledAT
    )
    {
        return new WeatherReportJob
        {
            Name = name,
            Description = description,
            JobActionType = jobActionType,
            JobFrequencyInMinutes = jobFrequencyInMinutes,
            JobScheduledAt = jobScheduledAT,
        };
    }
}

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

    // public async Task<List<WeatherReportJob>> GetScheduledJobsToRun()
    // {
    //     List<WeatherReportJob> currentJobs = await new List<WeatherReportJob>();
    //     return currentJobs;
    // }

    public async Task DoScheduledJob(WeatherReportJob job)
    {
        switch (job.JobActionType)
        {
            case WeatherJobActionType.CHECK_TEMPERATURE_QUALITY:
                break;

            case WeatherJobActionType.CHECK_WIND_QUALITY:
                break;

            default:
                await Console.Error.WriteLineAsync("JOB TYPE INVALID");
                break;
        }
    }

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
}


