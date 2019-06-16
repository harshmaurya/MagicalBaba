using System;
using System.Speech.Synthesis;
using MagicalBaba.BaseLibrary.Interfaces;

namespace MagicalBaba.Desktop.DesktopServices
{
    internal class WindowsSpeechToText : ITextToSpeechService
    {
        private readonly SpeechSynthesizer _reader = new SpeechSynthesizer();
        public void Read(string text)
        {
            _reader.SpeakAsync(text);
        }
    }
}
