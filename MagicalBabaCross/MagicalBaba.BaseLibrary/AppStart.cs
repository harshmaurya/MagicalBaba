using MagicalBaba.BaseLibrary.ViewModels;
using MvvmCross.Core.ViewModels;

namespace MagicalBaba.BaseLibrary
{
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        public void Start(object hint = null)
        {
            ShowViewModel<MainViewModel>();
        }
    }
}
