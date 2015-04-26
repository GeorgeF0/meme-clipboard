using System;
using StorageManager;
using System.Windows.Forms;

namespace MemeClipboard
{
    class Program
    {
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            var clipboardManager = new ClipboardManager.ClipboardManager();
            var imagePath = @"C:\Users\George\Documents\Pepperonin.png";
            var urlPath = @"C:\Users\George\Documents\Pepperonin.txt";

            var image = StorageManager.StorageManager.LoadImageFromFile(imagePath);
            var text = StorageManager.StorageManager.LoadTextFromFile(urlPath);

            //clipboardManager.AddImageToDataObject(image);
            //clipboardManager.AddTextToDataObject(text);
            //clipboardManager.FlushDataObjectToClipboard();

            var hotkeyManager = new HotKeyManager.HotKeyManager();
            hotkeyManager.RegisterHotKey(Keys.G, HotKeyManager.KeyModifiers.Control, () => Console.WriteLine("hey"));
            //hotkeyManager.RegisterHotKey(Keys.G, HotKeyManager.KeyModifiers.Control, () => Console.WriteLine("ho!"));

            clipboardManager.PrintAvailableClipboardFormats();
            Console.ReadLine();
        }
    }
}
