using Ninject;
using Ninject.Modules;

namespace ISIS.Web
{
    public class KernelModule : NinjectModule 
    {
        public override void Load()
        {
            Kernel.Bind<IKernel>()
                .ToConstant(Kernel);
        }
    }
}