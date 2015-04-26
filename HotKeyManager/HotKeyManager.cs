using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace HotKeyManager
{
    //http://stackoverflow.com/questions/3568513/how-to-create-keyboard-shortcut-in-windows-that-call-function-in-my-app/3569097#3569097
    public class HotKeyManager
    {
        [DllImport("user32")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private int nextFreeId = 0;
        private MessageWindow wnd;
        private Dictionary<HotKey, List<Action>> hotKeyActions = new Dictionary<HotKey, List<Action>>();
        private Dictionary<HotKey, List<int>> hotKeyIds = new Dictionary<HotKey, List<int>>();

        public HotKeyManager()
        {
            wnd = new MessageWindow(this);
        }

        public void RegisterHotKey(HotKey hotKey, Action action)
        {
            var id = System.Threading.Interlocked.Increment(ref nextFreeId);
            var success = RegisterHotKey(wnd.Handle, id, (uint)hotKey.Modifiers, (uint)hotKey.Key);
            if (success)
            {
                Console.WriteLine("Successfully registered hotkey " + hotKey + " with id: " + id);
                if (hotKeyActions.ContainsKey(hotKey))
                {
                    hotKeyActions[hotKey].Add(action);
                    hotKeyIds[hotKey].Add(id);
                }
                else
                {
                    hotKeyActions.Add(hotKey, new List<Action> { action });
                    hotKeyIds.Add(hotKey, new List<int> { id });
                }
            }
            else
            {
                Console.WriteLine("There was a problem registering hotkey " + hotKey + " with id: " + id);
            }
        }

        public void RegisterHotKey(Keys key, KeyModifiers keyModifiers, Action action)
        {
            RegisterHotKey(new HotKey(key, keyModifiers), action);
        }

        public void UnregisterHotKey(HotKey hotKey)
        {
            foreach (var id in hotKeyIds[hotKey])
            {
                UnregisterHotKey(wnd.Handle, id);
            }
            hotKeyActions.Remove(hotKey);
            hotKeyIds.Remove(hotKey);
        }

        public void UnregisterHotKey(Keys key, KeyModifiers keyModifiers)
        {
            UnregisterHotKey(new HotKey(key, keyModifiers));
        }

        public void UnregisterAllHotKeys()
        {
            foreach (var ids in hotKeyIds.Values)
            {
                foreach (var id in ids)
                {
                    UnregisterHotKey(wnd.Handle, id);
                }
            }
            hotKeyActions.Clear();
            hotKeyIds.Clear();
        }

        private void ExecuteActionsForHotKey(HotKey hotKey){
            var actions = hotKeyActions[hotKey];
            foreach (var action in actions)
            {
                action();
            }
        }

        private class MessageWindow : Form
        {
            private const int WM_HOTKEY = 0x312;
            private HotKeyManager hotKeyManagerInstance;

            public MessageWindow(HotKeyManager hotKeyManager):base()
            {
                hotKeyManagerInstance = hotKeyManager;
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_HOTKEY)
                {
                    var hotKey = new HotKey(m.LParam);
                    hotKeyManagerInstance.ExecuteActionsForHotKey(hotKey);
                }

                base.WndProc(ref m);
            }
        }
    }
}
