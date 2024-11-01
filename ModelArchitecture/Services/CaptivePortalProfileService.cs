namespace Services.CaptivePortalProfileService;

public enum AgeGroup
{
    Child,
    Teen,
    Adult,
    Senior
}

public record GuestProfile
{
    public int ID { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public int Age { get; set; }

    public AgeGroup AgeGroup
    {
        get
        {
            if (Age < 13)
            {
                return AgeGroup.Child;
            }
            else if (Age < 20)
            {
                return AgeGroup.Teen;
            }
            else if (Age < 60)
            {
                return AgeGroup.Adult;
            }
            else
            {
                return AgeGroup.Senior;
            }
        }
    }   

}