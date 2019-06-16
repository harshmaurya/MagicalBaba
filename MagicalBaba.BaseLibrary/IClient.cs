using System;

namespace MagicalBaba.BaseLibrary
{
    public interface IClient
    {
        event EventHandler<TextEnteredEventArgs> OnTextEntered;
    }
}