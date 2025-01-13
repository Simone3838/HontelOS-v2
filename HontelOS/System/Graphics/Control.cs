/*
* PROJECT:          HontelOS
* CONTENT:          Control class
* PROGRAMMERS:      Jort van Dalen
*/

using Cosmos.System;
using Cosmos.System.Graphics;
using HontelOS.System.Graphics.Controls;
using System;
using System.Collections.Generic;

namespace HontelOS.System.Graphics
{
    public abstract class Control
    {
        public Window Window;

        public Canvas c = Kernel.canvas;
        public Style Style = Kernel.style;

        public int X;
        public int Y;
        public int Width;
        public int Height;

        public bool IsDisabled = false;
        public bool IsSelected = false;
        public bool IsHovering = false;

        public Cursor Cursor = Cursor.Default;
        public ContextMenu ContextMenu;
        public ToolTip ToolTip;

        public List<Action> OnClick = new();
        public List<Action> OnClickSec = new();
        public List<Action> OnStartHover = new();
        public List<Action> OnEndHover = new();
        public List<Action> OnMouseMove = new();

        public Control(Window window)
        {
            Window = window;
            Window.Controls.Add(this);
        }

        public virtual void Draw() { }
        public virtual void Update()
        {
            if (Kernel.MouseClick())
                IsSelected = false;

            if (Kernel.MouseInArea(Window.ViewX + X, Window.ViewY + Y, Window.ViewX + X + Width, Window.ViewY + Y + Height))
            {
                Kernel.cursor = Cursor;

                if (ToolTip != null)
                    ToolTip.Show();

                if (!IsHovering)
                    foreach (var a in OnStartHover) a.Invoke();

                IsHovering = true;
            }
            else
            {
                if (ToolTip != null)
                    ToolTip.Hide();

                if (IsHovering)
                    foreach (var a in OnEndHover) a.Invoke();

                IsHovering = false;
            }

            if (IsHovering && Kernel.MouseClick())
            {
                foreach(var a in OnClick) a.Invoke();
                IsSelected = true;
            }

            if (IsSelected && Kernel.MouseClickSec())
                foreach(var a in OnClickSec) a.Invoke();
            if (Kernel.MouseClickSec() && ContextMenu != null && IsSelected)
                ContextMenu.Show();
            
            if(MouseManager.DeltaX != 0 || MouseManager.DeltaY != 0)
                foreach(var a in OnMouseMove) a.Invoke();
        }
    }
}
