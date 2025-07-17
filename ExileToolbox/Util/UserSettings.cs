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
            UserSettings.SelectedLeague = "Standard";
            UserSettings.SelectedGame = "Path of Exile";
        }
    }
}
