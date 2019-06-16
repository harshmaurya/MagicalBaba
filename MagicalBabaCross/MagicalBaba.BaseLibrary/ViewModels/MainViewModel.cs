using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MagicalBaba.BaseLibrary.DomainModel;
using MagicalBaba.BaseLibrary.Interfaces;
using MagicalBaba.BaseLibrary.Mappers;
using MagicalBaba.BaseLibrary.Services;
using MvvmCross.Core.ViewModels;

namespace MagicalBaba.BaseLibrary.ViewModels
{
    public class MainViewModel : MvxViewModel, IClient
    {
        private readonly ITextToSpeechService _textToSpeech;
        private readonly IThreadingServices _threadingServices;
        private readonly CardNameMapper _cardNameMapper = new CardNameMapper();
        public MagicalService Service { get; }
        private CommunicationType _currentCommunicationType;
        private ICommand _sendMessageCommand;
        private string _textEntry;
        private ICommand _resetCommand;
        private ICommand _textEnteredCommand;
        private string _spoofedText;
        private ICommand _showConfigurationCommand;
        private bool _showCard;
        private string _cardName;

        public MainViewModel(ITextToSpeechService textToSpeech, IThreadingServices threadingServices, MagicalService magicalService)
        {
            _textToSpeech = textToSpeech;
            _threadingServices = threadingServices;
            Dialogs = new ObservableCollection<Dialog>();
            Service = magicalService;
            Reset();
        }

        internal void Reset()
        {
            Service.CreateNewSession(this);
            Dialogs.Clear();
            CurrentCommunicationType = CommunicationType.NotStarted;
            ShowCard = false;
        }

        public CommunicationType CurrentCommunicationType
        {
            get => _currentCommunicationType;
            set
            {
                _currentCommunicationType = value;
                RaisePropertyChanged();
            }
        }

        public string TextEntry
        {
            get => _textEntry;
            set
            {
                _textEntry = value;
                RaisePropertyChanged();
            }
        }

        public string SpoofedText
        {
            get => _spoofedText;
            set
            {
                _spoofedText = value;
                RaisePropertyChanged();
            }
        }

        public string OnKeyEntered(string text)
        {
            var args = new TextEnteredEventArgs(text);
            RaiseOnTextEntered(args);
            SpoofedText = args.OutputText;
            return args.OutputText;
        }

        public ICommand ResetCommand => _resetCommand ?? (_resetCommand = new MvxCommand(Reset));

        public ICommand SendMessageCommand => _sendMessageCommand ?? (_sendMessageCommand = new MvxCommand(SendMessage));

        public ICommand TextEnteredCommand
        {
            get { return _textEnteredCommand ?? (_textEnteredCommand = new MvxCommand<string>(text => OnKeyEntered(text))); }
        }

        public ICommand ShowConfigurationCommand
        {
            get
            {
                return _showConfigurationCommand ??
                       (_showConfigurationCommand = new MvxCommand(() =>
                       {
                           ShowViewModel<ConfigurationViewModel>();
                       }));
            }
        }

        private void SendMessage()
        {
            switch (CurrentCommunicationType)
            {
                case CommunicationType.NotStarted:
                    {
                        AddDialogFromUserText();
                        DelayAction(() =>
                        {
                            var reply = Service.GetReply(CommunicationType.WakeUp);
                            CurrentCommunicationType = CommunicationType.WakeUp;
                            var babaDialog = new Dialog { Owner = Actor.Baba, Text = reply };
                            Dispatcher.RequestMainThreadAction(() => AddDialog(babaDialog, true));
                        }, 1000);

                        break;
                    }
                case CommunicationType.WakeUp:
                    {
                        AddDialogFromUserText();
                        DelayAction(() =>
                        {
                            var reply = Service.GetReply(CommunicationType.GetAnswer);
                            CurrentCommunicationType = CommunicationType.GetAnswer;
                            if (_cardNameMapper.IsValidCardName(reply))
                            {
                                CardName = reply;
                                ShowCard = true;
                                reply = _cardNameMapper.GetLongName(reply);
                            }
                            var babaDialog = new Dialog { Owner = Actor.Baba, Text = reply };
                            Dispatcher.RequestMainThreadAction(() => AddDialog(babaDialog, true));
                        }, 5000);
                        break;
                    }
            }
        }

        public string CardName
        {
            get => _cardName;
            set
            {
                _cardName = value;
                RaisePropertyChanged();
            }
        }

        public bool ShowCard
        {
            get => _showCard;
            set
            {
                _showCard = value;
                RaisePropertyChanged();
            }
        }

        private void AddDialogFromUserText()
        {
            var userDialog = new Dialog { Owner = Actor.User, Text = TextEntry };
            TextEntry = "";
            AddDialog(userDialog, false);
        }


        private void DelayAction(Action action, int delay)
        {
            Task.Factory.StartNew(() =>
            {
                _threadingServices.Delay(delay);
                action();
            });
        }

        private void AddDialog(Dialog dialog, bool readOutText)
        {
            if (readOutText)
                ReadOutText(dialog.Text);
            Dialogs.Add(dialog);
        }

        private void ReadOutText(string text)
        {
            _textToSpeech.Read(text);
        }

        public ObservableCollection<Dialog> Dialogs { get; set; }

        public event EventHandler<TextEnteredEventArgs> OnTextEntered;

        private void RaiseOnTextEntered(TextEnteredEventArgs args)
        {
            var handler = OnTextEntered;
            handler?.Invoke(this, args);
        }

    }
}
