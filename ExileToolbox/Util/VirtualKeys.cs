using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExileToolbox.Util
{
    public enum VirtualKeys : uint
    {
        // Reserve the lower three bits for modifier keys
        Ctrl = 1 << 0,
        Shift = 1 << 1,
        Alt = 1 << 2,

        // Non-modifiers go below here
        A = 1 << 3
    }

    [Flags]
    public enum ModKeys
    {
        None = 0x0000,
        Alt = 0x0001,
        Control = 0x0002,
        Shift = 0x0004,
        Win = 0x0008,
        NoRepeat = 0x4000
    }
}
