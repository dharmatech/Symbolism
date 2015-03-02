using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism.AlgebraicExpand;

using Symbolism.IsolateVariable;

namespace Symbolism.EliminateVariable
{
    public static class Extensions
    {
        // EliminateVarAnd
        // EliminateVarOr
        // EliminateVarLs
        // EliminateVar

        // EliminateVars

        public static MathObject EliminateVariableEqLs(this List<Equation> eqs, Symbol sym)
        {
            if (eqs.Any(elt => elt.Has(sym) && elt.AlgebraicExpand().Has(sym)) == false)
                return new And() { args = eqs.Select(elt => elt as MathObject).ToList() };

            // var eq = eqs.First(elt => elt.Has(sym));

            var eq = eqs.First(elt => elt.Has(sym) && elt.AlgebraicExpand().Has(sym));

            var rest = eqs.Except(new List<Equation>() { eq });

            var result = eq.IsolateVariableEq(sym);

            if (result is Equation)
            {
                var eq_sym = result as Equation;

                return new And() { args = rest.Select(elt => elt.Substitute(sym, eq_sym.b)).ToList() }.Simplify();

                // return new And() { args = rest.Select(rest_eq => rest_eq.SubstituteEq(eq_sym)).ToList() };

                // rest.Map(rest_eq => rest_eq.Substitute(eq_sym)
            }

            // Or(
            //     And(eq0, eq1, eq2, ...)
            //     And(eq3, eq4, eq5, ...)
            // )

            if (result is Or && (result as Or).args.All(elt => elt is And))
            {
                (result as Or).args.ForEach(elt => (elt as And).args.AddRange(rest));

                return new Or() { args = (result as Or).args.Select(elt => EliminateVariable(elt, sym)).ToList() };
            }

            if (result is Or)
            {
                var or = new Or();

                foreach (Equation eq_sym in (result as Or).args)
                    or.args.Add(new And() { args = rest.Select(rest_eq => rest_eq.Substitute(sym, eq_sym.b)).ToList() }.Simplify());

                return or;

                // (result as Or).Map(eq_sym => new And() { args = rest.Select(rest_eq => rest_eq.SubstituteEq(eq_sym)).ToList() });

                // (result as Or).Map(eq_sym => rest.Map(rest_eq => rest_eq.Substitute(eq_sym))
            }

            throw new Exception();
        }

        public static MathObject EliminateVariable(this MathObject expr, Symbol sym)
        {
            if (expr is And)
            {
                var eqs = (expr as And).args.Select(elt => elt as Equation);

                return EliminateVariableEqLs(eqs.ToList(), sym);
            }

            if (expr is Or)
            {
                return new Or() { args = (expr as Or).args.Select(and_expr => and_expr.EliminateVariable(sym)).ToList() };

                // expr.Map(and_expr => and_expr.EliminateVar(sym))
            }

            throw new Exception();
        }

        public static MathObject EliminateVariables(this MathObject expr, params Symbol[] syms)
        { return syms.Aggregate(expr, (result, sym) => result.EliminateVariable(sym)); }
    }
}
