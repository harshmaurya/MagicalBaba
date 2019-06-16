using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MagicalBaba.Desktop.ViewModel;

namespace MagicalBaba.Desktop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            FocusTextBox();
        }

        private void FocusTextBox()
        {
            TextEntryBox.Focus();
            Keyboard.Focus(TextEntryBox);
        }

        public MainWindowViewModel ViewModel
        {
            get
            {
                return Resources["MainWindowViewModel"] as MainWindowViewModel;
            }
        }

        private void TxtQuestionOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Return || e.Key == Key.CapsLock ||
                e.Key == Key.Capital || e.Key == Key.Delete || Keyboard.IsKeyDown(Key.LeftCtrl)
                || Keyboard.IsKeyDown(Key.RightCtrl)) return;
            e.Handled = true;
            var newText = ViewModel.OnKeyEntered(e.Key);
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
