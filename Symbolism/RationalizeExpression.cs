using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolism
{
    namespace RationalizeExpression
    {
        public static class Extensions
        {
            static MathObject RationalizeSum(MathObject u, MathObject v)
            {
                var m = u.Numerator();
                var r = u.Denominator();
                var n = v.Numerator();
                var s = v.Denominator();

                if (r == 1 && s == 1) return u + v;

                return RationalizeSum(m * s, n * r) / (r * s);
            }
            
            public static MathObject RationalizeExpression(this MathObject u)
            {
                if (u is Equation)
                    return new Equation(
                        (u as Equation).a.RationalizeExpression(),
                        (u as Equation).b.RationalizeExpression(),
                        (u as Equation).Operator);
                    
                if (u is Power)
                    return (u as Power).bas.RationalizeExpression() ^ (u as Power).exp;

                if (u is Product)
                    return
                        (u as Product).Map(elt => elt.RationalizeExpression());
                        
                if (u is Sum)
                {
                    var f = (u as Sum).elts[0];

                    var g = f.RationalizeExpression();
                    var r = (u - f).RationalizeExpression();

                    return RationalizeSum(g, r);
                }

                return u;
            }
        }
    }
}
