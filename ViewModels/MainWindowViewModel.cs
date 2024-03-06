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
using WebcamSample.Views;

namespace WebcamSample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public CameraWidgetViewModel Camera0 { get; set; }

        public MainWindowViewModel()
        {
            Camera0 = new();

        }

        /*
        public const int TicksPerSecond = 1;
        private readonly DispatcherTimer _timer = new() { Interval = new TimeSpan(0, 0, 0, 0, 1000 / TicksPerSecond) };



        private bool _cam0IsEnabled = true;
        public bool CamEnabled
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

        public MainWindowViewModel() : base()
        {
            Camera0 = new CameraWidgetViewModel();

            OfflineCamImage = new Bitmap("Assets/offline.jpg");
            CamEnabled = false;

            //_ = Task.Run(() => FeedUpdateLoop());
            _timer.Tick += delegate { OnTick(); };
            _timer.IsEnabled = true;
        }

        bool hit;
        public void OnTick()
        {
            hit = !hit;
            if(hit) return;
            if(feed != null && CamEnabled)
                feed.Draw();
        }

        public async void FeedUpdateLoop()
        {
            while(true)
            {
                if(feed != null && CamEnabled)
                    await Dispatcher.UIThread.InvokeAsync(() => feed.Draw());

                var count = CameraFeed.SourceCount;
                await Task.Delay(500);
            }
        }
        */
    }
}
