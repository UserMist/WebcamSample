using System;
using System.Runtime.InteropServices;

using SharpDX;
using SharpDX.MediaFoundation;

using Avalonia;
using Avalonia.Platform;
using Avalonia.Media.Imaging;
using System.Threading;
using System.Collections;
using System.Text.RegularExpressions;
using SharpDX.Direct3D9;

namespace WebcamSample.Core
{
    static class VideoHelper
    {
        private const float _cr = 1.13983f, _cgu = -0.39465f, _cgv = -0.58060f, _cb = 2.03211f;
        public static void YUV_to_RGB(in byte Y, in byte U, in byte V, out byte R, out byte G, out byte B)
        {
            var u = (sbyte)(U - 128);
            var v = (sbyte)(V - 128);
            var rShift = _cr * v;
            var gShift = _cgu * u + 0.58060f * v;
            var bShift = _cb * u;

            R = (byte) Math.Clamp(Y + rShift, 0, 255);
            G = (byte) Math.Clamp(Y + gShift, 0, 255);
            B = (byte) Math.Clamp(Y + bShift, 0, 255);
        }

        public static string? FormatNameFrom(Guid g)
        {
            switch(g.ToString())
            {
                case "31564d57-0000-0010-8000-00aa00389b71": return "Wmv1";
                case "32564d57-0000-0010-8000-00aa00389b71": return "Wmv2";
                case "33564d57-0000-0010-8000-00aa00389b71": return "Wmv3";
                case "20637664-0000-0010-8000-00aa00389b71": return "Dvc";
                case "30357664-0000-0010-8000-00aa00389b71": return "Dv50";
                case "35327664-0000-0010-8000-00aa00389b71": return "Dv25";
                case "33363248-0000-0010-8000-00aa00389b71": return "H263";
                case "34363248-0000-0010-8000-00aa00389b71": return "H264";
                case "35363248-0000-0010-8000-00aa00389b71": return "H265";
                case "43564548-0000-0010-8000-00aa00389b71": return "Hevc";
                case "53564548 -0000-0010-8000-00aa00389b71": return "HevcEs";
                case "30385056-0000-0010-8000-00aa00389b71": return "Vp80";
                case "30395056-0000-0010-8000-00aa00389b71": return "Vp90";
                case "3253534d -0000-0010-8000-00aa00389b71": return "MultisampledS2";
                case "3253344d-0000-0010-8000-00aa00389b71": return "M4S2";
                case "31435657-0000-0010-8000-00aa00389b71": return "Wvc1";
                case "30313050-0000-0010-8000-00aa00389b71": return "P010";
                case "34344941-0000-0010-8000-00aa00389b71": return "AI44";
                case "31687664-0000-0010-8000-00aa00389b71": return "Dvh1";
                case "64687664-0000-0010-8000-00aa00389b71": return "Dvhd";
                case "3153534d-0000-0010-8000-00aa00389b71": return "MultisampledS1";
                case "3334504d-0000-0010-8000-00aa00389b71": return "Mp43";
                case "5334504d-0000-0010-8000-00aa00389b71": return "Mp4s";
                case "5634504d-0000-0010-8000-00aa00389b71": return "Mp4v";
                case "3147504d-0000-0010-8000-00aa00389b71": return "Mpg1";
                case "47504a4d-0000-0010-8000-00aa00389b71": return "Mjpg";
                case "6c737664-0000-0010-8000-00aa00389b71": return "Dvsl";
                case "32595559-0000-0010-8000-00aa00389b71": return "YUY2";
                case "32315659-0000-0010-8000-00aa00389b71": return "Yv12";
                case "36313050-0000-0010-8000-00aa00389b71": return "P016";
                case "30313250-0000-0010-8000-00aa00389b71": return "P210";
                case "36313250-0000-0010-8000-00aa00389b71": return "P216";
                case "30323449-0000-0010-8000-00aa00389b71": return "I420";
                case "64737664-0000-0010-8000-00aa00389b71": return "Dvsd";
                case "54323459-0000-0010-8000-00aa00389b71": return "Y42T";
                case "3231564e-0000-0010-8000-00aa00389b71": return "NV12";
                case "3131564e-0000-0010-8000-00aa00389b71": return "NV11";
                case "30313259-0000-0010-8000-00aa00389b71": return "Y210";
                case "36313259-0000-0010-8000-00aa00389b71": return "Y216";
                case "30313459-0000-0010-8000-00aa00389b71": return "Y410";
                case "36313459-0000-0010-8000-00aa00389b71": return "Y416";
                case "50313459-0000-0010-8000-00aa00389b71": return "Y41P";
                case "54313459-0000-0010-8000-00aa00389b71": return "Y41T";
                case "39555659-0000-0010-8000-00aa00389b71": return "Yvu9";
                case "55595659-0000-0010-8000-00aa00389b71": return "Yvyu";
                case "56555949-0000-0010-8000-00aa00389b71": return "Iyuv";
                case "59565955-0000-0010-8000-00aa00389b71": return "Uyvy";
                case "56555941-0000-0010-8000-00aa00389b71": return "AYUV";
                case "4f303234-0000-0010-8000-00aa00389b71": return "Y420O";
                default: return null;
            }
        }

