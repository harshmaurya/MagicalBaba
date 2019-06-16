using System.Threading;
using MagicalBaba.BaseLibrary.Interfaces;

namespace MagicalBaba.Desktop.DesktopServices
{
    internal class ThreadingServices : IThreadingServices
    {
        public void Delay(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}
