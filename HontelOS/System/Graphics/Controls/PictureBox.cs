/*
* PROJECT:          HontelOS
* CONTENT:          Picture box control
* PROGRAMMERS:      Jort van Dalen
*/

using System.Drawing;
using Cosmos.System.Graphics;

namespace HontelOS.System.Graphics.Controls
{
    public class PictureBox : Control
    {
        public Image Image;

        public PictureBox(Image image, int x, int y, int width, int height, Window window) : base(window)
        {
            Image = image;

            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public override void Draw()
        {
            c.DrawImageStretchAlpha(Image as Bitmap, new Rectangle(Window.ViewX + X, Window.ViewY + Y, (int)Image.Width, (int)Image.Height), new Rectangle(Window.ViewX + X, Window.ViewY + Y, Width, Height));
        }
    }
}
