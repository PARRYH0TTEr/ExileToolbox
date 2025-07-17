using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

using System.Windows.Forms;
using System.Windows.Input;
using ExileToolbox.PriceCheck;

namespace ExileToolbox.Util
{
    public static class HotkeyWatcher
    {
        //TODO: Enable the user to register arbitrary keyboard combinations/hotkeys by implementing a register method.
        public static event Action HotKey_CtrlA;

        private static Thread _thread;
        private static bool _running;

        //private static Window _hotkeyWindow;
        //private static WindowInteropHelper _windowInteropHelper;
        //private static IntPtr _hotkeyWindowHandle;

        //private static NativeWindow _hotkeyWindow;

        private static HotkeyWatcherWindow _hotkeyWindow;

        // Might want to make the bitmask larger, i.e. by setting it to a larger data type
        private static uint _hotkeyBitmask = 0;
        private static bool _isHotkeyBeingHeld = false;

        private const int VK_Ctrl = 0x11;
        private const int VK_A = 0X41;




        // NativeWindow class whose only purpose is to receive a WM_HOTKEY window-message.
        public class HotkeyWatcherWindow : NativeWindow
        {

            private const int WM_HOTKEY = 0x0312;

            public HotkeyWatcherWindow()
            {
                this.CreateHandle(new CreateParams());
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_HOTKEY)
                {

                    HandleHotkey(m);

                    //int id = m.WParam.ToInt32();
                    //int lParamInt = m.LParam.ToInt32();
                    //int vk = lParamInt >> 16;
                    //int modifiers = lParamInt & 0xFFFF;

                    //Keys key = (Keys)vk;
                    //ModKeys modifier = (ModKeys)modifiers;

                    //Debug.WriteLine($"Hotkey ID {id} pressed: Modifiers = {modifier}, Key = {key}");
                }
                base.WndProc(ref m);
            }
        }

        public static void HotkeyWatcherWindow_Init()
        {
            _hotkeyWindow = new HotkeyWatcherWindow();
            bool success = DLLImports.RegisterHotKey(_hotkeyWindow.Handle, 1, (uint)(ModKeys.Control | ModKeys.NoRepeat), (uint)Keys.D);
            if (!success)
            {
                // do something or not...
            }
        }

        public static void HotkeyWatcherWindow_Cleanup()
        {
            if (_hotkeyWindow != null)
            {
                DLLImports.UnregisterHotKey(_hotkeyWindow.Handle, 42);
                _hotkeyWindow.DestroyHandle();
                _hotkeyWindow = null;
            }
        }

        public static void HandleHotkey(Message message)
        {
            int lParamInt = message.LParam.ToInt32();
            Keys virtualKey = (Keys)(lParamInt >> 16);
            ModKeys modifierKey = (ModKeys)(lParamInt & 0xFFFF);

            switch((modifierKey, virtualKey))
            {
                case (ModKeys.Control, Keys.D):
                    if (Helper.GetActiveWindowTitle() == UserSettings.SelectedGame)
                    {
                        //PriceChecker.InitiatePriceCheck();
                        //Debug.WriteLine(ClipboardWrapper.GetTopmostClipboardText());

                        string clipboardText = string.Empty;

                        Debug.WriteLine(ClipboardWrapper.EnsureParseableItem(out clipboardText).ToString());

                        if (clipboardText != null && clipboardText != string.Empty)
                        {
                            Debug.WriteLine(clipboardText);
                        }

                    }
                    break;
                default:
                    break;
            }
        }



        // Sets the bit associated with the given virtual key received
        public static void SetHotkeyBitmask(VirtualKeys vkb)
        {
            _hotkeyBitmask |= (uint)vkb;
        }

        // Zeros the bit associated with the given virtual key received
        public static void RemoveHotkeyBitmask(VirtualKeys vkb)
        {
            _hotkeyBitmask &= ~(uint)vkb;
        }
    }
}
