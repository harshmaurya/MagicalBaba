using System;

namespace MagicalBaba.BaseLibrary
{
    public class TextEnteredEventArgs : EventArgs
    {
        public string InputText { get; private set; }

        public string OutputText { get; internal set; }

        public TextEnteredEventArgs(string inputText)
        {
            InputText = inputText;
        }
    }
}
