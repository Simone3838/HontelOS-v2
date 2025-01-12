/*
* PROJECT:          HontelOS
* CONTENT:          System style
* PROGRAMMERS:      Jort van Dalen
*/

using Cosmos.System.Graphics.Fonts;
using System.Drawing;

namespace HontelOS.System.Graphics
{
    public class Style
    {
        #region General
        public Color PrimaryColor;

        public Font SystemFont;
        #endregion

        #region System
        public Color Dock_BackgroundColor;

        public Color TopBar_BackgroundColor;
        #endregion

        #region GUI
        public Color Window_BackgroundColor;
        public Color Window_HandleColor;
        public Color Window_HandleButtonGlowColor;

        public Color Button_NormalColor;
        public Color Button_HoverColor;
        public Color Button_PressedColor;
        public Color Button_DisabledColor;

        public Color TextBox_NormalColor;
        public Color TextBox_HoverColor;
        public Color TextBox_DisabledColor;

        public Color ContextMenu_BackgroundColor;
        public Color ContextMenu_HoverColor;

        public Color ToolTip_Color;
        public Color ToolTip_TextColor;

        public Color ItemsList_BackgroundColor;
        public Color ItemsList_HoverColor;
        public Color ItemsList_SelectedColor;
        #endregion

        public Style()
        {
            PrimaryColor = Color.FromArgb(60, 60, 255);

            SystemFont = PCScreenFont.Default;


            Dock_BackgroundColor = Color.FromArgb(194, 194, 194);

            TopBar_BackgroundColor = Color.FromArgb(194, 194, 194);


            Window_BackgroundColor = Color.White;
            Window_HandleColor = Color.FromArgb(243, 243, 243);
            Window_HandleButtonGlowColor = Color.LightGray;

            Button_NormalColor = Color.FromArgb(0, 122, 255);
            Button_HoverColor = Color.FromArgb(0, 150, 255);
            Button_PressedColor = Color.FromArgb(0, 100, 255);
            Button_DisabledColor = Color.Gray;

            TextBox_NormalColor = Color.LightGray;
            TextBox_HoverColor = Color.FromArgb(230, 230, 230);
            TextBox_DisabledColor = Color.Gray;

            ContextMenu_BackgroundColor = Color.FromArgb(240, 240, 240);
            ContextMenu_HoverColor = Color.FromArgb(180, 180, 180);

            ToolTip_Color = Color.White;
            ToolTip_TextColor = Color.Black;

            ItemsList_BackgroundColor = Color.LightGray;
            ItemsList_HoverColor = Color.FromArgb(230, 230, 230);
            ItemsList_SelectedColor = Color.FromArgb(0, 122, 255);
        }
    }
}
