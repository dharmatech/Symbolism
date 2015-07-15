using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolism
{
    namespace Denominator
    {
        public static class Extensions
        {
            public static MathObject Denominator(this MathObject obj)
            {
                if (obj is Fraction) return (obj as Fraction).denominator;

                if (obj is Power)
                {
                    if ((obj as Power).exp is Integer &&
                        ((obj as Power).exp as Integer).val < 0)
                        return (obj ^ -1);

                    if ((obj as Power).exp is Fraction &&
                        ((obj as Power).exp as Fraction).ToDouble().val < 0)
                        return (obj ^ -1);

                    return 1;
                }

                if (obj is Product)
                {
                    return
                        new Product() 
                        { elts = (obj as Product).elts.Select(elt => elt.Denominator()).ToList() }
                        .Simplify();
                }

                return 1;
            }
        }
    }

    
}
