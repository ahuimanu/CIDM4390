using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace webapi.Models
{
    public class VatsimMETAR
    {

        public static readonly string VATSIM_METAR_URL_PREFIX = "https://metar.vatsim.net/metar.php?id=";

        public static async Task<VatsimMETAR> GetVatsimMETARFromIDAsync(string id) {

            VatsimMETAR report = null;

            string url = $"{VATSIM_METAR_URL_PREFIX}{id}";
            HttpClient client = new HttpClient();
            HttpResponseMessage msg = await client.GetAsync(url);
            string raw = await msg.Content.ReadAsStringAsync();

            if(ValidateMETARID(id, raw))
            {
                DateTime obs = ParseObservationTimeFromString(raw);
                string obsRetrievedAsISO8601 = DateTime.UtcNow.ToString("s");
                report = new VatsimMETAR{
                    RetreivedTimeStamp = obsRetrievedAsISO8601,
                    ICAO = id,
                    Raw = raw,
                    ObservationTime = obs,
                };
            }

            return report;
        }

        public static DateTime ParseObservationTimeFromString(string raw) {

            // build regexps: https://regexr.com/
            string expr = @"([0-9])+Z";
            MatchCollection mc = Regex.Matches(raw, expr);

            string day = "";
            string hour = "";
            string minute = "";                                            

            if(mc.Count > 0){
                Console.WriteLine($"MATCH FOUND! {mc[0]}");

                string zuluTimestamp = mc[0].ToString();

                day = ParseDayFromMETARZuluTimeStamp(zuluTimestamp);
                hour = ParseHourFromMETARZuluTimeStamp(zuluTimestamp);
                minute = ParseMinuteFromMETARZuluTimeStamp(zuluTimestamp);

                Console.WriteLine($"Parsed: Day: {day} | Hour: {hour} | Minute: {minute}");

                return new DateTime(DateTime.UtcNow.Year,       //year
                                    DateTime.UtcNow.Month,      //month
                                    DateTime.UtcNow.Day,        //day
                                    Convert.ToInt32(hour),      //hour utc
                                    Convert.ToInt32(minute),    //minute utc
                                    0);                         //second utc

            } else {
                Console.WriteLine($"NO MATCH");
                return DateTime.Now;                
            }
        }

        public static string ParseDayFromMETARZuluTimeStamp(string zulu) {
            return zulu.Substring(0,2);
        }

        public static string ParseHourFromMETARZuluTimeStamp(string zulu) {
            return zulu.Substring(2,2);
        }

        public static string ParseMinuteFromMETARZuluTimeStamp(string zulu) {
            return zulu.Substring(4,2);
        }        

        public static bool ValidateMETARID(string id, string raw) {
            return raw.StartsWith(id);
        }     

        [Key]
        public string RetreivedTimeStamp { get; set; }
        public string Raw { get; set; }
        public string ICAO { get; set; }
        public DateTime ObservationTime { get; set; }


    }
}
