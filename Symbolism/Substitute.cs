using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolism
{
    namespace Substitute
    {
        public static class Extensions
        {
            public static MathObject Substitute(this MathObject obj, MathObject a, MathObject b)
            {
                if (obj == a) return b;

                if (obj is Equation)
                {
                    if ((obj as Equation).Operator == Equation.Operators.Equal)
                        return ((obj as Equation).a.Substitute(a, b) == (obj as Equation).b.Substitute(a, b)).Simplify();

                    if ((obj as Equation).Operator == Equation.Operators.NotEqual)
                        return ((obj as Equation).a.Substitute(a, b) != (obj as Equation).b.Substitute(a, b)).Simplify();

                    if ((obj as Equation).Operator == Equation.Operators.LessThan)
                        return ((obj as Equation).a.Substitute(a, b) < (obj as Equation).b.Substitute(a, b)).Simplify();

                    if ((obj as Equation).Operator == Equation.Operators.GreaterThan)
                        return ((obj as Equation).a.Substitute(a, b) > (obj as Equation).b.Substitute(a, b)).Simplify();

                    throw new Exception();
                }

                if (obj is Power) return (obj as Power).bas.Substitute(a, b) ^ (obj as Power).exp.Substitute(a, b);

                if (obj is Product)
                    return
                        (obj as Product).Map(elt => elt.Substitute(a, b));

                if (obj is Sum)
                    return
                        (obj as Sum).Map(elt => elt.Substitute(a, b));

                if (obj is Function)
                {
                    var obj_ = obj as Function;

                    return new Function(
                        obj_.name,
                        obj_.proc,
                        obj_.args.ConvertAll(arg => arg.Substitute(a, b)))
                    .Simplify();   
                }

                return obj;
            }

            public static MathObject SubstituteEq(this MathObject obj, Equation eq) => 
                obj.Substitute(eq.a, eq.b);

            public static MathObject SubstituteEqLs(this MathObject obj, List<Equation> eqs) => 
                eqs.Aggregate(obj, (a, eq) => a.SubstituteEq(eq));

            public static MathObject Substitute(this MathObject obj, MathObject a, int b) => 
                obj.Substitute(a, new Integer(b));

            public static MathObject Substitute(this MathObject obj, MathObject a, double b) => 
                obj.Substitute(a, new DoubleFloat(b));

        }
    }
}
