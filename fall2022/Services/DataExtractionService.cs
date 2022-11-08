namespace Services.DataExtractionService;

using HtmlAgilityPack;

public record AirportEntry {
    public string? ICAO {get; init;}
    public string? Name {get; init;}
}

public class WikipediaAirportScraper
{
    public static AirportEntry[] TexasAirports = 
    {
        // KDFW	Dallas Fort Worth International Airport
        new AirportEntry{ ICAO="KDFW", Name="Dallas Fort Worth International Airport"},
        
        // KIAH	George Bush Intercontinental/Houston Airport
        new AirportEntry{ ICAO="KIAH", Name="George Bush Intercontinental/Houston Airport"},
        
        // KDAL	Dallas Love Field
        new AirportEntry{ ICAO="KDAL", Name="Dallas Love Field"},

        // KAUS	Austin–Bergstrom International Airport
        new AirportEntry{ ICAO="KAUS", Name="Austin–Bergstrom International Airport"},

        // KHOU	William P. Hobby Airport
        new AirportEntry{ ICAO="KHOU", Name="William P. Hobby Airport"},

        // KSAT	San Antonio International Airport
        new AirportEntry{ ICAO="KSAT", Name="San Antonio International Airport"},

        // KELP	El Paso International Airport
        new AirportEntry{ ICAO="KELP", Name="El Paso International Airport"},

        // KMAF	Midland International Air and Space Port
        new AirportEntry{ ICAO="KMAF", Name="Midland International Air and Space Port"},

        // KLBB	Lubbock Preston Smith International Airport
        new AirportEntry{ ICAO="KLBB", Name="Lubbock Preston Smith International Airport"},

        // KAMA	Rick Husband Amarillo International Airport
        new AirportEntry{ ICAO="KAMA", Name="Rick Husband Amarillo International Airport"},

        // KMFE	McAllen Miller International Airport
        new AirportEntry{ ICAO="KMFE", Name="McAllen Miller International Airport"},

        // KCRP	Corpus Christi International Airport
        new AirportEntry{ ICAO="KCRP", Name="Corpus Christi International Airport"},

        // KHRL	Valley International Airport
        new AirportEntry{ ICAO="KHRL", Name="Valley International Airport"},

        // KGRK	Killeen–Fort Hood Regional Airport / Robert Gray Army Airfield
        new AirportEntry{ ICAO="KGRK", Name="Killeen–Fort Hood Regional Airport / Robert Gray Army Airfield"},

        // KBRO	Brownsville/South Padre Island International Airport
        new AirportEntry{ ICAO="KBRO", Name="Brownsville/South Padre Island International Airport"},

        // KLRD	Laredo International Airport
        new AirportEntry{ ICAO="KLRD", Name="Laredo International Airport"},

        // KCLL	Easterwood Field
        new AirportEntry{ ICAO="KCLL", Name="Easterwood Field"},

        // KABI	Abilene Regional Airport
        new AirportEntry{ ICAO="KABI", Name="Abilene Regional Airport"},

        // KACT	Waco Regional Airport
        new AirportEntry{ ICAO="KACT", Name="Waco Regional Airport"},

        // KSJT	San Angelo Regional Airport (Mathis Field)    
        new AirportEntry{ ICAO="KSJT", Name="San Angelo Regional Airport (Mathis Field)"},
    };

    public static bool ValidateTexasAirportICAO (string icao)
    {
        AirportEntry? result = Array.Find<AirportEntry>(TexasAirports, el => el.ICAO.Equals(icao));
        return result != null;
    }

}