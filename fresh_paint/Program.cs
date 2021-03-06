using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace fresh_paint
{
    internal class Program
    {
        private static string[] urlList;

        private static void Main(string[] args)
        {
            Console.WriteLine("fresh_paint by FunkyOlive\n"); //TODO add version hint
            urlList = Storage.importUrlList();

            //Input Handling
            for (; ; )
            {
                Console.WriteLine("Enter command or index of image url...");
                String input = Console.ReadLine();
                switch (input)
                {
                    /* case "undo":
                     *     //TODO implement undo
                     *     break;
                     *
                     * case "rewind":
                     *     //TODO implement rewind: jump back in history using Wallpaper.RestoreState()
                     *     break;
                     *
                     * case "import":
                     *     //TODO import a new url list, with an option to append it to the existing list
                     *     break;
                     *
                     * case "style":
                     *     //TODO implement Wallpaper.style options (live tryout while not touching wp history)
                     *     break;
                     */
                    case "?":
                        goto case "help";

                    case "help":
                        Console.WriteLine("Available commands:\n" +
                            "[any number] - selects the url at [number] from the imported list, downloads it and sets it as wallpaper\n" +
                            "random       - selects a random url from the imported list, downloads it and sets it as wallpaper\n" +
                            "exit         - exits the application\n" +
                            "? or help    - shows this list of commands\n");
                        break;

                    case "exit":
                        return;

                    case "random":
                        Random rng = new Random();
                        int urlIndex = rng.Next((int)urlList.LongLength);
                        setWallpaper(urlIndex);
                        break;

                    default:
                        if (int.TryParse(input, out urlIndex)
                            && 0 <= urlIndex && urlIndex < urlList.LongLength)
                        {
                            setWallpaper(urlIndex);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error: Unknown command or index out of bounds.");
                            break;
                        }
                }
            }
        }

        /* retrieves url at param urlIndex out of urlList,
         * downloads img at url to temp file,
         * and sets it as wallpaper
         */

        private static void setWallpaper(int urlIndex)
        {
            string url = urlList[urlIndex];
            Console.WriteLine("Url at index " + urlIndex + " is:\n" + "\"" + url + "\"");
            Console.WriteLine("Downloading..");

            string wallpaperPath = DownloadImageToTempBmp(url);
            Console.WriteLine("Downloaded image to temp file at: " + wallpaperPath);

            Console.WriteLine("Setting wallpaper..");
            Wallpaper.Set(wallpaperPath);
            Console.WriteLine("Success\n");
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