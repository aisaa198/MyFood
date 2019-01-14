using MyFood.BL.Modules;
using MyFood.IoHelpers;
using MyFood.MenuOptions;
using Ninject.Modules;

namespace MyFood.Modules
{
    class CliModule : NinjectModule
    {
        public override void Load()
        {
            var kernel = Kernel;
            kernel?.Load(new[] {new BlModule()});
            Bind<IGetDataFromUser>().To<GetDataFromUser>();
            Bind<IUserManagementsService>().To<UserManagementsService>().InSingletonScope();
            Bind<IMenu>().To<Menu>();
        }
    }
}
