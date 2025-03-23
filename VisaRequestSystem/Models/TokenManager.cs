using System;
using System.Collections.Generic;

namespace VisaRequestSystem.Models
{
    public static class TokenManager
    {
        private static Dictionary<string, int> requestCount = new Dictionary<string, int>
        {
            { "MED", 0 },
            { "TR", 0 },
            { "BS", 0 },
            { "GO", 0 }
        };

        private static int totalRequests = 0;
        private static int currentTokenId = 0;

        private const int MaxRequestsPerType = 25;
        private const int MaxRequestsPerDay = 200;


        private static List<string> reportLog = new List<string>();


        public static string GetToken(string visaType)
        {
            string prefix;

 
            switch (visaType)
            {
                case "Medical":
                    prefix = "MED";
                    break;
                case "Tourist":
                    prefix = "TR";
                    break;
                case "Business":
                    prefix = "BS";
                    break;
                case "Govt":
                    prefix = "GO";
                    break;
                default:
                    throw new ArgumentException("Invalid visa type.");
            }

            if (totalRequests >= MaxRequestsPerDay)
                return "Daily request limit reached.";


            if (requestCount[prefix] >= MaxRequestsPerType)
                return $"Request limit for {visaType} reached.";

            requestCount[prefix]++;
            totalRequests++;
            currentTokenId++;


            string token = $"{prefix}-{currentTokenId}";
            reportLog.Add($"{DateTime.Now:G}: Token {token} generated for {visaType}");
            return token;
        }


        public static void ResetCounts()
        {
            requestCount["MED"] = 0;
            requestCount["TR"] = 0;
            requestCount["BS"] = 0;
            requestCount["GO"] = 0;
            totalRequests = 0;
        }

        public static List<string> GetReport()
        {
            return new List<string>(reportLog);
        }
    }
}
