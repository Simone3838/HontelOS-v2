/*
* PROJECT:          HontelOS
* CONTENT:          Label control
* PROGRAMMERS:      Jort van Dalen
*/

using Cosmos.System.Graphics.Fonts;
using System.Drawing;

namespace HontelOS.System.Graphics.Controls
{
    public class Label : Control
    {
        public string Text;
        public Color Color;
        public Font Font;

        public Label(string text, Font font, Color color, int x, int y, Window window) : base(window)
        {
            Text = text;
            Color = color;
            Font = font;

            X = x;
            Y = y;
        }

        public override void Draw()
        {
            c.DrawString(Text, Font, Color, Window.ViewX + X, Window.ViewY + Y);
        }
    }
}
