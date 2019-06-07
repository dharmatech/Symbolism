/* Copyright 2013 Eduardo Cavazos

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using static Symbolism.ListConstructor;

namespace Symbolism
{
    public abstract class MathObject
    {
        //////////////////////////////////////////////////////////////////////
        public static implicit operator MathObject(int n) => new Integer(n);

        public static implicit operator MathObject(BigInteger n) => new Integer(n);

        public static implicit operator MathObject(bool val) => new Bool(val);

        public static implicit operator MathObject(double val) => new DoubleFloat(val);
        //////////////////////////////////////////////////////////////////////
        #region overloads for 'int'
        public static MathObject operator +(MathObject a, int b) => a + new Integer(b);
        public static MathObject operator -(MathObject a, int b) => a - new Integer(b);
        public static MathObject operator *(MathObject a, int b) => a * new Integer(b);
        public static MathObject operator /(MathObject a, int b) => a / new Integer(b);
        public static MathObject operator ^(MathObject a, int b) => a ^ new Integer(b);
        public static MathObject operator +(int a, MathObject b) => new Integer(a) + b;
        public static MathObject operator -(int a, MathObject b) => new Integer(a) - b;
        public static MathObject operator *(int a, MathObject b) => new Integer(a) * b;
        public static MathObject operator /(int a, MathObject b) => new Integer(a) / b;
        public static MathObject operator ^(int a, MathObject b) => new Integer(a) ^ b;
        #endregion
        //////////////////////////////////////////////////////////////////////
        #region overloads for 'BigInteger'
        public static MathObject operator +(MathObject a, BigInteger b) => a + new Integer(b);
        public static MathObject operator -(MathObject a, BigInteger b) => a - new Integer(b);
        public static MathObject operator *(MathObject a, BigInteger b) => a * new Integer(b);
        public static MathObject operator /(MathObject a, BigInteger b) => a / new Integer(b);
        public static MathObject operator ^(MathObject a, BigInteger b) => a ^ new Integer(b);
        public static MathObject operator +(BigInteger a, MathObject b) => new Integer(a) + b;
        public static MathObject operator -(BigInteger a, MathObject b) => new Integer(a) - b;
        public static MathObject operator *(BigInteger a, MathObject b) => new Integer(a) * b;
        public static MathObject operator /(BigInteger a, MathObject b) => new Integer(a) / b;
        public static MathObject operator ^(BigInteger a, MathObject b) => new Integer(a) ^ b;
        #endregion
        //////////////////////////////////////////////////////////////////////
        #region overloads for 'double'

        public static MathObject operator +(MathObject a, double b) => a + new DoubleFloat(b);
        public static MathObject operator -(MathObject a, double b) => a - new DoubleFloat(b);
        public static MathObject operator *(MathObject a, double b) => a * new DoubleFloat(b);
        public static MathObject operator /(MathObject a, double b) => a / new DoubleFloat(b);
        public static MathObject operator ^(MathObject a, double b) => a ^ new DoubleFloat(b);
        public static MathObject operator +(double a, MathObject b) => new DoubleFloat(a) + b;
        public static MathObject operator -(double a, MathObject b) => new DoubleFloat(a) - b;
        public static MathObject operator *(double a, MathObject b) => new DoubleFloat(a) * b;
        public static MathObject operator /(double a, MathObject b) => new DoubleFloat(a) / b;
        public static MathObject operator ^(double a, MathObject b) => new DoubleFloat(a) ^ b;

        #endregion
        //////////////////////////////////////////////////////////////////////
        public static Equation operator ==(MathObject a, MathObject b) => new Equation(a, b);
        public static Equation operator !=(MathObject a, MathObject b) => new Equation(a, b, Equation.Operators.NotEqual);
        public static Equation operator <(MathObject a, MathObject b) => new Equation(a, b, Equation.Operators.LessThan);
        public static Equation operator >(MathObject a, MathObject b) => new Equation(a, b, Equation.Operators.GreaterThan);

        public static Equation operator ==(MathObject a, double b) => new Equation(a, new DoubleFloat(b));
        public static Equation operator ==(double a, MathObject b) => new Equation(new DoubleFloat(a), b);

        public static Equation operator !=(MathObject a, double b) => new Equation(a, new DoubleFloat(b), Equation.Operators.NotEqual);
        public static Equation operator !=(double a, MathObject b) => new Equation(new DoubleFloat(a), b, Equation.Operators.NotEqual);

        public static Equation operator ==(MathObject a, int b) => new Equation(a, new Integer(b));
        public static Equation operator ==(int a, MathObject b) => new Equation(new Integer(a), b);
        public static Equation operator !=(MathObject a, int b) => new Equation(a, new Integer(b), Equation.Operators.NotEqual);
        public static Equation operator !=(int a, MathObject b) => new Equation(new Integer(a), b, Equation.Operators.NotEqual);
        //////////////////////////////////////////////////////////////////////
        public static MathObject operator +(MathObject a, MathObject b) => new Sum(a, b).Simplify();
        public static MathObject operator -(MathObject a, MathObject b) => new Difference(a, b).Simplify();
        public static MathObject operator *(MathObject a, MathObject b) => new Product(a, b).Simplify();
        public static MathObject operator /(MathObject a, MathObject b) => new Quotient(a, b).Simplify();
        public static MathObject operator ^(MathObject a, MathObject b) => new Power(a, b).Simplify();

        public static MathObject operator -(MathObject a) { return new Difference(a).Simplify(); }

        // Precedence is used for printing purposes.
        // Thus, the precedence values below do not necessarily reflect 
        // the C# operator precedence values.
        // For example, in C#, the precedence of ^ is lower than +.
        // But for printing purposes, we'd like ^ to have a 
        // higher precedence than +.

        public int Precedence()
        {
            if (this is Integer) return 1000;
            if (this is DoubleFloat) return 1000;
            if (this is Symbol) return 1000;
            if (this is Function) return 1000;
            if (this is Fraction) return 1000;
            if (this is Power) return 130;
            if (this is Product) return 120;
            if (this is Sum) return 110;

            Console.WriteLine(this.GetType().Name);

            throw new Exception();
        }

        public enum ToStringForms { Full, Standard }

        public static ToStringForms ToStringForm = ToStringForms.Full;

        public virtual string FullForm() => base.ToString();

        public virtual string StandardForm() => FullForm();

        public override string ToString()
        {
            if (ToStringForm == ToStringForms.Full) return FullForm();

            if (ToStringForm == ToStringForms.Standard) return StandardForm();

            throw new Exception();
        }

        public virtual MathObject Numerator() => this;

        public virtual MathObject Denominator() => 1;

        public override bool Equals(object obj)
        { throw new Exception("MathObject.Equals called - abstract class"); }

        public override int GetHashCode() => base.GetHashCode();
    }

    public class Equation : MathObject
    {
        public enum Operators { Equal, NotEqual, LessThan, GreaterThan }

        public readonly MathObject a;
        public readonly MathObject b;

        public Operators Operator;

        public Equation(MathObject x, MathObject y)
        { a = x; b = y; Operator = Operators.Equal; }

        public Equation(MathObject x, MathObject y, Operators op)
        { a = x; b = y; Operator = op; }

        public override string FullForm()
        {
            if (Operator == Operators.Equal) return a + " == " + b;
            if (Operator == Operators.NotEqual) return a + " != " + b;
            if (Operator == Operators.LessThan) return a + " < " + b;
            if (Operator == Operators.GreaterThan) return a + " > " + b;
            throw new Exception();
        }

        public override bool Equals(object obj) =>
            obj is Equation &&
            a.Equals((obj as Equation).a) &&
            b.Equals((obj as Equation).b) &&
            Operator == (obj as Equation).Operator;

        Boolean ToBoolean()
        {
            if (a is Bool && b is Bool) return (a as Bool).Equals(b);

            if (a is Equation && b is Equation) return (a as Equation).Equals(b);

            if (a is Integer && b is Integer) return ((Integer)a).Equals(b);
            if (a is DoubleFloat && b is DoubleFloat) return ((DoubleFloat)a).Equals(b);
            if (a is Symbol && b is Symbol) return ((Symbol)a).Equals(b);
            if (a is Sum && b is Sum) return ((Sum)a).Equals(b);
            if (a is Product && b is Product) return ((Product)a).Equals(b);
            if (a is Fraction && b is Fraction) return ((Fraction)a).Equals(b);
            if (a is Power && b is Power) return ((Power)a).Equals(b);
            if (a is Function && b is Function) return ((Function)a).Equals(b);

            if ((((object)a) == null) && (((object)b) == null)) return true;

            if (((object)a) == null) return false;

            if (((object)b) == null) return false;

            if (a.GetType() != b.GetType()) return false;

            Console.WriteLine("" + a.GetType() + " " + b.GetType());

            throw new Exception();
        }

        public static implicit operator Boolean(Equation eq)
        {
            if (eq.Operator == Operators.Equal)
                return (eq.a == eq.b).ToBoolean();

            if (eq.Operator == Operators.NotEqual)
                return !((eq.a == eq.b).ToBoolean());

            if (eq.Operator == Operators.LessThan)
                if (eq.a is Number && eq.b is Number)
                    return (eq.a as Number).ToDouble().val < (eq.b as Number).ToDouble().val;

            if (eq.Operator == Operators.GreaterThan)
                if (eq.a is Number && eq.b is Number)
                    return (eq.a as Number).ToDouble().val > (eq.b as Number).ToDouble().val;

            throw new Exception();
        }

        public MathObject Simplify()
        {
            if (a is Number && b is Number) return (bool)this;

            return this;
        }

        public override int GetHashCode() => new { a, b }.GetHashCode();

    }

    public class Bool : MathObject
    {
        public readonly bool val;

        public Bool(bool b) { val = b; }

        public override string FullForm() => val.ToString();

        public override bool Equals(object obj) => val == (obj as Bool)?.val;

        public override int GetHashCode() => val.GetHashCode();
    }

    //public class NotEqual
    //{
    //    public MathObject a;
    //    public MathObject b;

    //    public NotEqual(MathObject x, MathObject y)
    //    { a = x; b = y; }

    //    public static implicit operator Boolean(NotEqual eq)
    //    { return !((eq.a == eq.b).ToBoolean()); }
    //}

    public abstract class Number : MathObject
    {
        public abstract DoubleFloat ToDouble();
    }

    public class Integer : Number
    {
        public readonly BigInteger val;

        public Integer(int n) { val = n; }

        public Integer(BigInteger n) { val = n; }
                        
        public static implicit operator Integer(BigInteger n) => new Integer(n);

        // public static MathObject operator *(MathObject a, MathObject b) => new Product(a, b).Simplify();

        public static Integer operator +(Integer a, Integer b) => a.val + b.val;
        public static Integer operator -(Integer a, Integer b) => a.val - b.val;
        public static Integer operator *(Integer a, Integer b) => a.val * b.val;

        public override string FullForm() => val.ToString();

        public override bool Equals(object obj) => val == (obj as Integer)?.val;

        public override int GetHashCode() => val.GetHashCode();

        public override DoubleFloat ToDouble() => new DoubleFloat((double)val);
    }

    public class DoubleFloat : Number
    {
        public static double? tolerance;

        public readonly double val;

        public DoubleFloat(double n) { val = n; }

        public override string FullForm() => val.ToString("R");

        //public bool EqualWithinTolerance(DoubleFloat obj)
        //{
        //    if (tolerance.HasValue)
        //        return Math.Abs(val - obj.val) < tolerance;

        //    throw new Exception();
        //}

        public override bool Equals(object obj)
        {
            if (obj is DoubleFloat && tolerance.HasValue)
                return Math.Abs(val - (obj as DoubleFloat).val) < tolerance;

            if (obj is DoubleFloat) return val == ((DoubleFloat)obj).val;

            return false;
        }

        public override int GetHashCode() => val.GetHashCode();

        public override DoubleFloat ToDouble() => this;
    }

    public class Fraction : Number
    {
        public readonly Integer numerator;
        public readonly Integer denominator;

        public Fraction(Integer a, Integer b)
        { numerator = a; denominator = b; }

        public override string FullForm() => numerator + "/" + denominator;

        public override DoubleFloat ToDouble() => new DoubleFloat((double)numerator.val / (double)denominator.val);
        //////////////////////////////////////////////////////////////////////

        public override bool Equals(object obj) =>
            numerator == (obj as Fraction)?.numerator
            &&
            denominator == (obj as Fraction)?.denominator;

        public override int GetHashCode() => new { numerator, denominator }.GetHashCode();

        public override MathObject Numerator() => numerator;

        public override MathObject Denominator() => denominator;
    }

    public static class Rational
    {
        static BigInteger Div(BigInteger a, BigInteger b)
        { BigInteger rem; return BigInteger.DivRem(a, b, out rem); }
                
        static BigInteger Rem(BigInteger a, BigInteger b)
        { BigInteger rem; BigInteger.DivRem(a, b, out rem); return rem; }
                
        static BigInteger Gcd(BigInteger a, BigInteger b)
        {
            BigInteger r;
            while (b != 0)
            {
                r = Rem(a, b);
                a = b;
                b = r;
            }
            return BigInteger.Abs(a);
        }

        public static MathObject SimplifyRationalNumber(MathObject u)
        {
            if (u is Integer) return u;

            if (u is Fraction)
            {
                var u_ = (Fraction)u;
                var n = u_.numerator.val;
                var d = u_.denominator.val;
                                
                if (Rem(n, d) == 0) return Div(n, d);

                var g = Gcd(n, d);
                                
                if (d > 0) return new Fraction(Div(n, g), Div(d, g));
                                
                if (d < 0) return new Fraction(Div(-n, g), Div(-d, g));
            }

            throw new Exception();
        }

        public static Integer Numerator(MathObject u)
        {
            // (a / b) / (c / d)
            // (a / b) * (d / c)
            // (a * d) / (b * c)

            if (u is Integer) return (Integer)u;

            if (u is Fraction)
            {
                var u_ = u as Fraction;

                //return
                //    Numerator(u_.numerator).val
                //    *
                //    Denominator(u_.denominator).val;

                return
                    Numerator(u_.numerator)
                    *
                    Denominator(u_.denominator);
            }

            throw new Exception();
        }

        public static Integer Denominator(MathObject u)
        {
            // (a / b) / (c / d)
            // (a / b) * (d / c)
            // (a * d) / (b * c)

            if (u is Integer) return new Integer(1);

            if (u is Fraction)
            {
                var u_ = u as Fraction;

                return
                    Denominator(u_.numerator)
                    *
                    Numerator(u_.denominator);
            }

            throw new Exception();
        }

        public static Fraction EvaluateSum(MathObject v, MathObject w) =>        

            // a / b + c / d
            // a d / b d + c b / b d
            // (a d + c b) / (b d)

            new Fraction(
                Numerator(v) * Denominator(w) + Numerator(w) * Denominator(v),
                Denominator(v) * Denominator(w));
        
        public static Fraction EvaluateDifference(MathObject v, MathObject w) =>
            new Fraction(
                Numerator(v) * Denominator(w) - Numerator(w) * Denominator(v),
                Denominator(v) * Denominator(w));

        public static Fraction EvaluateProduct(MathObject v, MathObject w) => 
            new Fraction(
                Numerator(v) * Numerator(w),
                Denominator(v) * Denominator(w));

        public static MathObject EvaluateQuotient(MathObject v, MathObject w)
        {
            if (Numerator(w).val == 0) return new Undefined();

            return
                new Fraction(
                    Numerator(v) * Denominator(w),
                    Numerator(w) * Denominator(v));
        }

        public static MathObject EvaluatePower(MathObject v, BigInteger n)
        {
            if (Numerator(v).val != 0)
            {
                if (n > 0) return EvaluateProduct(EvaluatePower(v, n - 1), v);

                if (n == 0) return 1;
                
                if (n == -1) return new Fraction(Denominator(v), Numerator(v));

                if (n < -1)
                {
                    var s = new Fraction(Denominator(v), Numerator(v));

                    return EvaluatePower(s, -n);
                }
            }
                        
            if (n >= 1) return 0;
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
                                
                return EvaluateProduct(-1, v);
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
        public readonly String name;

        public Symbol(String str) { name = str; }

        public override string FullForm() => name;

        public override int GetHashCode() => name.GetHashCode();

        public override bool Equals(Object obj) =>
            obj is Symbol ? name == (obj as Symbol).name : false;
    }

    public static class ListConstructor
    {
        public static List<T> List<T>(params T[] items) => new List<T>(items);
    }

    public static class ListUtils
    {
        public static bool IsEmpty(this List<MathObject> obj) => obj.Count == 0;

        public static List<MathObject> Cons(this List<MathObject> obj, MathObject elt)
        {
            var res = new List<MathObject>(obj);
            res.Insert(0, elt);
            return res;
        }

        public static List<MathObject> Cdr(this List<MathObject> obj) => obj.GetRange(1, obj.Count - 1);

        public static bool equal(List<MathObject> a, List<MathObject> b)
        {
            if (a.Count == 0 && b.Count == 0) return true;

            if (a.Count == 0) return false;

            if (b.Count == 0) return false;

            if (a[0] == b[0]) return equal(a.Cdr(), b.Cdr());

            return false;
        }
    }

    public class Function : MathObject
    {
        public delegate MathObject Proc(params MathObject[] ls);

        public readonly String name;

        public readonly Proc proc;

        public readonly List<MathObject> args;
        
        public Function(string name, Proc proc, IEnumerable<MathObject> args)
        {
            this.name = name;
            this.proc = proc;
            this.args = new List<MathObject>(args);
        }
                
        public override bool Equals(object obj) =>
            GetType() == obj.GetType() &&
            name == (obj as Function).name &&
            ListUtils.equal(args, ((Function)obj).args);

        public MathObject Simplify() => proc == null ? this : proc(args.ToArray());

        public override string FullForm() => $"{name}({string.Join(", ", args)})";

        public MathObject Clone() => MemberwiseClone() as MathObject;

        public override int GetHashCode() => new { name, args }.GetHashCode();
    }

    public static class FunctionExtensions
    {
        //public static MathObject Map<T>(this T obj, Func<MathObject, MathObject> proc) where T : Function, new()
        //{
        //    // return new T() { args = obj.args.Select(proc).ToList() }.Simplify();

        //    // return 
            
        //}
    }

    public class And : Function
    {
        static MathObject AndProc(MathObject[] ls)
        {
            if (ls.Count() == 0) return true;

            if (ls.Count() == 1) return ls.First();

            if (ls.Any(elt => elt == false)) return false;

            if (ls.Any(elt => elt == true))
                return new And(ls.Where(elt => elt != true).ToArray()).Simplify();

            if (ls.Any(elt => elt is And))
            {
                var and = new And();

                foreach (var elt in ls)
                {
                    if (elt is And) and.args.AddRange((elt as And).args);

                    else and.args.Add(elt);
                }

                return and.Simplify();
            }
                        
            return new And(ls);
        }
                
        public And(params MathObject[] ls) : base("and", AndProc, ls) { }

        public And() : base("and", AndProc, new List<MathObject>()) { }

        public static And FromRange(IEnumerable<MathObject> ls) => new And(ls.ToArray());

        public MathObject Add(MathObject obj)
        {
            var ls = new List<MathObject>(args);

            ls.Add(obj);

            return And.FromRange(ls).Simplify();
        }

        public MathObject Map(Func<MathObject, MathObject> proc) => new And(args.Select(proc).ToArray()).Simplify();
    }

    public class Or : Function
    {
        static MathObject OrProc(params MathObject[] ls)
        {
            if (ls.Count() == 1) return ls.First();

            // 10 || false || 20   ->   10 || 20

            if (ls.Any(elt => elt == false))
                return new Or(ls.Where(elt => elt != false).ToArray()).Simplify();

            if (ls.Any(elt => (elt is Bool) && (elt as Bool).val)) return new Bool(true);

            if (ls.All(elt => (elt is Bool) && (elt as Bool).val == false)) return new Bool(false);

            if (ls.Any(elt => elt is Or))
            {
                var or = new Or();

                foreach (var elt in ls)
                {
                    if (elt is Or) or.args.AddRange((elt as Or).args);
                    else or.args.Add(elt);
                }

                return or.Simplify();
            }
                        
            return new Or(ls);
        }
                
        public Or(params MathObject[] ls) : base("or", OrProc, ls) { }

        public Or() : base("or", OrProc, new List<MathObject>()) { }

        public MathObject Map(Func<MathObject, MathObject> proc) => new Or(args.Select(proc).ToArray()).Simplify();
    }

    public static class OrderRelation
    {
        public static MathObject Base(MathObject u) => u is Power ? (u as Power).bas : u;

        public static MathObject Exponent(MathObject u) => u is Power ? (u as Power).exp : 1;

        public static MathObject Term(this MathObject u)
        {
            if (u is Product && ((Product)u).elts[0] is Number)
                return new Product() { elts = ((Product)u).elts.Cdr() };

            if (u is Product) return u;

            return new Product(u);
        }

        public static MathObject Const(this MathObject u) =>
            (u is Product && (u as Product).elts[0] is Number) ? (u as Product).elts[0] : 1;

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
            if (u is DoubleFloat && v is DoubleFloat) return ((DoubleFloat)u).val < ((DoubleFloat)v).val;

            // if (u is DoubleFloat && v is Integer) return ((DoubleFloat)u).val < ((Integer)v).val;

            if (u is DoubleFloat && v is Integer) return ((DoubleFloat)u).val < ((double)((Integer)v).val);

            if (u is DoubleFloat && v is Fraction) return
                ((DoubleFloat)u).val < ((double)((Fraction)v).numerator.val) / ((double)((Fraction)v).denominator.val);

            if (u is Integer && v is DoubleFloat) return ((double)((Integer)u).val) < ((DoubleFloat)v).val;

            if (u is Fraction && v is DoubleFloat) return
                ((double)((Fraction)u).numerator.val) / ((double)((Fraction)u).denominator.val) < ((DoubleFloat)v).val;

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
                return O3(
                    (u as Sum).elts.Reverse<MathObject>().ToList(),
                    (v as Sum).elts.Reverse<MathObject>().ToList());

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

            if (u is Number && !(v is Number)) return true;

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

    public class Power : MathObject
    {
        public readonly MathObject bas;
        public readonly MathObject exp;

        public Power(MathObject a, MathObject b) { bas = a; exp = b; }

        public override string FullForm() =>
            string.Format("{0} ^ {1}",
                bas.Precedence() < Precedence() ? $"({bas})" : $"{bas}",
                exp.Precedence() < Precedence() ? $"({exp})" : $"{exp}");

        public override string StandardForm()
        {
            // x ^ 1/2   ->   sqrt(x)

            if (exp == new Integer(1) / new Integer(2)) return $"sqrt({bas})";

            return string.Format("{0} ^ {1}",
                bas.Precedence() < Precedence() ? $"({bas})" : $"{bas}",
                exp.Precedence() < Precedence() ? $"({exp})" : $"{exp}");
        }

        public override bool Equals(object obj) =>
            obj is Power && bas == (obj as Power).bas && exp == (obj as Power).exp;

        public MathObject Simplify()
        {
            var v = bas;
            var w = exp;

            if (v == 0) return 0;
            if (v == 1) return 1;
            if (w == 0) return 1;
            if (w == 1) return v;

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

            if (v is DoubleFloat && w is Integer)
                return new DoubleFloat(Math.Pow(((DoubleFloat)v).val, (double) ((Integer)w).val));

            if (v is DoubleFloat && w is Fraction)
                return new DoubleFloat(Math.Pow(((DoubleFloat)v).val, ((Fraction)w).ToDouble().val));

            if (v is Integer && w is DoubleFloat)
                return new DoubleFloat(Math.Pow((double)((Integer)v).val, ((DoubleFloat)w).val));

            if (v is Fraction && w is DoubleFloat)
                return new DoubleFloat(Math.Pow(((Fraction)v).ToDouble().val, ((DoubleFloat)w).val));

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

        public override MathObject Numerator()
        {
            if (exp is Integer && exp < 0) return 1;

            if (exp is Fraction && exp < 0) return 1;

            return this;
        }

        public override MathObject Denominator()
        {
            if (exp is Integer && exp < 0) return this ^ -1;

            if (exp is Fraction && exp < 0) return this ^ -1;

            return 1;
        }

        public override int GetHashCode() => new { bas, exp }.GetHashCode();
    }

    public class Product : MathObject
    {
        public List<MathObject> elts;

        public Product(params MathObject[] ls)
        { elts = new List<MathObject>(ls); }

        public override string FullForm() =>
            string.Join(" * ", elts.ConvertAll(elt => elt.Precedence() < Precedence() ? $"({elt})" : $"{elt}"));

        public override string StandardForm()
        {
            if (this.Denominator() == 1)
            {
                if (this.Const() < 0 && this / this.Const() is Sum) return $"-({this * -1})";

                if (this.Const() < 0) return $"-{this * -1}";

                return string.Join(" * ",
                    elts.ConvertAll(elt => elt.Precedence() < Precedence() || (elt is Power && (elt as Power).exp != new Integer(1) / 2) ? $"({elt})" : $"{elt}"));
            }

            var expr_a = this.Numerator();
            var expr_b = this.Denominator();

            var expr_a_ = expr_a is Sum || (expr_a is Power && (expr_a as Power).exp != new Integer(1) / 2) ? $"({expr_a})" : $"{expr_a}";

            var expr_b_ = expr_b is Sum || expr_b is Product || (expr_b is Power && (expr_b as Power).exp != new Integer(1) / 2) ? $"({expr_b})" : $"{expr_b}";

            return $"{expr_a_} / {expr_b_}";
        }

        public override int GetHashCode() => elts.GetHashCode();

        public override bool Equals(object obj) =>
            obj is Product && ListUtils.equal(elts, (obj as Product).elts);

        static List<MathObject> MergeProducts(List<MathObject> pElts, List<MathObject> qElts)
        {
            if (pElts.Count == 0) return qElts;
            if (qElts.Count == 0) return pElts;

            var p = pElts[0];
            var ps = pElts.Cdr();

            var q = qElts[0];
            var qs = qElts.Cdr();

            var res = RecursiveSimplify(List(p, q));

            if (res.Count == 0) return MergeProducts(ps, qs);

            if (res.Count == 1) return MergeProducts(ps, qs).Cons(res[0]);

            if (ListUtils.equal(res, List(p, q))) return MergeProducts(ps, qElts).Cons(p);

            if (ListUtils.equal(res, List(q, p))) return MergeProducts(pElts, qs).Cons(q);

            throw new Exception();
        }

        static List<MathObject> SimplifyDoubleNumberProduct(DoubleFloat a, Number b)
        {
            double val = 0.0;

            if (b is DoubleFloat) val = a.val * ((DoubleFloat)b).val;

            if (b is Integer) val = a.val * (double)((Integer)b).val;

            if (b is Fraction) val = a.val * ((Fraction)b).ToDouble().val;

            if (val == 1.0) return new List<MathObject>() { };

            return List<MathObject>(new DoubleFloat(val));
        }

        public static List<MathObject> RecursiveSimplify(List<MathObject> elts)
        {
            if (elts.Count == 2)
            {
                if (elts[0] is Product && elts[1] is Product)
                    return MergeProducts(
                        ((Product)elts[0]).elts,
                        ((Product)elts[1]).elts);

                if (elts[0] is Product) return MergeProducts(((Product)elts[0]).elts, List(elts[1]));

                if (elts[1] is Product) return MergeProducts(List(elts[0]), ((Product)elts[1]).elts);

                //////////////////////////////////////////////////////////////////////

                if (elts[0] is DoubleFloat && elts[1] is Number)
                    return SimplifyDoubleNumberProduct((DoubleFloat)elts[0], (Number)elts[1]);

                if (elts[0] is Number && elts[1] is DoubleFloat)
                    return SimplifyDoubleNumberProduct((DoubleFloat)elts[1], (Number)elts[0]);

                //////////////////////////////////////////////////////////////////////

                if ((elts[0] is Integer || elts[0] is Fraction)
                    &&
                    (elts[1] is Integer || elts[1] is Fraction))
                {
                    var P = Rational.SimplifyRNE(new Product(elts[0], elts[1]));

                    if (P == 1) return new List<MathObject>() { };

                    return List(P);
                }

                if (elts[0] == 1) return List(elts[1]);
                if (elts[1] == 1) return List(elts[0]);

                var p = elts[0];
                var q = elts[1];

                if (OrderRelation.Base(p) == OrderRelation.Base(q))
                {
                    var res = OrderRelation.Base(p) ^ (OrderRelation.Exponent(p) + OrderRelation.Exponent(q));

                    if (res == 1) return new List<MathObject>() { };

                    return List(res);
                }

                if (OrderRelation.Compare(q, p)) return List(q, p);

                return List(p, q);
            }

            if (elts[0] is Product)
                return
                    MergeProducts(
                        ((Product)elts[0]).elts,
                        RecursiveSimplify(elts.Cdr()));

            return MergeProducts(
                List(elts[0]),
                RecursiveSimplify(elts.Cdr()));

            throw new Exception();
        }

        public MathObject Simplify()
        {
            if (elts.Count == 1) return elts[0];

            if (elts.Any(elt => elt == 0)) return 0;

            var res = RecursiveSimplify(elts);

            if (res.IsEmpty()) return 1;

            if (res.Count == 1) return res[0];

            // Without the below, the following throws an exception:
            // sqrt(a * b) * (sqrt(a * b) / a) / c

            if (res.Any(elt => elt is Product)) return new Product() { elts = res }.Simplify();

            return new Product() { elts = res };
        }

        public override MathObject Numerator() =>
            new Product() { elts = elts.Select(elt => elt.Numerator()).ToList() }.Simplify();

        public override MathObject Denominator() =>
            new Product() { elts = elts.Select(elt => elt.Denominator()).ToList() }.Simplify();
    }

    public class Sum : MathObject
    {
        public List<MathObject> elts;

        public Sum(params MathObject[] ls) { elts = new List<MathObject>(ls); }

        public override int GetHashCode() => elts.GetHashCode();

        public override bool Equals(object obj) =>
            obj is Sum && ListUtils.equal(elts, (obj as Sum).elts);

        static List<MathObject> MergeSums(List<MathObject> pElts, List<MathObject> qElts)
        {
            if (pElts.Count == 0) return qElts;
            if (qElts.Count == 0) return pElts;

            var p = pElts[0];
            var ps = pElts.Cdr();

            var q = qElts[0];
            var qs = qElts.Cdr();

            var res = RecursiveSimplify(List(p, q));

            if (res.Count == 0) return MergeSums(ps, qs);

            if (res.Count == 1) return MergeSums(ps, qs).Cons(res[0]);

            if (ListUtils.equal(res, List(p, q))) return MergeSums(ps, qElts).Cons(p);

            if (ListUtils.equal(res, List(q, p))) return MergeSums(pElts, qs).Cons(q);

            throw new Exception();
        }

        static List<MathObject> SimplifyDoubleNumberSum(DoubleFloat a, Number b)
        {
            double val = 0.0;

            if (b is DoubleFloat) val = a.val + ((DoubleFloat)b).val;

            if (b is Integer) val = a.val + (double)((Integer)b).val;

            if (b is Fraction) val = a.val + ((Fraction)b).ToDouble().val;

            if (val == 0.0) return new List<MathObject>() { };

            return new List<MathObject>() { new DoubleFloat(val) };
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
                        List(elts[1]));

                if (elts[1] is Sum)
                    return MergeSums(
                        List(elts[0]),
                        ((Sum)elts[1]).elts);

                //////////////////////////////////////////////////////////////////////

                if (elts[0] is DoubleFloat && elts[1] is Number)
                    return SimplifyDoubleNumberSum((DoubleFloat)elts[0], (Number)elts[1]);

                if (elts[0] is Number && elts[1] is DoubleFloat)
                    return SimplifyDoubleNumberSum((DoubleFloat)elts[1], (Number)elts[0]);

                //////////////////////////////////////////////////////////////////////

                if ((elts[0] is Integer || elts[0] is Fraction)
                    &&
                    (elts[1] is Integer || elts[1] is Fraction))
                {
                    var P = Rational.SimplifyRNE(new Sum(elts[0], elts[1]));

                    if (P == 0) return new List<MathObject>() { };

                    return List(P);
                }

                if (elts[0] == 0) return List(elts[1]);

                if (elts[1] == 0) return List(elts[0]);

                var p = elts[0];
                var q = elts[1];

                if (p.Term() == q.Term())
                {
                    var res = p.Term() * (p.Const() + q.Const());

                    if (res == 0) return new List<MathObject>() { };

                    return List(res);
                }

                if (OrderRelation.Compare(q, p)) return List(q, p);

                return List(p, q);
            }

            if (elts[0] is Sum)
                return
                    MergeSums(
                        ((Sum)elts[0]).elts, RecursiveSimplify(elts.Cdr()));

            return MergeSums(
                List(elts[0]), RecursiveSimplify(elts.Cdr()));
        }

        public MathObject Simplify()
        {
            if (elts.Count == 1) return elts[0];

            var res = RecursiveSimplify(elts);

            if (res.Count == 0) return 0;
            if (res.Count == 1) return res[0];

            return new Sum() { elts = res };
        }

        public override string FullForm() =>
            String.Join(" + ", elts.ConvertAll(elt => elt.Precedence() < Precedence() ? $"({elt})" : $"{elt}"));

        public override string StandardForm()
        {
            var result = string.Join(" ",
                elts
                    .ConvertAll(elt =>
                    {
                        var elt_ = elt.Const() < 0 ? elt * -1 : elt;

                        var elt__ = elt.Const() < 0 && elt_ is Sum || (elt is Power && (elt as Power).exp != new Integer(1) / 2) ? $"({elt_})" : $"{elt_}";

                        return elt.Const() < 0 ? $"- {elt__}" : $"+ {elt__}";
                    }));

            if (result.StartsWith("+ ")) return result.Remove(0, 2); // "+ x + y"   ->   "x + y"

            if (result.StartsWith("- ")) return result.Remove(1, 1); // "- x + y"   ->   "-x + y"

            return result;
        }
    }

    class Difference : MathObject
    {
        public readonly List<MathObject> elts;

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
        public readonly List<MathObject> elts;

        public Quotient(params MathObject[] ls)
        { elts = new List<MathObject>(ls); }

        public MathObject Simplify() => elts[0] * (elts[1] ^ -1);
    }

    public static class Constructors
    {
        public static MathObject sqrt(MathObject obj) => obj ^ (new Integer(1) / new Integer(2));

        public static MathObject and(params MathObject[] ls) => new And(ls).Simplify();

        public static MathObject or(params MathObject[] ls) => new Or(ls).Simplify();
    }
}
