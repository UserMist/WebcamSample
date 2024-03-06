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
using WebcamSample.ViewModels;

using Avalonia.Media;
using Avalonia.LogicalTree;

namespace WebcamSample.ViewModels
{
    public class CameraWidgetViewModel : ViewModelBase
    {
        /*
        private CameraFeed feed { get; set; }
        public bool CamEnabled { get; set; }
        public Bitmap Frame { get; set; }
        public Bitmap OfflineCamImage { get; set; }

        protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            feed = new();
            OfflineCamImage = new Bitmap("Assets/offline.jpg");
            CamEnabled = true;

            base.OnAttachedToLogicalTree(e);
        }

        protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromLogicalTree(e);

            feed.Bitmap.Dispose();
            Frame?.Dispose();
            Frame = null;
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

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            Dispatcher.UIThread.Post(InvalidateVisual, DispatcherPriority.Background);
        }
        */
    }
}