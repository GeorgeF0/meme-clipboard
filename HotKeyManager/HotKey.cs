using System;
using System.Windows.Forms;

namespace HotKeyManager
{
    public class HotKey
    {
        public Keys Key { get; set; }
        public KeyModifiers Modifiers { get; set; }

        public HotKey(Keys key, KeyModifiers modifiers)
        {
            Key = key;
            Modifiers = modifiers;
        }

        public HotKey(IntPtr hotKeyParam)
        {
            var param = (uint)hotKeyParam.ToInt64();
            Key = (Keys)((param & 0xffff0000) >> 16);
            Modifiers = (KeyModifiers)(param & 0x0000ffff);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            HotKey objAsHotKey = obj as HotKey;
            if (objAsHotKey == null) return false;

            return Key == objAsHotKey.Key && Modifiers == objAsHotKey.Modifiers;
        }

        public override int GetHashCode()
        {
            return (Key + "|" + Modifiers).GetHashCode();
        }

        public override string ToString()
        {
            return "[" + Key + "|" + Modifiers + "]";
        }
    }

    [Flags]
    public enum KeyModifiers
    {
        Alt = 1,
        Control = 2,
        Shift = 4,
        Windows = 8,
        NoRepeat = 0x4000
    }

}
