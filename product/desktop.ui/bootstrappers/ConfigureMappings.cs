using System;
using System.Collections.Generic;
using desktop.ui.handlers.domain;
using desktop.ui.presenters;
using utility;

namespace desktop.ui.bootstrappers
{
    public class ConfigureMappings : NeedStartup
    {
        public void run()
        {
            Map<AddedNewFamilyMember, PersonDetails>(x =>
            {
                return new PersonDetails
                {
                    id = x.id,
                    first_name = x.first_name,
                    last_name = x.last_name,
                };
            });
            Map<Person, AddedNewFamilyMember>(x =>
            {
                return new AddedNewFamilyMember
                {
                    id = x.id,
                    first_name = x.first_name,
                    last_name = x.last_name,
                };
            });
        }

        void Map<Input, Output>(Func<Input, Output> conversion)
        {
            MapperRegistery.Register(conversion);
        }
    }

    static public class MapperRegistery
    {
        static Dictionary<MapKey, object> mappings = new Dictionary<MapKey, object>();

        static public void Register<Input, Output>(Func<Input, Output> conversion)
        {
            mappings.Add(new MapKey<Input, Output>(), conversion);
        }

        static public Output Map<Input, Output>(Input item)
        {
            var converter = mappings[new MapKey<Input, Output>()];
            return converter.downcast_to<Func<Input, Output>>()(item);
        }
    }

    public interface MapKey
    {
    }

    public class MapKey<Input, Output> : MapKey
    {
        public bool Equals(MapKey<Input, Output> obj)
        {
            return !ReferenceEquals(null, obj);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (MapKey<Input, Output>)) return false;
            return Equals((MapKey<Input, Output>) obj);
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }
}