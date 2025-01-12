/*
* PROJECT:          HontelOS
* CONTENT:          About program for HontelOS
* PROGRAMMERS:      Jort van Dalen
*/

using System.Drawing;
using HontelOS.System.Graphics.Controls;
using Cosmos.Core;
using HontelOS.System.Graphics;

namespace HontelOS.System.Applications.About
{
    public class AboutProgram : Window
    {
        public AboutProgram() : base("About this PC", WindowStyle.Dialog, (int)Kernel.screenWidth / 2 - 150, (int)Kernel.screenHeight / 2 - 250, 600, 300)
        {
            new Label("HontelOS " + VersionInfo.Version + $" ({VersionInfo.VersionNumber})", Style.SystemFont, Color.Black, 25, 25, this);

            new Label("CPU: " + CPU.GetCPUBrandString(), Style.SystemFont, Color.Black, 25, 25 + Style.SystemFont.Height * 2, this);
            new Label("RAM: " + StorageSizeConverter.AutoConvert(StorageSize.Megabyte, (long)GCImplementation.GetAvailableRAM()).Item3, Style.SystemFont, Color.Black, 25, 25 + Style.SystemFont.Height * 3, this);
            new Label("Storage: " + StorageSizeConverter.AutoConvert(StorageSize.Byte, Kernel.fileSystem.GetTotalSize("0:\\")).Item3, Style.SystemFont, Color.Black, 25, 25 + Style.SystemFont.Height * 4, this);

            WindowManager.Register(this);
        }
    }
}
