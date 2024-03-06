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

namespace WebcamSample.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {

        private bool _cam0IsEnabled = true;
        public bool Cam0IsEnabled
        {
            get => _cam0IsEnabled; set
            {
                this.RaiseAndSetIfChanged(ref _cam0IsEnabled, value);
                Col = value ? Brushes.Black : Brushes.White;

                CameraFeed reader = new CameraFeed();
                reader.Draw();
                Frame = reader.Bitmap;
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

        void PopulateBitmap()
        {

        }

        public Bitmap OfflineCamImage;
        private Bitmap _frame;
        public Bitmap Frame { get => _frame; set => this.RaiseAndSetIfChanged(ref _frame, value); }

        public MainWindowViewModel() : base()
        {
            OfflineCamImage = new Bitmap("Assets/offline.jpg");
            Frame = OfflineCamImage;
        }

        //public string Frame { get; set; } = "/Assets/img.jpg";
    }
}
