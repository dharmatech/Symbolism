using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolism.SimplifyLogical
{
    public static class Extensions
    {
        static bool HasDuplicates(this IEnumerable<MathObject> ls)
        {
            foreach (var elt in ls) if (ls.Count(item => item.Equals(elt)) > 1) return true;
            
            return false;
        }

        static IEnumerable<MathObject> RemoveDuplicates(this IEnumerable<MathObject> seq)
        {
            var ls = new List<MathObject>();

            foreach (var elt in seq)
                if (ls.Any(item => item.Equals(elt)) == false)
                    ls.Add(elt);

            return ls;
        }

        public static MathObject SimplifyLogical(this MathObject expr)
        {

            if (expr is And && (expr as And).args.HasDuplicates())
                return And.FromRange((expr as And).args.RemoveDuplicates());
                        
            if (expr is Or && (expr as Or).args.HasDuplicates())
                return
                    Or.FromRange((expr as Or).args.RemoveDuplicates())
                    .SimplifyLogical();

            if (expr is Or) return (expr as Or).Map(elt => elt.SimplifyLogical());

            return expr;
        }
    }
}
