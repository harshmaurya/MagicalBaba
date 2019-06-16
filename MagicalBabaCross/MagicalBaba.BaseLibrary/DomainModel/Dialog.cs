using MvvmCross.Core.ViewModels;

namespace MagicalBaba.BaseLibrary.DomainModel
{
    public class Dialog : MvxNotifyPropertyChanged
    {
        private Actor _owner;
        private string _text;

        public Actor Owner
        {
            get => _owner;
            set
            {
                _owner = value;
                RaisePropertyChanged();
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                RaisePropertyChanged();
            }
        }
    }
}