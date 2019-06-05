using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

using Symbolism.Has;

namespace Symbolism
{
    namespace DegreeGpe
    {
        public static class Extensions
        {
            public static BigInteger DegreeMonomialGpe(this MathObject u, List<MathObject> v)
            {
                if (v.All(u.FreeOf)) return 0;

                if (v.Contains(u)) return 1;

                if (u is Power && ((Power)u).exp is Integer && ((Integer)((Power)u).exp).val > 1)
                    return ((Integer)((Power)u).exp).val;

                //if (u is Product)
                //    return ((Product)u).elts.Select(elt => elt.DegreeMonomialGpe(v)).Sum();

                if (u is Product)
                    return ((Product)u).elts.Select(elt => elt.DegreeMonomialGpe(v)).Aggregate(BigInteger.Add);

                return 0;
            }

            public static BigInteger DegreeGpe(this MathObject u, List<MathObject> v)
            {
                if (u is Sum)
                    return ((Sum)u).elts.Select(elt => elt.DegreeMonomialGpe(v)).Max();

                return u.DegreeMonomialGpe(v);
            }
        }
    }
}
