using System.Windows.Input;
using MagicalBaba.BaseLibrary.Interfaces;
using MagicalBaba.BaseLibrary.Services;
using MagicalBaba.Desktop.DesktopServices;
using MvvmCross.Platform;

namespace MagicalBaba.Desktop
{
    internal class BootStrapper
    {
        public static void Run()
        {
            var configService = new ConfigurationService();
            Mvx.RegisterSingleton<IConfigurationService>(configService);
            var magicalService = new MagicalService(configService);
            Mvx.RegisterSingleton(magicalService);
            Mvx.RegisterType<ITextToSpeechService, WindowsSpeechToText>();
            Mvx.RegisterType<IThreadingServices, ThreadingServices>();
        }


    }
}
