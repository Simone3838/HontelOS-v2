/*
* PROJECT:          HontelOS
* CONTENT:          Button control
* PROGRAMMERS:      Jort van Dalen
*/

using Cosmos.System;
using HontelOS.System.Graphics;
using System;
using System.Drawing;

namespace HontelOS.System.Graphics.Controls
{
    public class Button : Control
    {
        public string Text;

        public Button(string text, Action onClick, int x, int y, int width, int height, Window window) : base(window)
        {
            Text = text;
            OnClick.Add(onClick);

            X = x;
            Y = y;
            Width = width;
            Height = height;
            OnStartHover.Add(() => Window.IsDirty = true);
            OnEndHover.Add(() => Window.IsDirty = true);
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
    }
}
