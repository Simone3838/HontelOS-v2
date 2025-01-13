/*
* PROJECT:          HontelOS
* CONTENT:          Top bar element
* PROGRAMMERS:      Jort van Dalen
*/

using Cosmos.System.Graphics;
using System;
using HontelOS.Resources;
using HontelOS.System.Applications.About;
using HontelOS.System.Applications.Files;
using System.Drawing;
using Cosmos.HAL;
using HontelOS.System.Graphics;
using HontelOS.System.Graphics.Controls;

namespace HontelOS.System
{
    public class TopBar
    {
        Canvas c = Kernel.canvas;
        Style Style = Kernel.style;

        Bitmap logo = ResourceManager.HontelLogo;

        public void Draw()
        {
            c.DrawFilledRectangle(Style.TopBar_BackgroundColor, 0, 0, (int)Kernel.screenWidth, 32);
            c.DrawImage(logo, 4, 4, 12, 24);

            string time = $"{RTC.Hour.ToString("00")}:{RTC.Minute.ToString("00")}";
            c.DrawString(time, Style.SystemFont, Color.Black, (int)Kernel.screenWidth - time.Length * Style.SystemFont.Width - 5, 16 - Style.SystemFont.Height / 2);
        }

        public void Update()
        {
            if (Kernel.MouseInArea(0, 0, (int)Kernel.screenWidth, 32))
            {
                if (Kernel.MouseInArea(0, 0, 32, 32) && Kernel.MouseClick())
                {
                    string[] _items = { "Files", "About", "Restart", "Shutdown" };
                    Action<int>[] _actions =
                    {
                    index => new FilesProgram(),
                    index => new AboutProgram(),
                    index => Kernel.Reboot(),
                    index => Kernel.Shutdown(),
                    };
                    ContextMenu menu = new ContextMenu(_items, _actions, 0, 32, 150);
                    menu.Show();
                }
            }
        }
    }
}
