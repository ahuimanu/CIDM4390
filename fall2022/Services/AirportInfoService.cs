namespace Services.AirportInfoService;

public record Airport
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
}