using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExileToolbox.Parsing;
using ExileToolbox.Parsing.Types;
using ExileToolbox.PriceCheck.Trade;
using ExileToolbox.Util;
using ExileToolbox.Web.API;

namespace ExileToolbox.PriceCheck
{
    public static class PriceChecker
    {

        private static TradeClient _tradeClient;

        private static ParsedItemReturnContainer _itemReturnContainer;

        static PriceChecker()
        {
            _tradeClient = new TradeClient();
            ClipboardWrapper.ClipboardChanged += (itemTextString) => ParseItemTextString(itemTextString);
        }


        public static async void ParseItemTextString(string itemTextString)
        {
            _itemReturnContainer = Parser.ParseItem(itemTextString);
        }

        public static async void InitiatePriceCheck(string itemText)
        {
            Debug.WriteLine("PriceChecking initiated!");

            ParsedItemReturnContainer pIRContainer = Parser.ParseItem(itemText);

            TradeRequest tradeRequest = Helper.MapParsedItemToTradeRequest(pIRContainer.parsedItemCopy);

            string tradeRequestJson = Helper.TemplateToJsonString(tradeRequest);

            APIResponse tradeRequestResponse = await _tradeClient.PostTradeRequest(tradeRequestJson);

            if (!tradeRequestResponse.Successful)
            {
                // Do some logging
            }


        }
    }
}
