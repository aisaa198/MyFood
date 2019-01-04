using AutoMapper;
using MyFood.BL.Services;
using MyFood.BL.Services.Interfaces;
using MyFood.DAL.Modules;
using Ninject.Modules;

namespace MyFood.BL.Modules
{
    public class BlModule : NinjectModule
    {
        public override void Load()
        {
            var kernel = Kernel;
            kernel?.Load(new [] {new DalModule()});
            Bind<IRatesService>().To<RatesService>();
            Bind<IRecipesService>().To<RecipesService>();
            Bind<IMapper>().ToProvider<MapperProvider>();
        }
    }
}
