﻿using System.Collections.ObjectModel;

namespace ExileToolbox_UI.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public string Greeting { get; } = "Welcome to Avalonia! <-- Testing string";


        public ObservableCollection<string> tempLeagueList2 { get; set; } = new ObservableCollection<string>()
            {
                "Standard",
                "Mercenary"
            };
    }
}
