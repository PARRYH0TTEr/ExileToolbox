using System;
using System.Linq;
using System.Windows;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using ExileToolbox_UI.ViewModels;
using ExileToolbox_UI.Views;

namespace ExileToolbox_UI
{
    public partial class App : Avalonia.Application
    {

        private MainWindow? _mainWindow;

        private DateTime? _trayTimeClick;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
                // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
                DisableAvaloniaDataAnnotationValidation();

                // Uncomment this block if you want to show the ExileToolbox window on startup
                //
                //desktop.MainWindow = new MainWindow
                //{
                //    DataContext = new MainWindowViewModel(),
                //};
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }


        // Show (and initialize) the ExileToolbox window
        private void SystemTray_SettingsClick(object? sender, System.EventArgs e)
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (_mainWindow == null)
                {
                    _mainWindow = new MainWindow()
                    {
                        DataContext = new MainWindowViewModel()
                    };
                    desktop.MainWindow = _mainWindow;
                }

                if (!_mainWindow.IsVisible)
                {
                    _mainWindow.Show();
                }
                else
                {
                    _mainWindow.Activate();
                }
            }
        }

        // Exit the application
        private void SystemTray_ExitClick(object? sender, System.EventArgs e)
        {
            Environment.Exit(0);
        }


        private void OnTrayClicked(object? sender, EventArgs e)
        {
            if (_trayTimeClick == null)
            {
                _trayTimeClick = DateTime.Now;
                return;
            }

            var delta = (DateTime.Now - _trayTimeClick).Value;
            


        }

    }
}