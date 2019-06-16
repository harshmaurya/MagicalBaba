using System;
using System.Windows;
using MagicalBaba.Desktop.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace MagicalBaba.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        bool _setupComplete;
        private ShellWindow _shellWindow;

        protected override void OnActivated(EventArgs e)
        {
            if (!_setupComplete) DoSetup();
            base.OnActivated(e);
        }

        private void DoSetup()
        {
            var presenter = new CustomViewPresenter(_shellWindow);

            var setup = new Setup(Dispatcher, presenter);
            setup.Initialize();

            var start = Mvx.Resolve<IMvxAppStart>();
            start.Start();

            _setupComplete = true;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _shellWindow = new ShellWindow();
            _shellWindow.Show();
        }
    }
}
