using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism.Has;

using Symbolism.Trigonometric;

using Symbolism.CoefficientGpe;
using Symbolism.AlgebraicExpand;

using Symbolism.DegreeGpe;

namespace Symbolism.IsolateVariable
{
    public static class Extensions
    {
        public static MathObject IsolateVariableEq(this Equation eq, Symbol sym)
        {
            if (eq.Operator == Equation.Operators.NotEqual) return eq;

            if (eq.FreeOf(sym)) return eq;

            // sin(x) / cos(x) == y   ->   tan(x) == y

            if (eq.a is Product && (eq.a as Product).elts.Any(elt => elt == new Sin(sym)) &&
                eq.a is Product && (eq.a as Product).elts.Any(elt => elt == 1 / new Cos(sym)))
                return
                    (eq.a / new Sin(sym) * new Cos(sym) * new Tan(sym) == eq.b).IsolateVariableEq(sym);

            // A sin(x)^2 == B sin(x) cos(x)   ->   A sin(x)^2 / (B sin(x) cos(x)) == 1

            if (
                eq.a is Product && 
                (eq.a as Product).elts.Any(elt => 
                    (elt == new Sin(sym)) ||
                    ((elt is Power) && (elt as Power).bas == new Sin(sym) && (elt as Power).exp is Number)) &&

                eq.b is Product && 
                (eq.b as Product).elts.Any(elt => 
                    (elt == new Sin(sym)) || 
                    ((elt is Power) && (elt as Power).bas == new Sin(sym) && (elt as Power).exp is Number))
                )
                return
                    (eq.a / eq.b == 1).IsolateVariableEq(sym);

            if (eq.b.Has(sym)) return IsolateVariableEq(new Equation(eq.a - eq.b, 0), sym);

            if (eq.a == sym) return eq;

            // (a x^2 + c) / x == - b

            if (eq.a is Product &&
                (eq.a as Product).elts.Any(
                    elt =>
                        elt is Power &&
                        (elt as Power).bas == sym &&
                        (elt as Power).exp == -1))
                return IsolateVariableEq(eq.a * sym == eq.b * sym, sym);

            //if (eq.a is Product &&
            //    (eq.a as Product).elts.Any(
            //        elt =>
            //            elt is Power &&
            //            (elt as Power).bas == sym &&
            //            (elt as Power).exp is Integer &&
            //            ((elt as Power).exp as Integer).val < 0))
            //    return IsolateVariableEq(eq.a * sym == eq.b * sym, sym);


            // if (eq.a.Denominator() is Product &&
            //     (eq.a.Denominator() as Product).Any(elt => elt.Base() == sym)
            // 
            // 


            // (x + y)^(1/2) == z
            //
            // x == -y + z^2   &&   z >= 0

            if (eq.a is Power && (eq.a as Power).exp == new Integer(1) / 2)
                return IsolateVariableEq((eq.a ^ 2) == (eq.b ^ 2), sym);

            // 1 / sqrt(x) == y

            if (eq.a is Power && (eq.a as Power).exp == -new Integer(1) / 2)
                return (eq.a / eq.a == eq.b / eq.a).IsolateVariable(sym);

            // x ^ 2 == y
            // x ^ 2 - y == 0

            if (eq.a.AlgebraicExpand().DegreeGpe(new List<MathObject>() { sym }) == 2 &&
                eq.b != 0)
            {
                return
                    (eq.a - eq.b == 0).IsolateVariable(sym);
            }

            // a x^2 + b x + c == 0

            if (eq.a.AlgebraicExpand().DegreeGpe(new List<MathObject>() { sym }) == 2)
            {
                var a = eq.a.AlgebraicExpand().CoefficientGpe(sym, 2);
                var b = eq.a.AlgebraicExpand().CoefficientGpe(sym, 1);
                var c = eq.a.AlgebraicExpand().CoefficientGpe(sym, 0);

                if (a == null || b == null || c == null) return eq;

                return new Or(

                    new And(
                        sym == (-b + (((b ^ 2) - 4 * a * c) ^ (new Integer(1) / 2))) / (2 * a),
                        (a != 0).Simplify()
                        ).Simplify(),

                    new And(
                        sym == (-b - (((b ^ 2) - 4 * a * c) ^ (new Integer(1) / 2))) / (2 * a),
                        (a != 0).Simplify()
                        ).Simplify(),

                    new And(sym == -c / b, a == 0, (b != 0).Simplify()).Simplify(),

                    new And(
                        (a == 0).Simplify(),
                        (b == 0).Simplify(),
                        (c == 0).Simplify()
                        ).Simplify()

                ).Simplify();
            }


            // (x + y == z).IsolateVariable(x)

            if (eq.a is Sum && (eq.a as Sum).elts.Any(elt => elt.FreeOf(sym)))
            {
                var items = ((Sum)eq.a).elts.FindAll(elt => elt.FreeOf(sym));

                //return IsolateVariable(
                //    new Equation(
                //        eq.a - new Sum() { elts = items }.Simplify(),
                //        eq.b - new Sum() { elts = items }.Simplify()),
                //    sym);


                //var new_a = eq.a; items.ForEach(elt => new_a = new_a - elt);
                //var new_b = eq.b; items.ForEach(elt => new_b = new_b - elt);

                var new_a = Sum.FromRange((eq.a as Sum).elts.Where(elt => items.Contains(elt) == false)).Simplify();
                var new_b = eq.b; items.ForEach(elt => new_b = new_b - elt);

                // (new_a as Sum).Where(elt => items.Contains(elt) == false)

                return IsolateVariableEq(new Equation(new_a, new_b), sym);

                //return IsolateVariable(
                //    new Equation(
                //        eq.a + new Sum() { elts = items.ConvertAll(elt => elt * -1) }.Simplify(),
                //        eq.b - new Sum() { elts = items }.Simplify()),
                //    sym);
            }

            // a b + a c == d

            // a + a c == d

            if (eq.a is Sum && (eq.a as Sum).elts.All(elt => elt.DegreeGpe(new List<MathObject>() { sym }) == 1))
            {
                //return 
                //    (new Sum() { elts = (eq.a as Sum).elts.Select(elt => elt / sym).ToList() }.Simplify() == eq.b / sym)
                //    .IsolateVariable(sym);

                return
                    (sym * Sum.FromRange((eq.a as Sum).elts.Select(elt => elt / sym)).Simplify() == eq.b)
                    .IsolateVariable(sym);
            }

            // -sqrt(x) + z * x == y

            if (eq.a is Sum && eq.a.Has(sym ^ (new Integer(1) / 2))) return eq;

            // sqrt(a + x) - z * x == -y

            if (eq.a is Sum && eq.a.Has(elt => elt is Power && (elt as Power).exp == new Integer(1) / 2 && (elt as Power).bas.Has(sym)))
                return eq;

            if (eq.a is Sum && eq.AlgebraicExpand().Equals(eq)) return eq;

            if (eq.a is Sum) return eq.AlgebraicExpand().IsolateVariable(sym);
                        
            // (x + 1) / (x + 2) == 3

            if (eq.a.Numerator().Has(sym) && eq.a.Denominator().Has(sym))
            {
                return IsolateVariableEq(eq.a * eq.a.Denominator() == eq.b * eq.a.Denominator(), sym);
            }
            
            // sqrt(2 + x) * sqrt(3 + x) == y

            if (eq.a is Product && (eq.a as Product).elts.All(elt => elt.Has(sym))) return eq;

            if (eq.a is Product)
            {
                var items = ((Product)eq.a).elts.FindAll(elt => elt.FreeOf(sym));

                return IsolateVariableEq(
                    new Equation(
                        eq.a / Product.FromRange(items).Simplify(),
                        eq.b / Product.FromRange(items).Simplify()),
                    sym);
            }

            // x ^ -2 == y

            if (eq.a is Power &&
                (eq.a as Power).bas == sym &&
                (eq.a as Power).exp is Integer &&
                ((eq.a as Power).exp as Integer).val < 0)
                return (eq.a / eq.a == eq.b / eq.a).IsolateVariableEq(sym);

            if (eq.a is Power) return eq;

            // sin(x) == y

            // Or(x == asin(y), x  == Pi - asin(y))

            if (eq.a is Sin)
                return
                    new Or(
                        (eq.a as Sin).args[0] == new Asin(eq.b),
                        (eq.a as Sin).args[0] == new Symbol("Pi") - new Asin(eq.b))
                        .IsolateVariable(sym);
            
            // tan(x) == y

            // x == atan(t)

            if (eq.a is Tan)
                return
                    ((eq.a as Tan).args[0] == new Atan(eq.b))
                    .IsolateVariable(sym);
            
            // asin(x) == y
            //
            // x == sin(y)

            if (eq.a is Asin)
                return
                    ((eq.a as Asin).args[0] == new Sin(eq.b))
                    .IsolateVariable(sym);
            
            throw new Exception();
        }

        public static MathObject IsolateVariable(this MathObject obj, Symbol sym)
        {
            if (obj is Or) return Or.FromRange((obj as Or).args.Select(elt => elt.IsolateVariable(sym))).Simplify();
                                                
            if (obj is And) return And.FromRange((obj as And).args.Select(elt => elt.IsolateVariable(sym))).Simplify();

            if (obj is Equation) return (obj as Equation).IsolateVariableEq(sym);

            throw new Exception();
        }
    }
}
