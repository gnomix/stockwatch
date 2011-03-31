using System;

namespace solidware.financials.windows.ui.views.controls
{
    static public class ObjectExtensions
    {
        static public bool IsNumeric(this object value)
        {
            return value.Is<short>() || value.Is<int>() || value.Is<long>() || value.Is<decimal>() || value.Is<float>() || value.Is<double>();
        }

        static public bool IsNull<T>(this T item) where T : class
        {
            return item == null;
        }

        static public bool Is<T>(this object value)
        {
            return value is T;
        }
        public static T As<T>(this object value)
        {
            if (value is T)
                return (T)value;
            if(value is Observable)
            {
                return (T)((Observable)value).Value;
            }
            return (T)Convert.ChangeType(value, typeof (T));
        }
    }
}