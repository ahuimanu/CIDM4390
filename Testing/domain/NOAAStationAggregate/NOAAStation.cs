using System;
using System.Collections.Generic;

namespace domain.NOAAStationAggregate
{
    public class NOAAStation {
        public string StateAbbreviation { get; set; }
        public string StationName { get; set; }
        public string ICAO { get; set; }
        public string IATA { get; set; }
        public string SynopticNumber { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Elevation { get; set; }

        public override string ToString()
        {
            return $"ICAO: {this.ICAO} - IATA: {this.IATA}";
        }
    }
}
