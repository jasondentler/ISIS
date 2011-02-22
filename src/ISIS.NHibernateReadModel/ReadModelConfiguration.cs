using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using ConfOrm;
using ConfOrm.Mappers;
using ConfOrm.NH;
using ConfOrm.Patterns;
using ConfOrm.Shop.CoolNaming;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;

namespace ISIS.NHibernateReadModel
{
    public static class ReadModelConfiguration
    {

        static ReadModelConfiguration()
        {
            Configuration = new Configuration().Configure();
            Configuration.AddDeserializedMapping(GetMapping(), null);
            SessionFactory = Configuration.BuildSessionFactory();
        }

        public static Configuration Configuration { get; private set; }
        public static ISessionFactory SessionFactory { get; private set; }

        private static HbmMapping GetMapping()
        {
            var orm = new ObjectRelationalMapper();
            var mapper = new Mapper(orm, new CoolPatternsAppliersHolder(orm));

            orm.Patterns.Poids.Add(IsId);
            orm.Patterns.PoidStrategies.Add(new AssignedPoidPattern());
            
            var domainClasses = typeof (IEntity).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => typeof (IEntity).IsAssignableFrom(t))
                .ToArray();

            orm.TablePerClass(domainClasses);

            mapper.AddRootClassPattern(t => true, applier => applier.Lazy(false));

            var mapping = mapper.CompileMappingFor(domainClasses);
            return mapping;
        }
        
        private static bool IsId(MemberInfo subject)
        {
            var name = subject.Name;
            return subject.GetCustomAttributes(typeof (IdAttribute), true).Any()
                   || name.Equals("id", StringComparison.InvariantCultureIgnoreCase)
                   || name.Equals("poid", StringComparison.InvariantCultureIgnoreCase)
                   || (name.StartsWith(subject.DeclaringType.Name)
                       && name.Equals(subject.DeclaringType.Name + "id", StringComparison.InvariantCultureIgnoreCase));
        }

    }
}
