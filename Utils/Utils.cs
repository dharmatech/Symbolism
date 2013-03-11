using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public static class Misc
    {
        public static T Disp<T>(this T obj)
        {
            Console.WriteLine(obj);
            return obj;
        }

        public static T Disp<T>(this T obj, string format)
        {
            Console.WriteLine(string.Format(format, obj));
            return obj;
        }
    }
}
