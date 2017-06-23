using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using SimplePaletteQuantizer.Helpers;
using SimplePaletteQuantizer.Quantizers;
using SimplePaletteQuantizer.Quantizers.XiaolinWu;

namespace KaraSys.Helper
{
    public class SimplePaletteQuantizerHelper
    {
        public static byte[] OptimizeImage(byte[] byteArray, string fileName)
        {
            using (Image image = Image.FromStream(new MemoryStream(byteArray)))
            {
                ImageFormat jpegCodec = GetMineType(fileName);
                int parallelCount = GetParallelCount(fileName);
                IColorQuantizer activeQuantizer = GetColorQuantizer(fileName);

                if (jpegCodec != null)
                {
                    if (jpegCodec.Equals(ImageFormat.Jpeg) || jpegCodec.Equals(ImageFormat.Png)
                        || jpegCodec.Equals(ImageFormat.Tiff) || jpegCodec.Equals(ImageFormat.Bmp))
                    {
                        using (
                            var newBitmap = ImageBuffer.QuantizeImage(image, activeQuantizer, null, 256, parallelCount))
                        {
                            using (var ms = new MemoryStream())
                            {
                                newBitmap.Save(ms, jpegCodec);
                                return ms.ToArray();
                            }
                        }
                    }
                    else
                    {
                        return byteArray;
                    }
                }
            }

            return null;
        }

        private static ImageFormat GetMineType(string fileName)
        {
            fileName = fileName.ToLower();
            if (fileName.EndsWith("jpg") || fileName.EndsWith("jpeg"))
            {
                return ImageFormat.Jpeg;
            }
            if (fileName.EndsWith("bmp"))
            {
                return ImageFormat.Bmp;
            }
            if (fileName.EndsWith("gif"))
            {
                return ImageFormat.Gif;
            }
            if (fileName.EndsWith("tiff"))
            {
                return ImageFormat.Tiff;
            }
            if (fileName.EndsWith("png"))
            {
                return ImageFormat.Png;
            }

            return ImageFormat.Jpeg;
        }

        private static int GetParallelCount(string fileName)
        {
            fileName = fileName.ToLower();
            if (fileName.EndsWith("jpg") || fileName.EndsWith("jpeg"))
            {
                return 1;
            }
            if (fileName.EndsWith("bmp"))
            {
                return 1;
            }
            if (fileName.EndsWith("gif"))
            {
                return 1;
            }
            if (fileName.EndsWith("tiff"))
            {
                return 1;
            }
            if (fileName.EndsWith("png"))
            {
                return 1;
            }

            return 1;
        }

        private static IColorQuantizer GetColorQuantizer(string fileName)
        {
            fileName = fileName.ToLower();
            if (fileName.EndsWith("jpg") || fileName.EndsWith("jpeg"))
            {
                return new WuColorQuantizer();
            }
            if (fileName.EndsWith("bmp"))
            {
                return new WuColorQuantizer();
            }
            if (fileName.EndsWith("gif"))
            {
                return new WuColorQuantizer();
            }
            if (fileName.EndsWith("tiff"))
            {
                return new WuColorQuantizer();
            }
            if (fileName.EndsWith("png"))
            {
                return new WuColorQuantizer();
            }

            return new WuColorQuantizer();
        }

        // Lưu và nén hình chất lương cao
        public static byte[] CompressImage(byte[] byteArray, string fileName, int width, int height)
        {
            using (Image image = Image.FromStream(new MemoryStream(byteArray)))
            {
                ImageFormat jpegCodec = GetMineType(fileName);

                if (jpegCodec != null)
                {
                    if (jpegCodec.Equals(ImageFormat.Jpeg) || jpegCodec.Equals(ImageFormat.Png)
                        || jpegCodec.Equals(ImageFormat.Tiff) || jpegCodec.Equals(ImageFormat.Bmp))
                    {
                        if (image.Width < 120)
                        {
                            width = image.Width;
                        }
                        // Contruction new bitmap then repaint image with HighQuality
                        var thumbnailBitmap = new Bitmap(width, height);
                        var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
                        thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        var imageRectangle = new Rectangle(0, 0, width, height);
                        thumbnailGraph.DrawImage(image, imageRectangle);
                        thumbnailBitmap.Save(fileName, ImageFormat.Jpeg);
                        thumbnailGraph.Dispose();
                        thumbnailBitmap.Dispose();
                        image.Dispose();
                        using (var ms = new MemoryStream())
                        {
                            //newBitmap.Save(ms, jpegCodec);
                            return ms.ToArray();
                        }
                    }
                    return byteArray;
                }
            }

            return null;
        }
    }
}