using System;
using System.Linq;
using ISIS.Infrastructure;

namespace ISIS.ReadModel.Installer
{
    class Program
    {
        static void Main(string[] args)
        {
            var denormalizerType = typeof (IDenormalizer);
            var denormalizers = denormalizerType.Assembly.GetTypes()
                .Where(t => denormalizerType.IsAssignableFrom(t))
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => (IDenormalizer) Activator.CreateInstance(t));
            foreach (var denormalizer in denormalizers )
            {
                denormalizer.TryTeardown();
                denormalizer.Setup();
            }

        }
    }
}
