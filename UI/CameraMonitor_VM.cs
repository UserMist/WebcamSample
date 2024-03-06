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
using WebcamSample.UI.Core;

namespace WebcamSample.UI
{
    public class CameraMonitor_VM : UserControl
    {
        CameraFeed feed;
        public Bitmap OfflineCamImage { get; set; }
        public bool FeedEnabled { get; set; }

        public CameraMonitor_VM(CameraFeed feed, Bitmap offlineCamImage)
        {
            this.feed = feed;
            OfflineCamImage = offlineCamImage;
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            var rect = new Rect(512, 0, 256, 256);
            if (FeedEnabled)
            {
                feed.Draw();
                context.DrawImage(feed.Bitmap, rect);
            }
            else
            {
                context.DrawImage(OfflineCamImage, rect);
            }

            Dispatcher.UIThread.Post(InvalidateVisual, DispatcherPriority.Background);
        }
    }
}
