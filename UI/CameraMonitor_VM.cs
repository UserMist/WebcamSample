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
using WebcamSample.Core;

namespace WebcamSample.UI
{
    public class CameraMonitor_VM : UserControl
    {
        public CameraFeed Feed;

        public CameraMonitor_VM(CameraFeed feed)
        {
            Feed = feed;
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            var size = Feed.Bitmap.Size;
            context.DrawImage(Feed.Bitmap, new(0,0, size.Width, size.Height), Bounds);
            Dispatcher.UIThread.Post(InvalidateVisual, DispatcherPriority.Background);
        }
    }
}
