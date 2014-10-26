using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolism
{
    namespace ExpandProduct
    {
        public static class Extensions
        {
            public static MathObject ExpandProduct(this MathObject r, MathObject s)
            {
                if (r is Sum)
                {
                    var f = (r as Sum).elts[0];

                    return f.ExpandProduct(s) + (r - f).ExpandProduct(s);
                }

                if (s is Sum) return s.ExpandProduct(r);

                return r * s;
            }
        }
    }
}
