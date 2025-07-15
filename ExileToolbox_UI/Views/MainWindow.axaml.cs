using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Interactivity;

using System.Windows.Forms;
using System.Windows.Interop;
using System.Collections.Generic;
using Microsoft.VisualBasic;

using ExileToolbox.Util;
using System.Collections.ObjectModel;

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
            UserSettings.SetSelectedLeague(LeaguePicker.SelectedItem.ToString());
            Debug.WriteLine(LeaguePicker.SelectedItem.ToString());
        }


        public void TESTING_ShowPriceCheckWindow(object? sender, RoutedEventArgs e)
        {

        }

        public void TESTING_UpdatPriceCheckWindowBinding(object? sender, RoutedEventArgs e)
        {

        }


        //private void CalculateButton_OnClick(object? sender, RoutedEventArgs e)
        //{
        //    Debug.WriteLine("Click!");
        //    Debug.WriteLine($"Click! Celsius={Celsius.Text}");

        //    if (Celsius.Text != string.Empty)
        //    {
        //        Fahrenheit.Text = Celsius.Text;
        //    }
        //    else
        //    {
        //        Celsius.Text = string.Empty;
        //        Fahrenheit.Text = string.Empty;
        //    }
        //}

        //private void CalculateTemp(object? sender, RoutedEventArgs e)
        //{
        //    if (double.TryParse(Celsius.Text, out var C))
        //    {
        //        var F = C * (9d / 5d) + 32;
        //        Fahrenheit.Text = F.ToString("0.0");
        //    }
        //    else
        //    {
        //        Celsius.Text = string.Empty;
        //        Fahrenheit.Text= string.Empty;
        //    }
        //}


    }
}