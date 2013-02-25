#region Using directives

using System;
using System.Collections.Generic;

#endregion

namespace TCESS.ESales.CommonLayer.CommonLibrary
{
    public static class ExtensionMethods
    {
        public static void Update<T>(this IEnumerable<T> source, params Action<T>[] updates)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (updates == null)
                throw new ArgumentNullException("updates");

            foreach (T item in source)
            {
                foreach (Action<T> update in updates)
                {
                    update(item);
                }
            }
        }

        public static string FormatWith(this string format, params object[] args)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }
            //return the value
            return string.Format(format, args);
        }

        public static string FormatWith(this string format, IFormatProvider provider, params object[] args)
        {
            if (format == null)
            {
                throw new ArgumentNullException("format");
            }
            //return the value
            return string.Format(provider, format, args);
        }
    }
}