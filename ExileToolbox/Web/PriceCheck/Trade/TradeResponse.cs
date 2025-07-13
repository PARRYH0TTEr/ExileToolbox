using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ExileToolbox.Web.API.Errors;

namespace ExileToolbox.Web.PriceCheck.Trade
{
    public class TradeResponse
    {
        public bool Successful;
        public string Content;
        public ErrorMessage? Error;
        public HTTPRM? Httprm;
    }

    public class HTTPRM // Container for the status code and the associated reason phrase from the HttpResponseMessage
    {
        public int StatusCode;
        public string? ReasonPhrase;
    }

}
