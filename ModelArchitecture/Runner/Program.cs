using Services.CaptivePortalDataService;
using Services.CaptivePortalProfileJobService;
using Services.CaptivePortalProfileService;

// this is a quick console app to test the job scheduler. This is useful to see
// how the job scheduler works in a simple console app where the steps are all in one place.

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Make a new Guest Profile Job");


GuestProfileJob? job = GuestProfileJobFactory.CreateGuestProfileJob(
    "user@example.com",     // email
    DateTime.Now            // date
);

Console.WriteLine($"Submitting job for {job}");

// bang operator is the null-forgiving operator
GuestProfile profile = await GuestProfileJobScheduler.DoGuestProfileJobAsync(job!);