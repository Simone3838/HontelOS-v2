/*
* PROJECT:          HontelOS
* CONTENT:          HontelOS text editor program
* PROGRAMMERS:      Jort van Dalen
*/

using System;
using System.IO;
using System.Linq;
using HontelOS.System.Graphics;
using HontelOS.System.Graphics.Controls;

namespace HontelOS.System.Applications.TextEditor
{
    public class TextEditorProgram : Window
    {

        public TextAreaBox textArea;
        public string filePath = "";

        public TextEditorProgram(string arg) : base("Text Editor", WindowStyle.Normal, (int)Kernel.screenWidth / 2 - 450, (int)Kernel.screenHeight / 2 - 300, 900, 600)
        {
            new Button("Save", new Action(Save), 5, 5, 100, 25, this);

            textArea = new TextAreaBox("", 0, 35, Width, Height - 35, this);

            if (File.Exists(arg))
            {
                textArea.Text = File.ReadAllLines(arg).ToList();
                filePath = arg;
            }
            else
            {
                new MessageBox("Error!", "This file doesn't exist!", null, MessageBoxButtons.Ok);
                Close();
            }

            WindowManager.Register(this);
        }

        void Save()
        {
            try
            {
                string[] file = textArea.Text.ToArray();
                File.WriteAllLines(filePath, file);
            }
            catch (Exception ex)
            {
                new MessageBox("Error!", $"Failed to save: {ex.Message}", null, MessageBoxButtons.Ok);
            }
        }
    }
}
