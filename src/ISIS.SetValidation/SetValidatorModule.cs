using System;
using System.Linq;
using System.Reflection;
using Ninject.Modules;

namespace ISIS
{
    public class SetValidatorModule : NinjectModule 
    {

        public override void Load()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var maps = from type in types
                       where IsImplementation(type)
                       select new
                                  {
                                      type,
                                      interfaces = type
                           .GetInterfaces()
                           .Where(IsSetValidator)
                                  };

            foreach (var map in maps)
                foreach (var service in map.interfaces)
                    Kernel.Bind(service).To(map.type);

        }

        private bool IsImplementation(Type type)
        {
            return type.IsClass && !type.IsAbstract;
        }

        private bool IsSetValidator(Type interfaceType)
        {
            return interfaceType.IsGenericType
                   && interfaceType.GetGenericTypeDefinition() == typeof (ISetValidator<>);
        }

    }

}
