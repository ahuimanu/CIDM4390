namespace ooplib;

using System;
using System.Collections.Generic;

public class WeatherReportingStation
{
    public string Identifier{get; set;}
    public bool ProvidesForecasts{get; set;}

    public List<WeatherReportingStation> GetDefaultStations()
    {
        return new List<WeatherReportingStation>() 
        {
            // using object initializers
            new WeatherReportingStation { Identifier = "KAMA", ProvidesForecasts = True},
            new WeatherReportingStation { Identifier = "KLBB", ProvidesForecasts = True},
            new WeatherReportingStation { Identifier = "KMAF", ProvidesForecasts = True},
        };
    }
}

public abstract class WeatherReport
{

    // static init and readonly
    public static readonly float duration;

    static WeatherReport(){
        Random rand = new Random();
        duration = rand.NextSingle();
    }

    //virtual and abstract
    public virtual string[] GetHazards();

    // FIELD
    // permissable modifiers
    /*
    Static modifier:        static 
    Access modifiers:       public internal private protected 
    Inheritance modifier:   new 
    Unsafe code modifier:   unsafe 
    Read-only modifier:     readonly 
    Threading modifier:     volatile
    */
    private int version = 1;

    // CONSTANTS
    internal const int MAJORVERSION = 1;

    // PROPERTIES, creates an implicate instance variable
    /* notes: 
        - this is an automatic property as there is no expression body
        - this uses a property initializer
        - is init-only (init replaces the set)
    */
    public DateTime ReportDate {get; init;} = DateTime.Now;

    // EXPRESSION-BODIED PROPERTY
    public DateTime ForeCastEndDateTime => ReportDate + TimeSpan.FromHours(24);

    // INSTANCE CONSTRUCTOR
    public WeatherReport(DateTime reportDate)
    {
        ReportDate = reportDate;
    }

    // INSTANCE DECONSTRUCTOR
    /*
    A deconstructor assigns fields back to a set of variables when the instance will be destroyed.
    */
    public void Deconstruct(out DateTime reportTimestamp)
    {
        reportTimestamp = ReportDate;
    }

    // FINALIZER
    ~WeatherReport()
    {
        //take some actions when the instance is marked for garbage collection
    }

    // METHODS - for internal modification/regulation or for services/messages
    // permissible modifiers
    /*
        Static modifier: static 
        Access modifiers: public internal private protected 
        Inheritance modifiers: new virtual abstract override sealed 
        Partial method modifier: partial 
        Unmanaged code modifiers: unsafe extern 
        Asynchronous code modifier: async
    */
    public bool ReportIsOutOfDate()
    {
        TimeSpan span = DateTime.Now - ReportDate;
        return span.TotalDays > 2 ? true : false;
    }

    // EXPRESSION-BODIED METHODS
    // provides a shorthand using the 'fat arrow' operator, 
    // which replaces the braces and return statement
    bool ReportIsFresh => (DateTime.Now - ReportDate).TotalHours < 2 ? true : false;

    // METHOD OVERLOADING
    // depends on the number, type, and order of method arguments

    static int factor = 60;

    long MakeTime(int days, int hours, int minutes)
    {
        return minutes + (hours * factor) + (days * factor * 24);
    }

    float MakeTime(int minutes, int seconds)
    {
        return minutes + (seconds / factor);
    }

    // LOCAL METHODS
    public float EstimatedWaitTime()
    {

        //local method
        float ShortFactor(float waittime) => waittime * 1.3f;

        float wait = (DateTime.Now - ReportDate).TotalSeconds;
        if( wait < 30)
        {
            wait = ShortFactor(wait);
        }

        return wait;
    }
}

public class MetarWeatherReport : WeatherReport
{
    public WeatherReportingStation Station{ get; set;}
    public override string[] GetHazards()
    {
        return new string[]{"one", "two", "three"};
    }
}

public class TafWeatherReport : WeatherReport
{
    public WeatherReportingStation Station{ get; set;}
    public List<DateTime> ForeCastPeriods{get; set;}

    public override string[] GetHazards()
    {
        return new string[]{"one.1", "one.2", "one.3","two.1", "two.2", "two.3"};
    }    
}