using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace fresh_paint
{
    class Program
    {
        static void Main(string[] args)
        {
            string wallpaperPath = DownloadImageToTempFile("https://static.wixstatic.com/media/d7f4ba_082d76715a4f4ec09d204bd807090337~mv2.jpg/v1/fit/w_1678,h_618,q_90/d7f4ba_082d76715a4f4ec09d204bd807090337~mv2.jpg");            
            Wallpaper.Set(wallpaperPath);

            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();
        }

        /* downloads image from param url,
         * converts it to .bmp,
         * saves into temp file,
         * and returns that temp file location.
         */
        private static string DownloadImageToTempFile(string url)
        {
            string TempFilePath = Path.GetTempFileName();
            Console.WriteLine(TempFilePath);

            MemoryStream ms = new MemoryStream(
                new WebClient()
                .DownloadData(url));

            Image.FromStream(ms)
                .Save(TempFilePath, ImageFormat.Bmp);




            return TempFilePath;
        }


    
    }
}
