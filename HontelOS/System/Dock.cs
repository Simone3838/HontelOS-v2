/*
* PROJECT:          HontelOS
* CONTENT:          Dock element
* PROGRAMMERS:      Jort van Dalen
*/

using Cosmos.System.Graphics;
using HontelOS.Resources;
using HontelOS.System.Graphics;
using HontelOS.System.Graphics.Controls;
using System.Collections.Generic;
using System.Linq;

namespace HontelOS.System
{
    public class Dock
    {
        Canvas c = Kernel.canvas;
        Style Style = Kernel.style;

        public int dockWidth = 74;
        public int items = 1;
        public Dictionary<int, ToolTip> windowTooltips = new Dictionary<int, ToolTip>();
        ToolTip launchPadToolTip = new ToolTip("Launch pad", ToolTip.ToolTipOrginDirection.Down, 0, 0);

        Bitmap appList = ResourceManager.SystemAppListIcon;
        Bitmap applicationIcon = ResourceManager.SystemApplicationIcon;

        public Dock()
        {
            WindowManager.OnWindowsListUpdate.Add(OnItemsUpdate);
        }

        public void Draw()
        {
            int posX = (int)Kernel.screenWidth / 2 - dockWidth / 2;
            int posY = (int)Kernel.screenHeight - 94;
            int sizX = dockWidth;
            int sizY = 84;

            c.DrawFilledRoundedRectangle(Style.Dock_BackgroundColor, posX, posY, sizX, sizY, 10);

            c.DrawImage(appList, posX + 10, posY + 10, 64, 64);

            for (int i = 0; i < WindowManager.Windows.Count; i++)
            {
                if (WindowManager.Windows.Values.ToList()[i].Icon != null)
                    c.DrawImage(WindowManager.Windows.Values.ToList()[i].Icon, posX + 84 + i * 74, posY + 10, 64, 64);
                else
                    c.DrawImage(applicationIcon, posX + 84 + i * 74, posY + 10, 64, 64);
            }
        }

        public void Update()
        {
            items = WindowManager.Windows.Count + 1;
            dockWidth = items * 74 + 10;
            int posX = (int)Kernel.screenWidth / 2 - dockWidth / 2;
            int posY = (int)Kernel.screenHeight - 84;

            launchPadToolTip.Hide();

            foreach (var tt in windowTooltips.Values)
                tt.Hide();

            if (Kernel.MouseInArea(posX + 10, posY + 10, posX + 64, posY + 64))
            {
                launchPadToolTip.Show();

                if(Kernel.MouseClick())
                    Kernel.appListVisable = !Kernel.appListVisable;
            }
                
            for (int i = 0; i < WindowManager.Windows.Count; i++)
            {
                if (Kernel.MouseInArea(posX + 84 + i * 74, posY + 10, posX + 84 + i * 74 + 64, posY + 64))
                {
                    windowTooltips[WindowManager.Windows.Keys.ToList()[i]].Show();

                    if (Kernel.MouseClick())
                    {
                        WindowManager.SetFocused(WindowManager.Windows.Values.ToList()[i].WID);
                        WindowManager.Windows.Values.ToList()[i].IsVisable = !WindowManager.Windows.Values.ToList()[i].IsVisable;
                    }
                }
            }
        }

        void OnItemsUpdate()
        {
            items = WindowManager.Windows.Count + 1;
            dockWidth = items * 74 + 10;
            int posX = (int)Kernel.screenWidth / 2 - dockWidth / 2;
            int posY = (int)Kernel.screenHeight - 84;

            windowTooltips.Clear();

            launchPadToolTip.OrginX = posX + 38;
            launchPadToolTip.OrginY = posY;

            for (int i = 0; i < WindowManager.Windows.Count; i++)
                windowTooltips.Add(WindowManager.Windows.Keys.ToList()[i], new ToolTip(WindowManager.Windows.Values.ToList()[i].Title, ToolTip.ToolTipOrginDirection.Down, posX + 79 + 32 + i * 74, posY));
        }
    }
}
