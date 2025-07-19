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
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace ExileToolbox.Util
{
    public static class HotkeyWatcher
    {
        //TODO: Enable the user to register arbitrary keyboard combinations/hotkeys by implementing a register method.
        public static event Action Hotkey_CtrlD;

        private static HotkeyWatcherWindow _hotkeyWindow;

        static HotkeyWatcher()
        {

        }


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

                    HandleHotkeyEvent(m);

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

        public static void HandleHotkeyEvent(Message message)
        {
            // https://learn.microsoft.com/en-us/windows/win32/inputdev/wm-hotkey
            // 
            // Perhaps make this code more clear with separate expressions/statements instead of inlining the computations
            int lParamInt = message.LParam.ToInt32();
            Keys virtualKey = (Keys)(lParamInt >> 16);
            ModKeys modifierKey = (ModKeys)(lParamInt & 0xFFFF);

            switch((modifierKey, virtualKey))
            {
                case (ModKeys.Control, Keys.D):
                    if (Helper.GetActiveWindowTitle().Equals(UserSettings.SelectedGame))
                    {
                        //PriceChecker.InitiatePriceCheck();
                        //Debug.WriteLine(ClipboardWrapper.GetTopmostClipboardText());

                        //Debug.WriteLine("hit ctrlD");

                        CopyIngameItemText();

                        Hotkey_CtrlD.Invoke();

                        string clipboardText = string.Empty;

                        //Debug.WriteLine(ClipboardWrapper.EnsureParseableItem(out clipboardText).ToString());

                        if (clipboardText != null && clipboardText != string.Empty)
                        {
                            //Debug.WriteLine(clipboardText);
                        }

                    }
                    break;
                default:
                    break;
            }
        }


        public static void CopyIngameItemText()
        {

            if (Helper.GetActiveWindowTitle().Equals(UserSettings.SelectedGame))
            {

                InputSender.Input[] inputs =
                {
                    //InputSender.PressKey(VirtualKeys.Control),
                    InputSender.PressKey(VirtualKeys.Menu),
                    InputSender.PressKey(VirtualKeys.C),
                    //InputSender.ReleaseKey(VirtualKeys.Control),
                    InputSender.ReleaseKey(VirtualKeys.Menu),
                    InputSender.ReleaseKey(VirtualKeys.C)
                };

                uint SendInputResult = DLLImports.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(InputSender.Input)));

                if (SendInputResult == 0)
                {
                    Debug.WriteLine("SendInput failed, something went wrong...");
                    throw new Exception("SendInput failed. Win32 Error Code: " + Marshal.GetLastWin32Error());
                }
                else
                {
                    Debug.WriteLine("SendInput succeeded! Ctrl+Alt+C sent to game!");

                }
            }
        }
    }
}
