using System.Windows;
using MagicalBaba.BaseLibrary.ViewModels;
using MvvmCross.Wpf.Views;

namespace MagicalBaba.Desktop.Views
{
    /// <summary>
    /// Interaction logic for ConfigurationScreen.xaml
    /// </summary>
    public partial class ConfigurationScreen : MvxWpfView
    {
        public ConfigurationScreen()
        {
            InitializeComponent();
        }

        public new ConfigurationViewModel ViewModel
        {
            get
            {
                return (ConfigurationViewModel)base.ViewModel;
            }
            set
            {
                base.ViewModel = value;
            }
        }
    }
}
