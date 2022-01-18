using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;


namespace fresh_paint
{
    class Program
    {
        //TODO use installation or user directory
        private static readonly string urlListPath = "D:\\Documents\\SyncMain\\Coding\\beeple backdrop script\\urls.txt";

        static void Main(string[] args)
        {
            //TODO migrate txt => csv, then browse url attrs in csv

            string[] urlList = System.IO.File.ReadAllLines(urlListPath);

            Console.WriteLine("fresh_paint by FunkyOlive\nPress ctrl+c at any time to exit.\n"); //TODO add version hint
            Console.WriteLine("Read input file at: " + urlListPath);
            Console.WriteLine("There are " + urlList.LongLength + " saved image urls.");
            Console.WriteLine("Enter index of image url to download...");
            int urlIndex;
            while (!int.TryParse(Console.ReadLine(), out urlIndex) | urlIndex >= urlList.LongLength)
            {
                Console.WriteLine("Not a valid integer or out of bounds.");
                Console.WriteLine("Enter index of image url to download...");
            }
            string url = urlList[urlIndex];
            Console.WriteLine("Url at index " + urlIndex + " is: " + url);
            Console.WriteLine("Downloading..");

            string wallpaperPath = DownloadImageToTempBmp(url);
            Console.WriteLine("Downloaded image to temp file at: " + wallpaperPath);

            Console.WriteLine("Setting wallpaper..");
            Wallpaper.Set(wallpaperPath);
            //TODO add Wallpaper.style options (tryout live while not touching wp history)

            //TODO add key to undo or to jump back in history with Wallpaper.RestoreState()
            //TODO add key to run again
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
            //TODO add routine to clean out old temp files if recommended by windows guidelines or sth
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
