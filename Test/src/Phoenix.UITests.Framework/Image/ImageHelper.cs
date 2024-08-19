using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO.Compression;

namespace Phoenix.UITests.Framework.Image
{
    public class ImageHelper
    {
        public void CompareTwoImages(System.IO.Stream DownloadedImage, string ExpectedImageInBase64String)
        {
            var savedImageBitmap = new System.Drawing.Bitmap(DownloadedImage);

            var stream = new MemoryStream();
            savedImageBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

            byte[] imageBytes = stream.ToArray();
            string actualImageInBase64String = Convert.ToBase64String(imageBytes);

            if (!actualImageInBase64String.Contains(ExpectedImageInBase64String))
                throw new Exception(string.Format("Expected: {0} \nActual: {1}", ExpectedImageInBase64String, actualImageInBase64String));
        }


    }
}
