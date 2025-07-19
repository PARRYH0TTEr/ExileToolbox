using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExileToolbox.Util
{
    public static class InputSender
    {
        // https://www.codeproject.com/Articles/5264831/How-to-Send-Inputs-using-Csharp

        [StructLayout(LayoutKind.Sequential)]
        public struct Input
        {
            public InputType type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)] public MouseInput mi;
            [FieldOffset(0)] public KeyboardInput ki;
            [FieldOffset(0)] public HardwareInput hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KeyboardInput
        {
            public VirtualKeys wVk; // virtual key code
            public ushort wScan;  // scan code of the key we want to press
            public KeyEventFlags dwFlags; // flags about the input
            public uint time; // timestamp of the input. if set to zero, the OS writes its own timestamp
            public IntPtr dwExtraInfo; // contains additional info about the keystroke. Can be retrieved with the (unmanaged) GetMessageExtraInfo function
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MouseInput
        {
            public int x;
            public int y;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HardwareInput
        {
            public uint uMsg;
            public uint wParamL;
            public uint wParamH;
        }



        [Flags]
        public enum InputType
        {
            Mouse = 0,
            Keyboard = 1,
            Hardware = 2
        }

        [Flags]
        public enum KeyEventFlags
        {
            KeyDown = 0x0000,
            ExtendedKey = 0x0001,
            KeyUp = 0x0002,
            Unicode = 0x0004,
            Scancode = 0x0008
        }

        [Flags]
        public enum MouseEventFlags
        {
            Absolute = 0x8000,
            HWheel = 0x01000,
            Move = 0x0001,
            MoveNoCoalesce = 0x2000,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            VirtualDesk = 0x4000,
            Wheel = 0x0800,
            XDown = 0x0080,
            XUp = 0x0100
        }




        // Press the given virtual key down
        public static Input PressKey(VirtualKeys virtualKey)
        {
            return new Input
            {
                type = InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = virtualKey,
                        wScan = 0,
                        dwFlags = 0,
                        time = 0,
                        dwExtraInfo = DLLImports.GetMessageExtraInfo()
                    }
                }
            };
        }

        //NOTE: It is important that we do not send a 'release' input of any key that is being held down by the user
        // as this sets the keys state to be 'UP' or 'Released' even when it is still being held down.

        //TODO: Add additional checks (not necessarily here), that trim out any synthetic inputs which contain keys that are already being held down.

        // Release the given virtual key
        public static Input ReleaseKey(VirtualKeys virtualKey)
        {
            return new Input
            {
                type = InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = virtualKey,
                        wScan = 0,
                        dwFlags = KeyEventFlags.KeyUp,
                        time = 0,
                        dwExtraInfo = DLLImports.GetMessageExtraInfo()
                    }
                }
            };
        }

    }
}
