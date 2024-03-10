using System;
using Avalonia;
using Avalonia.Threading;
using WebcamSample.Core;

namespace WebcamSample.UI
{
    public class MainWindow_VM : ViewModelBase
    {
        public CameraFeed Feed0 { get; set; }
        public CameraWidget_VM Widget0 { get; set; }

        public MainWindow_VM()
        {
            //var offline = new Bitmap("Assets/offline.jpg");
            Feed0 = new CameraFeed(0, "Камера 0", new PixelSize(2,2));
            Widget0 = new CameraWidget_VM(Feed0);

            feedUpdater.Tick += delegate { CameraUpdate(); };
            feedUpdater.IsEnabled = true;
        }

        void CameraUpdate()
        {
            Feed0.TryUpdateBitmap();
        }

        public const int TicksPerSecond = 60;
        private readonly DispatcherTimer feedUpdater = new() { Interval = new TimeSpan(0, 0, 0, 0, 1000 / TicksPerSecond) };
    }
}
