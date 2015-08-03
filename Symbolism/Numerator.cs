using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolism
{
    namespace Numerator
    {
        public static class Extensions
        {
            public static MathObject Numerator(this MathObject obj)
            {
                if (obj is Fraction) return (obj as Fraction).numerator;

                if (obj is Power)
                {
                    if ((obj as Power).exp is Integer &&
                        ((obj as Power).exp as Integer).val < 0)
                        return 1;

                    if ((obj as Power).exp is Fraction &&
                        ((obj as Power).exp as Fraction).ToDouble().val < 0)
                        return 1;

                    return obj;
                }

                if (obj is Product)
                {
                    return
                        new Product()
                        { elts = (obj as Product).elts.Select(elt => elt.Numerator()).ToList() }
                        .Simplify();
                }

                return obj;
            }
        }
    }


}
