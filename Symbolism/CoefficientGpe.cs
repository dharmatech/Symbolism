using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

using Symbolism.Has;

namespace Symbolism
{
    namespace CoefficientGpe
    {
        public static class Extensions
        {
            public static Tuple<MathObject, BigInteger> CoefficientMonomialGpe(this MathObject u, MathObject x)
            {
                if (u == x) return Tuple.Create((MathObject)1, (BigInteger)1);

                if (u is Power &&
                    (u as Power).bas == x &&
                    (u as Power).exp is Integer &&
                    ((u as Power).exp as Integer).val > 1)
                    return Tuple.Create((MathObject)1, ((u as Power).exp as Integer).val);

                if (u is Product)
                {
                    var m = (BigInteger) 0;
                    var c = u;

                    foreach (var elt in (u as Product).elts)
                    {
                        var f = elt.CoefficientMonomialGpe(x);

                        if (f == null) return null;

                        if (f.Item2 != 0)
                        {
                            m = f.Item2;
                            c = u / (x ^ m);
                        }
                    }

                    return Tuple.Create(c, m);
                }

                if (u.FreeOf(x)) return Tuple.Create(u, (BigInteger)0);

                return null;
            }

            public static MathObject CoefficientGpe(this MathObject u, MathObject x, BigInteger j)
            {
                if (!(u is Sum))
                {
                    var f = u.CoefficientMonomialGpe(x);

                    if (f == null) return null;

                    if (f.Item2 == j) return f.Item1;

                    return 0;
                }

                if (u == x) return j == 1 ? 1 : 0;

                var c = (MathObject)0;

                foreach (var elt in (u as Sum).elts)
                {
                    var f = elt.CoefficientMonomialGpe(x);

                    if (f == null) return null;

                    if (f.Item2 == j) c = c + f.Item1;
                }

                return c;
            }
        }
    }
}
