using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace WebcamSample
{
    internal sealed class Program
    {

        [STAThread]
        public static void Main(string[] args)
        {
            BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

            var ass = Assembly.GetEntryAssembly();
            if(ass != null)
            {
                var name = Path.GetDirectoryName(ass.Location);
                if(name != null)
                    Directory.SetCurrentDirectory(name);
            }
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace()
                .UseReactiveUI();
    }
}
