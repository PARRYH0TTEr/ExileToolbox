using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExileToolbox_UI.PresentableTypes
{
    public class PT_ItemListing
    {
        public float Amount { get; set; }
        public string Currency { get; set; }
        //TODO: Add field with time since indexed (given the indexed time in a TradeListing, get the current time and subtract indexed time)

    }
}
