using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolism
{
    namespace Has
    {
        public static class Extensions
        {
            public static bool Has(this MathObject obj, MathObject a)
            {
                if (obj == a) return true;
                
                if (obj is Equation) return (obj as Equation).a.Has(a) || (obj as Equation).b.Has(a);

                if (obj is Power) return (((Power)obj).bas.Has(a) || ((Power)obj).exp.Has(a));

                if (obj is Product) return ((Product)obj).elts.Any(elt => elt.Has(a));
                if (obj is Sum) return ((Sum)obj).elts.Any(elt => elt.Has(a));
                if (obj is Function) return ((Function)obj).args.Any(elt => elt.Has(a));

                return false;
            }

            public static bool Has(this MathObject obj, Func<MathObject, bool> proc)
            {
                if (proc(obj)) return true;

                if (obj is Equation) return (obj as Equation).a.Has(proc) || (obj as Equation).b.Has(proc);

                if (obj is Power) return (obj as Power).bas.Has(proc) || (obj as Power).exp.Has(proc);

                if (obj is Product) return (obj as Product).elts.Any(elt => elt.Has(proc));
                if (obj is Sum) return (obj as Sum).elts.Any(elt => elt.Has(proc));
                if (obj is Function) return (obj as Function).args.Any(elt => elt.Has(proc));

                return false;
            }

            public static bool FreeOf(this MathObject obj, MathObject a) => !obj.Has(a);
        }
    }
}
