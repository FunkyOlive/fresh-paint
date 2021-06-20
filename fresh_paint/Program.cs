﻿using System;
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
            int urlIndex = 3;
            string url = getUrlFromTxtFile(urlIndex, "D:\\Eigene Dokumente\\SyncMain\\Coding\\beeple backdrop script\\urls.txt");
            Console.WriteLine("url at index "+urlIndex+" is "+ url);

            string wallpaperPath = DownloadImageToTempBmp(url);            
            Wallpaper.Set(wallpaperPath);

            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();
        }

        /*
         * opens textfile at param path,
         * gets line at param index,
         * and returns the line 
         */
        private static string getUrlFromTxtFile(int index, string path)
        {
            return System.IO.File.ReadLines(path)
                .ElementAt(index); ;
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
