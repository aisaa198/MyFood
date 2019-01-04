using MyFood.BL.Modules;
using Ninject;

namespace MyFood.WebApi
{
    internal class NinjectBootstrap
    {
        public static IKernel GetKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(new[] { new BlModule()});
            return kernel;
        }
    }
}
