/*
* PROJECT:          HontelOS
* CONTENT:          Picture box control
* PROGRAMMERS:      Jort van Dalen
*/

using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            c.DrawImage(Image, Window.ViewX + X, Window.ViewY + Y, Width, Height);
        }
    }
}
