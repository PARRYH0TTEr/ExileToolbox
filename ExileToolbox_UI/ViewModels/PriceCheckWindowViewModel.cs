using System.Collections.ObjectModel;
using System.Windows.Documents;

using ExileToolbox_UI.PresentableTypes;

namespace ExileToolbox_UI.ViewModels
{
    public partial class PriceCheckWindowViewModel : ViewModelBase
    {
        public string Greeting { get; } = "Welcome to Avalonia! <-- Testing string";


        public ObservableCollection<string> tempLeagueList2 { get; set; } = new ObservableCollection<string>()
            {
                "Standard",
                "Mercenary"
            };


        public ObservableCollection<PT_ItemListing> ptItemListings { get; set; } = new ObservableCollection<PT_ItemListing>();
    }
}
