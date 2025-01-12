/*
* PROJECT:          HontelOS
* CONTENT:          Keyboard manager extension
* PROGRAMMERS:      Jort van Dalen
*/

using Cosmos.System;

namespace HontelOS.System.Input
{
    public class KeyboardManagerExt
    {
        public static bool KeyAvailable = false;

        static KeyEvent KeyEvent;

        public static void Update()
        {
            KeyAvailable = false;
            if (KeyboardManager.KeyAvailable)
            {
                KeyEvent = KeyboardManager.ReadKey();
                KeyAvailable = true;
            }   
        }

        public static bool GetKey(ConsoleKeyEx key)
        {
            return KeyEvent.Key == key;
        }

        public static KeyEvent ReadKey()
        {
            if (KeyAvailable)
                return KeyEvent;
            else
                return null;
        }
    }
}
