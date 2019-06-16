using System.Windows.Input;
using MagicalBaba.BaseLibrary;
using MagicalBaba.Desktop.Views;

namespace MagicalBaba.Desktop
{
    class BootStrapper
    {
        private static MainWindow _mainWindow;

        public static void Run()
        {
            _mainWindow = new MainWindow();
            var viewModel = _mainWindow.ViewModel;
            var service = new MagicalService(ConfigurationService.Instance.GetConfiguration());
            viewModel.Initialize(service);
            RegisterHotkey();
            _mainWindow.Show();
        }

        private static void RegisterHotkey()
        {
            var settings = new RoutedCommand();
            settings.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Control));
            _mainWindow.CommandBindings.Add(new CommandBinding(settings, HotKeyPressed));
        }

        private static void HotKeyPressed(object sender, ExecutedRoutedEventArgs args)
        {
            var configScreen = new ConfigurationScreen();
            configScreen.ViewModel.Initialize(ConfigurationService.Instance);
            configScreen.ShowDialog();
            _mainWindow.ViewModel.Service.Configuration = ConfigurationService.Instance.GetConfiguration();
            _mainWindow.ViewModel.Reset();
        }
    }
}
