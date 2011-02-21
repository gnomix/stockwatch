using System;
using System.Reflection;

namespace utility
{
    static public class AttributeExtensions
    {
        static public bool is_decorated_with<T>(this object item) where T : Attribute
        {
            return item.GetType().is_decorated_with<T>();
        }

        static public bool is_decorated_with<T>(this ICustomAttributeProvider item) where T : Attribute
        {
            return item.IsDefined(typeof (T), true);
        }

        static public T attribute<T>(this object item) where T : Attribute
        {
            return item.GetType().attribute<T>();
        }

        static public T attribute<T>(this ICustomAttributeProvider item) where T : Attribute
        {
            return (T) item.GetCustomAttributes(typeof (T), true)[0];
        }
    }
}