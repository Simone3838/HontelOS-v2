/*
* PROJECT:          HontelOS
* CONTENT:          Canvas ultility functions
* PROGRAMMERS:      Jort van Dalen
*/

using Cosmos.System.Graphics;
using System;
using System.Drawing;

namespace HontelOS.System.Graphics
{
    public static class CanvasUtils
    {
        // From Szymekk's Cosmos optimization kit with modifications
        public static void DrawFilledRoundedRectangle(this Canvas c, Color color, int x, int y, int width, int height, int radius)
        {
            c.DrawFilledRectangle(color, x + radius, y, width - 2 * radius, height, true);
            c.DrawFilledRectangle(color, x, y + radius, radius, height - 2 * radius, true);
            c.DrawFilledRectangle(color, x + width - radius, y + radius, radius, height - 2 * radius, true);
            c.DrawFilledCircle(color, x + radius, y + radius, radius);
            c.DrawFilledCircle(color, x + width - radius - 1, y + radius, radius);
            c.DrawFilledCircle(color, x + radius, y + height - radius - 1, radius);
            c.DrawFilledCircle(color, x + width - radius - 1, y + height - radius - 1, radius);
        }
        // From Szymekk's Cosmos optimization kit with modifications
        public static void DrawRoundedRectangle(this Canvas c, Color color, int x, int y, int width, int height, int radius)
        {
            // Draw horizontal lines
            c.DrawLine(color, x + radius, y, x + width - radius, y); // Top horizontal line
            c.DrawLine(color, x + radius, y + height, x + width - radius, y + height); // Bottom horizontal line

            // Draw vertical lines
            c.DrawLine(color, x, y + radius, x, y + height - radius); // Left vertical line
            c.DrawLine(color, x + width, y + radius, x + width, y + height - radius); // Right vertical line

            // Draw the four corner arcs
            c.DrawArc(x + radius, y + radius, radius, radius, color, 180, 270); // Top-left corner
            c.DrawArc(x + width - radius, y + radius, radius, radius, color, 270, 360); // Top-right corner
            c.DrawArc(x + radius, y + height - radius, radius, radius, color, 90, 180); // Bottom-left corner
            c.DrawArc(x + width - radius, y + height - radius, radius, radius, color, 0, 90); // Bottom-right corner
        }
        // From Szymekk's Cosmos optimization kit with modifications
        public static void DrawFilledTopRoundedRectangle(this Canvas c, Color color, int x, int y, int width, int height, int radius)
        {
            c.DrawFilledRectangle(color, x + radius, y, width - 2 * radius, height, true);
            c.DrawFilledRectangle(color, x, y + radius, width, height - radius, true);
            c.DrawFilledCircle(color, x + radius, y + radius, radius);
            c.DrawFilledCircle(color, x + width - radius - 1, y + radius, radius);
        }

        public static void DrawFilledBottomRoundedRectangle(this Canvas c, Color color, int x, int y, int width, int height, int radius)
        {
            c.DrawFilledRectangle(color, x + radius, y, width - 2 * radius, height, true);
            c.DrawFilledRectangle(color, x, y, width, height - radius, true);
            c.DrawFilledCircle(color, x + radius, y + height - radius - 1, radius);
            c.DrawFilledCircle(color, x + width - radius - 1, y + height - radius - 1, radius);
        }
        // From Szymekk's Cosmos fork with modifications
        public static Bitmap GetImage(this Canvas c, int x, int y, int width, int height)
        {
            Bitmap bitmap = new((uint)width, (uint)height, ColorDepth.ColorDepth32);

            for (int posy = y, desty = 0; posy < y + height; posy++, desty++)
                for (int posx = x, destx = 0; posx < x + width; posx++, destx++)
                    bitmap.RawData[desty * width + destx] = c.GetPointColor(posx, posy).ToArgb();
            return bitmap;
        }
    }
}