using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism.LeadingCoefficientGpe;
using Symbolism.AlgebraicExpand;

namespace Symbolism
{
    namespace PolynomialGcd
    {
        using static Symbolism.PolynomialDivision.Extensions;

        public static class Extensions
        {
            public static MathObject PolynomialGcd(MathObject u, MathObject v, MathObject x)
            {
                if (u == 0 && v == 0) return 0;

                var u_ = u;
                var v_ = v;

                while (true)
                {
                    if (v_ == 0) return (u_ / u_.LeadingCoefficientGpe(x)).AlgebraicExpand();
                    
                    (u_, v_) = (v_, PolynomialDivision(u_, v_, x).remainder);
                }
            }
        }
    }
}
