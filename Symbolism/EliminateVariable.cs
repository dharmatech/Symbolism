using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public static MathObject EliminateVarEqLs(this List<Equation> eqs, Symbol sym)
        {
            var eq = eqs.First(elt => elt.Has(sym));

            var rest = eqs.Except(new List<Equation>() { eq });

            var result = eq.IsolateVariableEq(sym);

            if (result is Equation)
            {
                var eq_sym = result as Equation;

                return new And() { args = rest.Select(elt => elt.Substitute(sym, eq_sym.b)).ToList() }.Simplify();

                // return new And() { args = rest.Select(rest_eq => rest_eq.SubstituteEq(eq_sym)).ToList() };

                // rest.Map(rest_eq => rest_eq.Substitute(eq_sym)
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

        public static MathObject EliminateVar(this MathObject expr, Symbol sym)
        {
            if (expr is And)
            {
                var eqs = (expr as And).args.Select(elt => elt as Equation);

                return EliminateVarEqLs(eqs.ToList(), sym);
            }

            if (expr is Or)
            {
                return new Or() { args = (expr as Or).args.Select(and_expr => and_expr.EliminateVar(sym)).ToList() };

                // expr.Map(and_expr => and_expr.EliminateVar(sym))
            }

            throw new Exception();
        }

        public static MathObject EliminateVars(this MathObject expr, params Symbol[] syms)
        { return syms.Aggregate(expr, (result, sym) => result.EliminateVar(sym)); }


        public static List<Equation> EliminateVariable(this List<Equation> eqs, Symbol sym)
        {
            var eq = eqs.First(elt => elt.a.Has(sym) || elt.b.Has(sym));

            var rest = eqs.Except(new List<Equation>() { eq });

            var result = eq.IsolateVariableEq(sym);

            if (result is Equation)
            {
                var eq_sym = result as Equation;

                return rest.Select(elt => elt.Substitute(sym, eq_sym.b) as Equation).ToList();
            }

            if (result is Or)
            {
                // return new Or();

                //return new Or() 
                //{ 
                //    args = 
                //        (result as Or).args.Select(eq_elt => {

                //        var eq_sym = (eq_elt as Equation);

                //        return rest.Select(elt => new Equation(
                //            elt.a.Substitute(sym, eq_sym.a),
                //            elt.b.Substitute(sym, eq_sym.b)))
                //        .ToList();
                //        })
                //};
            }

            throw new Exception();
        }


        public static List<Equation> EliminateVariables(this List<Equation> eqs, params Symbol[] syms)
        {
            var result = eqs;

            foreach (var sym in syms)
                result = result.EliminateVariable(sym);

            return result;
        }
    }
}
