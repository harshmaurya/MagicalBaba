using System.Windows;
using MagicalBaba.Desktop.ViewModel;

namespace MagicalBaba.Desktop.Views
{
    /// <summary>
    /// Interaction logic for ConfigurationScreen.xaml
    /// </summary>
    public partial class ConfigurationScreen : Window
    {
        public ConfigurationScreen()
        {
            InitializeComponent();
        }

        public ConfigurationScreenViewModel ViewModel
        {
            get
            {
                return Resources["ViewModel"] as ConfigurationScreenViewModel;
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveClicked(object sender, RoutedEventArgs e)
        {
            ViewModel.SaveCommand.Execute(null);
            DialogResult = true;
        }
    }
}
