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

        public DirectBitmap c { get; private set; }
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
        public List<Action> OnEndClick = new();
        public List<Action> OnClickSec = new();
        public List<Action> OnStartHover = new();
        public List<Action> OnEndHover = new();
        public List<Action> OnMouseMove = new();

        public Control(Window window)
        {
            Window = window;
            c = Window.canvas;

            OnClick.Add(() => { Window.IsDirty = true; });
            OnEndClick.Add(() => { Window.IsDirty = true; });
            OnClickSec.Add(() => { Window.IsDirty = true; });
            OnStartHover.Add(() => { Window.IsDirty = true; });
            OnEndHover.Add(() => { Window.IsDirty = true; });

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

                if (MouseManager.DeltaX != 0 || MouseManager.DeltaY != 0)
                    foreach (var a in OnMouseMove) a.Invoke();

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

            if (IsSelected)
            {
                if(Kernel.MouseClickSec())
                    foreach (var a in OnClickSec) a.Invoke();

                if(MouseManager.MouseState != MouseState.Left && MouseManager.LastMouseState == MouseState.Left)
                    foreach (var a in OnEndClick) a.Invoke();
            } 
            if (Kernel.MouseClickSec() && ContextMenu != null && IsSelected)
                ContextMenu.Show();
        }
    }
}