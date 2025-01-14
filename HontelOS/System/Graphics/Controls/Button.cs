/*
* PROJECT:          HontelOS
* CONTENT:          Button control
* PROGRAMMERS:      Jort van Dalen
*/

using Cosmos.System;
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
    }
}
