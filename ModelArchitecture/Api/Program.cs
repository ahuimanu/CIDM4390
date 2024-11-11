// database services
using Services.CaptivePortalDataService;

// background worker
using JobWorker;
using Services.CaptivePortalProfileJobService;
using Services.CaptivePortalEmailService;

// SETUP
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var stations = new[]{""};

// ENDPOINTS
// GET /job/all
app.MapGet("/jobs/all", async () => {
    List<GuestProfileJob> jobs = await GuestProfileJobScheduler.GetGuestProfileJobsAsync();
    return jobs;
});

app.MapGet("/jobs/due", async () => {
    List<GuestProfileJob> jobs = await GuestProfileJobScheduler.GetScheduledJobsToRunAsync();
    return jobs;
});

// POST /job/create 
app.MapPost(
    "/jobs/create",
    // do stuff here
    async (GuestProfileJob? job) => {
        var output = await GuestProfileJobScheduler.ScheduleGuestProfileJobAsync(job!);
        return output;
    }
);

// GET /validate/{email}
app.MapGet(
    "/vadidate/{email}", 
    (string email) => {

        
        // call external service to validate email
        CaptivePortalEmailValidatorService emailValidator = new CaptivePortalEmailValidatorService();
        
        return emailValidator.IsValidEmail(email);
    }
);

// EXECUTE
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0#working-with-ports
app.Run("https://localhost:3000");
