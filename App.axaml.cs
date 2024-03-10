using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Splat;
using System.Diagnostics;
using System;
using WebcamSample.UI;

namespace WebcamSample
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if(ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindow_VM(),
                };

                desktop.ShutdownRequested += delegate
                {
                    if(desktop.MainWindow.DataContext is MainWindow_VM window) 
                        OnShutdown(window);
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        static void OnShutdown(MainWindow_VM window)
        {
            window.Feed0.Dispose();
        }
    }
}
