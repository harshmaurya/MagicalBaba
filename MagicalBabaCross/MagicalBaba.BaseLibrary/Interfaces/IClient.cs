using System;

namespace MagicalBaba.BaseLibrary.Interfaces
{
    public interface IClient
    {
        event EventHandler<TextEnteredEventArgs> OnTextEntered;
    }
}