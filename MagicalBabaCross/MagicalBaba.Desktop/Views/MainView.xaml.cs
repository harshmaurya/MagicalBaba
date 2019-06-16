using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Common.Utilities.Desktop;
using MagicalBaba.BaseLibrary.ViewModels;
using MvvmCross.Wpf.Views;

namespace MagicalBaba.Desktop.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : MvxWpfView
    {
        public new MainViewModel ViewModel
        {
            get { return (MainViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public MainView()
        {
            InitializeComponent();
            RegisterHotkey();
        }

        private void RegisterHotkey()
        {
            var settings = new RoutedCommand();
            settings.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(settings, HotKeyPressed));
        }

        private void HotKeyPressed(object sender, ExecutedRoutedEventArgs args)
        {
            ViewModel.ShowConfigurationCommand.Execute(null);
        }

        private void FocusTextBox()
        {
            TextEntryBox.Focus();
            Keyboard.Focus(TextEntryBox);
        }

        private void TxtQuestionOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Return || e.Key == Key.CapsLock ||
                e.Key == Key.Capital || e.Key == Key.Delete || Keyboard.IsKeyDown(Key.LeftCtrl)
                || Keyboard.IsKeyDown(Key.RightCtrl)) return;
            e.Handled = true;
            var newText = ViewModel.OnKeyEntered(KeysHelper.GetStringFromKey(e.Key, true));
            if (newText.Length > 2) return;
            var lastLocation = ((TextBox)sender).SelectionStart;
            ((TextBox)sender).Text = ((TextBox)sender).Text.Insert(lastLocation, newText);
            ((TextBox)sender).SelectionStart = lastLocation + 1;
        }

        private void TextEntryBoxEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                FocusTextBox();
            }
        }
    }
}
