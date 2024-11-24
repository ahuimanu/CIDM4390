using Services.CaptivePortalDataService;
using Services.CaptivePortalProfileService;
using Services.CaptivePortalProfileJobService;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Checking the Job Scheduler");


GuestProfileJob? job = GuestProfileJobFactory.CreateGuestProfileJob(
    "user@example.com",     // email
    DateTime.Now            // date
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






