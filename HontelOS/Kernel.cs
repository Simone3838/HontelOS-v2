/*
* PROJECT:          HontelOS
* CONTENT:          HontelOS kernel
* PROGRAMMERS:      Jort van Dalen
*/

using Sys = Cosmos.System;
using Cosmos.System.Graphics;
using System.Drawing;
using Cosmos.System;
using HontelOS.System;
using System.Collections.Generic;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Cosmos.Core;
using Cosmos.System.Graphics.Fonts;
using Cosmos.Core.Memory;
using HontelOS.Resources;
using System;
using HontelOS.System.Applications.PasswordWindow;
using HontelOS.System.Graphics;
using HontelOS.System.User;
using HontelOS.System.Processing;
using HontelOS.System.Input;

namespace HontelOS
{
    public class Kernel : Sys.Kernel
    {
        public static Canvas canvas;
        public static Style style;
        Dock dock;
        TopBar topBar;
        public static CosmosVFS fileSystem;

        public static Cursor cursor = Cursor.Default;

        public static uint screenWidth = 1920;
        public static uint screenHeight = 1200;

        static Bitmap logo = ResourceManager.HontelLogo;
        static Bitmap BG1 = ResourceManager.Background1;

        public static List<SystemControl> systemControls = new List<SystemControl>();
        public static List<Process> Processes = new List<Process>();

        public static bool appListVisable;
        public static bool isDEBUGMode = true;

        static bool mouseClickNotice = false;
        static bool mouseClickNotice1 = false;
        static bool mouseClickSecNotice = false;
        static bool mouseClickSecNotice1 = false;

        static int heapCounter = 4;

        //LockScreen
        public static bool isUnlocked { get; internal set; }

        protected override void BeforeRun()
        {
            try
            {
                style = new Style();

                fileSystem = new CosmosVFS();
                VFSManager.RegisterVFS(fileSystem);

                Settings.Reset();
                Settings.Load();

                string[] resFromSettings = Settings.Get("Resolution").Split('x');

                screenWidth = uint.Parse(resFromSettings[0]);
                screenHeight = uint.Parse(resFromSettings[1]);
                canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(screenWidth, screenHeight, ColorDepth.ColorDepth32));


                // Boot progress image
                canvas.DrawImage(logo, (int)screenWidth / 2 - (int)screenHeight / 4, (int)screenHeight / 4, (int)screenHeight / 2, (int)screenHeight / 2);
                canvas.Display();

                Cosmos.HAL.Global.PIT.Wait(5000);

                MouseManager.ScreenWidth = screenWidth;
                MouseManager.ScreenHeight = screenHeight;
                MouseManager.X = screenWidth / 2;
                MouseManager.Y = screenHeight / 2;

                dock = new Dock();
                topBar = new TopBar();

                if (VMTools.IsVMWare)
                    new MessageBox("Audio", "VMWare does not support the audio driver for the COSMOS kernel!", null, MessageBoxButtons.Ok);

                new PasswordWindow();

                appListVisable = false;
                isUnlocked = false;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    Crash.StopKernel(ex.Message, ex.InnerException.Message, "0x00000000", "0");
                else
                    Crash.StopKernel("Fatal dotnet exception occured.", ex.Message, "0x00000000", "0");
            }
        }

        protected override void Run()
        {
            try
            {
                cursor = Cursor.Default;
                UpdateSystem();

                // Drawing GUI
                canvas.DrawImage(BG1, 0, 0, true);

                WindowManager.Draw();

                if (isUnlocked)
                {
                    DrawDesktop();

                    if (appListVisable)
                        DrawAppList();

                    // Top Drawing GUI
                    topBar.Draw();
                    dock.Draw();
                }

                foreach (SystemControl gUIElement in systemControls)
                    gUIElement.Draw();

                DrawCursor(Cursors.GetCursorRawData(cursor), MouseManager.X, MouseManager.Y);

                if(isDEBUGMode)
                    DrawDebugInfo();

                canvas.Display();

                heapCounter--;
                if(heapCounter == 0)
                {
                    heapCounter = 4;
                    Heap.Collect();
                }
            }
            catch(Exception ex)
            {
                if (ex.InnerException != null)
                    Crash.StopKernel(ex.Message, ex.InnerException.Message, "0x00000000", "0");
                else
                    Crash.StopKernel("Fatal dotnet exception occured.", ex.Message, "0x00000000", "0");
            }
        }

