using System;

namespace solidware.financials.service.domain
{
    static public class ComparableExtensions
    {
        static public bool is_before<T>(this T left, T right) where T : IComparable<T>
        {
            return left.CompareTo(right) < 0;
        }

        static public bool is_after<T>(this T left, T right) where T : IComparable<T>
        {
            return left.CompareTo(right) > 0;
        }
    }
}