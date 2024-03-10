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
using WebcamSample.Core;

namespace WebcamSample.UI
{
    public class CameraWidget_VM : ViewModelBase
    {
        public CameraMonitor_VM Monitor { get; set; }

        public bool FeedEnabled
        {
            get => Monitor.Feed.IsEnabled;
            set => this.RaiseAndSetIfChanged(ref Monitor.Feed.IsEnabled, value);
        }

        public string FeedName
        {
            get => Monitor.Feed.Name;
            set => this.RaiseAndSetIfChanged(ref Monitor.Feed.Name, value);
        }

        public CameraWidget_VM(CameraFeed feed)
        {
            Monitor = new(feed);
        }

        public CameraWidget_VM(CameraMonitor_VM monitor)
        {
            Monitor = monitor;
        }
    }
}