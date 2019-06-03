using System;
using System.Collections.Generic;
using System.Text;

namespace Symbolism.Utils
{
    public static class Extensions
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

        public static MathObject DispLong(this MathObject obj, int indent = 0, bool comma = false)
        {
            if (obj is Or || obj is And)
            {
                Console.WriteLine(new String(' ', indent) + (obj as Function).name + "(");

                var i = 0;

                foreach (var elt in (obj as Function).args)
                {
                    if (i < (obj as Function).args.Count - 1)
                        elt.DispLong(indent + 2, comma: true);
                    else
                        elt.DispLong(indent + 2);

                    i++;
                }

                Console.WriteLine(new String(' ', indent) + ")" + (comma ? "," : ""));
            }

            else Console.WriteLine(new String(' ', indent) + obj + (comma ? "," : ""));

            return obj;
        }
    }
}
