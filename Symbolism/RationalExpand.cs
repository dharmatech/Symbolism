using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism.AlgebraicExpand;
using Symbolism.RationalizeExpression;

namespace Symbolism
{
    namespace RationalExpand
    {
        public static class Extensions
        {
            public static MathObject RationalExpand(this MathObject u)
            {
                var f = u.Numerator().AlgebraicExpand();
                var g = u.Denominator().AlgebraicExpand();

                if (g == 0) return false;

                var h = (f / g).RationalizeExpression();

                if (h == u) return u;

                return h.RationalExpand();
            }
        }
    }
}
