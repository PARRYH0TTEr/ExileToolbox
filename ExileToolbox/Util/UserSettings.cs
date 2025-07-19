using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExileToolbox.Util
{
    public static class UserSettings
    {
        public static string SelectedLeague;

        public static string SelectedGame;

        static UserSettings()
        {
            //TODO: don't set these constant values in the constructor, but rather from when an event is raised by the user
            // perhaps when selecting an item from a drop-down?
            UserSettings.SelectedLeague = "Standard";
            UserSettings.SelectedGame = "Path of Exile";
        }
    }
}
