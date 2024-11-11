using Services.CaptivePortalDataService;
using Services.CaptivePortalProfileService;
using Services.CaptivePortalProfileJobService;


// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Checking API");
// string answer = await WeatherDotGovAPI.GetLastestObservationForStationAsync("KAMA");
// Console.WriteLine($"answer is: {answer}");

Console.WriteLine("Checking the Job Scheduler");

GuestProfileJob? job = GuestProfileJobFactory.CreateGuestProfileJob(
    1,
    DateTime.Now,
    "user@example.com",
    true,
    DateTime.Now
);

Console.WriteLine($"Submitting job for {job}");

// the "bang" here is the null forgiving operator
await GuestProfileJobScheduler.ScheduleGuestProfileJobAsync(job!);

Console.WriteLine("Job Scheduled");

Console.WriteLine("Get Jobs");

using (var db = new CaptivePortalDbContext())
{
    var jobs = db.GuestProfileJobs.ToList<GuestProfileJob>();
    foreach (var wjob in jobs)
    {
        Console.WriteLine(wjob);
    }
}






