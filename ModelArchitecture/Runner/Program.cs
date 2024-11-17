using Services.CaptivePortalDataService;
using Services.CaptivePortalProfileService;
using Services.CaptivePortalProfileJobService;


// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Checking API");
// string answer = await WeatherDotGovAPI.GetLastestObservationForStationAsync("KAMA");
// Console.WriteLine($"answer is: {answer}");

Console.WriteLine("Checking the Job Scheduler");


GuestProfileJob? job = GuestProfileJobFactory.CreateGuestProfileJob(
    1,                      // id   
    DateTime.Now,           // date
    "user@example.com",     // email
    true                    // isEmailValid
);

Console.WriteLine($"Submitting job for {job}");

// the "bang" here is the null forgiving operator
using (var db = new CaptivePortalDbContext())
{
    var jobs = db.GuestProfileJobs.ToList<GuestProfileJob>();
    foreach (var wjob in jobs)
    {
        Console.WriteLine(wjob);
    }
}

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






