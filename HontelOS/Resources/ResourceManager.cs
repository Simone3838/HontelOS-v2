/*
* PROJECT:          HontelOS
* CONTENT:          Resource manager
* PROGRAMMERS:      Jort van Dalen
*/


using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;

namespace HontelOS.Resources
{
    public class ResourceManager
    {
        #region Images
        [ManifestResourceStream(ResourceName = "HontelOS.Resources.Images.Hontel_Logo.bmp")] readonly static byte[] Image__Hontel_LogoRaw; public readonly static Bitmap Image__Hontel_Logo = new Bitmap(Image__Hontel_LogoRaw, ColorOrder.RGB);

        [ManifestResourceStream(ResourceName = "HontelOS.Resources.Images.BG1.bmp")] readonly static byte[] Image__Background_1Raw; public readonly static Bitmap Image__Background_1 = new Bitmap(Image__Background_1Raw, ColorOrder.RGB);
        #endregion
        #region System
        [ManifestResourceStream(ResourceName = "HontelOS.Resources.Images.System_App_List.bmp")] public readonly static byte[] Image__System_App_ListRaw; public readonly static Bitmap Image__System_App_List = new Bitmap(Image__System_App_ListRaw, ColorOrder.RGB);

        [ManifestResourceStream(ResourceName = "HontelOS.Resources.Images.System_Application.bmp")] public readonly static byte[] Image__System_ApplicationRaw; public readonly static Bitmap Image__System_Application = new Bitmap(Image__System_ApplicationRaw, ColorOrder.RGB);
        #endregion
    }
}
