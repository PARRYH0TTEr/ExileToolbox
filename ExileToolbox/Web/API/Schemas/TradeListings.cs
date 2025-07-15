using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExileToolbox.Web.API.Schemas
{
    // -----------------------------------------------------------
    //                                                          //
    // If more info about a particular TradeListing is desired, //
    //  expand the class with more fields so the JSON           //
    //  deserializer can populate them.                         //
    //                                                          //
    // -----------------------------------------------------------
    public class TradeListings
    {
        [JsonPropertyName("result")]
        public List<TradeListing> Result { get; set; }
    }


    public class TradeListing
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("listing")]
        public ItemListing ItemListing { get; set; }
    }


    public class ItemListing
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("indexed")]
        public string IndexedDate { get; set; }

        [JsonPropertyName("stash")]
        public StashLocation StashLocation { get; set; }

        [JsonPropertyName("whisper")]
        public string WhisperMessage { get; set; }

        [JsonPropertyName("whisper_token")]
        public string WhisperToken { get; set; }

        [JsonPropertyName("account")]
        public AccountInfo AccountInfo { get; set; }

        [JsonPropertyName("price")]
        public PricingInfo PricingInfo { get; set; }
    }

    public class StashLocation
    {
        [JsonPropertyName("name")]
        public string TabName { get; set; }

        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }
    }


    public class AccountInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("online")]
        public OnlineStatus OnlineStatus { get; set; }

        [JsonPropertyName("lastCharacterName")]
        public string LastCharacterName { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("realm")]
        public string Realm { get; set; }
    }

    public class OnlineStatus
    {
        [JsonPropertyName("league")]
        public string League { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public class PricingInfo
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("amount")]
        public float Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }




    //public class StatCategory
    //{
    //    [JsonPropertyName("id")]
    //    public string Id { get; set; }

    //    [JsonPropertyName("label")]
    //    public string Label { get; set; }

    //    [JsonPropertyName("entries")]
    //    public List<StatEntry> Entries { get; set; }
    //}

    //public class StatEntry
    //{
    //    [JsonPropertyName("id")]
    //    public string ID { get; set; }

    //    [JsonPropertyName("text")]
    //    public string Text { get; set; }

    //    [JsonPropertyName("type")]
    //    public string Type { get; set; }
    //}

}
