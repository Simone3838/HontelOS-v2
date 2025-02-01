/*
* PROJECT:          HontelOS
* CONTENT:          HontelOS style manager
* PROGRAMMERS:      Jort van Dalen
*/

using HontelOS.System.User;

namespace HontelOS.System.Graphics
{
    public class StyleManager
    {
        public static Style Style { get; private set; }
        public static Style PreviousStyle { get; private set; }

        public static void Init()
        {
            var s = Settings.Get("Style");
            Style ns = new LightStyle();

            if(s == "D")
                ns = new DarkStyle();

            Style = ns;
            PreviousStyle = ns;
        }

        public static void SetStyle(Style style)
        {
            PreviousStyle = Style;
            Style = style;
            foreach (var a in SystemEvents.OnStyleChanged)
                a.Invoke();
        }
    }
}
