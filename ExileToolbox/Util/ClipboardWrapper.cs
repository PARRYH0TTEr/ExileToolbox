using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace ExileToolbox.Util
{
    // For now we only need to read from the Clipboard and not write to it, but in the future if we wish to implement 
    //  a feature similar to copy-pasting regexes and such like for Awakened Poe Trade, writing to the Clipboard is one
    //  way of implementing it.
    public static class ClipboardWrapper
    {
        public static string GetTopmostClipboardText()
        {
           return System.Windows.Clipboard.GetText();
        }

        public static bool EnsureParseableItem(out string topmostClipboardTextContainer)
        {
            string tmClipboardText = GetTopmostClipboardText();
            if (tmClipboardText.StartsWith("Item Class: "))
            {
                topmostClipboardTextContainer = tmClipboardText;
                return true;
            }
            topmostClipboardTextContainer = string.Empty;
            return false;
        }
    }
}
