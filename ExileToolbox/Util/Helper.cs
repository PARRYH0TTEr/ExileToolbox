﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Runtime.CompilerServices;
using ExileToolbox.Parsing.Types;
using System.Text.RegularExpressions;
using ExileToolbox.Web.API.Schemas;
using ExileToolbox.PriceCheck.Trade;

namespace ExileToolbox.Util
{
    public static class Helper
    {
        private static statsSchema statsJSONDeserialized;

        private static string selectedLeague;

        static Helper()
        {
            Init();
        }
        

        public static void Init()
        {
            string statsJSONPath = Path.Combine(AppContext.BaseDirectory, "API", "Databases", "stats.json");
            string statsJSONFile = File.ReadAllText(statsJSONPath);

            statsJSONDeserialized = JsonSerializer.Deserialize<statsSchema>(statsJSONFile);

            selectedLeague = UserSettings.SelectedLeague;
        }

        public static string RemoveAll(string originalString, string[] toBeRemovedSubStrings)
        {
            string retString = originalString;

            foreach (string subString in toBeRemovedSubStrings)
            {
                retString = retString.Replace(subString, "");
            }

            return retString;
        }

        // The parameter is assumed to look something like this string literal "#-#"
        public static int GetAvgDamage(string damageInterval)
        {
            // 1. Split to string[]
            // 2. Convert to int[]
            // 3. Reduce/Aggregate to combined damage
            // 4. Divide for average damage
            int avgDamage = (Array.ConvertAll(damageInterval.Split("-"), int.Parse)
                                                            .Aggregate(0, (acc, x) => acc + x)) / 2;

            return avgDamage;
        }

        // Maps a mod (affix) on an item to its associated modID in the stats.json file
        public static string MapModEntry(string modEntry, string affixGroup)
        {
            try
            {
                var affGroup = statsJSONDeserialized.Result.Single(afg => afg.Id.Equals(affixGroup, StringComparison.OrdinalIgnoreCase));

                var modId = affGroup.Entries.Single(aff => aff.Text.Equals(modEntry, StringComparison.Ordinal));

                return modId.ID;
            }
            catch (Exception ex) //TODO: Consider an implementation that filters the mapping of mods without using exceptions
            {                       
                return string.Empty;
            }
        }

        // Retrieve the numerical values in a given affix. Could be 0, 1 or 2 values
        public static List<int>? GetAffixValues(string modEntry)
        {

            var matches = Regex.Matches(modEntry, @"-?\d+");

            if (matches.Count == 0)
            {
                return null;
            }

            List<int> numbers = new List<int>();

            foreach (Match match in matches)
            {
                if (int.TryParse(match.Value, out int num))
                {
                    numbers.Add(num);
                }
            }

            return numbers;

        }

        // Map the parsed item to an equivalent TradeRequest type
        //TODO: For now, it only parses explicit mods, but will be expanded later
        //          In the future, implement selection of which mods to include in the query, as well as whether
        //          to include the values or not
        public static TradeRequest MapParsedItemToTradeRequest(ParsedItem parsedItem)
        {
            var statfilters = parsedItem.explicitAffixEntries
                                        .Where(entry => !string.IsNullOrEmpty(entry.AffixId))
                                        .Select(entry => new StatFilter
                                        {
                                            Id = entry.AffixId,
                                            Disabled = false,
                                            //Value = new StatFilterValue
                                            //{
                                            //    Min = entry.min,
                                            //    Max = entry.max
                                            //}
                                            Value = null
                                        }).ToList();


            var tradeRequest = new TradeRequest
            {
                Query = new Query
                {
                    Status = new Status
                    {
                        Option = "online"
                    },
                    Stats = new List<StatFilterGroup>
                    {
                        new StatFilterGroup
                        {
                            Type = "and",
                            Filters = statfilters
                        }
                    }
                }
            };

            return tradeRequest;

        }

        //public static T JsonStringToTemplate<T>(string json)
        //{

        //}


        // Simple helper method to convert a template to its Json-string equivalent
        public static string TemplateToJsonString<T>(T templatetInstance)
        {
            var serializationOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            return JsonSerializer.Serialize(templatetInstance, serializationOptions);
        }


        // Given a FetchRequest, format the trade site url with the request-id and selected league
        public static string FormatTradeSearchUrl(FetchingInfo fRequest)
        {
            return $"{Constants.POE2_TRADE_Search}/{selectedLeague}/{fRequest.Id}";
        }




        // Convenience-method to retrieve the title of the current active window
        // Used in HotkeyWatcher to only invoke methods if a PoE window is active.
        public static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder titleBuffer = new StringBuilder();
            IntPtr activeWindow_Handle = DLLImports.GetForegroundWindow();

            if (DLLImports.GetWindowText(activeWindow_Handle, titleBuffer, nChars) > 0)
            {
                return titleBuffer.ToString();
            }
            return null;
        }




    }
}


