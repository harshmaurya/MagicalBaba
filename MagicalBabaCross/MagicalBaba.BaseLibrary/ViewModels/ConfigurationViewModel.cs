using System.Windows.Input;
using MagicalBaba.BaseLibrary.DomainModel;
using MagicalBaba.BaseLibrary.Interfaces;
using MvvmCross.Core.ViewModels;

namespace MagicalBaba.BaseLibrary.ViewModels
{
    public class ConfigurationViewModel : MvxViewModel
    {
        private IConfigurationService _configurationService;
        private string _questionText;
        private string _helpText;
        private string _secretHotkey;
        private string _failureText;
        private ICommand _saveCommand;
        private ICommand _resetCommand;
        private ICommand _cancelCommand;

        public ConfigurationViewModel(IConfigurationService configurationService)
        {
            Initialize(configurationService);
        }

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
                RaisePropertyChanged();
            }
        }

        public string HelpText
        {
            get { return _helpText; }
            set
            {
                _helpText = value;
                RaisePropertyChanged();
            }
        }

        public string SecretHotkey
        {
            get { return _secretHotkey; }
            set
            {
                _secretHotkey = value;
                RaisePropertyChanged();
            }
        }

        public string FailureText
        {
            get { return _failureText; }
            set
            {
                _failureText = value;
                RaisePropertyChanged();
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new MvxCommand(() =>
                {
                    _configurationService.SaveConfiguration(new Configuration
                    {
                        FailureText = FailureText,
                        HelpText = HelpText,
                        QuestionText = QuestionText,
                        SecretHotkey = SecretHotkey
                    });
                    Close(this);
                }));
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new MvxCommand(() =>
                 {
                     Close(this);
                 }));
            }
        }

        

        public ICommand ResetCommand
        {
            get
            {
                return _resetCommand ?? (_resetCommand = new MvxCommand(() =>
                    {
                        _configurationService.Reset();
                        ApplyConfiguration(_configurationService.GetConfiguration());
                    }));
            }
        }
    }
}
