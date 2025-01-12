/*
* PROJECT:          HontelOS
* CONTENT:          Window manager
* PROGRAMMERS:      Jort van Dalen
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace HontelOS.System.Graphics
{
    public class WindowManager
    {
        public static Dictionary<int, Window> Windows = new Dictionary<int, Window>();
        public static int? FocusedWindow { get; private set; }

        public static List<Action> OnWindowsListUpdate = new();

        static int WIDcounter = -1;

        public static int Register(Window window)
        {
            WIDcounter++;
            window.WID = WIDcounter;
            Windows.Add(WIDcounter, window);
            SetFocused(WIDcounter);
            foreach(var a in OnWindowsListUpdate) a.Invoke();
            return WIDcounter;
        }

        public static void Unregister(int WID)
        {
            if (Windows.ContainsKey(WID))
            {
                Windows.Remove(WID);
                FocusedWindow = Windows.Keys.LastOrDefault();
                foreach (var a in OnWindowsListUpdate) a.Invoke();
            }
        }

        public static void Update()
        {
            foreach (Window w in Windows.Values)
                w.UpdateWindow();
        }

        public static void Draw()
        {
            foreach (var window in Windows)
                if (FocusedWindow != window.Key)
                    window.Value.DrawWindow();

            if (FocusedWindow.HasValue && Windows.ContainsKey(FocusedWindow.Value))
                Windows[FocusedWindow.Value].DrawWindow();
        }

        public static void SetFocused(int WID)
        {
            if (Windows.ContainsKey(WID))
                FocusedWindow = WID;
            else
                FocusedWindow = null;
        }

        public static bool IsAlive(int WID) { return Windows.ContainsKey(WID); }
    }
}
