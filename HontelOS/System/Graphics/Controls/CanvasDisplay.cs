/*
* PROJECT:          HontelOS
* CONTENT:          Canvas control
* PROGRAMMERS:      Jort van Dalen
*/

using System;

namespace HontelOS.System.Graphics.Controls
{
    public class CanvasDisplay : Control
    {
        public DirectBitmap Canvas;

        public CanvasDisplay(string text, Action onClick, int x, int y, int width, int height, Window window) : base(window)
        {
            Canvas = new DirectBitmap(width, height);

            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public override void Draw()
        {
            c.DrawImage(Canvas.Bitmap, X, Y);
        }

        public void SetSize(int width, int height)
        {
            Canvas.SetSize(width, height);
        }
    }
}
