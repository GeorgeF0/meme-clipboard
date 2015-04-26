using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;

namespace ClipboardManager
{
    public class ClipboardManager
    {
        //allows multiple data formats to be placed on the Clipboard at the same time
        private DataObject dataObject = new DataObject();

        public void FlushDataObjectToClipboard()
        {
            Clipboard.SetDataObject(dataObject, true);
            dataObject = new DataObject();
        }

        public void DropDataObject()
        {
            dataObject = new DataObject();
        }

        public void AddImageToDataObject(Image picture)
        {
            dataObject.SetImage(picture);
        }

        public void AddTextToDataObject(string text)
        {
            dataObject.SetText(text);
        }

        public void PutImageInClipboard(Image picture)
        {
            Clipboard.SetImage(picture);
        }

        public void PutTextInClipboard(string text)
        {
            Clipboard.SetText(text);
        }

        public List<string> GetAvailableClipboardDataFormats()
        {
            return Clipboard.GetDataObject().GetFormats().ToList();
        }

        public void PrintAvailableClipboardFormats()
        {
            GetAvailableClipboardDataFormats().ForEach(x => Console.WriteLine(x));
        }
    }
}
