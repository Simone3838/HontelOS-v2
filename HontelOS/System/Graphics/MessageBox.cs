/*
* PROJECT:          HontelOS
* CONTENT:          Message box window
* PROGRAMMERS:      Jort van Dalen
*/

using HontelOS.System.Graphics.Controls;
using System.Collections.Generic;
using System.Drawing;
using System;
using System.Threading.Tasks;
using Cosmos.Core;

namespace HontelOS.System.Graphics
{
    public class MessageBox : Window
    {

        Action<MessageBoxResult> onSubmit;

        public MessageBox(string title, string message, Action<MessageBoxResult> onSubmit, MessageBoxButtons buttons) : base(title, WindowStyle.Dialog, (int)Kernel.screenWidth / 2 - 400, (int)Kernel.screenHeight / 2 - 300, 200, 100 + 45)
        {
            Width = Style.SystemFont.Width * message.Length + 50;
            this.onSubmit = onSubmit;
            OnClose.Add(onClose);

            new Label(message, Style.SystemFont, Color.Black, 25, (Height - 45) / 2 - Style.SystemFont.Height / 2, this);

            WindowManager.Register(this);

            switch (buttons)
            {
                case MessageBoxButtons.Ok:
                    new Button("Ok", new Action(clickOk), 10, Height - 35, 50, 25, this);
                    break;
                case MessageBoxButtons.OkCancel:
                    new Button("Ok", new Action(clickOk), 10, Height - 35, 50, 25, this);
                    new Button("Cancel", new Action(clickCancel), 70, Height - 35, 75, 25, this);
                    break;
                case MessageBoxButtons.OkRetry:
                    new Button("Ok", new Action(clickOk), 10, Height - 35, 50, 25, this);
                    new Button("Retry", new Action(clickRetry), 70, Height - 35, 75, 25, this);
                    break;
                case MessageBoxButtons.YesNo:
                    new Button("Yes", new Action(clickYes), 10, Height - 35, 50, 25, this);
                    new Button("No", new Action(clickNo), 70, Height - 35, 50, 25, this);
                    break;
                case MessageBoxButtons.YesNoCancel:
                    new Button("Yes", new Action(clickYes), 10, Height - 35, 50, 25, this);
                    new Button("No", new Action(clickNo), 70, Height - 35, 50, 25, this);
                    new Button("Cancel", new Action(clickCancel), 130, Height - 35, 75, 25, this);
                    break;
                case MessageBoxButtons.AbortRetry:
                    new Button("Abort", new Action(clickAbort), 10, Height - 35, 75, 25, this);
                    new Button("Retry", new Action(clickRetry), 95, Height - 35, 75, 25, this);
                    break;
                case MessageBoxButtons.AbortRetryIgnore:
                    new Button("Abort", new Action(clickAbort), 10, Height - 35, 75, 25, this);
                    new Button("Retry", new Action(clickRetry), 95, Height - 35, 75, 25, this);
                    new Button("Ignore", new Action(clickIgnore), 180, Height - 35, 75, 25, this);
                    break;
                case MessageBoxButtons.CancelTryContinue:
                    new Button("Cancel", new Action(clickCancel), 10, Height - 35, 75, 25, this);
                    new Button("Try", new Action(clickTry), 95, Height - 35, 50, 25, this);
                    new Button("Continue", new Action(clickContinue), 155, Height - 35, 100, 25, this);
                    break;
            }
        }

        void clickOk() { onSubmit?.Invoke(MessageBoxResult.Ok); Close(); }
        void clickCancel() { onSubmit?.Invoke(MessageBoxResult.Cancel); Close(); }
        void clickYes() { onSubmit?.Invoke(MessageBoxResult.Yes); Close(); }
        void clickNo() { onSubmit?.Invoke(MessageBoxResult.No); Close(); }
        void clickRetry() { onSubmit?.Invoke(MessageBoxResult.Retry); Close(); }
        void clickAbort() { onSubmit?.Invoke(MessageBoxResult.Abort); Close(); }
        void clickIgnore() { onSubmit?.Invoke(MessageBoxResult.Ignore); Close(); }
        void clickTry() { onSubmit?.Invoke(MessageBoxResult.Try); Close(); }
        void clickContinue() { onSubmit?.Invoke(MessageBoxResult.Continue); Close(); }

        void onClose() => onSubmit?.Invoke(MessageBoxResult.WindowClosed);
    }

    public enum MessageBoxResult
    {
        Ok,
        Cancel,
        Yes,
        No,
        Retry,
        Abort,
        Ignore,
        Try,
        Continue,
        WindowClosed
    }

    public enum MessageBoxButtons
    {
        Ok,
        OkCancel,
        OkRetry,
        YesNo,
        YesNoCancel,
        AbortRetry,
        AbortRetryIgnore,
        CancelTryContinue
    }
}
