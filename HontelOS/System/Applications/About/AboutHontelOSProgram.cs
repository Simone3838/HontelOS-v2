/*
* PROJECT:          HontelOS
* CONTENT:          About HontelOS program for HontelOS
* PROGRAMMERS:      Jort van Dalen
*/

using System.Drawing;
using HontelOS.System.Graphics.Controls;
using HontelOS.System.Graphics;
using HontelOS.Resources;
using Cosmos.System.Graphics;

namespace HontelOS.System.Applications.About
{
    public class AboutHontelOSProgram : Window
    {
        public AboutHontelOSProgram() : base("About HontelOS", WindowStyle.Dialog, (int)Kernel.screenWidth / 2 - 150, (int)Kernel.screenHeight / 2 - 250, 600, 400)
        {
            Bitmap logo = Style.StyleType == StyleType.Light ? ResourceManager.HontelOSLogoBlack : ResourceManager.HontelOSLogoWhite;

            new PictureBox(logo, 25, 25, 550, 195, this);

            new Label("HontelOS " + VersionInfo.Version + $" ({VersionInfo.VersionNumber})", null, Color.Empty, 25, 235, this);

            new Label("Developed by Jort van Dalen", null, Color.Empty, 25, 235 + Style.SystemFont.Height * 2, this);
            new Label("Source code is available at:", null, Color.Empty, 25, 235 + Style.SystemFont.Height * 3, this);
            new Label("https://github.com/Schaapie-D2/HontelOS", null, Color.Empty, 25, 235 + Style.SystemFont.Height * 4, this);

            new Label("Copyright (c) 2025 Jort van Dalen", null, Color.Empty, 25, 235 + Style.SystemFont.Height * 6, this);
            new Label("All rights reserved", null, Color.Empty, 25, 235 + Style.SystemFont.Height * 7, this);

            WindowManager.Register(this);
        }
    }
}
