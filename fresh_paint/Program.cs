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
        private static readonly string urlListPath = "D:\\Eigene Dokumente\\SyncMain\\Coding\\beeple backdrop script\\urls.txt";

        static void Main(string[] args)
        {
            //TODO migrate txt => csv, then browse url attrs in csv

            string[] urlList = System.IO.File.ReadAllLines(urlListPath);
            Console.WriteLine("Read input file " + urlListPath);
            Console.WriteLine("There are " + urlList.Length + " saved image urls");
            Console.WriteLine("Enter index of image url to download...");
            int urlIndex;
            while (!int.TryParse(Console.ReadLine(), out urlIndex))
            {
                Console.WriteLine("Not a valid integer");
                Console.WriteLine("Enter index of image url to download...");
            }
            string url = urlList[urlIndex];
            Console.WriteLine("Url at index " + urlIndex + " is " + url);
            Console.WriteLine("Downloading..");

            string wallpaperPath = DownloadImageToTempBmp(url);
            Console.WriteLine("Downloaded image to temp file path " + wallpaperPath);

            Console.WriteLine("Setting wallpaper..");
            Wallpaper.Set(wallpaperPath);

            //TODO add key to undo or to jump back in history with Wallpaper.RestoreState()
            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();
        }


        /* downloads image from param url,
         * converts it to .bmp,
         * saves into temp file,
         * and returns that temp files location.
         */
        private static string DownloadImageToTempBmp(string url)
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
