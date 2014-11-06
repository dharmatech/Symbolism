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
        public static MathObject IsolateVariable(this Equation eq, Symbol sym)
        {
            if (eq.b.Has(sym)) return IsolateVariable(new Equation(eq.a - eq.b, 0), sym);

            if (eq.a == sym) return eq;

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

                var new_a = eq.a; items.ForEach(elt => new_a = new_a - elt);
                var new_b = eq.b; items.ForEach(elt => new_b = new_b - elt);

                return IsolateVariable(new Equation(new_a, new_b), sym);

                //return IsolateVariable(
                //    new Equation(
                //        eq.a + new Sum() { elts = items.ConvertAll(elt => elt * -1) }.Simplify(),
                //        eq.b - new Sum() { elts = items }.Simplify()),
                //    sym);
            }

            if (eq.a is Product)
            {
                var items = ((Product)eq.a).elts.FindAll(elt => elt.FreeOf(sym));

                return IsolateVariable(
                    new Equation(
                        eq.a / new Product() { elts = items }.Simplify(),
                        eq.b / new Product() { elts = items }.Simplify()),
                    sym);
            }

            throw new Exception();
        }
    }
}
