using Services.DataService;
using Services.WeatherService;
using Services.WeatherReportJobService;


// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Checking API");
// string answer = await WeatherDotGovAPI.GetLastestObservationForStationAsync("KAMA");
// Console.WriteLine($"answer is: {answer}");

Console.WriteLine("Checking the Job Scheduler");

WeatherReportJob? job = WeatherReportJobFactory.CreateWeatherReportJob(
    "KAMA Temperature",
    "KAMA Tempeature Check",
    WeatherJobActionType.CHECK_TEMPERATURE_QUALITY,
    new Random().Next(1,100),
    DateTime.Now
);

Console.WriteLine($"Submitting job for {job}");

// the "bang" here is the null forgiving operator
await WeatherReportJobScheduler.ScheduleWeatherReportJobAsync(job!);

Console.WriteLine("Job Scheduled");

Console.WriteLine("Get Jobs");

using (var db = new ApiDbContext())
{
    var jobs = db.WeatherReportJobs.ToList<WeatherReportJob>();
    foreach(var wjob in jobs){
        Console.WriteLine(wjob);
    }
}






