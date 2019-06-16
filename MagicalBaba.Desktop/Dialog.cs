using System.ComponentModel;
using System.Runtime.CompilerServices;
using MagicalBaba.BaseLibrary;

namespace MagicalBaba.Desktop
{
    public class Dialog : INotifyPropertyChanged
    {
        private Actor _owner;
        private string _text;

        public Actor Owner
        {
            get { return _owner; }
            set
            {
                _owner = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
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