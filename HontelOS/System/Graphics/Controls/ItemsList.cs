/*
* PROJECT:          HontelOS
* CONTENT:          Items list control
* PROGRAMMERS:      Jort van Dalen
*/

using Cosmos.System;
using Cosmos.System.Graphics.Fonts;
using HontelOS.System.Input;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace HontelOS.System.Graphics.Controls
{
    public class ItemsList : Control
    {
        public List<string> Items;
        public int SelectedIndex = -1;

        int scrollPosition = 0;
        const int itemHeight = 18;

        public List<Action<int>> OnSubmit = new();

        public ItemsList(List<string> items, int x, int y, int width, int height, Window window) : base(window)
        {
            Items = items;

            X = x;
            Y = y;
            Width = width;
            Height = height;
            OnMouseMove.Add(() => Window.IsDirty = true);
        }

        public override void Draw()
        {
            c.DrawFilledRoundedRectangle(Style.ItemsList_BackgroundColor, Window.ViewX + X, Window.ViewY + Y, Width, Height, 5);

            int visibleItemCount = Height / itemHeight;

            if (scrollPosition > Items.Count - visibleItemCount)
                scrollPosition = Math.Max(0, Items.Count - visibleItemCount);

            for (int i = 0; i < visibleItemCount; i++)
            {
                int itemIndex = i + scrollPosition;

                if (itemIndex >= Items.Count)
                    break;
                if (!string.IsNullOrEmpty(Items[itemIndex]))
                {
                    if (Kernel.MouseInArea(Window.ViewX + X, Window.ViewY + Y + i * itemHeight, Window.ViewX + X + Width, Window.ViewY + Y + i * itemHeight + itemHeight) && SelectedIndex != i)
                        c.DrawFilledRoundedRectangle(Style.ItemsList_HoverColor, Window.ViewX + X, Window.ViewY + Y + i * itemHeight, Width, itemHeight, 5);
                    if (Kernel.MouseInArea(Window.ViewX + X, Window.ViewY + Y + i * itemHeight, Window.ViewX + X + Width, Window.ViewY + Y + i * itemHeight + itemHeight) && Kernel.MouseClick())
                        SelectedIndex = i;
                    if (SelectedIndex == i)
                    {
                        c.DrawFilledRoundedRectangle(Style.ItemsList_SelectedColor, Window.ViewX + X, Window.ViewY + Y + i * itemHeight, Width, itemHeight, 5);
                        c.DrawString(Items[itemIndex], PCScreenFont.Default, Color.White, Window.ViewX + X + 2, Window.ViewY + Y + i * itemHeight);
                    }
                    else
                        c.DrawString(Items[itemIndex], PCScreenFont.Default, Color.Black, Window.ViewX + X + 2, Window.ViewY + Y + i * itemHeight);
                }
            }
        }

        public override void Update()
        {
            base.Update();
            if (IsSelected && KeyboardManagerExt.KeyAvailable)
            {
                var key = KeyboardManagerExt.ReadKey().Key;

                if (key == ConsoleKeyEx.UpArrow && SelectedIndex >= 1)
                    SelectedIndex--;
                else if (key == ConsoleKeyEx.DownArrow && SelectedIndex < Items.Count - 1)
                    SelectedIndex++;
                else if (key == ConsoleKeyEx.Enter)
                    foreach (var a in OnSubmit) a.Invoke(SelectedIndex);

                if((key == ConsoleKeyEx.UpArrow || key == ConsoleKeyEx.DownArrow) && SelectedIndex >= Items.Count)
                    SelectedIndex = Items.Count - 1;
            }
            if (IsHovering)
            {
                if (MouseManager.ScrollDelta > 0 && scrollPosition > 0)
                    scrollPosition--;
                else if (MouseManager.ScrollDelta < 0 && scrollPosition < Items.Count - Height / itemHeight)
                    scrollPosition++;

                Window.IsDirty = true;
            }
        }
    }
}
