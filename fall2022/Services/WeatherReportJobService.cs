namespace Services.WeatherReportJobService;

using DataService;
using WeatherReportService;

public enum WeatherJobActionType {
    CHECK_TEMPERATURE_QUALITY,
    CHECK_WIND_QUALITY,
}

public enum WeatherJobReconciliationAction {
    WRITE_TO_DB,
    WRITE_TO_LOG,
    WRITE_TO_EMAIL,
}

public record WeatherReportJobResult
{
    public int JobNumber; //job's id
    public WeatherStationObservation? Observation {get; set;}
    public bool Status {get; set;}
}

public record WeatherReportJob 
{
    public int ID{get; set;}
    public string? Name{get; set;}
    public string? Description {get; set;}
    public WeatherJobActionType JobType {get; set;} 

    //expressed in minutes
    public long JobFrequencyInMinutes {get; set;}
    public DateTime JobScheduledAt {get; set;}
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
        return new WeatherReportJob{
            Name = name,
            Description = description,
            JobType = jobActionType,
            JobFrequencyInMinutes = jobFrequencyInMinutes,
            JobScheduledAt = jobScheduledAT,
        };
    }
}

public class WeatherReportJobScheduler{
    public async static Task<WeatherReportJob> ScheduleWeatherReportJobAsync(WeatherReportJob job)
    {
        using (var db = new ApiDbContext())
        {
            await db.AddWeatherReportJobAsync(job);
        }
        return job;   
    }
}


