using System;

namespace MagicalBaba.BaseLibrary
{
    public class TextEnteredEventArgs : EventArgs
    {
        public string InputText { get; }

        public string OutputText { get; internal set; }

        public TextEnteredEventArgs(string inputText)
        {
            InputText = inputText;
        }
    }
}
