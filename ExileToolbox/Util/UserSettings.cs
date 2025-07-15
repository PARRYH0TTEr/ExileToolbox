using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExileToolbox.Util
{
    public static class UserSettings
    {
        private static string _selectedLeague;

        public static void SetSelectedLeague(string selectedLeague) { _selectedLeague = selectedLeague; }
        public static string GetSelectedLeague() {  return _selectedLeague; }

    }
}
