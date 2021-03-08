using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace domain.NOAAStationAggregate
{

    public class NOAAStationHelper
    {

        public static readonly string NOAA_STATION_SOURCE_URL = "https://aviationweather.gov/docs/metar/stations.txt";

        public static string GetNOAAStationString(string url)
        {
            HttpClient client = new HttpClient();
            return client.GetStringAsync(url).Result;
        }

        public static List<NOAAStation> GetNOAAStationsList()
        {

            List<NOAAStation> stationsList = new List<NOAAStation>();

            string stations = GetNOAAStationString(NOAA_STATION_SOURCE_URL);

            //credit: https://stackoverflow.com/a/1508217
            var lines = Regex.Split(stations, "\r\n|\r|\n");

            foreach(string line in lines)
            {
                if(line.Length == 83)
                {
                    NOAAStation sta = NOAAStationHelper.ParseStationLine(line);
                    if(sta.ICAO.Length == 4)
                    {
                        if(!stationsList.Contains(sta))
                        {
                            stationsList.Add(sta);                            
                        } else {
                            Console.WriteLine($"check this value: {sta}");
                        }
                    }
                }
            }

            return stationsList;
        }

        public static NOAAStation ParseStationLine(string line)
        {

            string output = "";
            string cd = line.Substring(0,3).Trim();
            output += $"cd:{cd}|";
            string station = line.Substring(3,17).Trim();
            output += $"station:{station}|";
            string icao = line.Substring(20, 6).Trim();
            output += $"icao:{icao}|";
            string iata = line.Substring(24, 6).Trim();
            output += $"iata:{iata}|";
            string synop = line.Substring(32, 7).Trim();
            output += $"synop:{synop}|";
            string lat = line.Substring(38, 8).Trim();
            output += $"lat:{lat}|";                  
            string lon = line.Substring(46, 8).Trim();
            output += $"lon:{lon}|";
            string elevation = line.Substring(54, 5).Trim();
            output += $"elevation:{elevation}|";

            NOAAStation sta = new NOAAStation {
                StateAbbreviation = cd,
                StationName = station,
                ICAO = icao,
                IATA = iata,
                SynopticNumber = synop,
                Latitude = lat,
                Longitude = lon,
                Elevation = elevation,
            };            

            return sta;
        }

        public static async Task<NOAAStation> ParseStationLineStringAync(string line)
        {
            return await Task<string>.Run(() => ParseStationLine(line));
        }

        private static bool SkipLine(string line)
        {
            bool skip = false;

            if(line.StartsWith("!"))
            {
                skip = true;
                return skip;
            }
            else if(line == "")
            {
                skip = true;
                return skip;                
            }
            else if(line.Length != 83)
            {
                skip = true;
                return skip;
            }

            return skip;
        }
   }
}