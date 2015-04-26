using System.IO;
using System.Drawing;

namespace StorageManager
{
    public class StorageManager
    {
        public static Image LoadImageFromFile(string url)
        {
            //might need to dispose this thing at some point
            //if it locks the bitmap file on disk
            //http://stackoverflow.com/questions/3386749/loading-a-file-to-a-bitmap-but-leaving-the-original-file-intact
            return Bitmap.FromFile(url);
        }

        public static string LoadTextFromFile(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
