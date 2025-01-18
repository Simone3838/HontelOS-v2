/*
* PROJECT:          HontelOS
* CONTENT:          Control window
* PROGRAMMERS:      Jort van Dalen
*/

using Cosmos.Core;
using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace HontelOS.System.Graphics
{
    public class Window : IWindow
    {
        Canvas c = Kernel.canvas;
        public DirectBitmap canvas;
        public Style Style = Kernel.style;

        public string Title { get; set; }
        public Image Icon { get; set; }
        public WindowStyle WindowStyle;
        public int WID { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int ViewX;
        public int ViewY;
        public int ViewXUpd;
        public int ViewYUpd;

        public bool IsVisable { get; set; } = true;
        public bool CanClose { get; set; } = true;

        public bool IsDirty { get; set; } = true;

        public bool DisableMaximizeButton = false;
        public bool DisableMinimizeButton = false;

        public List<Control> Controls = new List<Control>();

        public List<Action> OnClose = new();

        public Color BackgroundColor;

        bool isHoldingHandel = false;

        int oldX;
        int oldY;
        int oldWidth;
        int oldHeight;

        int dragOffsetX;
        int dragOffsetY;

        public Window(string title, WindowStyle windowStyle, int x, int y, int width, int height)
        {
            Title = title;
            WindowStyle = windowStyle;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            canvas = new DirectBitmap(width, height);
            BackgroundColor = Style.Window_BackgroundColor;
        }

        public virtual void CustomUpdate() { return; }

        public void DrawWindow()
        {
            if (!IsVisable)
                return;

            c.DrawRoundedRectangle(Color.Black, X - 1, Y - 1, Width + 1, 33, 10);

            if (WindowStyle == WindowStyle.Normal)
            {
                c.DrawRectangle(Color.Black, X - 1, Y + 16, Width + 1, Height + 16);
                c.DrawFilledTopRoundedRectangle(Style.Window_HandleColor, X, Y, Width, 32, 10);//handel

                if (Icon != null)
                {
                    c.DrawImage(Icon, X + 10, Y + 10, 16, 16, true);//icon
                    c.DrawString(Title, PCScreenFont.Default, Color.Black, X + 32, Y + 32 / 2 - PCScreenFont.Default.Height / 2);//title
                }
                else
                    c.DrawString(Title, PCScreenFont.Default, Color.Black, X + 10, Y + 32 / 2 - PCScreenFont.Default.Height / 2);//title

                if (Kernel.MouseInArea(X + Width - 32, Y, X + Width, Y + 32))
                    c.DrawFilledRectangle(Color.Red, X + Width - 32, Y, 32, 32, true);//red glow
                c.DrawString("X", PCScreenFont.Default, Color.Black, X + Width - 32 + 16 - PCScreenFont.Default.Width / 2, Y + 32 / 2 - PCScreenFont.Default.Height / 2);//close
                if (Kernel.MouseInArea(X + Width - 64, Y, X + Width - 32, Y + 32))
                    c.DrawFilledRectangle(Style.Window_HandleButtonGlowColor, X + Width - 64, Y, 32, 32, true);//gray glow
                c.DrawString("+", PCScreenFont.Default, Color.Black, X + Width - 64 + 16 - PCScreenFont.Default.Width / 2, Y + 32 / 2 - PCScreenFont.Default.Height / 2);//maximize
                if (Kernel.MouseInArea(X + Width - 96, Y, X + Width - 64, Y + 32))
                    c.DrawFilledRectangle(Style.Window_HandleButtonGlowColor, X + Width - 96, Y, 32, 32, true);//gray glow
                c.DrawString("-", PCScreenFont.Default, Color.Black, X + Width - 96 + 16 - PCScreenFont.Default.Width / 2, Y + 32 / 2 - PCScreenFont.Default.Height / 2);//minimize
            }
            else if (WindowStyle == WindowStyle.Dialog)
            {
                c.DrawRectangle(Color.Black, X - 1, Y + 16, Width + 1, Height + 16);
                c.DrawFilledTopRoundedRectangle(Style.Window_HandleColor, X, Y, Width, 32, 10);//handel

                if (Icon != null)
                {
                    c.DrawImage(Icon, X + 10, Y + 10, 16, 16, true);//icon
                    c.DrawString(Title, PCScreenFont.Default, Color.Black, X + 32, Y + 32 / 2 - PCScreenFont.Default.Height / 2);//title
                }
                else
                    c.DrawString(Title, PCScreenFont.Default, Color.Black, X + 10, Y + 32 / 2 - PCScreenFont.Default.Height / 2);//title

                if (Kernel.MouseInArea(X + Width - 32, Y, X + Width, Y + 32))
                    c.DrawFilledRectangle(Color.Red, X + Width - 32, Y, 32, 32, true);//red glow
                c.DrawString("X", PCScreenFont.Default, Color.Black, X + Width - 32 + 16 - PCScreenFont.Default.Width / 2, Y + 32 / 2 - PCScreenFont.Default.Height / 2);//close
            }
            else if (WindowStyle == WindowStyle.Borderless)
            {
                c.DrawRectangle(Color.Black, X - 1, Y - 1, Width + 1, Height + 1);
            }

            if (!IsDirty)
            {
                if (WindowStyle != WindowStyle.Borderless)
                    c.DrawImage(canvas.Bitmap, X, Y + 32, true);
                else
                    c.DrawImage(canvas.Bitmap, X, Y, true);

                return;
            }

            canvas.Clear(BackgroundColor);

            foreach (Control c in Controls)
                c.Draw();

            if (WindowStyle != WindowStyle.Borderless)
                c.DrawImage(canvas.Bitmap, X, Y + 32, true);
            else
                c.DrawImage(canvas.Bitmap, X, Y, true);

            IsDirty = false;
        }

        public void UpdateWindow()
        {
            if (WindowStyle == WindowStyle.Normal)
            {
                if (isHoldingHandel)
                { X = (int)MouseManager.X + dragOffsetX; Y = (int)MouseManager.Y + dragOffsetY; }

                if (Kernel.MouseInArea(X, Y, X + Width, Y + Height + 32))
                {
                    if (Kernel.MouseClick())
                        WindowManager.SetFocused(WID);

                    if (Kernel.MouseInArea(X, Y, X + Width - 96, Y + 32) && MouseManager.MouseState == MouseState.Left && MouseManager.LastMouseState != MouseState.Left && !isHoldingHandel && WindowManager.FocusedWindow == WID)
                    { dragOffsetX = X - (int)MouseManager.X; dragOffsetY = Y - (int)MouseManager.Y; isHoldingHandel = true; }

                    if (Kernel.MouseInArea(X + Width - 32, Y, X + Width, Y + 32) && Kernel.MouseClick())
                        Close();
                    if (Kernel.MouseInArea(X + Width - 64, Y, X + Width - 32, Y + 32) && Kernel.MouseClick())
                        Maximize();
                    if (Kernel.MouseInArea(X + Width - 96, Y, X + Width - 64, Y + 32) && Kernel.MouseClick())
                        Minimize();
                }

                ViewX = X; ViewY = Y + 32;
            }
            else if (WindowStyle == WindowStyle.Dialog)
            {
                if (isHoldingHandel)
                { X = (int)MouseManager.X - dragOffsetX; Y = (int)MouseManager.Y - dragOffsetY; }

                if (Kernel.MouseInArea(X, Y, X + Width, Y + Height + 32))
                {
                    if (Kernel.MouseClick())
                        WindowManager.SetFocused(WID);

                    if (Kernel.MouseInArea(X, Y, X + Width - 32, Y + 32) && MouseManager.MouseState == MouseState.Left && MouseManager.LastMouseState != MouseState.Left && !isHoldingHandel && WindowManager.FocusedWindow == WID)
                    { dragOffsetX = (int)MouseManager.X - X; dragOffsetY = (int)MouseManager.Y - Y; isHoldingHandel = true; }

                    if (Kernel.MouseInArea(X + Width - 32, Y, X + Width, Y + 32) && Kernel.MouseClick())
                        Close();
                }

                ViewX = X; ViewY = Y + 32;
            }
            else if (WindowStyle == WindowStyle.Borderless)
            {
                if (isHoldingHandel)
                { X = (int)MouseManager.X - dragOffsetX; Y = (int)MouseManager.Y - dragOffsetY; }

                if (Kernel.MouseInArea(X, Y, X + Width, Y + Height))
                {
                    if (Kernel.MouseClick())
                        WindowManager.SetFocused(WID);
                }

                ViewX = X; ViewY = Y;
            }

            if (Y < 32)
                Y = 32;

            if (MouseManager.MouseState != MouseState.Left && isHoldingHandel)
                isHoldingHandel = false;

            foreach (Control c in Controls)
                c.Update();

            CustomUpdate();
        }

        public void Close()
        {
            if (CanClose)
            {
                foreach(var a in OnClose) a.Invoke();
                WindowManager.Unregister(WID);
            }
        }

        public void ForceClose()
        {
            foreach (var a in OnClose) a.Invoke();
            WindowManager.Unregister(WID);
        }

        public void Maximize()
        {
            if (X == 0 && Y == 32 && Width == Kernel.screenWidth && Height == Kernel.screenHeight - 32)
                Resize(oldX, oldY, oldWidth, oldHeight);
            else
            {
                oldX = X; oldY = Y; oldWidth = Width; oldHeight = Height;
                Resize(0, 32, (int)Kernel.screenWidth, (int)Kernel.screenHeight - 32);
            }
            WindowManager.SetFocused(WID);
            IsDirty = true;
        }

        public void Minimize() => IsVisable = false;

        public void Resize(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            canvas = new DirectBitmap(width, height);
            IsDirty = true;
        }
    }

    public enum WindowStyle
    {
        /// <summary>
        /// A window with a handel and view area with the minimize, maximize and close buttons
        /// </summary>
        Normal,
        /// <summary>
        /// The same as normal but only with the close button
        /// </summary>
        Dialog,
        /// <summary>
        /// A window only with a view area
        /// </summary>
        Borderless
    }
}