using MyFood.DAL.Repositories;
using Ninject.Modules;

namespace MyFood.DAL.Modules
{
    public class DalModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRatesRepository>().To<RatesRepository>();
            Bind<IRecipesRepository>().To<RecipesRepository>();
        }
    }
}
