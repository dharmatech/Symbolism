using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism.CoefficientGpe;
using Symbolism.AlgebraicExpand;

namespace Symbolism.IsolateVariable
{
    public static class Extensions
    {
        public static MathObject IsolateVariableEq(this Equation eq, Symbol sym)
        {
            if (eq.FreeOf(sym)) return eq;

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

            if (eq.a.AlgebraicExpand().DegreeGpe(new List<MathObject>() { sym }) == 2)
            {
                var a = eq.a.AlgebraicExpand().CoefficientGpe(sym, 2);
                var b = eq.a.AlgebraicExpand().CoefficientGpe(sym, 1);
                var c = eq.a.AlgebraicExpand().CoefficientGpe(sym, 0);

                //return
                //    sym == (-b - (((b ^ 2) - 4 * a * c) ^ (new Integer(1) / 2))) / (2 * a);

                return
                    new Or(
                        sym == (-b + (((b ^ 2) - 4 * a * c) ^ (new Integer(1) / 2))) / (2 * a),
                        sym == (-b - (((b ^ 2) - 4 * a * c) ^ (new Integer(1) / 2))) / (2 * a)
                        ).Simplify();
            }



            if (eq.a is Sum)
            {
                var items = ((Sum)eq.a).elts.FindAll(elt => elt.FreeOf(sym));

                //return IsolateVariable(
                //    new Equation(
                //        eq.a - new Sum() { elts = items }.Simplify(),
                //        eq.b - new Sum() { elts = items }.Simplify()),
                //    sym);


                //var new_a = eq.a; items.ForEach(elt => new_a = new_a - elt);
                //var new_b = eq.b; items.ForEach(elt => new_b = new_b - elt);

                var new_a = new Sum() { elts = (eq.a as Sum).elts.Where(elt => items.Contains(elt) == false).ToList() }.Simplify();
                var new_b = eq.b; items.ForEach(elt => new_b = new_b - elt);

                // (new_a as Sum).Where(elt => items.Contains(elt) == false)

                return IsolateVariableEq(new Equation(new_a, new_b), sym);

                //return IsolateVariable(
                //    new Equation(
                //        eq.a + new Sum() { elts = items.ConvertAll(elt => elt * -1) }.Simplify(),
                //        eq.b - new Sum() { elts = items }.Simplify()),
                //    sym);
            }

            if (eq.a is Product)
            {
                var items = ((Product)eq.a).elts.FindAll(elt => elt.FreeOf(sym));

                return IsolateVariableEq(
                    new Equation(
                        eq.a / new Product() { elts = items }.Simplify(),
                        eq.b / new Product() { elts = items }.Simplify()),
                    sym);
            }

            throw new Exception();
        }

        public static MathObject IsolateVariableOr(this Or obj, Symbol sym)
        {
            return new Or() { args = obj.args.ConvertAll(elt => (elt as Equation).IsolateVariableEq(sym)) }.Simplify();
        }

        public static MathObject IsolateVariable(this MathObject obj, Symbol sym)
        {
            if (obj is Or) return (obj as Or).IsolateVariableOr(sym);

            if (obj is Equation) return (obj as Equation).IsolateVariableEq(sym);

            throw new Exception();
        }
    }
}