        #region Drawing
        void DrawDesktop()
        {
            // TODO
        }

        void DrawAppList()
        {
            canvas.DrawFilledRectangle(Color.FromArgb(64, 128, 128, 128), 0, 0, (int)screenWidth, (int)screenHeight);
        }

        void DrawCursor(int[] cursor, uint x, uint y)
        {
            for (uint h = 0; h < cursor[3]; h++)
            {
                for (uint w = 0; w < cursor[2]; w++)
                {
                    int pixelX = (int)(w + x - cursor[0]);
                    int pixelY = (int)(h + y - cursor[1]);

                    if (pixelX >= 0 && pixelX < screenWidth && pixelY >= 0 && pixelY < screenHeight)
                    {
                        if (cursor[h * cursor[2] + w + 4] == 1)
                        {
                            canvas.DrawPoint(Color.Black, pixelX, pixelY);
                        }
                        else if (cursor[h * cursor[2] + w + 4] == 2)
                        {
                            canvas.DrawPoint(Color.White, pixelX, pixelY);
                        }
                    }
                }
            }
        }
        #endregion

        #region System
        void UpdateSystem()
        {
            mouseClickNotice1 = false;
            KeyboardManagerExt.Update();

            if (!appListVisable)
            {
                foreach (SystemControl c in systemControls)
                    c.Update();
                WindowManager.Update();
                foreach (var p in Processes)
                    p.Update();
            }

            // Update top GUI
            if (isUnlocked)
            {
                dock.Update();
                topBar.Update();
            }

            MouseManager.ResetScrollDelta();
        }

        public static void Reboot()
        {
            ShutdownPrepare();
            Sys.Power.Reboot();
        }
        public static void Shutdown()
        {
            ShutdownPrepare();
            Sys.Power.Shutdown();
        }

        static void ShutdownPrepare()
        {
            canvas.Clear();
            canvas.DrawImage(logo, (int)screenWidth / 2 - (int)screenHeight / 4, (int)screenHeight / 4, (int)screenHeight / 2, (int)screenHeight / 2);
            canvas.Display();

            foreach (int wid in WindowManager.Windows.Keys)
                WindowManager.Unregister(wid);
            foreach (Process p in Processes)
                p.Kill();
        }
        #endregion

        #region Input
        public static bool MouseInArea(int x1, int y1, int x2, int y2)
        {
            int MX = (int)MouseManager.X;
            int MY = (int)MouseManager.Y;
            return (MX >= x1 && MX <= x2 && MY >= y1 && MY <= y2);
        }

        public static bool MouseClick()
        {
            if (MouseManager.MouseState != MouseState.Left)
            { mouseClickNotice = false; mouseClickNotice1 = false; }
            if (MouseManager.MouseState == MouseState.Left && !mouseClickNotice)
            { mouseClickNotice = true; mouseClickNotice1 = true; }

            if (mouseClickNotice1)
                return true;
            return false;
        }
        public static bool MouseClickSec()
        {
            if (MouseManager.MouseState != MouseState.Right)
            { mouseClickSecNotice = false; mouseClickSecNotice1 = false; }
            if (MouseManager.MouseState == MouseState.Right && !mouseClickSecNotice)
            { mouseClickSecNotice = true; mouseClickSecNotice1 = true; }

            if (mouseClickSecNotice1)
                return true;
            return false;
        }
        #endregion

        #region DEBUG
        void DrawDebugInfo()
        {
            canvas.DrawString("RAM usage: " + StorageSizeConverter.Convert(StorageSize.Byte, GCImplementation.GetUsedRAM(), StorageSize.Megabyte).ToString(), PCScreenFont.Default, Color.Blue, 0, 0);
            canvas.DrawString("Canvas Type: " + canvas.Name(), PCScreenFont.Default, Color.Blue, 0, PCScreenFont.Default.Height * 1);
            canvas.DrawString("Processes: " + Processes.Count, PCScreenFont.Default, Color.Blue, 0, PCScreenFont.Default.Height * 2);
        }
        #endregion
    }
}
