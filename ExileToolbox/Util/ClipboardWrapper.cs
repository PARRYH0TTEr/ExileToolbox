using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private static ClipboardWrapperWindow _clipboardWindow;

        public static event Action<string> ClipboardChanged;


        // Unfortunately, we cannot simply create a NativeWindow in the static constructor, since that does not start its thread message loop.
        // Thus we need to call this Init function somewhere from the main thread
        public static void ClipboardWrapperWindow_Init()
        {
            _clipboardWindow = new ClipboardWrapperWindow();
            DLLImports.AddClipboardFormatListener(_clipboardWindow.Handle);
        }


        public static void ClipboardWrapperWindow_Cleanup()
        {
            if (_clipboardWindow != null)
            {
                _clipboardWindow.DestroyHandle();
                _clipboardWindow = null;
            }
        }

        public class ClipboardWrapperWindow : NativeWindow
        {
            private const int WM_CLIPBOARDUPDATE = 0x031D;

            public ClipboardWrapperWindow()
            {
                this.CreateHandle(new CreateParams());
            }

            protected override void WndProc(ref Message m)
            {

                if (m.Msg == WM_CLIPBOARDUPDATE)
                {

                    string clipboardText = string.Empty;

                    if (EnsureParseableItem(out clipboardText)) { HandleClipboardUpdateEvent(clipboardText); }
                }

                base.WndProc(ref m);
            }
        }

        public static bool EnsureParseableItem(out string topmostClipboardTextContainer)
        {
            string tmClipboardText = System.Windows.Clipboard.GetText();
            if (tmClipboardText.StartsWith("Item Class: "))
            {
                topmostClipboardTextContainer = tmClipboardText;
                return true;
            }
            topmostClipboardTextContainer = string.Empty;
            return false;
        }

        public static void HandleClipboardUpdateEvent(string tmClipboardText)
        {
            //Debug.WriteLine($"Received this text: \n\n\n {tmClipboardText}");

            ClipboardChanged.Invoke(tmClipboardText);

        }

    }
}
