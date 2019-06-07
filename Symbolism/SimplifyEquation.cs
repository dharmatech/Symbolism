using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolism.SimplifyEquation
{
    public static class Extensions
    {
        public static MathObject SimplifyEquation(this MathObject expr)
        {
            // 10 * x == 0   ->   x == 0
            // 10 * x != 0   ->   x == 0

            if (expr is Equation &&
                (expr as Equation).a is Product &&
                ((expr as Equation).a as Product).elts.Any(elt => elt is Number) &&
                ((expr as Equation).b == 0))
                return new Equation(
                    Product.FromRange(((expr as Equation).a as Product).elts.Where(elt => !(elt is Number))).Simplify(),
                    0,
                    (expr as Equation).Operator).Simplify();

            // x ^ 2 == 0   ->   x == 0
            // x ^ 2 != 0   ->   x == 0

            if (expr is Equation &&
                (expr as Equation).b == 0 &&
                (expr as Equation).a is Power &&
                ((expr as Equation).a as Power).exp is Integer &&
                (((expr as Equation).a as Power).exp as Integer).val > 0)
                return ((expr as Equation).a as Power).bas == 0;

            if (expr is And) return (expr as And).Map(elt => elt.SimplifyEquation());

            if (expr is Or) return (expr as Or).Map(elt => elt.SimplifyEquation());

            return expr;
        }
    }
}
