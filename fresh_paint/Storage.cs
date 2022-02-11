using System;

namespace fresh_paint
{
    internal class Storage
    {
        //TODO migrate txt => csv
        //TODO handle file not found vs. empty file

        private static readonly string urlListPath = "D:\\Documents\\SyncMain\\Coding\\beeple backdrop script\\urls.txt";

        internal static string[] getUrlList()
        {
            string[] urlList = System.IO.File.ReadAllLines(urlListPath);
            Console.WriteLine("Read input file at: " + urlListPath);
            Console.WriteLine("Imported " + urlList.LongLength + " saved image urls.");
            return urlList;
        }
    }
}