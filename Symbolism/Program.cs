using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Symbolism
{
    public abstract class MathObject
    {
        //////////////////////////////////////////////////////////////////////
        #region overloads for 'int'

        public static MathObject operator +(MathObject a, int b)
        { return a + new Integer(b); }

        public static MathObject operator -(MathObject a, int b)
        { return a - new Integer(b); }

        public static MathObject operator *(MathObject a, int b)
        { return a * new Integer(b); }

        public static MathObject operator /(MathObject a, int b)
        { return a / new Integer(b); }

        public static MathObject operator ^(MathObject a, int b)
        { return a ^ new Integer(b); }


        public static MathObject operator +(int a, MathObject b)
        { return new Integer(a) + b; }

        public static MathObject operator -(int a, MathObject b)
        { return new Integer(a) - b; }

        public static MathObject operator *(int a, MathObject b)
        { return new Integer(a) * b; }

        public static MathObject operator /(int a, MathObject b)
        { return new Integer(a) / b; }

        public static MathObject operator ^(int a, MathObject b)
        { return new Integer(a) ^ b; }
        #endregion
        //////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        #region overloads for 'double'

        public static MathObject operator +(MathObject a, double b)
        { return a + new Double(b); }

        public static MathObject operator -(MathObject a, double b)
        { return a - new Double(b); }

        public static MathObject operator *(MathObject a, double b)
        { return a * new Double(b); }

        public static MathObject operator /(MathObject a, double b)
        { return a / new Double(b); }

        public static MathObject operator ^(MathObject a, double b)
        { return a ^ new Double(b); }


        public static MathObject operator +(double a, MathObject b)
        { return new Double(a) + b; }

        public static MathObject operator -(double a, MathObject b)
        { return new Double(a) - b; }

        public static MathObject operator *(double a, MathObject b)
        { return new Double(a) * b; }

        public static MathObject operator /(double a, MathObject b)
        { return new Double(a) / b; }

        public static MathObject operator ^(double a, MathObject b)
        { return new Double(a) ^ b; }

        #endregion
        //////////////////////////////////////////////////////////////////////

        public static Equation operator ==(MathObject a, MathObject b)
        { return new Equation(a, b); }

        public static Equation operator !=(MathObject a, MathObject b)
        { return new Equation(a, b); }

        //////////////////////////////////////////////////////////////////////
        public static MathObject operator +(MathObject a, MathObject b)
        { return new Sum(a, b).Simplify(); }

        public static MathObject operator -(MathObject a, MathObject b)
        { return new Difference(a, b).Simplify(); }

        public static MathObject operator *(MathObject a, MathObject b)
        { return new Product(a, b).Simplify(); }

        public static MathObject operator /(MathObject a, MathObject b)
        { return new Quotient(a, b).Simplify(); }

        public static MathObject operator ^(MathObject a, MathObject b)
        { return new Power(a, b).Simplify(); }

        public static MathObject operator -(MathObject a)
        { return new Difference(a).Simplify(); }

        public MathObject Substitute(MathObject a, MathObject b)
        {
            if (this == a) return b;

            if (this is Power) return ((Power)this).Substitute(a, b);

            if (this is Product) return ((Product)this).Substitute(a, b);

            if (this is Sum) return ((Sum)this).Substitute(a, b);

            if (this is Function) return ((Function)this).Substitute(a, b);

            return this;
        }

        public int Precedence()
        {
            if (this is Integer) return 1000;
            if (this is Symbol) return 1000;
            if (this is Function) return 1000;
            if (this is Product) return 120;
            if (this is Sum) return 110;
            if (this is Power) return 60;

            Console.WriteLine(this.GetType().Name);

            throw new Exception();
        }
    }

    public class Equation
    {
        public MathObject a;
        public MathObject b;

        public Equation(MathObject x, MathObject y)
        { a = x; b = y; }

        public String ObjectString()
        { return "Equation(" + a + ", " + b + ")"; }

        public override string ToString()
        { return a + " == " + b; }

        public Boolean ToBoolean()
        {
            if (a is Integer && b is Integer) return ((Integer)a).Equals(b);
            if (a is Double && b is Double) return ((Double)a).Equals(b);
            if (a is Symbol && b is Symbol) return ((Symbol)a).Equals(b);
            if (a is Sum && b is Sum) return ((Sum)a).Equals(b);
            if (a is Product && b is Product) return ((Product)a).Equals(b);
            if (a is Fraction && b is Fraction) return ((Fraction)a).Equals(b);
            if (a is Power && b is Power) return ((Power)a).Equals(b);
            if (a is Function && b is Function) return ((Function)a).Equals(b);

            if (a.GetType() != b.GetType()) return false;

            Console.WriteLine("" + a.GetType() + " " + b.GetType());

            throw new Exception();
        }

        public static implicit operator Boolean(Equation eq)
        { return (eq.a == eq.b).ToBoolean(); }
    }

    public abstract class Number : MathObject { }

    public class Integer : Number
    {
        public int val;

        public Integer(int n) { val = n; }

        public string ObjectPrint()
        { return "Integer(" + val.ToString() + ")"; }

        public override string ToString()
        { return val.ToString(); }

        public override bool Equals(object obj)
        {
            if (obj is Integer) return val == ((Integer)obj).val;

            return false;
        }

        public override int GetHashCode()
        { return val.GetHashCode(); }
    }

    public class Double : Number
    {
        public double val;

        public Double(double n) { val = n; }

        public string ObjectPrint()
        { return "Double(" + val.ToString("R") + ")"; }

        public override string ToString()
        { return val.ToString("R"); }

        public override bool Equals(object obj)
        {
            if (obj is Double) return val == ((Double)obj).val;
            return false;
        }

        public override int GetHashCode()
        { return val.GetHashCode(); }
    }

    public class Fraction : Number
    {
        public Integer numerator;
        public Integer denominator;

        public Fraction(Integer a, Integer b)
        { numerator = a; denominator = b; }

        public override string ToString()
        { return "Fraction(" + numerator + ", " + denominator + ")"; }

        public Double ToDouble() { return new Double((double)numerator.val / (double)denominator.val); }
        //////////////////////////////////////////////////////////////////////

        public override bool Equals(object obj)
        {
            if (obj is Fraction) return
                numerator.val == ((Fraction)obj).numerator.val
                &&
                denominator.val == ((Fraction)obj).denominator.val;
            return false;
        }

        public override int GetHashCode()
        {
            return numerator.val.GetHashCode() + denominator.val.GetHashCode();
        }

        //////////////////////////////////////////////////////////////////////
    }

    public static class Rational
    {
        static int Div(int a, int b)
        { int rem; return Math.DivRem(a, b, out rem); }

        static int Rem(int a, int b)
        { int rem; Math.DivRem(a, b, out rem); return rem; }

        static int Gcd(int a, int b)
        {
            int r;
            while (b != 0)
            {
                r = Rem(a, b);
                a = b;
                b = r;
            }
            return Math.Abs(a);
        }

        public static MathObject SimplifyRationalNumber(MathObject u)
        {
            if (u is Integer) return u;

            if (u is Fraction)
            {
                var u_ = (Fraction)u;
                var n = u_.numerator.val;
                var d = u_.denominator.val;

                if (Rem(n, d) == 0) return new Integer(Div(n, d));

                var g = Gcd(n, d);

                if (d > 0) return new Fraction(new Integer(Div(n, g)), new Integer(Div(d, g)));

                if (d < 0) return new Fraction(new Integer(Div(-n, g)), new Integer(Div(-d, g)));
            }

            throw new Exception();
        }

        public static Integer Numerator(MathObject u)
        {
            // (a / b) / (c / d)
            // (a / b) * (d / c)
            // (a * d) / (b * c)

            if (u is Integer) return (Integer)u;

            // if (u is Fraction) return Numerator(((Fraction)u).numerator);

            if (u is Fraction)
                return
                    new Integer(
                        Numerator(((Fraction)u).numerator).val
                        *
                        Denominator(((Fraction)u).denominator).val);

            throw new Exception();
        }

        public static Integer Denominator(MathObject u)
        {
            // (a / b) / (c / d)
            // (a / b) * (d / c)
            // (a * d) / (b * c)

            if (u is Integer) return new Integer(1);

            // if (u is Fraction) return Denominator(((Fraction)u).denominator);

            if (u is Fraction)
                return
                    new Integer(
                        Denominator(((Fraction)u).numerator).val
                        *
                        Numerator(((Fraction)u).denominator).val);

            throw new Exception();
        }

        public static Fraction EvaluateSum(MathObject v, MathObject w)
        {
            // a / b + c / d
            // a d / b d + c b / b d
            // (a d + c b) / (b d)
            return
                new Fraction(
                    new Integer(Numerator(v).val * Denominator(w).val + Numerator(w).val * Denominator(v).val),
                    new Integer(Denominator(v).val * Denominator(w).val));
        }

        public static Fraction EvaluateDifference(MathObject v, MathObject w)
        {
            return
                new Fraction(
                    new Integer(Numerator(v).val * Denominator(w).val - Numerator(w).val * Denominator(v).val),
                    new Integer(Denominator(v).val * Denominator(w).val));
        }

        public static Fraction EvaluateProduct(MathObject v, MathObject w)
        {
            return
                new Fraction(
                    new Integer(Numerator(v).val * Numerator(w).val),
                    new Integer(Denominator(v).val * Denominator(w).val));
        }

        public static MathObject EvaluateQuotient(MathObject v, MathObject w)
        {
            if (Numerator(w).val == 0) return new Undefined();

            return
                new Fraction(
                    new Integer(Numerator(v).val * Denominator(w).val),
                    new Integer(Numerator(w).val * Denominator(v).val));
        }

        public static MathObject EvaluatePower(MathObject v, int n)
        {
            if (Numerator(v).val != 0)
            {
                if (n > 0) return EvaluateProduct(EvaluatePower(v, n - 1), v);
                if (n == 0) return new Integer(1);
                if (n == -1)
                    return new Fraction(new Integer(Denominator(v).val), new Integer(Numerator(v).val));
                if (n < -1)
                {
                    var s = new Fraction(new Integer(Denominator(v).val), new Integer(Numerator(v).val));
                    return EvaluatePower(s, -n);
                }
            }

            if (n >= 1) return new Integer(0);
            if (n <= 0) return new Undefined();

            throw new Exception();
        }

        public static MathObject SimplifyRNERec(MathObject u)
        {
            if (u is Integer) return u;

            if (u is Fraction)
                if (Denominator((Fraction)u).val == 0) return new Undefined();
                else return u;

            if (u is Sum && ((Sum)u).elts.Count == 1)
            { return SimplifyRNERec(((Sum)u).elts[0]); }

            if (u is Difference && ((Difference)u).elts.Count == 1)
            {
                var v = SimplifyRNERec(((Difference)u).elts[0]);

                if (v == new Undefined()) return v;

                return EvaluateProduct(new Integer(-1), v);
            }

            if (u is Sum && ((Sum)u).elts.Count == 2)
            {
                var v = SimplifyRNERec(((Sum)u).elts[0]);
                var w = SimplifyRNERec(((Sum)u).elts[1]);

                if (v == new Undefined() || w == new Undefined())
                    return new Undefined();

                return EvaluateSum(v, w);
            }

            if (u is Product && ((Product)u).elts.Count == 2)
            {
                var v = SimplifyRNERec(((Product)u).elts[0]);
                var w = SimplifyRNERec(((Product)u).elts[1]);

                if (v == new Undefined() || w == new Undefined())
                    return new Undefined();

                return EvaluateProduct(v, w);
            }

            if (u is Difference && ((Difference)u).elts.Count == 2)
            {
                var v = SimplifyRNERec(((Difference)u).elts[0]);
                var w = SimplifyRNERec(((Difference)u).elts[1]);

                if (v == new Undefined() || w == new Undefined())
                    return new Undefined();

                return EvaluateDifference(v, w);
            }

            if (u is Fraction)
            {
                var v = SimplifyRNERec(((Fraction)u).numerator);
                var w = SimplifyRNERec(((Fraction)u).denominator);

                if (v == new Undefined() || w == new Undefined())
                    return new Undefined();

                return EvaluateQuotient(v, w);
            }

            if (u is Power)
            {
                var v = SimplifyRNERec(((Power)u).bas);

                if (v == new Undefined()) return v;

                return EvaluatePower(v, ((Integer)((Power)u).exp).val);
            }

            throw new Exception();
        }

        public static MathObject SimplifyRNE(MathObject u)
        {
            var v = SimplifyRNERec(u);
            if (v is Undefined) return v;
            return SimplifyRationalNumber(v);
        }
    }

    public class Undefined : MathObject { }

    public static class MiscUtils { }

    public class Symbol : MathObject
    {
        public String name;

        public Symbol(String str) { name = str; }

        public string ObjectPrint()
        { return "Symbol(" + name + ")"; }

        public override string ToString()
        { return name; }

        public override int GetHashCode() { return name.GetHashCode(); }

        public override bool Equals(Object obj)
        { return name == (obj as Symbol).name; }
    }

    public class Function : MathObject
    {
        public String name;

        public List<MathObject> args;

        public delegate MathObject Proc(params MathObject[] ls);

        public Proc proc;

        public override bool Equals(object obj)
        {
            if ((GetType() == obj.GetType()) &&
                (name == ((Function)obj).name) &&
                (ListUtils.equal(args, ((Function)obj).args)))
                return true;
            return false;
        }

        public MathObject Simplify()
        {
            if (proc != null) return proc(args.ToArray());

            return this;
        }

        public override string ToString()
        {
            var str = new StringBuilder();

            str.Append(name);
            str.Append("(");

            for (var i = 0; i < args.Count - 1; i++)
                args.ForEach(arg => str.Append(arg + ", "));

            str.Append(args[args.Count - 1]);

            str.Append(")");

            return str.ToString();
        }

        public MathObject Substitute(MathObject a, MathObject b)
        {
            var obj = (Function)this.MemberwiseClone();

            obj.args = args.ConvertAll(arg => arg.Substitute(a, b));

            return obj.Simplify();
        }
    }

    public class Sin : Function
    {
        MathObject SinProc(params MathObject[] ls)
        {
            if (ls[0] is Double)
                return new Double(Math.Sin(((Double)ls[0]).val));

            return new Sin(ls[0]);
        }

        public Sin(MathObject param)
        {
            name = "sin";
            args = new List<MathObject>() { param };
            proc = SinProc;
        }
    }

    public static class ListUtils
    {
        public static bool IsEmpty(this List<MathObject> obj)
        { return obj.Count == 0; }

        public static List<MathObject> Cons(this List<MathObject> obj, MathObject elt)
        {
            var res = new List<MathObject>(obj);
            res.Insert(0, elt);
            return res;
        }

        public static List<MathObject> Cdr(this List<MathObject> obj)
        { return obj.GetRange(1, obj.Count - 1); }

        public static bool equal(List<MathObject> a, List<MathObject> b)
        {
            if (a.Count == 0 && b.Count == 0) return true;

            if (a.Count == 0) return false;

            if (b.Count == 0) return false;

            if (a[0] == b[0]) return equal(a.Cdr(), b.Cdr());

            return false;
        }
    }

    public static class OrderRelation
    {
        public static MathObject Base(MathObject u)
        {
            if (u is Power) return ((Power)u).bas;

            return u;
        }

        public static MathObject Exponent(MathObject u)
        {
            if (u is Power) return ((Power)u).exp;

            return new Integer(1);
        }

        public static MathObject Term(MathObject u)
        {
            if (u is Product && ((Product)u).elts[0] is Number)
                return new Product() { elts = ((Product)u).elts.Cdr() };

            if (u is Product) return u;

            return new Product() { elts = new List<MathObject>() { u } };
        }

        public static MathObject Const(MathObject u)
        {
            if (u is Product)
            {
                var res = (Product)u;

                if (res.elts[0] is Integer) return res.elts[0];

                if (res.elts[0] is Fraction) return res.elts[0];

                if (res.elts[0] is Double) return res.elts[0];

                return new Integer(1);
            }

            return new Integer(1);
        }

        public static bool O3(List<MathObject> uElts, List<MathObject> vElts)
        {
            if (uElts.IsEmpty()) return true;
            if (vElts.IsEmpty()) return false;

            var u = uElts.First();
            var v = vElts.First();

            return (!(u == v)) ?
                Compare(u, v) :
                O3(uElts.Cdr(), vElts.Cdr());
        }

        public static bool Compare(MathObject u, MathObject v)
        {
            if (u is Double && v is Double) return ((Double)u).val < ((Double)v).val;

            if (u is Double && v is Integer) return ((Double)u).val < ((Integer)v).val;

            if (u is Double && v is Fraction) return
                ((Double)u).val < ((double)((Fraction)v).numerator.val) / ((double)((Fraction)v).denominator.val);

            if (u is Integer && v is Double) return ((Integer)u).val < ((Double)v).val;

            if (u is Fraction && v is Double) return
                ((double)((Fraction)u).numerator.val) / ((double)((Fraction)u).denominator.val) < ((Double)v).val;

            if (u is Integer)
                return Compare(new Fraction((Integer)u, new Integer(1)), v);

            if (v is Integer)
                return Compare(u, new Fraction((Integer)v, new Integer(1)));

            if (u is Fraction && v is Fraction)
            {
                var u_ = (Fraction)u;
                var v_ = (Fraction)v;

                // a / b   <   c / d
                //
                // (a d) / (b d)   <   (c b) / (b d)

                return
                    (u_.numerator.val * v_.denominator.val)
                    <
                    (v_.numerator.val * u_.denominator.val);
            }

            if (u is Symbol && v is Symbol)
                return
                    String.Compare(
                        ((Symbol)u).name,
                        ((Symbol)v).name) < 0;

            if (u is Product && v is Product)
            {
                // var a = new List<MathObject>(((Product)u).elts.Cdr());
                var a = new List<MathObject>(((Product)u).elts);
                a.Reverse();

                // var b = new List<MathObject>(((Product)v).elts.Cdr());
                var b = new List<MathObject>(((Product)v).elts);
                b.Reverse();

                return O3(a, b);
            }

            if (u is Sum && v is Sum)
            {
                // var a = new List<MathObject>(((Sum)u).elts.Cdr());
                var a = new List<MathObject>(((Sum)u).elts);
                a.Reverse();

                // var b = new List<MathObject>(((Sum)v).elts.Cdr());
                var b = new List<MathObject>(((Sum)v).elts);
                b.Reverse();

                return O3(a, b);
            }

            if (u is Power && v is Power)
            {
                var u_ = (Power)u;
                var v_ = (Power)v;

                return (u_.bas == v_.bas) ?
                    Compare(u_.exp, v_.exp) :
                    Compare(u_.bas, v_.bas);
            }

            if (u is Function && v is Function)
            {
                var u_ = (Function)u;
                var v_ = (Function)v;

                return u_.name == v_.name ?
                    O3(u_.args, v_.args) :
                    String.Compare(u_.name, v_.name) < 0;
            }

            if ((u is Integer || u is Fraction || u is Double)
                &&
                !(v is Integer || v is Fraction || v is Double))
                return true;

            if (u is Product &&
                (v is Power || v is Sum || v is Function || v is Symbol))
                return Compare(u, new Product(v));

            if (u is Power && (v is Sum || v is Function || v is Symbol))
                return Compare(u, new Power(v, new Integer(1)));

            if (u is Sum && (v is Function || v is Symbol))
                return Compare(u, new Sum(v));

            if (u is Function && v is Symbol)
            {
                var u_ = (Function)u;
                var v_ = (Symbol)v;

                return u_.name == v_.name ?
                    false :
                    Compare(new Symbol(u_.name), v);
            }

            return !Compare(v, u);
        }
    }

    class Power : MathObject
    {
        public MathObject bas;
        public MathObject exp;

        public Power(MathObject a, MathObject b) { bas = a; exp = b; }

        public string ObjectPrint()
        { return "Power(" + bas + ", " + exp + ")"; }

        public override string ToString()
        {
            var str = new StringBuilder();

            if (bas.Precedence() < Precedence()) str.Append("(");
            str.Append(bas);
            if (bas.Precedence() < Precedence()) str.Append(")");

            str.Append(" ^ ");

            if (exp.Precedence() < Precedence()) str.Append("(");
            str.Append(exp);
            if (exp.Precedence() < Precedence()) str.Append(")");

            return str.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is Power)
                return bas == ((Power)obj).bas && exp == ((Power)obj).exp;
            return false;
        }

        public MathObject Simplify()
        {
            var v = bas;
            var w = exp;

            if (v == new Integer(0)) return new Integer(0);
            if (v == new Integer(1)) return new Integer(1);
            if (w == new Integer(0)) return new Integer(1);
            if (w == new Integer(1)) return v;

            // Logic from MPL/Scheme:
            //
            //if (v is Integer && w is Integer)
            //    return
            //        new Integer(
            //            (int)Math.Pow(((Integer)v).val, ((Integer)w).val));

            // C# doesn't have built-in rationals. So:
            // 1 / 3 -> 3 ^ -1 -> 0.333... -> (int)... -> 0

            //if (v is Integer && w is Integer && ((Integer)w).val > 1)
            //    return
            //        new Integer(
            //            (int)Math.Pow(((Integer)v).val, ((Integer)w).val));

            var n = w;

            if ((v is Integer || v is Fraction) && n is Integer)
                return Rational.SimplifyRNE(new Power(v, n));

            if (v is Double && w is Integer)
                return new Double(Math.Pow(((Double)v).val, ((Integer)w).val));

            if (v is Double && w is Fraction)
                return new Double(Math.Pow(((Double)v).val, ((Fraction)w).ToDouble().val));

            if (v is Integer && w is Double)
                return new Double(Math.Pow(((Integer)v).val, ((Double)w).val));

            if (v is Fraction && w is Double)
                return new Double(Math.Pow(((Fraction)v).ToDouble().val, ((Double)w).val));

            if (v is Power && w is Integer)
            { return ((Power)v).bas ^ (((Power)v).exp * w); }

            if (v is Product && w is Integer)
            {
                var list = new List<MathObject>();

                ((Product)v).elts.ForEach(elt => list.Add(elt ^ w));

                return new Product() { elts = list }.Simplify();
            }

            return new Power(v, w);
        }

        public MathObject Substitute(MathObject a, MathObject b)
        { return bas.Substitute(a, b) ^ exp.Substitute(a, b); }
    }

    class Product : MathObject
    {
        public List<MathObject> elts;

        public Product(params MathObject[] ls)
        { elts = new List<MathObject>(ls); }

        public string ObjectString()
        {
            var str = new StringBuilder();

            str.Append("Product(");

            for (var i = 0; i < elts.Count - 1; i++)
            {
                str.Append(elts[i]);
                str.Append(", ");
            }

            str.Append(elts[elts.Count - 1]);
            str.Append(")");

            return str.ToString();
        }

        public override string ToString()
        {
            var str = new StringBuilder();

            for (var i = 0; i < elts.Count - 1; i++)
            {
                if (elts[i].Precedence() < Precedence()) str.Append("(");
                str.Append(elts[i]);
                if (elts[i].Precedence() < Precedence()) str.Append(")");

                str.Append(" * ");
            }

            if (elts[elts.Count - 1].Precedence() < Precedence()) str.Append("(");
            str.Append(elts[elts.Count - 1]);
            if (elts[elts.Count - 1].Precedence() < Precedence()) str.Append(")");

            return str.ToString();
        }

        //////////////////////////////////////////////////////////////////////
        public override int GetHashCode() { return elts.GetHashCode(); }

        public override bool Equals(object obj)
        { return ListUtils.equal(elts, ((Product)obj).elts); }
        //////////////////////////////////////////////////////////////////////

        static List<MathObject> MergeProducts(List<MathObject> pElts, List<MathObject> qElts)
        {
            if (pElts.Count == 0) return qElts;
            if (qElts.Count == 0) return pElts;

            var p = pElts[0];
            var ps = pElts.Cdr();

            var q = qElts[0];
            var qs = qElts.Cdr();

            var res = RecursiveSimplify(new List<MathObject>() { p, q });

            if (res.Count == 0) return MergeProducts(ps, qs);

            if (res.Count == 1) return MergeProducts(ps, qs).Cons(res[0]);

            if (ListUtils.equal(res, new List<MathObject>() { p, q }))
                return MergeProducts(ps, qElts).Cons(p);

            if (ListUtils.equal(res, new List<MathObject>() { q, p }))
                return MergeProducts(pElts, qs).Cons(q);

            throw new Exception();
        }

        static List<MathObject> SimplifyDoubleNumberProduct(Double a, Number b)
        {
            double val = 0.0;

            if (b is Double) val = a.val * ((Double)b).val;

            if (b is Integer) val = a.val * ((Integer)b).val;

            if (b is Fraction) val = a.val * ((Fraction)b).ToDouble().val;

            if (val == 1.0) return new List<MathObject>() { };

            return new List<MathObject>() { new Double(val) };
        }

        public static List<MathObject> RecursiveSimplify(List<MathObject> elts)
        {
            if (elts.Count == 2)
            {
                if (elts[0] is Product && elts[1] is Product)
                    return MergeProducts(
                        ((Product)elts[0]).elts,
                        ((Product)elts[1]).elts);

                if (elts[0] is Product)
                    return MergeProducts(
                        ((Product)elts[0]).elts,
                        new List<MathObject>() { elts[1] });

                if (elts[1] is Product)
                    return MergeProducts(
                        new List<MathObject>() { elts[0] },
                        ((Product)elts[1]).elts);

                //////////////////////////////////////////////////////////////////////

                if (elts[0] is Double && elts[1] is Number)
                    return SimplifyDoubleNumberProduct((Double)elts[0], (Number)elts[1]);

                if (elts[0] is Number && elts[1] is Double)
                    return SimplifyDoubleNumberProduct((Double)elts[1], (Number)elts[0]);

                //////////////////////////////////////////////////////////////////////

                if ((elts[0] is Integer || elts[0] is Fraction)
                    &&
                    (elts[1] is Integer || elts[1] is Fraction))
                {
                    var P =
                        Rational.SimplifyRNE(new Product(elts[0], elts[1]));

                    if (P is Integer && ((Integer)P).val == 1)
                        return new List<MathObject>() { };

                    return new List<MathObject>() { P };
                }

                if (elts[0] is Integer && ((Integer)elts[0]).val == 1)
                    return new List<MathObject>() { elts[1] };

                if (elts[1] is Integer && ((Integer)elts[1]).val == 1)
                    return new List<MathObject>() { elts[0] };

                var p = elts[0];
                var q = elts[1];

                if (OrderRelation.Base(p) == OrderRelation.Base(q))
                {
                    // var res = Base(p) ^ (Exponent(p) + Exponent(q));

                    var res =
                        new Power(
                            OrderRelation.Base(p),
                            new Sum(
                                OrderRelation.Exponent(p),
                                OrderRelation.Exponent(q)).Simplify()
                            ).Simplify();

                    if (res is Integer && ((Integer)res).val == 1)
                        return new List<MathObject>() { };
                    else
                        return new List<MathObject>() { res };
                }

                if (OrderRelation.Compare(q, p))
                    return new List<MathObject>() { q, p };

                return new List<MathObject>() { p, q };
            }

            if (elts[0] is Product)
                return
                    MergeProducts(
                        ((Product)elts[0]).elts,
                        RecursiveSimplify(elts.Cdr()));

            return MergeProducts(
                new List<MathObject>() { elts[0] },
                RecursiveSimplify(elts.Cdr()));

            throw new Exception();
        }

        public MathObject Simplify()
        {
            if (elts.Count == 1) return elts[0];

            Func<MathObject, bool> IsZero = obj =>
            {
                if (obj is Integer) return ((Integer)obj).val == 0;
                return false;
            };

            if (elts.Any(IsZero)) return new Integer(0);

            var res = RecursiveSimplify(elts);

            if (res.IsEmpty()) return new Integer(1);

            if (res.Count == 1) return res[0];

            return new Product() { elts = res };
        }

        public MathObject Substitute(MathObject a, MathObject b)
        {
            return
                new Product() { elts = elts.ConvertAll(elt => elt.Substitute(a, b)) }
                .Simplify();
        }

    }

    class Sum : MathObject
    {
        public List<MathObject> elts;

        public Sum(params MathObject[] ls)
        { elts = new List<MathObject>(ls); }

        //////////////////////////////////////////////////////////////////////
        public override int GetHashCode()
        { return elts.GetHashCode(); }

        public override bool Equals(object obj)
        { return ListUtils.equal(elts, ((Sum)obj).elts); }
        //////////////////////////////////////////////////////////////////////

        static List<MathObject> MergeSums(List<MathObject> pElts, List<MathObject> qElts)
        {
            if (pElts.Count == 0) return qElts;
            if (qElts.Count == 0) return pElts;

            var p = pElts[0];
            var ps = pElts.Cdr();

            var q = qElts[0];
            var qs = qElts.Cdr();

            var res = RecursiveSimplify(new List<MathObject>() { p, q });

            if (res.Count == 0) return MergeSums(ps, qs);

            if (res.Count == 1) return MergeSums(ps, qs).Cons(res[0]);

            if (ListUtils.equal(res, new List<MathObject>() { p, q }))
                return MergeSums(ps, qElts).Cons(p);

            if (ListUtils.equal(res, new List<MathObject>() { q, p }))
                return MergeSums(pElts, qs).Cons(q);

            throw new Exception();
        }

        static List<MathObject> SimplifyDoubleNumberSum(Double a, Number b)
        {
            double val = 0.0;

            if (b is Double) val = a.val + ((Double)b).val;

            if (b is Integer) val = a.val + ((Integer)b).val;

            if (b is Fraction) val = a.val + ((Fraction)b).ToDouble().val;

            if (val == 0.0) return new List<MathObject>() { };

            return new List<MathObject>() { new Double(val) };
        }

        static List<MathObject> RecursiveSimplify(List<MathObject> elts)
        {
            if (elts.Count == 2)
            {
                if (elts[0] is Sum && elts[1] is Sum)
                    return MergeSums(
                        ((Sum)elts[0]).elts,
                        ((Sum)elts[1]).elts);

                if (elts[0] is Sum)
                    return MergeSums(
                        ((Sum)elts[0]).elts,
                        new List<MathObject>() { elts[1] });

                if (elts[1] is Sum)
                    return MergeSums(
                        new List<MathObject>() { elts[0] },
                        ((Sum)elts[1]).elts);

                //////////////////////////////////////////////////////////////////////

                if (elts[0] is Double && elts[1] is Number)
                    return SimplifyDoubleNumberSum((Double)elts[0], (Number)elts[1]);

                if (elts[0] is Number && elts[1] is Double)
                    return SimplifyDoubleNumberSum((Double)elts[1], (Number)elts[0]);

                //////////////////////////////////////////////////////////////////////

                if ((elts[0] is Integer || elts[0] is Fraction)
                    &&
                    (elts[1] is Integer || elts[1] is Fraction))
                {
                    var P =
                        Rational.SimplifyRNE(new Sum(elts[0], elts[1]));

                    if (P is Integer && ((Integer)P).val == 0)
                        return new List<MathObject>() { };

                    return new List<MathObject>() { P };
                }

                if (elts[0] is Integer && elts[1] is Integer)
                {
                    var res = ((Integer)elts[0]).val + ((Integer)elts[1]).val;

                    return res == 0 ?
                        new List<MathObject>() :
                        new List<MathObject>() { new Integer(res) };
                }

                if (elts[0] is Integer && ((Integer)elts[0]).val == 0)
                    return new List<MathObject>() { elts[1] };

                if (elts[1] is Integer && ((Integer)elts[1]).val == 0)
                    return new List<MathObject>() { elts[0] };

                var p = elts[0];
                var q = elts[1];

                if (OrderRelation.Term(p) == OrderRelation.Term(q))
                {
                    var res =
                        new Product(
                            OrderRelation.Term(p),
                            new Sum(
                                OrderRelation.Const(p),
                                OrderRelation.Const(q)).Simplify()
                            ).Simplify();

                    if (res is Integer && ((Integer)res).val == 0)
                        return new List<MathObject>() { };
                    else
                        return new List<MathObject>() { res };
                }

                if (OrderRelation.Compare(q, p))
                    return new List<MathObject>() { q, p };

                return new List<MathObject>() { p, q };
            }

            if (elts[0] is Sum)
                return
                    MergeSums(
                        ((Sum)elts[0]).elts, RecursiveSimplify(elts.Cdr()));

            return MergeSums(
                new List<MathObject>() { elts[0] }, RecursiveSimplify(elts.Cdr()));
        }

        public MathObject Simplify()
        {
            if (elts.Count == 1) return elts[0];

            var res = RecursiveSimplify(elts);

            if (res.Count == 0) return new Integer(0);
            if (res.Count == 1) return res[0];

            return new Sum() { elts = res };
        }

        //////////////////////////////////////////////////////////////////////
        public override string ToString()
        {
            var str = new StringBuilder();

            for (var i = 0; i < elts.Count - 1; i++)
            {
                if (elts[i].Precedence() < Precedence()) str.Append("(");
                str.Append(elts[i]);
                if (elts[i].Precedence() < Precedence()) str.Append(")");

                str.Append(" + ");
            }

            if (elts[elts.Count - 1].Precedence() < Precedence()) str.Append("(");
            str.Append(elts[elts.Count - 1]);
            if (elts[elts.Count - 1].Precedence() < Precedence()) str.Append(")");

            return str.ToString();
        }

        public string ObjectPrint()
        {
            var str = new StringBuilder();

            str.Append("Sum(");

            for (var i = 0; i < elts.Count - 1; i++)
            {
                str.Append(elts[i]);
                str.Append(", ");
            }

            str.Append(elts[elts.Count - 1]);
            str.Append(")");

            return str.ToString();
        }

        //public static MathObject operator +(Sum a, MathObject b)
        //{ return new Sum(a, b).Simplify(); }

        public MathObject Substitute(MathObject a, MathObject b)
        {
            return
                new Sum() { elts = elts.ConvertAll(elt => elt.Substitute(a, b)) }
                .Simplify();
        }
    }

    class Difference : MathObject
    {
        public List<MathObject> elts;

        public Difference(params MathObject[] ls)
        { elts = new List<MathObject>(ls); }

        public MathObject Simplify()
        {
            if (elts.Count == 1) return -1 * elts[0];

            if (elts.Count == 2) return elts[0] + -1 * elts[1];

            throw new Exception();
        }
    }

    class Quotient : MathObject
    {
        public List<MathObject> elts;

        public Quotient(params MathObject[] ls)
        { elts = new List<MathObject>(ls); }

        public MathObject Simplify()
        { return elts[0] * (elts[1] ^ -1); }
    }

    class Program
    {
        static void Main(string[] args)
        {
            {
                var x = new Symbol("x");
                var y = new Symbol("y");
                var z = new Symbol("z");

                Func<int, Integer> Int = (n) => new Integer(n);

                Action<Equation> AssertIsTrue = (eq) =>
                {
                    if (!eq) Console.WriteLine(eq.ToString());
                };

                AssertIsTrue(x + x == 2 * x);

                AssertIsTrue(x + x == 2 * x);

                AssertIsTrue(x + x + x == 3 * x);

                AssertIsTrue(5 + x + 2 == 7 + x);

                AssertIsTrue(3 + x + 5 + x == 8 + 2 * x);

                AssertIsTrue(4 * x + 3 * x == 7 * x);

                AssertIsTrue(x + y + z + x + y + z == 2 * x + 2 * y + 2 * z);

                AssertIsTrue(10 - x == 10 + x * -1);

                AssertIsTrue(x * y / 3 == Int(1) / 3 * x * y);

                AssertIsTrue(x / y == x * (y ^ -1));

                AssertIsTrue(x / 3 == x * (Int(1) / 3));

                AssertIsTrue(6 * x * y / 3 == 2 * x * y);

                AssertIsTrue((((x ^ Int(1) / 2) ^ Int(1) / 2) ^ 8) == (x ^ 2));

                AssertIsTrue(((((x * y) ^ (Int(1) / 2)) * (z ^ 2)) ^ 2) == (x * y * (z ^ 4)));

                AssertIsTrue(x / x == Int(1));

                AssertIsTrue(x / y * y / x == Int(1));

                AssertIsTrue((x ^ 2) * (x ^ 3) == (x ^ 5));

                AssertIsTrue(x + y + x + z + 5 + z == 5 + 2 * x + y + 2 * z);

                AssertIsTrue(((Int(1) / 2) * x + (Int(3) / 4) * x) == Int(5) / 4 * x);

                AssertIsTrue(1.2 * x + 3 * x == 4.2 * x);

                AssertIsTrue(3 * x + 1.2 * x == 4.2 * x);

                AssertIsTrue(1.2 * x * 3 * y == 3.5999999999999996 * x * y);

                AssertIsTrue(3 * x * 1.2 * y == 3.5999999999999996 * x * y);

                AssertIsTrue(3.4 * x * 1.2 * y == 4.08 * x * y);

                // Difference

                AssertIsTrue(-x == -1 * x);

                AssertIsTrue(x - y == x + -1 * y);

                // Console.WriteLine((x == Int(4)).ToString());

                // Console.WriteLine((Equation)(x == Int(4)));

                // Console.WriteLine(new Equation() { a = x, b = y });

                // Console.WriteLine((Boolean)new Equation() { a = x, b = y });

                // var val = (Boolean)new Equation() { a = x, b = y };

                //if (new Equation() { a = x, b = x })
                //    Console.WriteLine("Equation to bool");

                //Console.WriteLine((Int(2) == Int(3)).ToString());

                //Console.WriteLine(Int(1) != Int(2));

                //Console.WriteLine((x == y * 5).ToString());

                //Console.WriteLine((x == y).ToString());

                //Console.WriteLine("expr");
                //Console.WriteLine(x == x);
                //Console.WriteLine(x == y);

                //{
                //    var a = new Double(1.0);
                //    var b = new Double(1.0);
                //    var c = a;

                //    Console.WriteLine(a.Equals(a));
                //    Console.WriteLine(a.Equals(b));
                //    Console.WriteLine(a.Equals(c));

                //    Decimal d = 1.10M;
                //    Decimal e = 1.100M;

                //    Console.WriteLine(d);
                //    Console.WriteLine(e);

                //    Console.WriteLine(d == e);

                //}

                AssertIsTrue(Int(10).Substitute(Int(10), Int(20)) == Int(20));
                AssertIsTrue(Int(10).Substitute(Int(15), Int(20)) == Int(10));

                AssertIsTrue(new Double(1.0).Substitute(new Double(1.0), new Double(2.0)) == new Double(2.0));
                AssertIsTrue(new Double(1.0).Substitute(new Double(1.5), new Double(2.0)) == new Double(1.0));

                AssertIsTrue((Int(1) / 2).Substitute(Int(1) / 2, Int(3) / 4) == Int(3) / 4);
                AssertIsTrue((Int(1) / 2).Substitute(Int(1) / 3, Int(3) / 4) == Int(1) / 2);

                AssertIsTrue(x.Substitute(x, y) == y);
                AssertIsTrue(x.Substitute(y, y) == x);

                AssertIsTrue((x ^ y).Substitute(x, Int(10)) == (10 ^ y));
                AssertIsTrue((x ^ y).Substitute(y, Int(10)) == (x ^ 10));

                AssertIsTrue((x ^ y).Substitute(x ^ y, Int(10)) == Int(10));

                AssertIsTrue((x * y * z).Substitute(x, y) == ((y ^ 2) * z));
                AssertIsTrue((x * y * z).Substitute(x * y * z, x) == x);

                AssertIsTrue((x + y + z).Substitute(x, y) == ((y * 2) + z));
                AssertIsTrue((x + y + z).Substitute(x + y + z, x) == x);

                AssertIsTrue((0 ^ x) == Int(0));
                AssertIsTrue((1 ^ x) == Int(1));
                AssertIsTrue((x ^ 0) == Int(1));
                AssertIsTrue((x ^ 1) == x);

                //Console.WriteLine((10 * x * (z + 2) == y).ToString());

                //Console.WriteLine((x + y * 2 * (z + 4) + (x ^ 2) == z).ToString());

                //Console.WriteLine((x + y * 2 * (z + 4) + (x ^ 2) + (x * 3 ^ (2 + z)) == z).ToString());

                Console.WriteLine();

                Console.WriteLine(((((x * y) ^ (Int(1) / 2)) * (z ^ 2)) ^ 2).ToString());

                Console.WriteLine(
                    ((((x * y) ^ (Int(1) / 2)) * (z ^ 2)) ^ 2)
                        .Substitute(x, Int(10))
                        .Substitute(y, Int(20))
                        .Substitute(z, Int(3))
                        );


                //{
                //    var tbl = new Dictionary<Object, int>();

                //    tbl.Add(new Integer(1), 100);

                //    tbl.Add(1, 200);

                //    Console.WriteLine(tbl[new Integer(1)]);
                //    Console.WriteLine(tbl[1]);
                //}

                Func<MathObject, MathObject> sin = arg => new Sin(arg).Simplify();

                Console.WriteLine(sin(x));

                Console.WriteLine(sin(new Double(3.14159 / 2)));

                Console.WriteLine(sin(x + y) + sin(x + y));

                Console.WriteLine(sin(x + x));

                Console.WriteLine(sin(x + x).Substitute(x, Int(1)));

                Console.WriteLine(sin(x + x).Substitute(x, new Double(1.0)));

                {
                    var a = sin(2 * x);

                    var b = a.Substitute(x, y);

                    Console.WriteLine(a);
                    Console.WriteLine(b);
                }

                {
                    var a = new List<MathObject>() { x, y, z };
                    var b = new List<MathObject>() { x, y, z };

                    Console.WriteLine(a == b);

                    Console.WriteLine(a.SequenceEqual(b));

                }

                {
                    var a = x * y;
                    var b = x + y;

                    Console.WriteLine(a == b);

                    // Console.WriteLine(((Product)a).Equals(b));

                    // Console.WriteLine(new Integer(10) == 10);
                }
            }

            Console.ReadLine();
        }
    }
}
