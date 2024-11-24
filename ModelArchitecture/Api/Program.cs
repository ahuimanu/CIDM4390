// database services
using Services.CaptivePortalDataService;
using Services.CaptivePortalEmailService;
using Services.CaptivePortalProfileJobService;

// SETUP
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// ENDPOINTS

// PROFILE ////////////////////////////////////////////////////////////////////
app.MapPost(
    "/profile/create/{email}",
    async (string email) =>
    {
        var job = GuestProfileJobFactory.CreateGuestProfileJob(email, DateTime.Now);
        var output = await GuestProfileJobScheduler.DoGuestProfileJobAsync(job!);
        return output;
    }
);

// GET /profile/get/{email}
app.MapGet(
    "/profile/get/{email}",
    async (string email) =>
    {
        return await CaptivePortalDbContext.GetGuestProfileAsync(email);
    }
);


// GET /profile/all
app.MapGet(
    "/profile/all",
    async () =>
    {
        return await CaptivePortalDbContext.GetAllGuestProfilesAsync();
    }
);

// VALIDATE ///////////////////////////////////////////////////////////////////
// GET /validate/{email}
app.MapGet(
    "/vadidate/{email}",
    (string email) =>
    {
        // call external service to validate email
        CaptivePortalEmailValidatorService emailValidator = new CaptivePortalEmailValidatorService();
        return emailValidator.IsValidEmail(email);
    }
);

// GET /validate/{email}
app.MapGet(
    "/vadidate/exists/{email}",
    async (string email) =>
    {
        // call external service to validate email
        string emailFound = await CaptivePortalDbContext.CheckEmailExistsAsync(email);
        return emailFound;
    }
);

// EXECUTE
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0#working-with-ports
app.Run("https://localhost:3000");