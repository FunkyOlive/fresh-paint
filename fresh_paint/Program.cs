using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;


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

            Console.WriteLine("fresh_paint by FunkyOlive\n"); //TODO add version hint

            Console.WriteLine("Read input file at: " + urlListPath);
            Console.WriteLine("Imported " + urlList.LongLength + " saved image urls.");

            //Endless loop to read user input
            for(;;)
            {
                Console.WriteLine("Enter index of image url to download...");
                String input = Console.ReadLine();

                //handling int inputs
                if (int.TryParse(input, out int urlIndex) && 0 <= urlIndex && urlIndex < urlList.LongLength)
                {
                    //TODO refactor: extract
                    string url = urlList[urlIndex];
                    Console.WriteLine("Url at index " + urlIndex + " is:\n" + "\"" + url + "\"");
                    Console.WriteLine("Downloading..");

                    string wallpaperPath = DownloadImageToTempBmp(url);
                    Console.WriteLine("Downloaded image to temp file at: " + wallpaperPath);

                    Console.WriteLine("Setting wallpaper..");
                    Wallpaper.Set(wallpaperPath);
                    Console.WriteLine("Success\n");
                }
                else
                {
                    //handling string inputs
                    switch (input)
                    {
                        /*case "new":
                            //TODO add command to undo or to jump back in history with Wallpaper.RestoreState()
                            //TODO add Wallpaper.style options (live tryout while not touching wp history)
                            break;*/
                        case "random":
                            Random rng = new Random();
                            urlIndex = rng.Next((int)urlList.LongLength);

                            //TODO refactor: extract
                            string url = urlList[urlIndex];
                            Console.WriteLine("Url at index " + urlIndex + " is:\n" + "\"" + url + "\"");
                            Console.WriteLine("Downloading..");

                            string wallpaperPath = DownloadImageToTempBmp(url);
                            Console.WriteLine("Downloaded image to temp file at: " + wallpaperPath);

                            Console.WriteLine("Setting wallpaper..");
                            Wallpaper.Set(wallpaperPath);
                            Console.WriteLine("Success\n");

                            break;

                        default:
                            Console.WriteLine("Not a valid integer or out of bounds.");
                            break;
                    }                    
                }
            }            
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

            MemoryStream ms = new MemoryStream(
                new WebClient()
                .DownloadData(url));

            Image.FromStream(ms)
                .Save(TempFilePath, ImageFormat.Bmp);
            
            return TempFilePath;
        }


    
    }
}
