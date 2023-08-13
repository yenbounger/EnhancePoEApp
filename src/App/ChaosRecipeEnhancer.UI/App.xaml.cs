﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using ChaosRecipeEnhancer.UI.Services;
using ChaosRecipeEnhancer.UI.Utilities;
using ChaosRecipeEnhancer.UI.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace ChaosRecipeEnhancer.UI;

internal partial class App
{
    private readonly SingleInstance _singleInstance = new("EnhancePoE");

    public App()
    {
        if (!_singleInstance.Claim()) Shutdown();
        
        // set up services (for IoC)
        Services = ConfigureServices();
        
        SetupUnhandledExceptionHandling();
    }
    
    public IServiceProvider Services { get; }
    
    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<IItemSetManagerService, ItemSetManagerService>();

        return services.BuildServiceProvider();
    }
    
    private void OnStartup(object sender, StartupEventArgs e)
    {
        var settingsView = new SettingsWindow();
        settingsView.Show();
        _singleInstance.PingedByOtherProcess += (_, _) => Dispatcher.Invoke(settingsView.Show);
    }
    
    private void SetupUnhandledExceptionHandling()
    {
        // Catch exceptions from all threads in the AppDomain.
        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
            ShowUnhandledException(args.ExceptionObject as Exception, "AppDomain.CurrentDomain.UnhandledException",
                false);

        // Catch exceptions from each AppDomain that uses a task scheduler for async operations.
        TaskScheduler.UnobservedTaskException += (_, args) =>
            ShowUnhandledException(args.Exception, "TaskScheduler.UnobservedTaskException", false);

        // Catch exceptions from a single specific UI dispatcher thread.
        Dispatcher.UnhandledException += (_, args) =>
        {
            // If we are debugging, let Visual Studio handle the exception and take us to the code that threw it.
            if (Debugger.IsAttached) return;

            args.Handled = true;
            ShowUnhandledException(args.Exception, "Dispatcher.UnhandledException", true);
        };
    }

    private static void ShowUnhandledException(Exception e, string unhandledExceptionType, bool promptUserForShutdown)
    {
        var messageBoxTitle = $"Unexpected Error Occurred: {unhandledExceptionType}";
        var messageBoxMessage = $"The following exception occurred:\n\n{e}";
        var messageBoxButtons = MessageBoxButton.OK;

        if (promptUserForShutdown)
        {
            messageBoxMessage += "\n\n\nPlease report this issue on github or discord :)";
            messageBoxButtons = MessageBoxButton.OK;
        }

        // Let the user decide if the app should die or not (if applicable).
        if (MessageBox.Show(messageBoxMessage, messageBoxTitle, messageBoxButtons) == MessageBoxResult.Yes)
            Current.Shutdown();
    }
}