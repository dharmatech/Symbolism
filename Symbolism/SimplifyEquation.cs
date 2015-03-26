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
            if (expr is Equation &&
                (expr as Equation).a is Product &&
                ((expr as Equation).a as Product).elts.Any(elt => elt is Number) &&
                ((expr as Equation).b == 0))
            {
                return new Equation(
                    new Product() { elts = ((expr as Equation).a as Product).elts.Where(elt => !(elt is Number)).ToList() }.Simplify(),
                    0,
                    (expr as Equation).Operator).Simplify();
            }

            if (expr is And) return (expr as And).Map(elt => elt.SimplifyEquation());

            if (expr is Or) return (expr as Or).Map(elt => elt.SimplifyEquation());

            return expr;
        }
    }
}
