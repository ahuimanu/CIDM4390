using System;

namespace domain.VatsimMETARAggregate
{
    public class VatsimMETAR
    {
        public string RetreivedTimeStamp { get; set; }
        public string Raw { get; set; }
        public string ICAO { get; set; }
        public DateTime ObservationTime { get; set; }
    }
}