        [DllImport("mf.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "MFEnumDeviceSources")]
        private unsafe static extern int MFEnumDeviceSources_(void* param0, void* param1, void* param2);
        private unsafe static void enumDeviceSources(MediaAttributes attributesRef, out nint pSourceActivateOut, out int cSourceActivateRef)
        {
            nint intPtr = CppObject.ToCallbackPtr<MediaAttributes>(attributesRef);
            Result result;
            fixed(int* ptr = &cSourceActivateRef) fixed(nint* ptr2 = &pSourceActivateOut)
            {
                result = MFEnumDeviceSources_((void*) intPtr, ptr2, ptr);
            }
            result.CheckError();
        }

        public static Activate[] EnumDeviceSources(MediaAttributes attributesRef)
        {
            enumDeviceSources(attributesRef, out nint devicePtr, out int devicesCount);
            var result = new Activate[devicesCount];
            unsafe
            {
                var address = (void**)devicePtr;
                for(var i = 0; i < devicesCount; i++)
                    result[i] = new Activate(new nint(address[i]));
            }
            return result;
        }
    }

    public class CameraFeed
    {
        public string Name;
        public WriteableBitmap Bitmap { get; set; }
        public PixelSize Downsampling { get; private set; }

        public int SourceId { get; private set; }
        public int SourceWidth { get; private set; }
        public int SourceHeight { get; private set; }
        public string? SourceSubtype { get; private set; }

        private PixelFormat bitmapSubtype;
        private SourceReader sourceReader;
        private int bitmapByteDepth;
        private int sourceByteDepthNum, sourceByteDepthDenom;

        public static int SourceCount => GetSources().Length;

        #region Source -> Bitmap copying
        //4:4:4 format
        static unsafe void AYUV_to_RGBA64(byte* input, ushort* data, int width, int height, int inputCellCount, int pixelStepX, int pixelStepY, int extraDataStride = 0)
        {
            for(int pixelY = 0; pixelY < height; pixelY++)
            {
                for(int pixelX = 0; pixelX < width; pixelX++)
                {
                    int inputByteId = 4*(pixelX*pixelStepX + pixelY*pixelStepY * inputCellCount);

                    byte V = input[inputByteId];
                    byte U = input[inputByteId+1];
                    byte Y = input[inputByteId+2];
                    byte A = input[inputByteId+3];

                    VideoHelper.YUV_to_RGB(Y, U, V, out var R, out var G, out var B);

                    int byteId = 4*(pixelX + pixelY*width) + pixelY*extraDataStride;
                    data[byteId] = (ushort) (R << 8);
                    data[byteId+1] = (ushort) (G << 8);
                    data[byteId+2] = (ushort) (B << 8);
                    data[byteId+3] = (ushort) (A << 8);
                }
            }
        }

        //4:2:2 format
        static unsafe void YUY2_to_RGB24(byte* input, byte* data, int width, int height, int inputCellCount, int pixelStepX, int pixelStepY, int extraDataStride = 0)
        {
            for(int pixelY = 0; pixelY < height; pixelY++)
            {
                for(int pixelX = 0; pixelX < width; pixelX++)
                {
                    int inputByteId = 2*(pixelX*pixelStepX + pixelY*pixelStepY * inputCellCount);

                    bool isFirst = inputByteId % 4 == 0;
                    byte Y = input[inputByteId];
                    byte a = input[inputByteId+1];
                    // omit Y //
                    byte b = input[inputByteId+3];

                    int byteId = 3*(pixelX + pixelY*width) + pixelY*extraDataStride;
                    VideoHelper.YUV_to_RGB(Y, isFirst ? a : b, isFirst ? b : a, out data[byteId], out data[byteId + 1], out data[byteId + 2]);
                }
            }
        }

        //4:2:2 format
        static unsafe void UYVY_to_RGB24(byte* input, byte* data, int width, int height, int inputCellCount, int pixelStepX, int pixelStepY, int extraDataStride = 0)
        {
            for(int pixelY = 0; pixelY < height; pixelY++)
            {
                for(int pixelX = 0; pixelX < width; pixelX++)
                {
                    int inputByteId = 2*(pixelX*pixelStepX + pixelY*pixelStepY * inputCellCount);

                    bool isFirst = inputByteId % 4 == 0;
                    byte a = input[inputByteId];
                    byte Y = input[inputByteId+1];
                    byte b = input[inputByteId+2];
                    // omit Y //

                    int byteId = 3*(pixelX + pixelY*width) + pixelY*extraDataStride;
                    VideoHelper.YUV_to_RGB(Y, isFirst ? a : b, isFirst ? b : a, out data[byteId], out data[byteId + 1], out data[byteId + 2]);
                }
            }
        }
        #endregion

        static long packLong(in int a, in int b) => (long) a << 32 | (long) b;
        static void unpackLong(in long v, out int a, out int b)
        {
            a = (int) (v >> 32);
            b = (int) (v << 32 >> 32);
        }

        public static Activate[] GetSources()
        {
            var attributes = new MediaAttributes(1);
            attributes.Set(CaptureDeviceAttributeKeys.SourceType.Guid, CaptureDeviceAttributeKeys.SourceTypeVideoCapture.Guid);
            return VideoHelper.EnumDeviceSources(attributes);
        }

        #region Source and bitmap initialization
        SourceReader createSourceReader(int sourceId, int streamId, int mediaTypeId)
        {
            SourceId = sourceId;
            var source = GetSources()[SourceId].ActivateObject<MediaSource>();

            source.CreatePresentationDescriptor(out PresentationDescriptor presentationDescriptor);
            var presentationCount = presentationDescriptor.StreamDescriptorCount;
            for(var i = 0; i < presentationCount; i++)
            {
                presentationDescriptor.GetStreamDescriptorByIndex(i, out var isSelected, out StreamDescriptor streamDescriptor);
            }

            var sourceReader = new SourceReader(source);

            using(var mt = sourceReader.GetNativeMediaType(streamId, mediaTypeId))
            {
                unpackLong(mt.Get(MediaTypeAttributeKeys.FrameSize), out var sourceWidth, out var sourceHeight);
                unpackLong(mt.Get(MediaTypeAttributeKeys.FrameRate), out var frameRateNumerator, out var frameRateDenominator);
                unpackLong(mt.Get(MediaTypeAttributeKeys.PixelAspectRatio), out var aspectRatioNumerator, out var aspectRatioDenominator);
                SourceSubtype = VideoHelper.FormatNameFrom(mt.Get(MediaTypeAttributeKeys.Subtype));
                SourceWidth = sourceWidth; SourceHeight = sourceHeight;
            }

            switch(SourceSubtype)
            {
                case "AYUV":
                    sourceByteDepthNum = 4;
                    sourceByteDepthDenom = 1;
                    bitmapByteDepth = 8;
                    bitmapSubtype = PixelFormats.Rgba64;
                    break;

                case "YUY2":
                    sourceByteDepthNum = 2;
                    sourceByteDepthDenom = 1;
                    bitmapByteDepth = 3;
                    bitmapSubtype = PixelFormats.Rgb24;
                    break;

                case "Uyvy":
                    sourceByteDepthNum = 2;
                    sourceByteDepthDenom = 1;
                    bitmapByteDepth = 3;
                    bitmapSubtype = PixelFormats.Rgb24;
                    break;

                default: throw new NotImplementedException($"Videofeed of format \"{SourceSubtype}\" is not yet supported");
            }

            return sourceReader;
        }

        public WriteableBitmap createBitmap(PixelSize step)
        {
            var dataStride = SourceWidth * bitmapByteDepth;
            GCHandle pinnedArray = GCHandle.Alloc(new byte[dataStride * SourceHeight], GCHandleType.Pinned);

            var ret = new WriteableBitmap(bitmapSubtype, AlphaFormat.Opaque, pinnedArray.AddrOfPinnedObject(), new PixelSize(SourceWidth/step.Width, SourceHeight/step.Height), new Vector(96, 96), dataStride);
            pinnedArray.Free();

            Downsampling = step;
            return ret;
        }
        #endregion

        public CameraFeed(int sourceId, string name, PixelSize step)
        {
            Name = name;
            sourceReader = createSourceReader(sourceId, 0, 0);
            Bitmap = createBitmap(step);
            ReaderThread = new(ReadLoop);
            ReaderThread.Start();
        }

        public Thread ReaderThread;
        public Sample? Sample;
        public bool IsEnabled;
        public void ReadLoop()
        {
            while(true)
            {
                if(IsEnabled && Sample == null)
                {
                    {
                        int readStreamIndex; SourceReaderFlags readFlags; long timestamp;
                        var sample = sourceReader.ReadSample(SourceReaderIndex.AnyStream, SourceReaderControlFlags.None, out readStreamIndex, out readFlags, out timestamp);

                        if(sample == null)
                            sample = sourceReader.ReadSample(SourceReaderIndex.AnyStream, SourceReaderControlFlags.None, out readStreamIndex, out readFlags, out timestamp);

                        if(sample != null)
                            Sample = sample;
                    }
                }

                Thread.Sleep(50);
            }
        }

        public unsafe void TryUpdateBitmap()
        {
            if(Sample == null) return;

            lock(Sample)
            {
                unsafe
                {
                    using var locked = Bitmap.Lock();
                    using var sourceBuffer = Sample.GetBufferByIndex(Sample.BufferCount - 1);
                    var sourcePointer = sourceBuffer.Lock(out var maxLength, out var currentLength);

                    byte* newData = (byte*)locked.Address;
                    byte* oldData = (byte*)sourcePointer.ToPointer();

                    int bitmapStepX = Downsampling.Width, bitmapStepY = Downsampling.Height;
                    switch(SourceSubtype)
                    {
                        case "AYUV": AYUV_to_RGBA64(oldData, (ushort*) newData, Bitmap.PixelSize.Width, Bitmap.PixelSize.Height, SourceWidth, bitmapStepX, bitmapStepY, 0); break;
                        case "YUY2": YUY2_to_RGB24(oldData, newData, Bitmap.PixelSize.Width, Bitmap.PixelSize.Height, SourceWidth, bitmapStepX, bitmapStepY, 0); break;
                        case "Uyvy": UYVY_to_RGB24(oldData, newData, Bitmap.PixelSize.Width, Bitmap.PixelSize.Height, SourceWidth, bitmapStepX, bitmapStepY, 0); break;
                        default: throw new NotImplementedException();
                    }

                    sourceBuffer.Unlock();
                }
            }

            Sample.Dispose();
            Sample = null;
        }
    }
}
