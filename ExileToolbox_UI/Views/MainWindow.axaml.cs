using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;

using System.Windows.Forms;
using System.Windows.Interop;
using System.Collections.Generic;
using Microsoft.VisualBasic;

using ExileToolbox.Util;
using System.Collections.ObjectModel;

using ExileToolbox_UI.ViewModels;
using System;

namespace ExileToolbox_UI.Views
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();


            //this.WindowState = WindowState.Minimized;


            //trayIcon = new NotifyIcon()
            //{
            //    Icon = new System.Drawing.Icon("Assets/ExileToolboxLogo_SystemTray.ico"),
            //    Visible = true,
            //    Text = "Exile Toolbox"
            //};

            //var trayIcon_ContextMenu = new ContextMenuStrip();
            //trayIcon_ContextMenu.Items.Add("Show", null, (s, e) => ShowFromTray());
            //trayIcon_ContextMenu.Items.Add("Exit", null, (s, e) => ExitApp());
            //trayIcon.ContextMenuStrip = trayIcon_ContextMenu;

            //trayIcon.DoubleClick += (s, e) => ShowFromTray();



            List<string> tempLeagueList = new List<string>() { "Standard", "Mercenary" };

            LeaguePicker_SetItems(tempLeagueList);

            LeaguePicker.SelectionChanged += (s, e) => LeaguePicker_PropagateSelection(s, e);


            HotkeyWatcher.HotKey_CtrlA += () => Debug.WriteLine("Delegate method invoked!");

        }

        // Overwriting the OnClosing event to instead minimize the application window to the system tray
        protected override void OnClosing(WindowClosingEventArgs e)
        {
            base.OnClosing(e);

            e.Cancel = true;
            this.Hide();
        }

        
        public void LeaguePicker_SetItems(List<string> leagues)
        {
            LeaguePicker.ItemsSource = leagues;
        }


        public void LeaguePicker_PropagateSelection(object? sender, SelectionChangedEventArgs e)
        {
            //UserSettings.SetSelectedLeague(LeaguePicker.SelectedItem.ToString());
            UserSettings.SelectedLeague = LeaguePicker.SelectedItem.ToString();
            Debug.WriteLine(LeaguePicker.SelectedItem.ToString());
        }


        public void TESTING_ShowPriceCheckWindow(object? sender, RoutedEventArgs e)
        {
            ((App)App.Current)._priceCheckWindow = new PriceCheckWindow()
            {
                DataContext = new PriceCheckWindowViewModel()
            };

            ((App)App.Current)._priceCheckWindow.Show();

        }

        public void TESTING_UpdatPriceCheckWindowBinding(object? sender, RoutedEventArgs e)
        {

            var vm = ((PriceCheckWindowViewModel)((App)App.Current)._priceCheckWindow.DataContext);

            vm.ptItemListings.Clear();

            Random random = new Random();

            vm.ptItemListings.Add(new PresentableTypes.PT_ItemListing{ Amount = (float)random.Next(1, 11), Currency = "Exalted Orb" });
            vm.ptItemListings.Add(new PresentableTypes.PT_ItemListing{ Amount = (float)random.Next(1, 11), Currency = "Exalted Orb" });

        }



        public void TESTING_HotkeyWatcher_Init(object? sender, RoutedEventArgs e) { HotkeyWatcher.HotkeyWatcherWindow_Init(); }


        public void TESTING_HotkeyWatcher_Cleanup(object? sender, RoutedEventArgs e) { HotkeyWatcher.HotkeyWatcherWindow_Cleanup(); }

    }
}   