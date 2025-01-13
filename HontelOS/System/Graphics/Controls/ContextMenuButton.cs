/*
* PROJECT:          HontelOS
* CONTENT:          Context menu button control
* PROGRAMMERS:      Jort van Dalen
*/

using Cosmos.System;
using System;
using System.Drawing;

namespace HontelOS.System.Graphics.Controls
{
    public class ContextMenuButton : Control
    {
        public string Text;
        string[] items;
        Action<int>[] actions;

        public ContextMenuButton(string text, string[] items, Action<int>[] actionsForItems, int x, int y, int width, int height, Window window) : base(window)
        {
            Text = text;
            this.items = items;
            actions = actionsForItems;

            Width = width;
            Height = height;
            X = x;
            Y = y;
        }

        public override void Draw()
        {
            if (IsDisabled)
                c.DrawFilledRoundedRectangle(Style.Button_DisabledColor, Window.ViewX + X, Window.ViewY + Y, Width, Height, 5);
            else if (IsHovering && MouseManager.MouseState == MouseState.Left)
                c.DrawFilledRoundedRectangle(Style.Button_PressedColor, Window.ViewX + X, Window.ViewY + Y, Width, Height, 5);
            else if (IsHovering)
                c.DrawFilledRoundedRectangle(Style.Button_HoverColor, Window.ViewX + X, Window.ViewY + Y, Width, Height, 5);
            else
                c.DrawFilledRoundedRectangle(Style.Button_NormalColor, Window.ViewX + X, Window.ViewY + Y, Width, Height, 5);
            c.DrawString(Text, Style.SystemFont, Color.White, Window.ViewX + X + Width / 2 - Style.SystemFont.Width * Text.Length / 2, Window.ViewY + Y + Height / 2 - Style.SystemFont.Height / 2);
        }

        public override void Update()
        {
            base.Update();
            if (IsHovering && Kernel.MouseClick())
            {
                ContextMenu menu = new ContextMenu(items, actions, Window.ViewX + X, Window.ViewY + Y + Height, Width);
                menu.Show();
            }
        }
    }
}
