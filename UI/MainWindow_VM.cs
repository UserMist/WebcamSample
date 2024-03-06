using System;
using System.Drawing;
using Avalonia;
using Avalonia.Media.Imaging;
using ReactiveUI;
using Brushes = Avalonia.Media.Brushes;
using IBrush = Avalonia.Media.IBrush;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using System.IO;
using System.Reflection;
using System.Threading;
using Avalonia.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using WebcamSample.UI.Core;

namespace WebcamSample.UI
{
    public class MainWindow_VM : ViewModelBase
    {
        public CameraFeed Feed0 { get; set; }
        public CameraWidget_VM Camera0 { get; set; }

        public MainWindow_VM()
        {
            var offline = new Bitmap("Assets/offline.jpg");
            Feed0 = new(0);
            var monitor = new CameraMonitor_VM(Feed0, offline);
            Camera0 = new(monitor);
        }

        /*
        public const int TicksPerSecond = 1;
        private readonly DispatcherTimer _timer = new() { Interval = new TimeSpan(0, 0, 0, 0, 1000 / TicksPerSecond) };



        private bool _cam0IsEnabled = true;
        public bool FeedEnabled
        {
            get => _cam0IsEnabled; set
            {
                if(value)
                {
                    if(feed == null) feed = new();
                    Frame = feed.Bitmap;
                }
                else
                {
                    Frame = OfflineCamImage;
                }

                this.RaiseAndSetIfChanged(ref _cam0IsEnabled, value);
            }
        }

        private IBrush _col = Brushes.Wheat;
        public IBrush Col
        {
            get => _col; set
            {
                this.RaiseAndSetIfChanged(ref _col, value);
            }
        }

        CameraFeed feed;

        public Bitmap OfflineCamImage;
        private Bitmap _frame;
        public Bitmap Frame { get => _frame; set => this.RaiseAndSetIfChanged(ref _frame, value); }

        public MainWindow_VM() : base()
        {
            Camera0 = new CameraWidget_VM();

            OfflineCamImage = new Bitmap("Assets/offline.jpg");
            FeedEnabled = false;

            //_ = Task.Run(() => FeedUpdateLoop());
            _timer.Tick += delegate { OnTick(); };
            _timer.FeedEnabled = true;
        }

        bool hit;
        public void OnTick()
        {
            hit = !hit;
            if(hit) return;
            if(feed != null && FeedEnabled)
                feed.Draw();
        }

        public async void FeedUpdateLoop()
        {
            while(true)
            {
                if(feed != null && FeedEnabled)
                    await Dispatcher.UIThread.InvokeAsync(() => feed.Draw());

                var count = CameraFeed.SourceCount;
                await Task.Delay(500);
            }
        }
        */
    }
}
