using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Common.Utilities.Desktop;
using MagicalBaba.BaseLibrary;

namespace MagicalBaba.Desktop.ViewModel
{
    public class ConfigurationScreenViewModel : INotifyPropertyChanged
    {
        private IConfigurationService _configurationService;
        private string _questionText;
        private string _helpText;
        private string _secretHotkey;
        private string _failureText;
        private ICommand _saveCommand;
        private ICommand _resetCommand;

        public void Initialize(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            ApplyConfiguration(_configurationService.GetConfiguration());
        }

        private void ApplyConfiguration(Configuration configuration)
        {
            QuestionText = configuration.QuestionText;
            HelpText = configuration.HelpText;
            FailureText = configuration.FailureText;
            SecretHotkey = configuration.SecretHotkey;
        }

        public string QuestionText
        {
            get { return _questionText; }
            set
            {
                _questionText = value;
                OnPropertyChanged();
            }
        }

        public string HelpText
        {
            get { return _helpText; }
            set
            {
                _helpText = value;
                OnPropertyChanged();
            }
        }

        public string SecretHotkey
        {
            get { return _secretHotkey; }
            set
            {
                _secretHotkey = value;
                OnPropertyChanged();
            }
        }

        public string FailureText
        {
            get { return _failureText; }
            set
            {
                _failureText = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(o => _configurationService.SaveConfiguration(new Configuration
                    {
                        FailureText = FailureText,
                        HelpText = HelpText,
                        QuestionText = QuestionText,
                        SecretHotkey = SecretHotkey
                    })));
            }
        }

        public ICommand ResetCommand
        {
            get
            {
                return _resetCommand ?? (_resetCommand = new RelayCommand(o =>
                    {
                        _configurationService.Reset();
                        ApplyConfiguration(_configurationService.GetConfiguration());
                    }));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
