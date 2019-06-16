using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using Common.Utilities.Desktop;
using MagicalBaba.BaseLibrary;

namespace MagicalBaba.Desktop.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged, IClient
    {
        public MagicalService Service { get; private set; }
        private CommunicationType _currentCommunicationType;
        private ICommand _sendMessageCommand;
        private string _textEntry;
        private ICommand _resetCommand;

        public MainWindowViewModel()
        {
            Dialogs = new ObservableCollection<Dialog>();
            UiDispatcher = Dispatcher.CurrentDispatcher;
        }

        public void Initialize(MagicalService service)
        {
            Service = service;
            Reset();
        }

        internal void Reset()
        {
            Service.CreateNewSession(this);
            Dialogs.Clear();
            CurrentCommunicationType = CommunicationType.NotStarted;
        }

        public CommunicationType CurrentCommunicationType
        {
            get { return _currentCommunicationType; }
            set
            {
                _currentCommunicationType = value;
                OnPropertyChanged("CurrentCommunicationType");
            }
        }

        public Dispatcher UiDispatcher { get; private set; }


        public string TextEntry
        {
            get { return _textEntry; }
            set
            {
                _textEntry = value;
                OnPropertyChanged("TextEntry");
            }
        }

        public string OnKeyEntered(Key key)
        {
            var text = KeysHelper.GetStringFromKey(key, true);
            var args = new TextEnteredEventArgs(text);
            RaiseOnTextEntered(args);
            return args.OutputText;
        }

        public ICommand ResetCommand
        {
            get { return _resetCommand ?? (_resetCommand = new RelayCommand(o => Reset())); }
        }

        public ICommand SendMessageCommand
        {
            get { return _sendMessageCommand ?? (_sendMessageCommand = new RelayCommand(SendMessage)); }
        }

        private void SendMessage(object o)
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
                            UiDispatcher.Invoke(() => AddDialog(babaDialog, true));
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
                            var babaDialog = new Dialog { Owner = Actor.Baba, Text = reply };
                            UiDispatcher.Invoke(() => AddDialog(babaDialog, true));
                        }, 2000);
                        break;
                    }
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
            ThreadPool.QueueUserWorkItem(state =>
            {
                Thread.Sleep(delay);
                action();
            });
        }

        private void AddDialog(Dialog dialog, bool readOutText)
        {
            Dialogs.Add(dialog);
            if (readOutText)
                ReadOutText(dialog.Text);
        }

        private static void ReadOutText(string text)
        {
            var reader = new SpeechSynthesizer();
            reader.SpeakAsync(text);
        }

        public ObservableCollection<Dialog> Dialogs { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event EventHandler<TextEnteredEventArgs> OnTextEntered;

        private void RaiseOnTextEntered(TextEnteredEventArgs args)
        {
            var handler = OnTextEntered;
            if (handler != null)
            {
                handler(this, args);
            }
        }
    }
}
