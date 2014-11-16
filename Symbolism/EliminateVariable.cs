using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism.IsolateVariable;

namespace Symbolism.EliminateVariable
{
    public static class Extensions
    {
        public static List<Equation> EliminateVariable(this List<Equation> eqs, Symbol sym)
        {
            var eq = eqs.First(elt => elt.a.Has(sym) || elt.b.Has(sym));

            var rest = eqs.Except(new List<Equation>() { eq });

            var result = eq.IsolateVariable(sym);

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
