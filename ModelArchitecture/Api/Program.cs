// database services
using Services.CaptivePortalDataService;

// background worker
// using JobWorker;
using Services.CaptivePortalProfileJobService;
using Services.CaptivePortalEmailService;

// SETUP
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddHostedService<Worker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// ENDPOINTS

app.MapPost(
    "/profile/create/{email}",
    async (string email) => {
        var job = GuestProfileJobFactory.CreateGuestProfileJob(email, DateTime.Now);
        var output = await GuestProfileJobScheduler.DoGuestProfileJobAsync(job!);
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
