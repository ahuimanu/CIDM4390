namespace Services.CaptivePortalProfileJobService;

using Services.CaptivePortalDataService;
using Services.CaptivePortalEmailService;
using Services.CaptivePortalProfileService;
using Services.CaptivePortalProfileJobService;


public record GuestProfileJob
{
    public int ID { get; set; }
    public string? Email { get; set; }
    public bool IsEmailValid { get; set; }

}


