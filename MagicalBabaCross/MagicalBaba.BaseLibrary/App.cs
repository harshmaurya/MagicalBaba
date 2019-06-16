using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;

namespace MagicalBaba.BaseLibrary
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            CreatableTypes()
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart(new AppStart());
        }
    }
}
