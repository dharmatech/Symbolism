using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism.ExpandProduct;
using Symbolism.ExpandPower;

namespace Symbolism
{
    namespace AlgebraicExpand
    {
        public static class Extensions
        {
            public static MathObject AlgebraicExpand(this MathObject u)
            {
                if (u is Equation)
                {
                    var eq = u as Equation;

                    return eq.a.AlgebraicExpand() == eq.b.AlgebraicExpand();
                }

                if (u is Sum)
                    return (u as Sum).Map(elt => elt.AlgebraicExpand());
                
                if (u is Product)
                {
                    var v = (u as Product).elts[0];

                    return v.AlgebraicExpand()
                        .ExpandProduct( (u / v).AlgebraicExpand() );
                }

                if (u is Power)
                {
                    var bas = (u as Power).bas;
                    var exp = (u as Power).exp;

                    if (exp is Integer && (exp as Integer).val >= 2)
                        return bas.AlgebraicExpand().ExpandPower((exp as Integer).val);
                    else 
                        return u;
                }
                                
                if (u is Function)
                {
                    var u_ = u as Function;

                    return new Function(
                        u_.name,
                        u_.proc,
                        u_.args.ConvertAll(elt => elt.AlgebraicExpand()))
                    .Simplify();
                }

                return u;
            }
        }
    }
    
}
