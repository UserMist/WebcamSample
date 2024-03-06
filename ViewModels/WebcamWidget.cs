using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace WebcamSample.ViewModels
{
    internal class WebcamWidget
    {
        public WriteableBitmap Bitmap = new WriteableBitmap(new PixelSize(50, 50), new Vector(1, 1));
        public CameraFeed Reader;

        public void OnClick()
        {

        }
    }
}
