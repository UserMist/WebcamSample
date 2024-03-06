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
using Avalonia.Controls;
using System.Threading.Tasks;
using System.Diagnostics;

using Avalonia.Media;
using Avalonia.LogicalTree;
using Avalonia.Media.Immutable;

namespace WebcamSample.UI
{
    public class CameraWidget_VM : ViewModelBase
    {
        public CameraMonitor_VM Monitor { get; set; }

        private bool _feedEnabled;
        public bool FeedEnabled { get => _feedEnabled; set { Monitor.FeedEnabled = value; this.RaiseAndSetIfChanged(ref _feedEnabled, value); } }

        public CameraWidget_VM(CameraMonitor_VM monitor)
        {
            this.Monitor = monitor;
        }
    }
}