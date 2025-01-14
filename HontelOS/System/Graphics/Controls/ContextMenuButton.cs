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
                c.DrawFilledRoundedRectangle(Style.Button_DisabledColor, X, Y, Width, Height, 5);
            else if (IsHovering && MouseManager.MouseState == MouseState.Left)
                c.DrawFilledRoundedRectangle(Style.Button_PressedColor, X, Y, Width, Height, 5);
            else if (IsHovering)
                c.DrawFilledRoundedRectangle(Style.Button_HoverColor, X, Y, Width, Height, 5);
            else
                c.DrawFilledRoundedRectangle(Style.Button_NormalColor, X, Y, Width, Height, 5);

            c.DrawString(Text, Style.SystemFont, Color.White, X + Width / 2 - Style.SystemFont.Width * Text.Length / 2, Y + Height / 2 - Style.SystemFont.Height / 2);
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
