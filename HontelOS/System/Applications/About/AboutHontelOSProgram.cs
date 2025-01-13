/*
* PROJECT:          HontelOS
* CONTENT:          About HontelOS program for HontelOS
* PROGRAMMERS:      Jort van Dalen
*/

using System.Drawing;
using HontelOS.System.Graphics.Controls;
using HontelOS.System.Graphics;

namespace HontelOS.System.Applications.About
{
    public class AboutHontelOSProgram : Window
    {
        public AboutHontelOSProgram() : base("About HontelOS", WindowStyle.Dialog, (int)Kernel.screenWidth / 2 - 150, (int)Kernel.screenHeight / 2 - 250, 600, 300)
        {
            new Label("HontelOS " + VersionInfo.Version + $" ({VersionInfo.VersionNumber})", Style.SystemFont, Color.Black, 25, 25, this);

            new Label("Developed by Jort van Dalen", Style.SystemFont, Color.Black, 25, 25 + Style.SystemFont.Height * 2, this);
            new Label("Source code is available at:", Style.SystemFont, Color.Black, 25, 25 + Style.SystemFont.Height * 3, this);
            new Label("https://github.com/Schaapie-D2/HontelOS", Style.SystemFont, Color.Black, 25, 25 + Style.SystemFont.Height * 4, this);

            new Label("Copyright (c) 2025 Jort van Dalen", Style.SystemFont, Color.Black, 25, 25 + Style.SystemFont.Height * 6, this);
            new Label("All rights reserved", Style.SystemFont, Color.Black, 25, 25 + Style.SystemFont.Height * 7, this);

            WindowManager.Register(this);
        }
    }
}
