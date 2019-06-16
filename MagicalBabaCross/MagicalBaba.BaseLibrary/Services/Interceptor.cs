using System.Text;
using MagicalBaba.BaseLibrary.Interfaces;

namespace MagicalBaba.BaseLibrary
{
    internal class Interceptor : IInterceptor
    {
        private readonly string _hotkey;
        private readonly string _replacementText;
        private int _hotKeyCount;
        private int _currentTextPosition;
        private readonly StringBuilder _recordedText = new StringBuilder();
        private bool _shouldRecordText;

        public Interceptor(string hotkey, string replacementText)
        {
            _hotkey = hotkey;
            _replacementText = replacementText;
        }

        public string Intercept(string text)
        {
            if (text.Equals(_hotkey))
            {
                _shouldRecordText = _hotKeyCount == 0;
                _hotKeyCount++;
                return "";
            }
            if (_shouldRecordText)
                _recordedText.Append(text);
            if (_hotKeyCount == 0) return text;
            return _currentTextPosition < _replacementText.Length ? _replacementText[_currentTextPosition++].ToString() : text;
        }

        public string GetSecretText()
        {
            return _hotKeyCount >= 2 ? _recordedText.ToString() : string.Empty;
        }
    }
}