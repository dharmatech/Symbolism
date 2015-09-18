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
using System.Text;

using Symbolism;
using Utils;

using Symbolism.Trigonometric;

using Symbolism.Has;
using Symbolism.Substitute;

using Symbolism.Denominator;

using Symbolism.LogicalExpand;
using Symbolism.SimplifyEquation;
using Symbolism.SimplifyLogical;

using Symbolism.DegreeGpe;
using Symbolism.CoefficientGpe;
using Symbolism.AlgebraicExpand;
using Symbolism.IsolateVariable;
using Symbolism.EliminateVariable;
using Symbolism.DeepSelect;

using Symbolism.RationalizeExpression;

using static Symbolism.Constructors;

using static Symbolism.Trigonometric.Constructors;

namespace Tests
{
    public static class Extensions
    {
        public static MathObject AssertEqTo(this MathObject a, MathObject b)
        {
            if (!(a == b)) Console.WriteLine((a == b).ToString());

            return a;
        }

        public static MathObject AssertEqToDouble(this MathObject a, MathObject b, double tolerance = 0.000001)
        {
            if (
                Math.Abs(
                    ((a as Equation).b as DoubleFloat).val
                    -
                    ((b as Equation).b as DoubleFloat).val)
                > tolerance)
            {
                Console.WriteLine("{0} and {1} are not equal", a, b);
            }

            return a;
        }

        public static MathObject DispLong(this MathObject obj, int indent = 0)
        {
            if (obj is Or || obj is And)
            {
                Console.WriteLine(new String(' ', indent) + (obj as Function).name + "(");

                foreach (var elt in (obj as Function).args) elt.DispLong(indent + 2);
                
                Console.WriteLine(new String(' ', indent) + ")");
            }

            else
            {
                Console.WriteLine(new String(' ', indent) + obj);
            }

            return obj;
        }

        public static MathObject MultiplyBothSidesBy(this MathObject obj, MathObject item)
        {
            if (obj is Equation)
                return (obj as Equation).a * item == (obj as Equation).b * item;

            throw new Exception();
        }

        public static MathObject AddToBothSides(this MathObject obj, MathObject item)
        {
            if (obj is Equation)
                return (obj as Equation).a + item == (obj as Equation).b + item;

            throw new Exception();
        }


    }

    public class Obj2
    {
        public Symbol ΣFx;
        public Symbol ΣFy;
        public Symbol m;
        public Symbol ax;
        public Symbol ay;

        public Symbol F1, F2;
        public Symbol th1, th2;
        public Symbol F1x, F2x;
        public Symbol F1y, F2y;

        public Obj2(string name)
        {
            ΣFx = new Symbol($"{name}.ΣFx");
            ΣFy = new Symbol($"{name}.ΣFy");

            m = new Symbol($"{name}.m");

            ax = new Symbol($"{name}.ax");
            ay = new Symbol($"{name}.ay");

            F1 = new Symbol($"{name}.F1");
            F2 = new Symbol($"{name}.F2");

            th1 = new Symbol($"{name}.th1");
            th2 = new Symbol($"{name}.th2");

            F1x = new Symbol($"{name}.F1x");
            F2x = new Symbol($"{name}.F2x");

            F1y = new Symbol($"{name}.F1y");
            F2y = new Symbol($"{name}.F2y");
        }

        public And Equations()
        {
            return new And(

                F1x == F1 * cos(th1),
                F1y == F1 * sin(th1),

                F2x == F2 * cos(th2),
                F2y == F2 * sin(th2),

                ΣFx == F1x + F2x,
                ΣFx == m * ax,

                ΣFy == F1y + F2y,
                ΣFy == m * ay

                );
        }
    }

    public class Obj3
    {
        public Symbol ΣFx;
        public Symbol ΣFy;
        public Symbol m;
        public Symbol ax;
        public Symbol ay;

        public Symbol F1, F2, F3;
        public Symbol th1, th2, th3;
        public Symbol F1x, F2x, F3x;
        public Symbol F1y, F2y, F3y;

        public Obj3(string name)
        {
            ΣFx = new Symbol($"{name}.ΣFx");
            ΣFy = new Symbol($"{name}.ΣFy");

            m = new Symbol($"{name}.m");

            ax = new Symbol($"{name}.ax");
            ay = new Symbol($"{name}.ay");

            F1 = new Symbol($"{name}.F1");
            F2 = new Symbol($"{name}.F2");
            F3 = new Symbol($"{name}.F3");

            th1 = new Symbol($"{name}.th1");
            th2 = new Symbol($"{name}.th2");
            th3 = new Symbol($"{name}.th3");

            F1x = new Symbol($"{name}.F1x");
            F2x = new Symbol($"{name}.F2x");
            F3x = new Symbol($"{name}.F3x");

            F1y = new Symbol($"{name}.F1y");
            F2y = new Symbol($"{name}.F2y");
            F3y = new Symbol($"{name}.F3y");
        }

        public And Equations()
        {
            return new And(

                F1x == F1 * cos(th1),
                F1y == F1 * sin(th1),

                F2x == F2 * cos(th2),
                F2y == F2 * sin(th2),

                F3x == F3 * cos(th3),
                F3y == F3 * sin(th3),

                ΣFx == F1x + F2x + F3x,
                ΣFx == m * ax,

                ΣFy == F1y + F2y + F3y,
                ΣFy == m * ay

                );
        }
    }

    public class Obj5
    {
        public Symbol ΣFx;
        public Symbol ΣFy;
        public Symbol m;
        public Symbol ax;
        public Symbol ay;

        public Symbol F1, F2, F3, F4, F5;
        public Symbol th1, th2, th3, th4, th5;
        public Symbol F1x, F2x, F3x, F4x, F5x;
        public Symbol F1y, F2y, F3y, F4y, F5y;

        public Obj5(string name)
        {
            ΣFx = new Symbol($"{name}.ΣFx");
            ΣFy = new Symbol($"{name}.ΣFy");

            m = new Symbol($"{name}.m");

            ax = new Symbol($"{name}.ax");
            ay = new Symbol($"{name}.ay");

            F1 = new Symbol($"{name}.F1");
            F2 = new Symbol($"{name}.F2");
            F3 = new Symbol($"{name}.F3");
            F4 = new Symbol($"{name}.F4");
            F5 = new Symbol($"{name}.F5");

            th1 = new Symbol($"{name}.th1");
            th2 = new Symbol($"{name}.th2");
            th3 = new Symbol($"{name}.th3");
            th4 = new Symbol($"{name}.th4");
            th5 = new Symbol($"{name}.th5");

            F1x = new Symbol($"{name}.F1x");
            F2x = new Symbol($"{name}.F2x");
            F3x = new Symbol($"{name}.F3x");
            F4x = new Symbol($"{name}.F4x");
            F5x = new Symbol($"{name}.F5x");

            F1y = new Symbol($"{name}.F1y");
            F2y = new Symbol($"{name}.F2y");
            F3y = new Symbol($"{name}.F3y");
            F4y = new Symbol($"{name}.F4y");
            F5y = new Symbol($"{name}.F5y");
        }

        public And Equations()
        {
            return new And(

                F1x == F1 * cos(th1),
                F1y == F1 * sin(th1),

                F2x == F2 * cos(th2),
                F2y == F2 * sin(th2),

                F3x == F3 * cos(th3),
                F3y == F3 * sin(th3),

                F4x == F4 * cos(th4),
                F4y == F4 * sin(th4),

                F5x == F5 * cos(th5),
                F5y == F5 * sin(th5),

                ΣFx == F1x + F2x + F3x + F4x + F5x,
                ΣFx == m * ax,

                ΣFy == F1y + F2y + F3y + F4y + F5y,
                ΣFy == m * ay

                );
        }
    }
    
    public class KinematicObjectABC
    {
        public Symbol xA, yA, vxA, vyA, vA, thA;
        public Symbol xB, yB, vxB, vyB, vB, thB;
        public Symbol xC, yC, vxC, vyC, vC, thC;

        public Symbol tAB, tBC, tAC;

        public Symbol ax, ay;

        public KinematicObjectABC(string name)
        {
            xA = new Symbol($"{name}.xA");
            yA = new Symbol($"{name}.yA");

            vxA = new Symbol($"{name}.vxA");
            vyA = new Symbol($"{name}.vyA");

            vA = new Symbol($"{name}.vA");
            thA = new Symbol($"{name}.thA");


            xB = new Symbol($"{name}.xB");
            yB = new Symbol($"{name}.yB");

            vxB = new Symbol($"{name}.vxB");
            vyB = new Symbol($"{name}.vyB");

            vB = new Symbol($"{name}.vB");
            thB = new Symbol($"{name}.thB");


            xC = new Symbol($"{name}.xC");
            yC = new Symbol($"{name}.yC");

            vxC = new Symbol($"{name}.vxC");
            vyC = new Symbol($"{name}.vyC");

            vC = new Symbol($"{name}.vC");
            thC = new Symbol($"{name}.thC");

            tAB = new Symbol($"{name}.tAB");
            tBC = new Symbol($"{name}.tBC");
            tAC = new Symbol($"{name}.tAC");

            ax = new Symbol($"{name}.ax");
            ay = new Symbol($"{name}.ay");
        }

        public And EquationsAB() =>

            new And(

                vxB == vxA + ax * tAB,
                vyB == vyA + ay * tAB,

                xB == xA + vxA * tAB + ax * (tAB ^ 2) / 2,
                yB == yA + vyA * tAB + ay * (tAB ^ 2) / 2
                
                );

        public And EquationsBC() =>

            new And(

                vxC == vxB + ax * tBC,
                vyC == vyB + ay * tBC,

                xC == xB + vxB * tBC + ax * (tBC ^ 2) / 2,
                yC == yB + vyB * tBC + ay * (tBC ^ 2) / 2

                );

        public And EquationsAC() =>

            new And(

                vxC == vxA + ax * tAC,
                vyC == vyA + ay * tAC,

                xC == xA + vxA * tAC + ax * (tAC ^ 2) / 2,
                yC == yA + vyA * tAC + ay * (tAC ^ 2) / 2

                );

        public And TrigEquationsA() =>
        
            new And(

                vxA == vA * cos(thA),
                vyA == vA * sin(thA)

                );
        
    }
    
    class Program
    {
        static void AssertEqual(DoubleFloat a, DoubleFloat b, double tolerance = 0.00000001)
        {
            if (Math.Abs(a.val - b.val) > tolerance)
                Console.WriteLine("{0} and {1} are not equal", a.val, b.val);
        }

        static void AssertEqual(MathObject a, Double b, double tolerance = 0.00000001)
        {
            var x = (DoubleFloat)a;
            var y = new DoubleFloat(b);

            if (Math.Abs(x.val - y.val) > tolerance)
                Console.WriteLine("{0} and {1} are not equal", x.val, y.val);
        }

        static void Assert(bool val, string str) { if (!val) Console.WriteLine(str); }

        static List<Equation> Kinematic(Symbol s, Symbol u, Symbol v, Symbol a, Symbol t)
        {
            return new List<Equation>()
            {
                v == u + a * t,
                s == (u + v) * t / 2
            };
        }

        static List<Equation> Kinematic(Symbol sA, Symbol sB, Symbol vA, Symbol vB, Symbol a, Symbol tA, Symbol tB)
        {
            return new List<Equation>()
            {
                vB == vA + a * (tB - tA),
                sB - sA == (vA + vB) * (tB - tA) / 2
            };
        }

        static void Main(string[] args)
        {
            Action<Equation> AssertIsTrue = (eq) =>
            {
                if (!eq) Console.WriteLine(eq.ToString());
            };

            Action<Equation> AssertIsFalse = (eq) =>
            {
                if (eq) Console.WriteLine(eq.ToString());
            };

            {
                var a = new Symbol("a");
                var b = new Symbol("b");
                var c = new Symbol("c");
                var d = new Symbol("d");

                var x = new Symbol("x");
                var y = new Symbol("y");
                var z = new Symbol("z");

                Func<int, Integer> Int = (n) => new Integer(n);

                {
                    DoubleFloat.tolerance = 0.000000001;

                    Assert(new DoubleFloat(1.2).Equals(new DoubleFloat(1.2)), "new DoubleFloat(1.2).Equals(new DoubleFloat(1.2))");

                    Assert(new DoubleFloat(1.20000001).Equals(new DoubleFloat(1.20000002)) == false, "new DoubleFloat(1.20000001).Equals(new DoubleFloat(1.20000002)) == false");

                    Assert(new DoubleFloat(1.2000000000001).Equals(new DoubleFloat(1.200000000002)), "new DoubleFloat(1.2000000000001).Equals(new DoubleFloat(1.200000000002))");

                    Assert(new DoubleFloat(1.2).Equals(new DoubleFloat(1.23)) == false, "new DoubleFloat(1.2).Equals(new DoubleFloat(1.23)) == false");

                    DoubleFloat.tolerance = null;
                }

                #region Simplify

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

                AssertIsTrue(x / x == 1);

                AssertIsTrue(x / y * y / x == 1);

                AssertIsTrue((x ^ 2) * (x ^ 3) == (x ^ 5));

                AssertIsTrue(x + y + x + z + 5 + z == 5 + 2 * x + y + 2 * z);

                AssertIsTrue(((Int(1) / 2) * x + (Int(3) / 4) * x) == Int(5) / 4 * x);

                AssertIsTrue(1.2 * x + 3 * x == 4.2 * x);

                AssertIsTrue(3 * x + 1.2 * x == 4.2 * x);

                AssertIsTrue(1.2 * x * 3 * y == 3.5999999999999996 * x * y);

                AssertIsTrue(3 * x * 1.2 * y == 3.5999999999999996 * x * y);

                AssertIsTrue(3.4 * x * 1.2 * y == 4.08 * x * y);

                AssertIsTrue((a == b) == (a == b));

                #endregion

                #region Power.Simplify

                AssertIsTrue((0 ^ x) == 0);
                AssertIsTrue((1 ^ x) == 1);
                AssertIsTrue((x ^ 0) == 1);
                AssertIsTrue((x ^ 1) == x);

                #endregion

                // Product.Simplify

                AssertIsTrue(x * 0 == 0);

                // Difference

                AssertIsTrue(-x == -1 * x);

                AssertIsTrue(x - y == x + -1 * y);

                #region Substitute

                AssertIsTrue(Int(10).Substitute(Int(10), 20) == 20);
                AssertIsTrue(Int(10).Substitute(Int(15), 20) == 10);

                AssertIsTrue(new DoubleFloat(1.0).Substitute(new DoubleFloat(1.0), 2.0) == 2.0);
                AssertIsTrue(new DoubleFloat(1.0).Substitute(new DoubleFloat(1.5), 2.0) == 1.0);

                AssertIsTrue((Int(1) / 2).Substitute(Int(1) / 2, Int(3) / 4) == Int(3) / 4);
                AssertIsTrue((Int(1) / 2).Substitute(Int(1) / 3, Int(3) / 4) == Int(1) / 2);

                AssertIsTrue(x.Substitute(x, y) == y);
                AssertIsTrue(x.Substitute(y, y) == x);

                AssertIsTrue((x ^ y).Substitute(x, 10) == (10 ^ y));
                AssertIsTrue((x ^ y).Substitute(y, 10) == (x ^ 10));

                AssertIsTrue((x ^ y).Substitute(x ^ y, 10) == 10);

                AssertIsTrue((x * y * z).Substitute(x, y) == ((y ^ 2) * z));
                AssertIsTrue((x * y * z).Substitute(x * y * z, x) == x);

                AssertIsTrue((x + y + z).Substitute(x, y) == ((y * 2) + z));
                AssertIsTrue((x + y + z).Substitute(x + y + z, x) == x);

                AssertIsTrue(
                    ((((x * y) ^ (Int(1) / 2)) * (z ^ 2)) ^ 2)
                        .Substitute(x, 10)
                        .Substitute(y, 20)
                        .Substitute(z, 3)
                        == 16200
                        );

                #region Equation.Substitute

                AssertIsTrue((x == y).Substitute(y, z) == (x == z));

                AssertIsTrue((x != y).Substitute(y, z) == (x != z));

                (x == 0).Substitute(x, 0).AssertEqTo(true);

                (x == 0).Substitute(x, 1).AssertEqTo(false);

                (x != 0).Substitute(x, 0).AssertEqTo(false);

                (x != 0).Substitute(x, 1).AssertEqTo(true);

                #endregion

                #endregion

                AssertIsTrue(sin(new DoubleFloat(3.14159 / 2)) == 0.99999999999911982);

                AssertIsTrue(sin(x + y) + sin(x + y) == 2 * sin(x + y));

                AssertIsTrue(sin(x + x) == sin(2 * x));

                AssertIsTrue(sin(x + x).Substitute(x, 1) == sin(Int(2)));

                AssertIsTrue(sin(x + x).Substitute(x, 1.0) == 0.90929742682568171);

                AssertIsTrue(sin(2 * x).Substitute(x, y) == sin(2 * y));

                // Product.RecursiveSimplify

                AssertIsTrue(1 * x == x);

                AssertIsTrue(x * 1 == x);

                AssertIsTrue(x != y);

                AssertIsTrue(x != 10);

                // ==(double a, MathObject b)

                AssertIsTrue(1.0 == new DoubleFloat(3.0) - 2.0);

                AssertIsTrue((a == b) != (a != b));
                
                (sqrt(a * b) * (sqrt(a * b) / a) / c)
                    .AssertEqTo(b / c);
                
                #region Equation.ToString

                Assert((x == y).ToString() == "x == y", "x == y");

                Assert((x != y).ToString() == "x != y", "x != y");

                #endregion

                #region Function.ToString

                Assert(new And().ToString() == "And()", "And()");

                #endregion

                #region Equation.Simplify

                (new Integer(0) == new Integer(0)).Simplify().AssertEqTo(true);

                (new Integer(0) == new Integer(1)).Simplify().AssertEqTo(false);

                (new Integer(0) != new Integer(1)).Simplify().AssertEqTo(true);

                (new Integer(0) != new Integer(0)).Simplify().AssertEqTo(false);

                #endregion

                #region And

                new And().Simplify().AssertEqTo(true);

                new And(10).Simplify().AssertEqTo(10);

                new And(true).Simplify().AssertEqTo(true);

                new And(false).Simplify().AssertEqTo(false);

                new And(10, 20, 30).Simplify().AssertEqTo(new And(10, 20, 30));

                new And(10, false, 20).Simplify().AssertEqTo(false);

                new And(10, true, 20).Simplify().AssertEqTo(new And(10, 20));

                new And(10, new And(20, 30), 40)
                    .Simplify()
                    .AssertEqTo(new And(10, 20, 30, 40));

                #endregion

                #region Or

                new Or(10).Simplify().AssertEqTo(10);

                new Or(true).Simplify().AssertEqTo(true);

                new Or(false).Simplify().AssertEqTo(false);

                new Or(10, 20, false).Simplify().AssertEqTo(new Or(10, 20));

                new Or(false, false).Simplify().AssertEqTo(false);

                new Or(10, true, 20, false).Simplify().AssertEqTo(true);

                new Or(10, false, 20).Simplify().AssertEqTo(new Or(10, 20));

                new Or(10, new Or(20, 30), 40)
                    .Simplify()
                    .AssertEqTo(new Or(10, 20, 30, 40));

                #endregion

                #region Function.Map

                new And(1, 2, 3, 4, 5, 6).Map(elt => elt * 2)
                    .AssertEqTo(new And(2, 4, 6, 8, 10, 12));

                new And(1, 2, 3, 4, 5, 6).Map(elt => (elt is Integer) && (elt as Integer).val % 2 == 0 ? elt : false)
                    .AssertEqTo(false);

                new Or(1, 2, 3).Map(elt => elt * 2)
                    .AssertEqTo(new Or(2, 4, 6));

                new Or(1, 2, 3, 4, 5, 6).Map(elt => (elt is Integer) && (elt as Integer).val % 2 == 0 ? elt : false)
                    .AssertEqTo(new Or(2, 4, 6));

                #endregion Function.Map

                #region Sum

                Assert((x + y).Equals(x * y) == false, "(x + y).Equals(x * y)");

                #endregion
                
                {
                    (x < y).Substitute(x, 10).Substitute(y, 20).AssertEqTo(true);

                    (x > y).Substitute(x, 10).Substitute(y, 20).AssertEqTo(false);
                }

                var Pi = new Symbol("Pi");

                #region Sin

                {
                    sin(0).AssertEqTo(0);

                    sin(Pi).AssertEqTo(0);

                    sin(-10).AssertEqTo(-sin(10));

                    sin(-x).AssertEqTo(-sin(x));

                    sin(-5 * x).AssertEqTo(-sin(5 * x));

                    // sin(k/n pi) for n = 1 2 3 4 6

                    sin(-2 * Pi).AssertEqTo(0);
                    sin(-1 * Pi).AssertEqTo(0);
                    sin( 2 * Pi).AssertEqTo(0);
                    sin( 3 * Pi).AssertEqTo(0);

                    sin(-7 * Pi / 2).AssertEqTo(1);
                    sin(-5 * Pi / 2).AssertEqTo(-1);
                    sin(-3 * Pi / 2).AssertEqTo(1);
                    sin(-1 * Pi / 2).AssertEqTo(-1);
                    sin( 1 * Pi / 2).AssertEqTo(1);
                    sin( 3 * Pi / 2).AssertEqTo(-1);
                    sin( 5 * Pi / 2).AssertEqTo(1);
                    sin( 7 * Pi / 2).AssertEqTo(-1);
                    
                    sin(-4 * Pi / 3).AssertEqTo( sqrt(3)/2);
                    sin(-2 * Pi / 3).AssertEqTo(-sqrt(3)/2);
                    sin(-1 * Pi / 3).AssertEqTo(-sqrt(3)/2);
                    sin( 1 * Pi / 3).AssertEqTo( sqrt(3)/2);
                    sin( 2 * Pi / 3).AssertEqTo( sqrt(3)/2);
                    sin( 4 * Pi / 3).AssertEqTo(-sqrt(3)/2);
                    sin( 5 * Pi / 3).AssertEqTo(-sqrt(3)/2);
                    sin( 7 * Pi / 3).AssertEqTo( sqrt(3)/2);

                    sin(-3 * Pi / 4).AssertEqTo(-1/sqrt(2));
                    sin(-1 * Pi / 4).AssertEqTo(-1/sqrt(2));
                    sin( 1 * Pi / 4).AssertEqTo( 1/sqrt(2));
                    sin( 3 * Pi / 4).AssertEqTo( 1/sqrt(2));
                    sin( 5 * Pi / 4).AssertEqTo(-1/sqrt(2));
                    sin( 7 * Pi / 4).AssertEqTo(-1/sqrt(2));
                    sin( 9 * Pi / 4).AssertEqTo( 1/sqrt(2));
                    sin(11 * Pi / 4).AssertEqTo( 1/sqrt(2));

                    var half = new Integer(1) / 2;

                    sin(-5 * Pi / 6).AssertEqTo(-half);
                    sin(-1 * Pi / 6).AssertEqTo(-half);
                    sin( 1 * Pi / 6).AssertEqTo( half);
                    sin( 5 * Pi / 6).AssertEqTo( half);
                    sin( 7 * Pi / 6).AssertEqTo(-half);
                    sin(11 * Pi / 6).AssertEqTo(-half);
                    sin(13 * Pi / 6).AssertEqTo( half);
                    sin(17 * Pi / 6).AssertEqTo( half);

                    // sin(a/b pi) where a/b > 1/2 (i.e. not in first quadrant)

                    sin(15 * Pi / 7).AssertEqTo( sin(1 * Pi / 7));
                    sin( 8 * Pi / 7).AssertEqTo(-sin(1 * Pi / 7));
                    sin( 4 * Pi / 7).AssertEqTo( sin(3 * Pi / 7));

                    // sin( a + b + ... + n * pi ) where abs(n) >= 2

                    sin(x - 3 * Pi).AssertEqTo(sin(x + Pi));
                    sin(x - 2 * Pi).AssertEqTo(sin(x));
                    sin(x + 2 * Pi).AssertEqTo(sin(x));
                    sin(x + 3 * Pi).AssertEqTo(sin(x + Pi));
                    sin(x + 7 * Pi / 2).AssertEqTo(sin(x + 3 * Pi / 2));

                    // sin( a + b + ... + n/2 * pi )

                    sin(x - 3 * Pi / 2).AssertEqTo( cos(x));
                    sin(x - 1 * Pi / 2).AssertEqTo(-cos(x));
                    sin(x + 1 * Pi / 2).AssertEqTo( cos(x));
                    sin(x + 3 * Pi / 2).AssertEqTo(-cos(x));
                }


                {
                    sin(Pi + x).AssertEqTo(-sin(x));
                    
                    sin(Pi + x + y).AssertEqTo(-sin(x + y));
                }

                {
                    // var Pi = new Symbol("Pi");

                    cos(Pi + x).AssertEqTo(-cos(x));

                    cos(Pi + x + y).AssertEqTo(-cos(x + y));
                }


                #endregion

                #region Cos

                {
                    // var Pi = new Symbol("Pi");

                    cos(0).AssertEqTo(1);

                    cos(Pi).AssertEqTo(-1);

                    cos(-10).AssertEqTo(cos(10));

                    cos(-10 * x).AssertEqTo(cos(10 * x));

                    cos(3 * Pi).AssertEqTo(-1);

                    cos(2 * Pi * 3 / 4).AssertEqTo(0);

                    // cos( a + b + ... + n * pi ) where abs(n) >= 2

                    cos(x - 3 * Pi).AssertEqTo(cos(x + Pi));
                    cos(x + 3 * Pi).AssertEqTo(cos(x + Pi));

                    cos(x - 2 * Pi).AssertEqTo(cos(x));
                    cos(x + 2 * Pi).AssertEqTo(cos(x));

                    cos(x + Pi * 7 / 2).AssertEqTo(cos(x + Pi * 3 / 2));

                    // cos( a + b + ... + n/2 * pi )

                    cos(x - Pi * 3 / 2).AssertEqTo(-sin(x));
                    cos(x - Pi * 1 / 2).AssertEqTo(sin(x));
                    cos(x + Pi * 1 / 2).AssertEqTo(-sin(x));
                    cos(x + Pi * 3 / 2).AssertEqTo(sin(x));
                }


                #endregion
                
                #region Has

                Assert(a.Has(elt => elt == a), "a.Has(elt => elt == a)");

                Assert(a.Has(elt => elt == b) == false, "a.Has(elt => elt == b) == false");

                Assert((a == b).Has(elt => elt == a), "Has - 3");

                Assert((a == b).Has(elt => elt == c) == false, "Has - 4");

                Assert(((a + b) ^ c).Has(elt => elt == a + b), "Has - 5");

                Assert(((a + b) ^ c).Has(elt => (elt is Power) && (elt as Power).exp == c), "Has - 6");

                Assert((x * (a + b + c)).Has(elt => (elt is Sum) && (elt as Sum).Has(b)), "Has - 7");

                Assert((x * (a + b + c)).Has(elt => (elt is Sum) && (elt as Sum).elts.Any(obj => obj == b)), "Has - 8");

                Assert((x * (a + b + c)).Has(elt => (elt is Product) && (elt as Product).elts.Any(obj => obj == b)) == false, "Has - 9");

                #endregion

                #region FreeOf

                Assert((a + b).FreeOf(b) == false, "(a + b).FreeOf(b)");
                Assert((a + b).FreeOf(c) == true, "(a + b).FreeOf(c)");
                Assert(((a + b) * c).FreeOf(a + b) == false, "((a + b) * c).FreeOf(a + b)");
                Assert((sin(x) + 2 * x).FreeOf(sin(x)) == false, "(sin(x) + 2 * x).FreeOf(sin(x))");
                Assert(((a + b + c) * d).FreeOf(a + b) == true, "((a + b + c) * d).FreeOf(a + b)");
                Assert(((y + 2 * x - y) / x).FreeOf(x) == true, "((y + 2 * x - y) / x).FreeOf(x)");
                Assert(((x * y) ^ 2).FreeOf(x * y) == true, "((x * y) ^ 2).FreeOf(x * y)");

                #endregion

                #region Denominator
                {
                    ((new Integer(2) / 3) * ((x * (x + 1)) / (x + 2)) * (y ^ z))
                        .Denominator()
                        .AssertEqTo(3 * (x + 2));
                }
                #endregion
                
                #region LogicalExpand

                new And(new Or(a, b), c)
                    .LogicalExpand()
                    .AssertEqTo(
                        new Or(
                            new And(a, c),
                            new And(b, c)));

                new And(a, new Or(b, c))
                    .LogicalExpand()
                    .AssertEqTo(new Or(new And(a, b), new And(a, c)));

                new And(a, new Or(b, c), d)
                    .LogicalExpand()
                    .AssertEqTo(
                        new Or(
                            new And(a, b, d),
                            new And(a, c, d)));

                new And(new Or(a == b, b == c), x == y)
                    .LogicalExpand()
                    .AssertEqTo(
                        new Or(
                            new And(a == b, x == y),
                            new And(b == c, x == y)));

                new And(
                    new Or(a == b, b == c),
                    new Or(c == d, d == a),
                    x == y)
                    .LogicalExpand()
                    .AssertEqTo(
                        new Or(
                            new And(a == b, c == d, x == y),
                            new And(a == b, d == a, x == y),
                            new And(b == c, c == d, x == y),
                            new And(b == c, d == a, x == y)));

                #endregion

                #region SimplifyEquation

                (2 * x == 0)
                    .SimplifyEquation()
                    .AssertEqTo(x == 0);

                (2 * x != 0)
                    .SimplifyEquation()
                    .AssertEqTo(x != 0);

                ((x ^ 2) == 0)
                    .SimplifyEquation()
                    .AssertEqTo(x == 0);

                #endregion

                #region SimplifyLogical

                new And(a, b, c, a)
                    .SimplifyLogical()
                    .AssertEqTo(new And(a, b, c));

                #endregion SimplifyLogical

                #region DegreeGpe

                {
                    var w = new Symbol("w");

                    Assert(
                        ((3 * w * x ^ 2) * (y ^ 3) * (z ^ 4)).DegreeGpe(new List<MathObject>() { x, z }) == 6,
                        "((3 * w * x ^ 2) * (y ^ 3) * (z ^ 4)).DegreeGpe(new List<MathObject>() { x, z })");

                    Assert(
                        ((a * x ^ 2) + b * x + c).DegreeGpe(new List<MathObject>() { x }) == 2,
                        "((a * x ^ 2) + b * x + c).DegreeGpe(new List<MathObject>() { x })");

                    Assert(
                        (a * (sin(x) ^ 2) + b * sin(x) + c).DegreeGpe(new List<MathObject>() { sin(x) }) == 2,
                        "(a * (sin(x) ^ 2) + b * sin(x) + c).DegreeGpe(new List<MathObject>() { sin(x) })");

                    Assert(
                        (2 * (x ^ 2) * y * (z ^ 3) + w * x * (z ^ 6)).DegreeGpe(new List<MathObject>() { x, z }) == 7,
                        "(2 * (x ^ 2) * y * (z ^ 3) + w * x * (z ^ 6)).DegreeGpe(new List<MathObject>() { x, z })");
                }

                #endregion

                #region CoefficientGpe

                AssertIsTrue((a * (x ^ 2) + b * x + c).CoefficientGpe(x, 2) == a);

                AssertIsTrue((3 * x * (y ^ 2) + 5 * (x ^ 2) * y + 7 * x + 9).CoefficientGpe(x, 1) == 3 * (y ^ 2) + 7);

                AssertIsTrue((3 * x * (y ^ 2) + 5 * (x ^ 2) * y + 7 * x + 9).CoefficientGpe(x, 3) == 0);

                Assert(
                    (3 * sin(x) * (x ^ 2) + 2 * x + 4).CoefficientGpe(x, 2) == null,
                    "(3 * sin(x) * (x ^ 2) + 2 * x + 4).CoefficientGpe(x, 2) == null");

                #endregion

                #region AlgebraicExpand

                AssertIsTrue(
                    ((x + 2) * (x + 3) * (x + 4)).AlgebraicExpand()
                    ==
                    24 + 26 * x + 9 * (x ^ 2) + (x ^ 3));

                AssertIsTrue(
                    ((x + y + z) ^ 3).AlgebraicExpand()
                    ==
                    (x ^ 3) + (y ^ 3) + (z ^ 3) +
                    3 * (x ^ 2) * y +
                    3 * (y ^ 2) * x +
                    3 * (x ^ 2) * z +
                    3 * (y ^ 2) * z +
                    3 * (z ^ 2) * x +
                    3 * (z ^ 2) * y +
                    6 * x * y * z);

                AssertIsTrue(
                    (((x + 1) ^ 2) + ((y + 1) ^ 2)).AlgebraicExpand()
                    ==
                    2 + 2 * x + (x ^ 2) + 2 * y + (y ^ 2));

                AssertIsTrue(
                    ((((x + 2) ^ 2) + 3) ^ 2).AlgebraicExpand()
                    ==
                    49 + 56 * x + 30 * (x ^ 2) + 8 * (x ^ 3) + (x ^ 4));

                AssertIsTrue(
                    sin(x * (y + z)).AlgebraicExpand()
                    ==
                    sin(x * y + x * z));


                AssertIsTrue(
                    (a * (b + c) == x * (y + z)).AlgebraicExpand()
                    ==
                    (a * b + a * c == x * y + x * z));

                #endregion

                #region IsolateVariable

                (x + y + z == 0).IsolateVariable(a).AssertEqTo(x + y + z == 0);

                // (x * a + x * b == 0).IsolateVariable(x).Disp();

                (x * (a + b) - x * a - x * b + x == c)
                    .IsolateVariable(x)
                    .AssertEqTo(x == c);

                new And(x == y, a == b)
                    .IsolateVariable(b)
                    .AssertEqTo(new And(x == y, b == a));

                new Or(new And(y == x, z == x), new And(b == x, c == x))
                    .IsolateVariable(x)
                    .AssertEqTo(new Or(new And(x == y, x == z), new And(x == b, x == c)));

                Assert((0 == x - y).IsolateVariableEq(x).Equals(x == y), "(0 == x - y).IsolateVariable(x).Equals(x == y)");

                

                (a * (x ^ 2) + b * x + c == 0)
                    .IsolateVariable(x)
                    .AssertEqTo(

                        new Or(

                            new And(
                                x == (-b + sqrt((b ^ 2) + -4 * a * c)) / (2 * a),
                                a != 0
                            ),

                            new And(
                                x == (-b - sqrt((b ^ 2) + -4 * a * c)) / (2 * a),
                                a != 0
                            ),

                            new And(x == -c / b, a == 0, b != 0),

                            new And(a == 0, b == 0, c == 0)
                        )
                    );

                (a * (x ^ 2) + c == 0)
                    .IsolateVariable(x)
                    .AssertEqTo(

                        new Or(
                            new And(
                                x == sqrt(-4 * a * c) / (2 * a),
                                a != 0
                            ),

                            new And(
                                x == -sqrt(-4 * a * c) / (2 * a),
                                a != 0
                            ),

                            new And(a == 0, c == 0)
                        )
                    );

                // a x^2 + b x + c == 0
                // a x^2 + c == - b x
                // (a x^2 + c) / x == - b

                ((a * (x ^ 2) + c) / x == -b)
                    .IsolateVariable(x)
                    .AssertEqTo(

                    new Or(

                            new And(
                                x == (-b + sqrt((b ^ 2) + -4 * a * c)) / (2 * a),
                                a != 0
                            ),

                            new And(
                                x == (-b - sqrt((b ^ 2) + -4 * a * c)) / (2 * a),
                                a != 0
                            ),

                            new And(x == -c / b, a == 0, b != 0),

                            new And(a == 0, b == 0, c == 0)
                        )
                );

                (sqrt(x + y) == z).IsolateVariable(x).AssertEqTo(x == (z ^ 2) - y);

                (a * b + a == c)
                    .IsolateVariable(a)
                    .AssertEqTo(a == c / (b + 1));

                (a * b + a * c == d)
                    .IsolateVariable(a)
                    .AssertEqTo(a == d / (b + c));

                (1 / sqrt(x) == y)
                    .IsolateVariable(x)
                    .AssertEqTo(x == (y ^ -2));

                (y == sqrt(x) / x)
                    .IsolateVariable(x)
                    .AssertEqTo(x == (y ^ -2));

                (-sqrt(x) + z * x == y)
                    .IsolateVariable(x)
                    .AssertEqTo(-sqrt(x) + z * x == y);

                (sqrt(a + x) - z * x == -y)
                    .IsolateVariable(x)
                    .AssertEqTo(sqrt(a + x) - z * x == -y);

                (sqrt(2 + x) * sqrt(3 + x) == y)
                    .IsolateVariable(x)
                    .AssertEqTo(sqrt(2 + x) * sqrt(3 + x) == y);

                #endregion

                #region EliminateVariable

                new And((x ^ 3) == (y ^ 5), z == x)
                .EliminateVariable(x)
                .AssertEqTo((z ^ 3) == (y ^ 5));

                new And((x ^ 3) == (y ^ 5), z == (x ^ 7))
                .EliminateVariable(x)
                .AssertEqTo(new And((x ^ 3) == (y ^ 5), z == (x ^ 7)));

                #endregion
            }

            #region EliminateVariable
            
            {

                var x = new Symbol("x");
                var y = new Symbol("y");
                var z = new Symbol("z");

                var eqs = new And()
                {
                    args = 
                    {
                        (x ^ 2) - 4 == 0,
                        y + x == 0,
                        x + z == 10
                    }
                }
                .Simplify();

                var half = new Integer(1) / 2;

                // Func<MathObject, MathObject> sqrt = obj => obj ^ half;

                ((x ^ 2) - 4 == 0)
                    .IsolateVariableEq(x)
                    .AssertEqTo(new Or(x == half * sqrt(16), x == -half * sqrt(16)));

                eqs.EliminateVariable(x)
                    .AssertEqTo(
                        new Or(
                            new And(
                                half * sqrt(16) + y == 0,
                                half * sqrt(16) + z == 10
                            ),
                            new And(
                                -half * sqrt(16) + y == 0,
                                -half * sqrt(16) + z == 10
                            )
                        )
                    );


            }
            
            {
                var a = new Symbol("a");
                var x = new Symbol("x");
                var y = new Symbol("y");
                var z = new Symbol("z");

                new Or(
                    new And(x == y, x == z, x == a),
                    new And(x == -y, x == z, x == a)
                    )
                    .EliminateVariable(x)
                    .AssertEqTo(
                        new Or(
                            new And(y == z, y == a),
                            new And(-y == z, -y == a)
                        )
                    )
                    .EliminateVariable(y)
                    .AssertEqTo(new Or(z == a, z == a));
            }
            
            {
                var x = new Symbol("x");
                var y = new Symbol("y");
                var z = new Symbol("z");

                new And(y != z, y == x, y == 10)
                    .EliminateVariable(y)
                    .AssertEqTo(new And(x != z, x == 10));
            }

            #endregion

            #region PSE Example 2.6

            {
                var sAC = new Symbol("sAC");
                var sAB = new Symbol("sAB");

                var vA = new Symbol("vA");
                var vB = new Symbol("vB");
                var vC = new Symbol("vC");

                var a = new Symbol("a");

                var tAC = new Symbol("tAC");
                var tAB = new Symbol("tAB");

                var eqs = new And(tAB == tAC / 2);

                eqs.args.AddRange(Kinematic(sAC, vA, vC, a, tAC));
                eqs.args.AddRange(Kinematic(sAB, vA, vB, a, tAB));

                var vals = new List<Equation>() { vA == 10, vC == 30, tAC == 10 };

                eqs
                    .EliminateVariables(tAB, sAC, vB, sAB)
                    .IsolateVariable(a)
                    .AssertEqTo(a == (vC - vA) / tAC)
                    .SubstituteEqLs(vals)
                    .AssertEqTo(a == 2);

                eqs
                    .EliminateVariables(vB, a, tAB, sAC)
                    .AssertEqTo(sAB == tAC / 4 * (2 * vA + (vC - vA) / 2))
                    .SubstituteEqLs(vals)
                    .AssertEqTo(sAB == 75);
            }

            #endregion

            #region PSE Example 2.7
            {
                // s = 
                // u = 63
                // v =  0
                // a =
                // t =  2

                var s = new Symbol("s");
                var u = new Symbol("u");
                var v = new Symbol("v");
                var a = new Symbol("a");
                var t = new Symbol("t");

                var eqs = new And();

                eqs.args.AddRange(Kinematic(s, u, v, a, t));

                var vals = new List<Equation>() { u == 63, v == 0, t == 2.0 };

                eqs
                    .EliminateVariable(s)
                    .AssertEqTo(v == a * t + u)
                    .IsolateVariable(a)
                    .AssertEqTo(a == (v - u) / t)
                    .SubstituteEqLs(vals)
                    .AssertEqTo(a == -31.5);

                eqs
                    .EliminateVariable(a)
                    .SubstituteEqLs(vals)
                    .AssertEqTo(s == 63.0);
            }
            #endregion

            #region PSE Example 2.8
            {
                // car
                //
                // s1 =  
                // u1 = 45
                // v1 = 45
                // a1 =  0
                // t1 = 

                // officer
                //
                // s2 =  
                // u2 =  0
                // v2 = 
                // a2 =  3
                // t2

                var s1 = new Symbol("s1");
                var u1 = new Symbol("u1");
                var v1 = new Symbol("v1");
                var a1 = new Symbol("a1");
                var t1 = new Symbol("t1");

                var s2 = new Symbol("s2");
                var u2 = new Symbol("u2");
                var v2 = new Symbol("v2");
                var a2 = new Symbol("a2");
                var t2 = new Symbol("t2");

                var eqs = new And(
                    u1 == v1,
                    s1 == s2,
                    t2 == t1 - 1);

                eqs.args.AddRange(Kinematic(s1, u1, v1, a1, t1));
                eqs.args.AddRange(Kinematic(s2, u2, v2, a2, t2));

                var vals = new List<Equation>() 
                {
                    v1 == 45.0,
                    u2 == 0,
                    a2 == 3
                };

                eqs
                    .EliminateVariables(s2, t1, a1, s1, v2, u1)
                    .IsolateVariable(t2)
                    .SubstituteEqLs(vals)
                    .AssertEqTo(new Or(t2 == -0.96871942267131317, t2 == 30.968719422671313));
            }
            #endregion

            #region PSE Example 2.12

            {
                var yA = new Symbol("yA");
                var yB = new Symbol("yB");
                var yC = new Symbol("yC");
                var yD = new Symbol("yD");

                var tA = new Symbol("tA");
                var tB = new Symbol("tB");
                var tC = new Symbol("tC");
                var tD = new Symbol("tD");

                var vA = new Symbol("vA");
                var vB = new Symbol("vB");
                var vC = new Symbol("vC");
                var vD = new Symbol("vD");

                var a = new Symbol("a");

                var eqs = new And();

                eqs.args.AddRange(Kinematic(yA, yB, vA, vB, a, tA, tB));
                eqs.args.AddRange(Kinematic(yB, yC, vB, vC, a, tB, tC));
                eqs.args.AddRange(Kinematic(yC, yD, vC, vD, a, tC, tD));

                var vals = new List<Equation>()
                {
                    yA == 50,
                    yC == 50,
                    vA == 20,
                    vB == 0,
                    a == -9.8,
                    tA == 0,
                    tD == 5
                };

                // velocity and position at t = 5.00 s

                DoubleFloat.tolerance = 0.000000001;

                eqs
                    .EliminateVariables(tB, tC, vC, yB, yD)
                    .SubstituteEqLs(vals)
                    .AssertEqTo(new Or(vD == -29.000000000000004, vD == -29.000000000000007));

                eqs
                    .EliminateVariables(tB, tC, vC, yB, vD)
                    .IsolateVariable(yD)
                    .SubstituteEqLs(vals)
                    .AssertEqTo(new Or(yD == 27.499999999, yD == 27.499999999));

                DoubleFloat.tolerance = null;
            }

            #endregion
            
            #region PSE Example 4.3
            {
                // A long-jumper leaves the ground at an angle of 20.0° above
                // the horizontal and at a speed of 11.0 m/s.

                // (a) How far does he jump in the horizontal direction?
                // (Assume his motion is equivalent to that of a particle.)

                // (b) What is the maximum height reached?

                var xA = new Symbol("xA");
                var xB = new Symbol("xB");
                var xC = new Symbol("xC");

                var yA = new Symbol("yA");
                var yB = new Symbol("yB");
                var yC = new Symbol("yC");

                var vxA = new Symbol("vxA");
                var vxB = new Symbol("vxB");
                var vxC = new Symbol("vxC");

                var vyA = new Symbol("vyA");
                var vyB = new Symbol("vyB");
                var vyC = new Symbol("vyC");

                var tAB = new Symbol("tAB");
                var tAC = new Symbol("tAC");

                var ax = new Symbol("ax");
                var ay = new Symbol("ay");

                var vA = new Symbol("vA");
                var thA = new Symbol("thA");

                var Pi = new Symbol("Pi");

                var eqs = new And(

                    vxA == vA * cos(thA),
                    vyA == vA * sin(thA),

                    tAC == 2 * tAB,


                    vxB == vxA + ax * tAB,
                    vyB == vyA + ay * tAB,

                    xB == xA + vxA * tAB + ax * (tAB ^ 2) / 2,
                    yB == yA + vyA * tAB + ay * (tAB ^ 2) / 2,


                    vxC == vxA + ax * tAB,
                    vyC == vyA + ay * tAB,

                    xC == xA + vxA * tAC + ax * (tAC ^ 2) / 2,
                    yC == yA + vyA * tAC + ay * (tAC ^ 2) / 2

                    );

                var zeros = new List<Equation>() { xA == 0, yA == 0, ax == 0, vyB == 0 };

                var vals = new List<Equation>() { thA == (20).ToRadians(), vA == 11.0, ay == -9.8, Pi == Math.PI };

                eqs
                    .EliminateVariables(xB, yC, vxB, vxC, vyC, yB, tAC, vxA, vyA, tAB)
                    .SubstituteEqLs(zeros)
                    .AssertEqTo(xC == -2 * cos(thA) * sin(thA) * (vA ^ 2) / ay)
                    .SubstituteEqLs(vals)
                    .AssertEqTo(xC == 7.9364592624562507);

                eqs
                    .EliminateVariables(xB, yC, vxB, vxC, vyC, xC, vxA, tAC, vyA, tAB)
                    .SubstituteEqLs(zeros)
                    .AssertEqTo(yB == -(sin(thA) ^ 2) * (vA ^ 2) / (2 * ay))
                    .SubstituteEqLs(vals)
                    .AssertEqTo(yB == 0.72215873425009314);
            }

            #endregion
            
            #region PSE Example 4.3 KinematicObjectABC
            {
                // A long-jumper leaves the ground at an angle of 20.0° above
                // the horizontal and at a speed of 11.0 m/s.

                // (a) How far does he jump in the horizontal direction?
                // (Assume his motion is equivalent to that of a particle.)

                // (b) What is the maximum height reached?

                var obj = new KinematicObjectABC("obj");

                var yB = new Symbol("yB");
                var xC = new Symbol("xC");
                var ay = new Symbol("ay");
                var thA = new Symbol("thA");
                var vA = new Symbol("vA");

                var Pi = new Symbol("Pi");

                var eqs = new And(

                    obj.TrigEquationsA(),

                    obj.tAC == 2 * obj.tAB,

                    obj.EquationsAB(),
                    obj.EquationsAC()
                    
                    );

                var vals = new List<Equation>()
                {
                    obj.xA == 0,
                    obj.yA == 0,

                    obj.vA == vA,
                    obj.thA == thA,

                    obj.yB == yB,
                    obj.vyB == 0,

                    obj.xC == xC,

                    obj.ax == 0,
                    obj.ay == ay
                };

                var numerical_vals = new List<Equation>()
                {
                    thA == (20).ToRadians(),
                    vA == 11,
                    ay == -9.8,
                    Pi == Math.PI
                    
                };

                // xC
                eqs
                    .SubstituteEqLs(vals)

                    .EliminateVariables(
                        obj.vxA, obj.vyA, obj.vyC, obj.vxC, obj.vxB,
                        obj.xB, yB, obj.yC,
                        obj.tAC, obj.tAB
                    )
                    
                    .AssertEqTo(xC == -2 * cos(thA) * sin(thA) * (vA ^ 2) / ay)

                    .SubstituteEqLs(numerical_vals)

                    .AssertEqTo(xC == 7.9364592624562507);

                // yB
                eqs
                    .SubstituteEqLs(vals)

                    .EliminateVariables(
                        obj.tAB, obj.tAC,
                        obj.vxA, obj.vxB, obj.vxC, obj.vyC, obj.vyA,
                        obj.xB, xC, obj.yC
                    )
                    
                    .AssertEqTo(yB == -(sin(thA) ^ 2) * (vA ^ 2) / (2 * ay))

                    .SubstituteEqLs(numerical_vals)

                    .AssertEqTo(yB == 0.72215873425009314);

            }
            #endregion
            
            #region PSE 5E Example 4.5

            {
                var xA = new Symbol("xA");
                var xB = new Symbol("xB");
                var xC = new Symbol("xC");

                var yA = new Symbol("yA");
                var yB = new Symbol("yB");
                var yC = new Symbol("yC");

                var vxA = new Symbol("vxA");
                var vxB = new Symbol("vxB");
                var vxC = new Symbol("vxC");

                var vyA = new Symbol("vyA");
                var vyB = new Symbol("vyB");
                var vyC = new Symbol("vyC");

                var tAB = new Symbol("tAB");
                var tAC = new Symbol("tAC");

                var ax = new Symbol("ax");
                var ay = new Symbol("ay");

                var vA = new Symbol("vA");
                var thA = new Symbol("thA");

                var vC = new Symbol("vC");

                var Pi = new Symbol("Pi");

                var eqs = new And(

                    vxA == vA * cos(thA),
                    vyA == vA * sin(thA),

                    // tAC == 2 * tAB,

                    // vxB == vxA + ax * tAB,
                    // vyB == vyA + ay * tAB,

                    // xB == xA + vxA * tAB + ax * (tAB ^ 2) / 2,
                    // yB == yA + vyA * tAB + ay * (tAB ^ 2) / 2,

                    vxC == vxA + ax * tAC,
                    vyC == vyA + ay * tAC,

                    // xC == xA + vxA * tAC + ax * (tAC ^ 2) / 2,
                    yC == yA + vyA * tAC + ay * (tAC ^ 2) / 2,

                    vC == sqrt((vxC ^ 2) + (vyC ^ 2)),

                    ay != 0
                );

                var zeros = new List<Equation>() { ax == 0, yC == 0 };
                var vals = new List<Equation>() { yA == 45, vA == 20, thA == (30).ToRadians(), ay == -9.8, Pi == Math.PI };

                DoubleFloat.tolerance = 0.00001;

                eqs
                    .EliminateVariables(vC, vxA, vxC, vyC, vyA)
                    .IsolateVariable(tAC)
                    .LogicalExpand().SimplifyEquation().SimplifyLogical()
                    .CheckVariable(ay)
                    .AssertEqTo(
                        new Or(
                            new And(
                                tAC == -(sin(thA) * vA + sqrt((sin(thA) ^ 2) * (vA ^ 2) + 2 * ay * (yC - yA))) / ay,
                                ay != 0),
                            new And(
                                tAC == -(sin(thA) * vA - sqrt((sin(thA) ^ 2) * (vA ^ 2) + 2 * ay * (yC - yA))) / ay,
                                ay != 0)))
                    .SubstituteEqLs(zeros)
                    .SubstituteEqLs(vals)
                    .AssertEqTo(new Or(tAC == 4.2180489012229376, tAC == -2.1772325746923267));

                eqs
                    .SubstituteEqLs(zeros)
                    .EliminateVariables(vxC, vxA, vyA, vyC, tAC)
                    .SimplifyEquation().SimplifyLogical()
                    .CheckVariable(ay)
                    .AssertEqTo(
                        new Or(
                            new And(
                                ay != 0,
                                vC == sqrt((cos(thA) ^ 2) * (vA ^ 2) + ((sin(thA) * vA - (sin(thA) * vA + sqrt((sin(thA) ^ 2) * (vA ^ 2) + -2 * ay * yA))) ^ 2))),
                            new And(
                                ay != 0,
                                vC == sqrt((cos(thA) ^ 2) * (vA ^ 2) + ((sin(thA) * vA - (sin(thA) * vA - sqrt((sin(thA) ^ 2) * (vA ^ 2) + -2 * ay * yA))) ^ 2)))))
                    .SubstituteEqLs(vals)
                    .AssertEqTo(new Or(vC == 35.805027579936315, vC == 35.805027579936322));

                DoubleFloat.tolerance = null;
            }

            #endregion

            #region PSE 5E Example 4.6

            {
                var xA = new Symbol("xA");
                var xB = new Symbol("xB");

                var yA = new Symbol("yA");
                var yB = new Symbol("yB");

                var vxA = new Symbol("vxA");
                var vxB = new Symbol("vxB");

                var vyA = new Symbol("vyA");
                var vyB = new Symbol("vyB");

                var tAB = new Symbol("tAB");

                var ax = new Symbol("ax");
                var ay = new Symbol("ay");

                var Pi = new Symbol("Pi");

                var eqs = new And(

                    vxB == vxA + ax * tAB,
                    vyB == vyA + ay * tAB,

                    xB == xA + vxA * tAB + ax * (tAB ^ 2) / 2,
                    yB == yA + vyA * tAB + ay * (tAB ^ 2) / 2,

                    vxA != 0,

                    ay != 0
                );

                var vals = new List<Equation>() { xA == 0, yA == 100, vxA == 40, vyA == 0, yB == 0, ax == 0, ay == -9.8, Pi == Math.PI };

                var zeros = vals.Where(eq => eq.b == 0).ToList();

                DoubleFloat.tolerance = 0.00001;

                eqs
                    .EliminateVariables(vxB, vyB, tAB)
                    .IsolateVariable(xB)
                    .LogicalExpand().SimplifyEquation()
                    .CheckVariable(ay)
                    .CheckVariable(vxA).SimplifyLogical()
                    .SubstituteEq(ax == 0)
                    .AssertEqTo(
                        new Or(
                            new And(
                                vxA != 0,
                                xB == -1 * (ay ^ -1) * (vxA ^ 2) * (-1 * (-1 * (vxA ^ -1) * vyA + ay * (vxA ^ -2) * xA) + sqrt(((-1 * (vxA ^ -1) * vyA + ay * (vxA ^ -2) * xA) ^ 2) + 2 * ay * (vxA ^ -2) * ((vxA ^ -1) * vyA * xA - ay / 2 * (vxA ^ -2) * (xA ^ 2) + -1 * yA + yB))),
                                ay * (vxA ^ -2) != 0,
                                ay != 0),
                            new And(
                                vxA != 0,
                                xB == -1 * (ay ^ -1) * (vxA ^ 2) * (-1 * (-1 * (vxA ^ -1) * vyA + ay * (vxA ^ -2) * xA) + -1 * sqrt(((-1 * (vxA ^ -1) * vyA + ay * (vxA ^ -2) * xA) ^ 2) + 2 * ay * (vxA ^ -2) * ((vxA ^ -1) * vyA * xA - ay / 2 * (vxA ^ -2) * (xA ^ 2) + -1 * yA + yB))),
                                ay * (vxA ^ -2) != 0,
                                ay != 0)))
                    .SubstituteEqLs(zeros)
                    .AssertEqTo(
                        new Or(
                            new And(
                                vxA != 0,
                                xB == -1 / ay * (vxA ^ 2) * sqrt(-2 * ay * (vxA ^ -2) * yA),
                                ay / (vxA ^ 2) != 0,
                                ay != 0),
                            new And(
                                vxA != 0,
                                xB == 1 / ay * (vxA ^ 2) * sqrt(-2 * ay * (vxA ^ -2) * yA),
                                ay / (vxA ^ 2) != 0,
                                ay != 0)))
                    .SubstituteEqLs(vals)
                    .AssertEqTo(new Or(xB == 180.70158058105022, xB == -180.70158058105022));

                eqs
                    .EliminateVariables(vxB, xB, tAB)
                    .IsolateVariable(vyB)
                    .LogicalExpand().SimplifyEquation()
                    .CheckVariable(ay)
                    .AssertEqTo(
                        new Or(
                            new And(
                                vyB == -1 * ay * sqrt(2 * (ay ^ -1) * ((ay ^ -1) / 2 * (vyA ^ 2) + -1 * yA + yB)),
                    // (ay ^ -1) != 0,
                                vxA != 0,
                                ay != 0),
                            new And(
                                vyB == ay * sqrt(2 * (ay ^ -1) * ((ay ^ -1) / 2 * (vyA ^ 2) + -1 * yA + yB)),
                    // (ay ^ -1) != 0,
                                vxA != 0,
                                ay != 0)))
                    .SubstituteEqLs(zeros)
                    .AssertEqTo(
                        new Or(
                          new And(
                              vyB == -ay * sqrt(-2 / ay * yA),
                    // 1 / ay != 0,
                              vxA != 0,
                              ay != 0),
                          new And(
                              vyB == ay * sqrt(-2 / ay * yA),
                    // 1 / ay != 0,
                              vxA != 0,
                              ay != 0)))
                    .SubstituteEqLs(vals)
                    .AssertEqTo(new Or(vyB == 44.271887242357309, vyB == -44.271887242357309));

                DoubleFloat.tolerance = null;
            }

            #endregion

            #region PSE 5E Example 4.7

            {
                var xA = new Symbol("xA");
                var yA = new Symbol("yA");

                var xB = new Symbol("xB");
                var yB = new Symbol("yB");

                var vxA = new Symbol("vxA");
                var vyA = new Symbol("vyA");

                var vxB = new Symbol("vxB");
                var vyB = new Symbol("vyB");

                var tAB = new Symbol("tAB");

                var ax = new Symbol("ax");
                var ay = new Symbol("ay");

                var th = new Symbol("th");
                var d = new Symbol("d");

                var Pi = new Symbol("Pi");

                var eqs = new And(

                    cos(th) == (xB - xA) / d,
                    sin(th) == (yA - yB) / d,

                    vxB == vxA + ax * tAB,
                    vyB == vyA + ay * tAB,

                    xB == xA + vxA * tAB + ax * (tAB ^ 2) / 2,
                    yB == yA + vyA * tAB + ay * (tAB ^ 2) / 2,

                    yB != 0,

                    ay != 0
                );

                var vals = new List<Equation>() { xA == 0, yA == 0, vxA == 25, vyA == 0, ax == 0, ay == -9.8, th == (35).ToRadians(), Pi == Math.PI };

                var zeros = vals.Where(eq => eq.b == 0).ToList();

                DoubleFloat.tolerance = 0.00001;

                eqs
                    .SubstituteEqLs(zeros)
                    .EliminateVariables(vxB, vyB, d, yB, tAB)
                    .IsolateVariable(xB)
                    .LogicalExpand()
                    .CheckVariable(ay)
                    .SimplifyEquation()
                    .AssertEqTo(
                        new Or(
                            new And(
                                xB == -(sin(th) / cos(th) + sqrt((cos(th) ^ -2) * (sin(th) ^ 2))) * (vxA ^ 2) / ay,
                                ay / (vxA ^ 2) != 0,
                                sin(th) / cos(th) * xB != 0,
                                ay != 0),
                            new And(
                                xB == -(sin(th) / cos(th) - sqrt((cos(th) ^ -2) * (sin(th) ^ 2))) * (vxA ^ 2) / ay,
                                ay / (vxA ^ 2) != 0,
                                sin(th) / cos(th) * xB != 0,
                                ay != 0)))
                    .SubstituteEqLs(vals)
                    .SimplifyEquation()
                    .AssertEqTo(
                        new Or(
                            new And(
                                xB == 89.312185996136435,
                                xB != 0),
                            new And(
                                xB == 7.0805039835788038E-15,
                                xB != 0)));

                eqs
                    .SubstituteEqLs(zeros)
                    .EliminateVariables(vxB, vyB, d, xB, tAB)
                    .IsolateVariable(yB)
                    .LogicalExpand()
                    .CheckVariable(yB)
                    .AssertEqTo(
                        new And(
                            yB == 2 * (sin(th) ^ 2) * (vxA ^ 2) / ay / (cos(th) ^ 2),
                            -ay * (cos(th) ^ 2) / (sin(th) ^ 2) / (vxA ^ 2) / 2 != 0,
                            yB != 0,
                            ay != 0))
                    .SubstituteEqLs(vals)
                    .AssertEqTo(
                        new And(
                            yB == -62.537065888482395,
                            yB != 0));

                eqs
                    .SubstituteEqLs(zeros)
                    .EliminateVariables(vxB, vyB, d, xB, yB)
                    .IsolateVariable(tAB)
                    .LogicalExpand().CheckVariable(ay).SimplifyEquation().SimplifyLogical()
                    .AssertEqTo(
                        new Or(
                            new And(
                                tAB == -(sin(th) * vxA / cos(th) + sqrt((sin(th) ^ 2) * (vxA ^ 2) / (cos(th) ^ 2))) / ay,
                                ay != 0,
                                sin(th) * tAB * vxA / cos(th) != 0),
                            new And(
                                tAB == -(sin(th) * vxA / cos(th) - sqrt((sin(th) ^ 2) * (vxA ^ 2) / (cos(th) ^ 2))) / ay,
                                ay != 0,
                                sin(th) * tAB * vxA / cos(th) != 0)))
                    .SubstituteEqLs(vals)
                    .CheckVariable(tAB).SimplifyEquation()
                    .AssertEqTo(
                        new And(
                            tAB == 3.5724874398454571,
                            tAB != 0));

                eqs
                    .SubstituteEqLs(zeros)
                    .EliminateVariables(vxB, d, tAB, xB, yB)
                    .IsolateVariable(vyB)
                    .LogicalExpand()
                    .CheckVariable(ay)
                    .SimplifyEquation()
                    .CheckVariable(ay)
                    .AssertEqTo(
                        new Or(
                            new And(
                                vyB == -ay * (sin(th) * vxA / (ay * cos(th)) + sqrt((sin(th) ^ 2) * (vxA ^ 2) / ((ay ^ 2) * (cos(th) ^ 2)))),
                                sin(th) * vxA * vyB / (ay * cos(th)) != 0,
                                ay != 0),
                            new And(
                                vyB == -ay * (sin(th) * vxA / (ay * cos(th)) - sqrt((sin(th) ^ 2) * (vxA ^ 2) / ((ay ^ 2) * (cos(th) ^ 2)))),
                                sin(th) * vxA * vyB / (ay * cos(th)) != 0,
                                ay != 0)))
                    .SubstituteEqLs(vals)
                    .CheckVariable(vyB)
                    .SimplifyEquation()
                    .CheckVariable(vyB)
                    .AssertEqTo(
                        new And(
                            vyB == -35.010376910485483,
                            vyB != 0));

                DoubleFloat.tolerance = null;
            }

            #endregion

            #region PSE 5E P4.9

            {
                // In a local bar, a customer slides an empty beer mug
                // down the counter for a refill. The bartender is momentarily 
                // distracted and does not see the mug, which slides
                // off the counter and strikes the floor 1.40 m from the
                // base of the counter. If the height of the counter is 
                // 0.860 m, (a) with what velocity did the mug leave the
                // counter and (b) what was the direction of the mug’s 
                // velocity just before it hit the floor?
                
                var xA = new Symbol("xA");
                var yA = new Symbol("yA");

                var xB = new Symbol("xB");
                var yB = new Symbol("yB");

                var vxA = new Symbol("vxA");
                var vyA = new Symbol("vyA");

                var vxB = new Symbol("vxB");
                var vyB = new Symbol("vyB");

                var tAB = new Symbol("tAB");

                var ax = new Symbol("ax");
                var ay = new Symbol("ay");

                var thB = new Symbol("thB");
                var vB = new Symbol("vB");

                var eqs = new And(

                    vxB == vxA + ax * tAB,
                    vyB == vyA + ay * tAB,

                    tan(thB) == vyB / vxB,

                    xB == xA + vxA * tAB + ax * (tAB ^ 2) / 2,
                    yB == yA + vyA * tAB + ay * (tAB ^ 2) / 2,

                    xB != 0
                );

                var vals = new List<Equation>() { xA == 0, yA == 0.86, /* vxA */ vyA == 0, xB == 1.4, yB == 0, /* vxB vyB vB thB */ /* tAB */ ax == 0, ay == -9.8 };

                var zeros = vals.Where(eq => eq.b == 0).ToList();

                DoubleFloat.tolerance = 0.00001;

                eqs
                    .SubstituteEqLs(zeros)
                    .EliminateVariables(thB, vxB, vyB, tAB)
                    .IsolateVariable(vxA)
                    .LogicalExpand()
                    .AssertEqTo(
                        new Or(
                            new And(
                                vxA == ay * (xB ^ 2) / yA / 4 * sqrt(-8 / ay * (xB ^ -2) * yA),
                                2 / ay * (xB ^ -2) * yA != 0,
                                xB != 0),
                            new And(
                                vxA == -ay * (xB ^ 2) / yA / 4 * sqrt(-8 / ay * (xB ^ -2) * yA),
                                2 / ay * (xB ^ -2) * yA != 0,
                                xB != 0)))
                    .SubstituteEqLs(vals)
                    .AssertEqTo(new Or(vxA == -3.3417722634053204, vxA == 3.3417722634053204));

                eqs
                    .SubstituteEqLs(zeros)
                    .EliminateVariables(vxB, vyB, tAB, vxA)
                    .LogicalExpand()
                    .CheckVariable(xB)
                    .SimplifyLogical()
                    .IsolateVariable(thB)
                    .AssertEqTo(
                        new And(
                            -tan(thB) / ay != 0,
                            thB == new Atan(-2 * yA / xB),
                            xB != 0))
                    .SubstituteEqLs(vals)
                    .AssertEqTo(
                        new And(
                            0.1020408163265306 * tan(thB) != 0,
                            thB == -0.88760488150470185));

                DoubleFloat.tolerance = null;
            }

            #endregion

            #region SumDifferenceFormulaFunc

            // sin(u) cos(v) - cos(u) sin(v) -> sin(u - v)

            Func<MathObject, MathObject> SumDifferenceFormulaFunc = elt =>
            {
                if (elt is Sum)
                {
                    var items = new List<MathObject>();

                    foreach (var item in (elt as Sum).elts)
                    {
                        if (
                            item is Product &&
                            (item as Product).elts[0] == -1 &&
                            (item as Product).elts[1] is Cos &&
                            (item as Product).elts[2] is Sin
                            )
                        {
                            var u_ = ((item as Product).elts[1] as Cos).args[0];
                            var v_ = ((item as Product).elts[2] as Sin).args[0];

                            Func<MathObject, bool> match = obj =>
                                obj is Product &&
                                (obj as Product).elts[0] is Cos &&
                                (obj as Product).elts[1] is Sin &&

                                ((obj as Product).elts[1] as Sin).args[0] == u_ &&
                                ((obj as Product).elts[0] as Cos).args[0] == v_;

                            if (items.Any(obj => match(obj)))
                            {
                                items = items.Where(obj => match(obj) == false).ToList();

                                items.Add(sin(u_ - v_));
                            }
                            else items.Add(item);
                        }
                        else items.Add(item);
                    }

                    return new Sum() { elts = items }.Simplify();
                }

                return elt;
            };

            {
                var u = new Symbol("u");
                var v = new Symbol("v");

                (sin(u) * cos(v) - cos(u) * sin(v))
                    .DeepSelect(SumDifferenceFormulaFunc)
                    .AssertEqTo(sin(u - v));
            }

            #endregion
            
            #region SumDifferenceFormulaFunc 

            // sin(u) cos(v) + cos(u) sin(v) -> sin(u + v)

            Func<MathObject, MathObject> SumDifferenceFormulaAFunc = elt =>
            {
                if (elt is Sum)
                {
                    var items = new List<MathObject>();

                    foreach (var item in (elt as Sum).elts)
                    {
                        if (
                            item is Product &&
                            (item as Product).elts[0] is Cos &&
                            (item as Product).elts[1] is Sin
                            )
                        {
                            var u_ = ((item as Product).elts[0] as Cos).args[0];
                            var v_ = ((item as Product).elts[1] as Sin).args[0];

                            Func<MathObject, bool> match = obj =>
                                obj is Product &&
                                (obj as Product).elts[0] is Cos &&
                                (obj as Product).elts[1] is Sin &&

                                ((obj as Product).elts[1] as Sin).args[0] == u_ &&
                                ((obj as Product).elts[0] as Cos).args[0] == v_;

                            if (items.Any(obj => match(obj)))
                            {
                                items = items.Where(obj => match(obj) == false).ToList();

                                items.Add(sin(u_ + v_));
                            }
                            else items.Add(item);
                        }
                        else items.Add(item);
                    }

                    return new Sum() { elts = items }.Simplify();
                }

                return elt;
            };

            {
                var u = new Symbol("u");
                var v = new Symbol("v");

                (sin(u) * cos(v) + cos(u) * sin(v))
                    .DeepSelect(SumDifferenceFormulaAFunc)
                    .AssertEqTo(sin(u + v));
            }

            #endregion
            
            #region DoubleAngleFormulaFunc

            // sin(u) cos(u) -> sin(2 u) / 2

            Func<MathObject, MathObject> DoubleAngleFormulaFunc =
                    elt =>
                    {
                        if (elt is Product)
                        {
                            var items = new List<MathObject>();

                            foreach (var item in (elt as Product).elts)
                            {
                                if (item is Sin)
                                {
                                    var sym = (item as Sin).args.First();

                                    if (items.Any(obj => (obj is Cos) && (obj as Cos).args.First() == sym))
                                    {
                                        items = items.Where(obj => ((obj is Cos) && (obj as Cos).args.First() == sym) == false).ToList();

                                        items.Add(sin(2 * sym) / 2);
                                    }
                                    else items.Add(item);
                                }

                                else if (item is Cos)
                                {
                                    var sym = (item as Cos).args.First();

                                    if (items.Any(obj => (obj is Sin) && (obj as Sin).args.First() == sym))
                                    {
                                        items = items.Where(obj => ((obj is Sin) && (obj as Sin).args.First() == sym) == false).ToList();

                                        items.Add(sin(2 * sym) / 2);
                                    }
                                    else items.Add(item);
                                }

                                else items.Add(item);

                            }
                            return new Product() { elts = items }.Simplify();
                        }
                        return elt;
                    };

            #endregion

            #region SinCosToTanFunc


            // sin(x) / cos(x) -> tan(x)

            Func<MathObject, MathObject> SinCosToTanFunc = elt =>
            {
                if (elt is Product)
                {
                    if ((elt as Product).elts.Any(obj1 =>
                            obj1 is Sin &&
                            (elt as Product).elts.Any(obj2 => obj2 == 1 / cos((obj1 as Sin).args[0]))))
                    {
                        var sin_ = (elt as Product).elts.First(obj1 =>
                            obj1 is Sin &&
                            (elt as Product).elts.Any(obj2 => obj2 == 1 / cos((obj1 as Sin).args[0])));

                        var arg = (sin_ as Sin).args[0];

                        return elt * cos(arg) / sin(arg) * tan(arg);
                    }

                    return elt;
                }

                return elt;
            };

            #endregion

            {
                var x = new Symbol("x");
                var y = new Symbol("y");

                (sin(x) / cos(x)).DeepSelect(SinCosToTanFunc)

                    .AssertEqTo(tan(x));

                (y * sin(x) / cos(x)).DeepSelect(SinCosToTanFunc)

                    .AssertEqTo(tan(x) * y);

                (sin(x) * sin(y) / cos(x) / cos(y))
                    .DeepSelect(SinCosToTanFunc)
                    .DeepSelect(SinCosToTanFunc)

                    .AssertEqTo(tan(x) * tan(y));
            }
            
            #region PSE 5E P4.11

            {
                // One strategy in a snowball fight is to throw a first snowball
                // at a high angle over level ground. While your opponent is watching
                // the first one, you throw a second one at a low angle and timed
                // to arrive at your opponent before or at the same time as the first one.

                // Assume both snowballs are thrown with a speed of 25.0 m/s.

                // The first one is thrown at an angle of 70.0° with respect to the horizontal. 

                // (a) At what angle should the second (lowangle) 
                // snowball be thrown if it is to land at the same
                // point as the first?

                // (b) How many seconds later should the second snowball 
                // be thrown if it is to land at the same time as the first?
                
                var xA = new Symbol("xA");
                var yA = new Symbol("yA");

                var vxA = new Symbol("vxA");
                var vyA = new Symbol("vyA");

                var vA = new Symbol("vA");

                var thA = new Symbol("thA");

                var xB = new Symbol("xB");
                var yB = new Symbol("yB");

                var vxB = new Symbol("vxB");
                var vyB = new Symbol("vyB");

                var tAB = new Symbol("tAB");
                var tAC = new Symbol("tAC");

                var ax = new Symbol("ax");
                var ay = new Symbol("ay");

                var Pi = new Symbol("Pi");

                var eqs = new And(

                    vxA == vA * cos(thA),
                    vyA == vA * sin(thA),

                    vxB == vxA + ax * tAB,
                    vyB == vyA + ay * tAB,

                    xB == xA + vxA * tAB + ax * (tAB ^ 2) / 2,
                    yB == yA + vyA * tAB + ay * (tAB ^ 2) / 2
                );

                DoubleFloat.tolerance = 0.00001;

                {
                    var vals = new List<Equation>() { xA == 0, yA == 0, /* vxA vyA */ vA == 25.0, /* thA == 70.0, */ /* xB == 20.497, */ /* yB */ /* vxB */ vyB == 0, /* tAB */ ax == 0, ay == -9.8, Pi == Math.PI };

                    var zeros = vals.Where(eq => eq.b == 0).ToList();

                    {
                        // thA = ... || thA = ...

                        var expr = eqs
                            .SubstituteEqLs(zeros)
                            .EliminateVariables(yB, vxA, vyA, vxB, tAB)
                            .DeepSelect(DoubleAngleFormulaFunc)
                            .IsolateVariable(thA);

                        // th_delta = ...

                        var th1 = ((expr as Or).args[0] as Equation).b;
                        var th2 = ((expr as Or).args[1] as Equation).b;

                        var th_delta = new Symbol("th_delta");

                        eqs
                            .Add(th_delta == (th1 - th2).AlgebraicExpand())
                            .SubstituteEqLs(zeros)

                            .EliminateVariables(yB, vxA, vyA, vxB, tAB)

                            .DeepSelect(DoubleAngleFormulaFunc)
                            .EliminateVariable(xB)

                            .AssertEqTo(th_delta == asin(sin(2 * thA)) - Pi / 2)

                            .SubstituteEq(thA == (70).ToRadians())
                            .SubstituteEq(Pi == Math.PI)

                            .AssertEqTo(th_delta == -0.87266462599716454)
                            ;
                    }

                    {
                        // tAB = ...

                        var tAB_eq = eqs
                            .SubstituteEqLs(zeros)
                            .EliminateVariables(yB, vxA, vyA, vxB, xB)
                            .IsolateVariable(tAB);

                        new And(
                            new Or(thA == (20).ToRadians(), thA == (70).ToRadians()),
                            tAB_eq,
                            tAC == tAB * 2)
                            .LogicalExpand()
                            .EliminateVariables(thA, tAB)

                            .AssertEqTo(new Or(
                                tAC == -2 * sin(Pi / 9) * vA / ay,
                                tAC == -2 * sin(7 * Pi / 18) * vA / ay))

                            .SubstituteEqLs(vals)
                            .AssertEqTo(
                                new Or(
                                    tAC == 1.7450007312534115,
                                    tAC == 4.794350106050552));
                    }
                }

                DoubleFloat.tolerance = null;
            }

            #endregion


            #region PSE 5E P4.13

            {
                // An artillery shell is fired with an initial velocity of 
                // 300 m/s at 55.0° above the horizontal. It explodes on a
                // mountainside 42.0 s after firing. What are the x and y
                // coordinates of the shell where it explodes, relative to its
                // firing point?
                
                var xA = new Symbol("xA");
                var yA = new Symbol("yA");

                var vxA = new Symbol("vxA");
                var vyA = new Symbol("vyA");

                var vA = new Symbol("vA");
                var thA = new Symbol("thA");

                var xB = new Symbol("xB");
                var yB = new Symbol("yB");

                var vxB = new Symbol("vxB");
                var vyB = new Symbol("vyB");

                var tAB = new Symbol("tAB");

                var ax = new Symbol("ax");
                var ay = new Symbol("ay");

                var Pi = new Symbol("Pi");

                var eqs = new And(
                    vxA == vA * cos(thA),
                    vyA == vA * sin(thA),

                    vxB == vxA + ax * tAB,
                    vyB == vyA + ay * tAB,

                    xB == xA + vxA * tAB + ax * (tAB ^ 2) / 2,
                    yB == yA + vyA * tAB + ay * (tAB ^ 2) / 2
                );

                DoubleFloat.tolerance = 0.00001;

                {
                    var vals = new List<Equation>() { xA == 0, yA == 0, /* vxA vyA */ vA == 300.0, thA == (55).ToRadians(), /* xB yB vxB vyB */ tAB == 42, ax == 0, ay == -9.8, Pi == Math.PI };

                    var zeros = vals.Where(eq => eq.b == 0).ToList();

                    {
                        eqs
                            .SubstituteEqLs(zeros)
                            .EliminateVariable(vxA)
                            .EliminateVariable(vyA)

                            .AssertEqTo(
                                new And(
                                    vxB == cos(thA) * vA,
                                    vyB == ay * tAB + sin(thA) * vA,
                                    xB == cos(thA) * tAB * vA,
                                    yB == ay * (tAB ^ 2) / 2 + sin(thA) * tAB * vA))

                            .SubstituteEqLs(vals)

                            .AssertEqTo(
                                new And(
                                    vxB == 172.07293090531385,
                                    vyB == -165.85438671330249,
                                    xB == 7227.0630980231817,
                                    yB == 1677.7157580412968))

                            ;
                    }
                }

                DoubleFloat.tolerance = null;
            }

            #endregion

            #region PSE 5E P4.15

            {
                // A projectile is fired in such a way that its horizontal
                // range is equal to three times its maximum height.
                //
                // What is the angle of projection? 
                // Give your answer to three significant figures.
                
                var xA = new Symbol("xA");
                var yA = new Symbol("yA");

                var vxA = new Symbol("vxA");
                var vyA = new Symbol("vyA");

                var vA = new Symbol("vA");
                var thA = new Symbol("thA");


                var xB = new Symbol("xB");
                var yB = new Symbol("yB");

                var vxB = new Symbol("vxB");
                var vyB = new Symbol("vyB");


                var xC = new Symbol("xC");
                var yC = new Symbol("yC");

                var vxC = new Symbol("vxC");
                var vyC = new Symbol("vyC");


                var tAB = new Symbol("tAB");
                var tBC = new Symbol("tBC");

                var ax = new Symbol("ax");
                var ay = new Symbol("ay");

                var Pi = new Symbol("Pi");

                var eqs = new And(

                    xC - xA == 3 * yB,

                    tAB == tBC,


                    vxA == vA * cos(thA),
                    vyA == vA * sin(thA),

                    vxB == vxA + ax * tAB,
                    vyB == vyA + ay * tAB,

                    xB == xA + vxA * tAB + ax * (tAB ^ 2) / 2,
                    yB == yA + vyA * tAB + ay * (tAB ^ 2) / 2,


                    vxC == vxB + ax * tBC,
                    vyC == vyB + ay * tBC,

                    xC == xB + vxB * tBC + ax * (tBC ^ 2) / 2,
                    yC == yB + vyB * tBC + ay * (tBC ^ 2) / 2

                );

                DoubleFloat.tolerance = 0.00001;

                {
                    var vals = new List<Equation>() 
                    { 
                        xA == 0, yA == 0, /* vxA vyA vA thA */ /* xB yB vxB */ vyB == 0, /* tAB tBC */ 
                        /* xC */ yC == 0,

                        ax == 0, ay == -9.8, Pi == Math.PI 
                    };

                    var zeros = vals.Where(eq => eq.b == 0).ToList();

                    eqs
                        .SubstituteEqLs(zeros)
                        .EliminateVariables(xC, tAB, vxA, vyA, vxB, xB, yB, vxC, vyC, tBC)
                        .IsolateVariable(thA)
                        .AssertEqTo(thA == new Atan(new Integer(4) / 3));
                }

                DoubleFloat.tolerance = null;
            }

            #endregion

            #region PSE 5E P4.17

            {
                // A cannon with a muzzle speed of 1000 m/s is used to
                // start an avalanche on a mountain slope. The target is 
                // 2000 m from the cannon horizontally and 800 m above
                // the cannon.
                //
                // At what angle, above the horizontal, should the cannon be fired?

                var xA = new Symbol("xA");
                var yA = new Symbol("yA");

                var vxA = new Symbol("vxA");
                var vyA = new Symbol("vyA");

                var vA = new Symbol("vA");
                var thA = new Symbol("thA");


                var xB = new Symbol("xB");
                var yB = new Symbol("yB");

                var vxB = new Symbol("vxB");
                var vyB = new Symbol("vyB");


                var xC = new Symbol("xC");
                var yC = new Symbol("yC");

                var vxC = new Symbol("vxC");
                var vyC = new Symbol("vyC");


                var tAB = new Symbol("tAB");
                var tBC = new Symbol("tBC");

                var ax = new Symbol("ax");
                var ay = new Symbol("ay");

                var Pi = new Symbol("Pi");

                var phi = new Symbol("phi");

                var eqs = new And(

                    vxA == vA * cos(thA),
                    vyA == vA * sin(thA),

                    vxB == vxA + ax * tAB,
                    vyB == vyA + ay * tAB,

                    xB == xA + vxA * tAB + ax * (tAB ^ 2) / 2,
                    yB == yA + vyA * tAB + ay * (tAB ^ 2) / 2
                );

                DoubleFloat.tolerance = 0.00001;

                {
                    var vals = new List<Equation>() 
                    { 
                        xA ==    0, yA ==   0, /* vxA vyA */ vA == 1000, /* thA */ 
                        xB == 2000, yB == 800.0, /* vxB vyB */ 
                        /* tAB */ ax == 0, ay == -9.8, Pi == Math.PI 
                    };

                    var zeros = vals.Where(eq => eq.b == 0).ToList();

                    {
                        eqs
                            .SubstituteEqLs(zeros)
                            .EliminateVariables(vxA, vyA, vxB, vyB, tAB)

                            .MultiplyBothSidesBy(cos(thA) ^ 2).AlgebraicExpand()
                            .Substitute(cos(thA) ^ 2, (1 + cos(2 * thA)) / 2)
                            .DeepSelect(DoubleAngleFormulaFunc).AlgebraicExpand()
                            .AddToBothSides(-sin(2 * thA) * xB / 2)
                            .AddToBothSides(-yB / 2)
                            .MultiplyBothSidesBy(2 / xB).AlgebraicExpand()

                            // yB / xB = tan(phi)
                            // yB / xB = sin(phi) / cos(phi)

                            // phi = atan(yB / xB)

                            .Substitute(cos(2 * thA) * yB / xB, cos(2 * thA) * sin(phi) / cos(phi))
                            .MultiplyBothSidesBy(cos(phi)).AlgebraicExpand()
                            .DeepSelect(SumDifferenceFormulaFunc)
                            .IsolateVariable(thA)

                            .Substitute(phi, new Atan(yB / xB).Simplify())

                            .AssertEqTo(
                                new Or(
                                    thA == -(asin(ay * cos(atan(yB / xB)) * (vA ^ -2) * xB + -1 * cos(atan(yB / xB)) * yB / xB) - atan(yB / xB)) / 2,
                                    thA == -(-asin(ay * cos(atan(yB / xB)) * (vA ^ -2) * xB - cos(atan(yB / xB)) * yB / xB) - atan(yB / xB) + Pi) / 2))

                            .SubstituteEqLs(vals)

                            .AssertEqTo(
                                new Or(
                                    thA == 0.39034573609628065,
                                    thA == -1.5806356857788124))
                            ;
                    }
                }

                DoubleFloat.tolerance = null;
            }

            #endregion
            
            #region PSE 5E P4.19

            {
                // A placekicker must kick a football from a point 36.0 m
                // (about 40 yards) from the goal, and half the crowd
                // hopes the ball will clear the crossbar, which is 3.05 m
                // high. When kicked, the ball leaves the ground with a
                // speed of 20.0 m/s at an angle of 53.0° to the horizontal.
                //
                // (a) By how much does the ball clear or fall short of
                //     clearing the crossbar ?
                //
                // (b) Does the ball approach the crossbar while still 
                //     rising or while falling ?
                
                Func <MathObject, MathObject> sqrt = obj => obj ^ (new Integer(1) / 2);

                var xA = new Symbol("xA");
                var yA = new Symbol("yA");

                var vxA = new Symbol("vxA");
                var vyA = new Symbol("vyA");

                var vA = new Symbol("vA");
                var thA = new Symbol("thA");


                var xB = new Symbol("xB");
                var yB = new Symbol("yB");

                var vxB = new Symbol("vxB");
                var vyB = new Symbol("vyB");
                
                var tAB = new Symbol("tAB");
                
                var ax = new Symbol("ax");
                var ay = new Symbol("ay");

                var Pi = new Symbol("Pi");

                var cleared_by = new Symbol("cleared_by");

                var goal_height = new Symbol("goal_height");
                
                var eqs = new And(

                    vxA == vA * cos(thA),
                    vyA == vA * sin(thA),

                    vxB == vxA + ax * tAB,
                    vyB == vyA + ay * tAB,

                    xB == xA + vxA * tAB + ax * (tAB ^ 2) / 2,
                    yB == yA + vyA * tAB + ay * (tAB ^ 2) / 2,

                    cleared_by == yB - goal_height
                );

                DoubleFloat.tolerance = 0.00001;

                {
                    var vals = new List<Equation>()
                    {
                        xA == 0, yA == 0, /* vxA vyA */ vA == 20, thA == (53).ToRadians(),
                        xB == 36, /* yB */ /* vxB vyB */ 
                        /* tAB */ ax == 0, ay == -9.8, Pi == Math.PI,

                        goal_height == 3.05
                    };

                    var zeros = vals.Where(eq => eq.b == 0).ToList();

                    {
                        eqs
                            .SubstituteEqLs(zeros)
                            .EliminateVariables(vxA, vyA, vxB, vyB, tAB, yB)

                            .AssertEqTo(
                                cleared_by == -goal_height + sin(thA) / cos(thA) * xB + ay / 2 * (cos(thA) ^ -2) * (vA ^ -2) * (xB ^ 2)
                                )

                            .SubstituteEqLs(vals)

                            .AssertEqTo(cleared_by == 0.88921618776713007);
                    }

                    {
                        eqs
                            .SubstituteEqLs(zeros)

                            .EliminateVariables(cleared_by, vxA, vyA, vxB, tAB, yB)
                            .IsolateVariable(vyB)

                            .AssertEqTo(vyB == sin(thA) * vA + ay / cos(thA) / vA * xB)
                            
                            .SubstituteEqLs(vals)

                            .AssertEqTo(vyB == -13.338621888454744);
                    }
                }

                DoubleFloat.tolerance = null;
            }

            #endregion
            
            #region PSE 5E P4.21

            {
                // A firefighter a distance d from a burning building directs 
                // a stream of water from a fire hose at angle θi above
                // the horizontal as in Figure P4.20.If the initial speed of
                // the stream is vi, at what height h does the water strike
                // the building?
                
                var xA = new Symbol("xA");
                var yA = new Symbol("yA");

                var vxA = new Symbol("vxA");
                var vyA = new Symbol("vyA");

                var vA = new Symbol("vA");
                var thA = new Symbol("thA");


                var xB = new Symbol("xB");
                var yB = new Symbol("yB");

                var vxB = new Symbol("vxB");
                var vyB = new Symbol("vyB");

                var tAB = new Symbol("tAB");

                var ax = new Symbol("ax");
                var ay = new Symbol("ay");

                var Pi = new Symbol("Pi");

                var d = new Symbol("d");
                var thi = new Symbol("thi");
                var vi = new Symbol("vi");
                var h = new Symbol("h");
                
                var eqs = new And(

                    vxA == vA * cos(thA),
                    vyA == vA * sin(thA),

                    vxB == vxA + ax * tAB,
                    vyB == vyA + ay * tAB,

                    xB == xA + vxA * tAB + ax * (tAB ^ 2) / 2,
                    yB == yA + vyA * tAB + ay * (tAB ^ 2) / 2
                    
                );

                DoubleFloat.tolerance = 0.00001;

                {
                    var vals = new List<Equation>()
                    {
                        xA == 0, yA == 0, /* vxA vyA */ vA == vi, thA == thi,
                        xB == d, yB == h, /* vxB vyB */ 
                        /* tAB */ ax == 0, ay == -9.8, Pi == Math.PI
                    };

                    var zeros = vals.Where(eq => eq.b == 0).ToList();
                    
                    {
                        eqs
                            .SubstituteEqLs(zeros)
                            .EliminateVariables(vxA, vyA, vxB, vyB, tAB)
                            
                            .SubstituteEqLs(vals.Where(eq => eq.b is Symbol).ToList())

                            .AssertEqTo(

                                h == d * sin(thi) / cos(thi) + ay * (d ^ 2) / (cos(thi) ^ 2) / (vi ^ 2) / 2
                                
                                );
                    }
                }

                DoubleFloat.tolerance = null;
            }

            #endregion
            
            #region PSE 5E P4.23

            {
                // A basketball star covers 2.80 m horizontally in a jump to
                // dunk the ball. His motion through space can be modeled as 
                // that of a particle at a point called his center of mass. 
                // His center of mass is at elevation 1.02 m when he leaves 
                // the floor. It reaches a maximum height of 1.85 m above 
                // the floor and is at elevation 0.900 m when he touches down
                // again.

                // Determine:

                // (a) his time of flight (his “hang time”)

                // (b) his horizontal and (c) vertical velocity components at the instant of takeoff

                // (d) his takeoff angle. 

                // (e) For comparison, determine the hang time of a
                // whitetail deer making a jump with center-of-mass elevations
                // y_i = 1.20 m
                // y_max = 2.50 m
                // y_f = 0.700 m
                
                var xA = new Symbol("xA");
                var yA = new Symbol("yA");

                var vxA = new Symbol("vxA");
                var vyA = new Symbol("vyA");

                var vA = new Symbol("vA");
                var thA = new Symbol("thA");


                var xB = new Symbol("xB");
                var yB = new Symbol("yB");

                var vxB = new Symbol("vxB");
                var vyB = new Symbol("vyB");


                var tAB = new Symbol("tAB");


                var xC = new Symbol("xC");
                var yC = new Symbol("yC");

                var vxC = new Symbol("vxC");
                var vyC = new Symbol("vyC");


                var tBC = new Symbol("tBC");

                var tAC = new Symbol("tAC");

                var ax = new Symbol("ax");
                var ay = new Symbol("ay");

                var Pi = new Symbol("Pi");

                var eqs = new And(

                    //vxA == vA * cos(thA),
                    //vyA == vA * sin(thA),

                    vxB == vxA + ax * tAB,
                    vyB == vyA + ay * tAB,

                    xB == xA + vxA * tAB + ax * (tAB ^ 2) / 2,
                    yB == yA + vyA * tAB + ay * (tAB ^ 2) / 2,


                    vxC == vxB + ax * tBC,
                    vyC == vyB + ay * tBC,

                    xC == xB + vxB * tBC + ax * (tBC ^ 2) / 2,
                    yC == yB + vyB * tBC + ay * (tBC ^ 2) / 2,

                    tAC == tAB + tBC,

                    // vyA / vxA == tan(thA),

                    tan(thA) == vyA / vxA,

                    ay != 0

                );

                DoubleFloat.tolerance = 0.00001;

                {
                    var vals = new List<Equation>()
                    {
                        xA == 0,    yA == 1.02, /* vxA vyA vA thA */
                        /* xB */    yB == 1.85, /* vxB            */ vyB == 0,
                        xC == 2.80, yC == 0.9,  /* vxC vyC        */

                        /* tAB tBC */ ax == 0, ay == -9.8, Pi == Math.PI
                    };

                    var zeros = vals.Where(eq => eq.b == 0).ToList();

                    {
                        eqs
                            .SubstituteEqLs(zeros)

                            .EliminateVariables(thA, vxB, xB, vxC, vyC, vxA, vyA, tAB)

                            .CheckVariable(ay).SimplifyEquation().SimplifyLogical()

                            .EliminateVariable(tBC)

                            .LogicalExpand().SimplifyEquation().CheckVariable(ay).SimplifyLogical()

                            .AssertEqTo(

                                new Or(
                                    new And(ay != 0, tAC == (ay ^ -1) * sqrt(-2 * ay * (-1 * yA + yB)) + -1 * (ay ^ -1) * sqrt(2 * ay * (-1 * yB + yC))),
                                    new And(ay != 0, tAC == (ay ^ -1) * sqrt(-2 * ay * (-1 * yA + yB)) + (ay ^ -1) * sqrt(2 * ay * (-1 * yB + yC))),
                                    new And(ay != 0, tAC == -1 * (ay ^ -1) * sqrt(-2 * ay * (-1 * yA + yB)) + -1 * (ay ^ -1) * sqrt(2 * ay * (-1 * yB + yC))),
                                    new And(ay != 0, tAC == -1 * (ay ^ -1) * sqrt(-2 * ay * (-1 * yA + yB)) + (ay ^ -1) * sqrt(2 * ay * (-1 * yB + yC)))))

                            .SubstituteEqLs(vals)

                            .AssertEqTo(

                                new Or(
                                    tAC == 0.028747849043843032,
                                    tAC == -0.85188272280886768,
                                    tAC == 0.85188272280886768,
                                    tAC == -0.028747849043843032));
                    }

                    {
                        eqs
                            .SubstituteEqLs(zeros)

                            .EliminateVariables(thA, vxB, vxC, xB)

                            .IsolateVariable(vxA)

                            .EliminateVariables(tAC, vyC, tAB, vyA)

                            .SimplifyEquation().CheckVariable(ay)

                            .EliminateVariable(tBC)

                            .LogicalExpand().SimplifyEquation().CheckVariable(ay).SimplifyLogical()

                            .AssertEqTo(

                                new Or(
                                    new And(ay != 0, vxA == xC * ((-1 * sqrt(-2 * (ay ^ -1) * (-1 * yA + yB)) + -1 * (ay ^ -1) * sqrt(2 * ay * (-1 * yB + yC))) ^ -1)),
                                    new And(ay != 0, vxA == xC * ((-1 * sqrt(-2 * (ay ^ -1) * (-1 * yA + yB)) + (ay ^ -1) * sqrt(2 * ay * (-1 * yB + yC))) ^ -1)),
                                    new And(ay != 0, vxA == xC * ((sqrt(-2 * (ay ^ -1) * (-1 * yA + yB)) + -1 * (ay ^ -1) * sqrt(2 * ay * (-1 * yB + yC))) ^ -1)),
                                    new And(ay != 0, vxA == xC * ((sqrt(-2 * (ay ^ -1) * (-1 * yA + yB)) + (ay ^ -1) * sqrt(2 * ay * (-1 * yB + yC))) ^ -1))))

                            .SubstituteEqLs(vals)

                            .AssertEqTo(

                                new Or(
                                    vxA == 97.398591307814215,
                                    vxA == -3.286837407346058,
                                    vxA == 3.286837407346058,
                                    vxA == -97.398591307814215));
                    }

                    {
                        eqs
                            .SubstituteEqLs(zeros)

                            .EliminateVariables(thA, vxA, vxC, vyC, vxB, xB, tAB, tAC, tBC)

                            .SimplifyEquation().CheckVariable(ay).SimplifyLogical()

                            .IsolateVariable(vyA)

                            .LogicalExpand().SimplifyEquation().CheckVariable(ay)

                            .AssertEqTo(
                                new Or(
                                    new And(ay != 0, vyA == ay * sqrt(-2 * (ay ^ -1) * (-1 * yA + yB))),
                                    new And(ay != 0, vyA == -1 * ay * sqrt(-2 * (ay ^ -1) * (-1 * yA + yB)))))

                            .SubstituteEqLs(vals)

                            .AssertEqTo(
                                new Or(
                                    vyA == -4.0333608814486217,
                                    vyA == 4.0333608814486217));
                    }

                    {
                        eqs
                            .SubstituteEqLs(zeros)

                            .EliminateVariables(vxA, vyA, vxB, xB, vxC, tBC, tAB, vyC, tAC)

                            .LogicalExpand()
                            .SimplifyEquation()
                            .SimplifyLogical()
                            .CheckVariable(ay)

                            .AssertEqTo(

                                new Or(
                                    new And(ay != 0, tan(thA) == -1 * (xC ^ -1) * sqrt(-2 * ay * (-1 * yA + yB)) * ((ay ^ -1) * sqrt(-2 * ay * (-1 * yA + yB)) + -1 * sqrt(2 * (ay ^ -1) * (-1 * yB + yC)))),
                                    new And(ay != 0, tan(thA) == -1 * (xC ^ -1) * sqrt(-2 * ay * (-1 * yA + yB)) * ((ay ^ -1) * sqrt(-2 * ay * (-1 * yA + yB)) + sqrt(2 * (ay ^ -1) * (-1 * yB + yC)))),
                                    new And(ay != 0, tan(thA) == (xC ^ -1) * sqrt(-2 * ay * (-1 * yA + yB)) * (-1 * (ay ^ -1) * sqrt(-2 * ay * (-1 * yA + yB)) + -1 * sqrt(2 * (ay ^ -1) * (-1 * yB + yC)))),
                                    new And(ay != 0, tan(thA) == (xC ^ -1) * sqrt(-2 * ay * (-1 * yA + yB)) * (-1 * (ay ^ -1) * sqrt(-2 * ay * (-1 * yA + yB)) + sqrt(2 * (ay ^ -1) * (-1 * yB + yC))))

                                    ))

                            .IsolateVariable(thA)

                            .SubstituteEqLs(vals)

                            .AssertEqTo(
                                new Or(
                                    thA == 0.88702813023277882,
                                    thA == -0.041387227947930878,
                                    thA == -0.041387227947930878,
                                    thA == 0.88702813023277882));

                    }
                }

                DoubleFloat.tolerance = null;
            }

            #endregion

            #region PSE 5E E5.1

            {
                // A hockey puck having a mass of 0.30 kg slides on the horizontal, 
                // frictionless surface of an ice rink. Two forces act on
                // the puck, as shown in Figure 5.5.The force F1 has a magnitude 
                // of 5.0 N, and the force F2 has a magnitude of 8.0 N.

                // Determine both the magnitude and the direction of the puck’s acceleration.

                // Determine the components of a third force that,
                // when applied to the puck, causes it to have zero acceleration.
                
                var F = new Symbol("F");
                var th = new Symbol("th");

                var Fx = new Symbol("Fx");
                var Fy = new Symbol("Fy");


                var F1 = new Symbol("F1");
                var th1 = new Symbol("th1");

                var F1x = new Symbol("F1x");
                var F1y = new Symbol("F1y");
                

                var F2 = new Symbol("F2");
                var th2 = new Symbol("th2");

                var F2x = new Symbol("F2x");
                var F2y = new Symbol("F2y");


                var F3 = new Symbol("F3");
                var th3 = new Symbol("th3");

                var F3x = new Symbol("F3x");
                var F3y = new Symbol("F3y");


                var a = new Symbol("a");

                var ax = new Symbol("ax");
                var ay = new Symbol("ay");

                var m = new Symbol("m");

                var Pi = new Symbol("Pi");

                var eqs = new And(

                    Fx == F * cos(th),
                    Fy == F * sin(th),

                    Fx == ax * m,
                    Fy == ay * m,

                    Fx == F1x + F2x + F3x,
                    Fy == F1y + F2y + F3y,

                    F1x == F1 * cos(th1), F1y == F1 * sin(th1),

                    F2x == F2 * cos(th2), F2y == F2 * sin(th2),

                    F3x == F3 * cos(th3), F3y == F3 * sin(th3),

                    a == sqrt((ax ^ 2) + (ay ^ 2))

                    );

                DoubleFloat.tolerance = 0.00001;

                {
                    var vals = new List<Equation>()
                    {

                        m == 0.3,

                        F1 == 5.0, th1 == (-20).ToRadians(),
                        F2 == 8.0, th2 == (60).ToRadians(),

                        F3 == 0,

                        Pi == Math.PI
                    };

                    var zeros = vals.Where(eq => eq.b == 0).ToList();

                    // a 
                    {
                        eqs
                            .SubstituteEqLs(zeros)
                            .EliminateVariables(ax, ay, Fx, Fy, F, F1x, F1y, F2x, F2y, F3x, F3y)
                            .DeepSelect(SinCosToTanFunc)
                            .EliminateVariable(th)
                            
                            .AssertEqTo(

                                a ==
                                    sqrt(
                                        ((cos(th1) * F1 + cos(th2) * F2) ^ 2) * (m ^ -2) +
                                        (cos(atan(((cos(th1) * F1 + cos(th2) * F2) ^ -1) * (F1 * sin(th1) + F2 * sin(th2)))) ^ -2) *
                                        ((cos(th1) * F1 + cos(th2) * F2) ^ 2) *
                                        (m ^ -2) *
                                        (sin(atan(((cos(th1) * F1 + cos(th2) * F2) ^ -1) * (F1 * sin(th1) + F2 * sin(th2)))) ^ 2))

                            )

                            .SubstituteEqLs(vals)

                            .Substitute(3, 3.0)

                            //.DispLong()

                            .AssertEqTo(a == 33.811874017759315);
                    }

                    // th
                    {
                        eqs
                            .SubstituteEqLs(zeros)
                            .EliminateVariables(a, F, Fx, Fy, ax, ay, F1x, F1y, F2x, F2y, F3x, F3y)
                            .DeepSelect(SinCosToTanFunc)
                            .IsolateVariable(th)

                            .AssertEqTo(

                                th == atan((F1 * sin(th1) + F2 * sin(th2)) / (cos(th1) * F1 + cos(th2) * F2))

                                )

                            .SubstituteEqLs(vals)

                            .Substitute(3, 3.0)

                            .AssertEqTo(th == 0.54033704850428876);
                    }
                }

                {
                    var vals = new List<Equation>()
                    {

                        m == 0.3,

                        F1 == 5.0, th1 == (-20).ToRadians(),
                        F2 == 8.0, th2 == (60).ToRadians(),

                        ax == 0, ay == 0,

                        Pi == Math.PI
                    };

                    var zeros = vals.Where(eq => eq.b == 0).ToList();
                    
                    // F3x
                    {
                        eqs
                            .SubstituteEqLs(zeros)
                            .EliminateVariables(F3, th3, F3y, F1x, F2x, Fx, F, Fy, F1y, F2y, a)
                            .IsolateVariable(F3x)
                            
                            .AssertEqTo(F3x == -1 * cos(th1) * F1 + -1 * cos(th2) * F2)

                            .SubstituteEqLs(vals)
                            
                            .AssertEqTo(F3x == -8.6984631039295444);
                    }


                    // F3y
                    {
                        eqs
                            .SubstituteEqLs(zeros)
                            .EliminateVariables(F3, th3, F3x, F1x, F2x, Fx, F, Fy, F1y, F2y, a)
                            .IsolateVariable(F3y)

                            .AssertEqTo(F3y == -1 * F1 * sin(th1) + -1 * F2 * sin(th2))
                            
                            .SubstituteEqLs(vals)

                            // .DispLong()

                            .Substitute(3, 3.0)

                            .AssertEqTo(F3y == -5.2181025136471657);
                    }   
                }
            }

            #endregion
            
            #region PSE 5E E5.4

            {
                // A traffic light weighing 125 N hangs from a cable tied to two
                // other cables fastened to a support. The upper cables make
                // angles of 37.0° and 53.0° with the horizontal. Find the tension
                // in the three cables.
                
                var F = new Symbol("F");    // total force magnitude
                var th = new Symbol("th");  // total force direction

                var Fx = new Symbol("Fx");  // total force x-component
                var Fy = new Symbol("Fy");  // total force y-component


                var F1 = new Symbol("F1");
                var th1 = new Symbol("th1");

                var F1x = new Symbol("F1x");
                var F1y = new Symbol("F1y");


                var F2 = new Symbol("F2");
                var th2 = new Symbol("th2");

                var F2x = new Symbol("F2x");
                var F2y = new Symbol("F2y");


                var F3 = new Symbol("F3");
                var th3 = new Symbol("th3");

                var F3x = new Symbol("F3x");
                var F3y = new Symbol("F3y");


                var a = new Symbol("a");

                var ax = new Symbol("ax");
                var ay = new Symbol("ay");

                var m = new Symbol("m");

                var Pi = new Symbol("Pi");

                var eqs = new And(

                    Fx == F * cos(th),
                    Fy == F * sin(th),

                    Fx == ax * m,
                    Fy == ay * m,

                    Fx == F1x + F2x + F3x,
                    Fy == F1y + F2y + F3y,

                    F1x == F1 * cos(th1), F1y == F1 * sin(th1),
                    F2x == F2 * cos(th2), F2y == F2 * sin(th2),
                    F3x == F3 * cos(th3), F3y == F3 * sin(th3),

                    a == sqrt((ax ^ 2) + (ay ^ 2))

                    );

                DoubleFloat.tolerance = 0.00001;

                {
                    var vals = new List<Equation>()
                    {

                        // m 

                        /* F1 */    th1 == (180 - 37).ToRadians(),  // F1x F1y
                        /* F2 */    th2 == (53).ToRadians(),        // F2x F2y
                        F3 == 125,  th3 == (270).ToRadians(),       // F3x F3y
                        
                        ax == 0,    ay == 0,

                        Pi == Math.PI
                    };

                    var zeros = vals.Where(eq => eq.b == 0).ToList();

                    // F1
                    {
                        eqs
                            .SubstituteEqLs(zeros)
                            .EliminateVariables(Fx, Fy, F, F1x, F1y, F2x, F2y, F2, F3x, F3y, a)
                            .IsolateVariable(F1)
                            
                            .AssertEqTo(F1 == (F3 * sin(th3) - cos(th3) * F3 * sin(th2) / cos(th2)) / (cos(th1) * sin(th2) / cos(th2) - sin(th1)))

                            .SubstituteEqLs(vals)

                            .AssertEqTo(F1 == 75.226877894006023);
                    }

                    // F2
                    {
                        eqs
                            .SubstituteEqLs(zeros)
                            .EliminateVariables(Fx, Fy, F, F1x, F1y, F2x, F2y, F1, F3x, F3y, a)
                            .IsolateVariable(F2)

                            .AssertEqTo(F2 == (cos(th3) * F3 * sin(th1) / cos(th1) - F3 * sin(th3)) / (sin(th2) - cos(th2) * sin(th1) / cos(th1)))
                            
                            .SubstituteEqLs(vals)

                            .AssertEqTo(F2 == 99.829438755911582);
                    }

                }
            }

            #endregion
            
            #region PSE 5E E5.6

            {
                // A crate of mass m is placed on a frictionless inclined plane of
                // angle θ. (a) Determine the acceleration of the crate after it is
                // released.

                // (b) Suppose the crate is released from rest at the top of
                // the incline, and the distance from the front edge of the crate
                // to the bottom is d. How long does it take the front edge to
                // reach the bottom, and what is its speed just as it gets there?
                
                var F = new Symbol("F");    // total force magnitude
                var th = new Symbol("th");  // total force direction

                var Fx = new Symbol("Fx");  // total force x-component
                var Fy = new Symbol("Fy");  // total force y-component


                var F1 = new Symbol("F1");
                var th1 = new Symbol("th1");

                var F1x = new Symbol("F1x");
                var F1y = new Symbol("F1y");


                var F2 = new Symbol("F2");
                var th2 = new Symbol("th2");

                var F2x = new Symbol("F2x");
                var F2y = new Symbol("F2y");


                //var F3 = new Symbol("F3");
                //var th3 = new Symbol("th3");

                //var F3x = new Symbol("F3x");
                //var F3y = new Symbol("F3y");


                var a = new Symbol("a");

                var ax = new Symbol("ax");
                var ay = new Symbol("ay");

                var m = new Symbol("m");

                var n = new Symbol("n");
                var g = new Symbol("g");

                var incline = new Symbol("incline");

                var Pi = new Symbol("Pi");
                
                var xA = new Symbol("xA");
                var yA = new Symbol("yA");

                var vxA = new Symbol("vxA");
                var vyA = new Symbol("vyA");

                var vA = new Symbol("vA");
                var thA = new Symbol("thA");


                var xB = new Symbol("xB");
                var yB = new Symbol("yB");

                var vxB = new Symbol("vxB");
                var vyB = new Symbol("vyB");


                var tAB = new Symbol("tAB");
                
                var d = new Symbol("d");
                
                var eqs = new And(

                    Fx == F * cos(th),
                    Fy == F * sin(th),

                    Fx == ax * m,
                    Fy == ay * m,

                    Fx == F1x + F2x, //+ F3x,
                    Fy == F1y + F2y, //+ F3y,

                    F1x == F1 * cos(th1), F1y == F1 * sin(th1),
                    F2x == F2 * cos(th2), F2y == F2 * sin(th2),
                    //F3x == F3 * cos(th3), F3y == F3 * sin(th3),

                    a == sqrt((ax ^ 2) + (ay ^ 2)),

                    xB == xA + vxA * tAB + ax * (tAB ^ 2) / 2,

                    vxB == vxA + ax * tAB,

                    d != 0

                    );

                DoubleFloat.tolerance = 0.00001;

                {
                    var vals = new List<Equation>()
                    {

                        // m 
                        
                        F1 == n,        th1 == 90 * Pi / 180,            // F1x F1y
                        F2 == m * g,    th2 == 270 * Pi / 180 + incline, // F2x F2y
                        //F3 == 125,    th3 == (270).ToRadians(),        // F3x F3y
                        
                        /* ax */  ay == 0,

                        // Pi == Math.PI

                        xA == 0, xB == d, vxA == 0
                    };

                    var zeros = vals.Where(eq => eq.b == 0).ToList();

                    // ax
                    {
                        eqs
                            .SubstituteEqLs(zeros)
                            .EliminateVariables(a, F)
                            .DeepSelect(SinCosToTanFunc)
                            .EliminateVariables(th, Fx, F1x, F2x, Fy, F1y, F2y, vxB, xB)
                            .SubstituteEqLs(vals)
                            .EliminateVariable(n)
                            .IsolateVariable(ax)

                            .AssertEqTo(
                                new And(
                                    ax == g * sin(incline),
                                    d != 0));
                    }

                    // tAB
                    {
                        eqs
                            .SubstituteEqLs(zeros)
                            .EliminateVariables(a, F)
                            .DeepSelect(SinCosToTanFunc)
                            .EliminateVariables(th, Fx, F1x, F2x, Fy, F1y, F2y, ax, vxB)
                            .SubstituteEqLs(vals)
                            .EliminateVariable(n)
                            .IsolateVariable(tAB).LogicalExpand().CheckVariable(d)
                            
                            .AssertEqTo(
                                new Or(
                                    new And(
                                        tAB == - sqrt(2 * d * g * sin(incline)) / sin(incline) / g,
                                        - g * sin(incline) / 2 != 0,
                                        d != 0),
                                    new And(
                                        tAB == sqrt(2 * d * g * sin(incline)) / sin(incline) / g,
                                        -g * sin(incline) / 2 != 0,
                                        d != 0))
                            );
                    }

                    // vxB
                    {
                        eqs
                            .SubstituteEqLs(zeros)
                            .EliminateVariables(a, F)
                            .DeepSelect(SinCosToTanFunc)
                            .EliminateVariables(th, Fx, F1x, F2x, Fy, F1y, F2y, ax, tAB)
                            .SubstituteEqLs(vals)
                            .CheckVariable(d)
                            .EliminateVariable(n)

                            .AssertEqTo(
                                new Or(
                                    new And(
                                        -g * sin(incline) / 2 != 0,
                                        vxB == -sqrt(2 * d * g * sin(incline)),
                                        d != 0
                                    ),
                                    new And(
                                        -g * sin(incline) / 2 != 0,
                                        vxB == sqrt(2 * d * g * sin(incline)),
                                        d != 0))
                            );
                    }
                }
            }

            #endregion
            
            #region PSE 5E E5.9

            {
                // When two objects of unequal mass are hung vertically over a
                // frictionless pulley of negligible mass, as shown in Figure
                // 5.15a, the arrangement is called an Atwood machine. The device 
                // is sometimes used in the laboratory to measure the freefall
                // acceleration.
                //
                // Determine the magnitude of the acceleration of the two 
                // objects and the tension in the lightweight cord.
                
                var F_m1 = new Symbol("F_m1");      // total force on mass 1
                var F_m2 = new Symbol("F_m2");      // total force on mass 2

                var F1_m1 = new Symbol("F1_m1");    // force 1 on mass 1
                var F2_m1 = new Symbol("F2_m1");    // force 2 on mass 1

                var F1_m2 = new Symbol("F1_m2");    // force 1 on mass 2
                var F2_m2 = new Symbol("F2_m2");    // force 2 on mass 2

                var m1 = new Symbol("m1");
                var m2 = new Symbol("m2");

                var a = new Symbol("a");

                var T = new Symbol("T");

                var g = new Symbol("g");


                var eqs = new And(

                    F_m1 == F1_m1 - F2_m1,
                    F_m2 == F2_m2 - F1_m2,

                    F_m1 == m1 * a,
                    F_m2 == m2 * a,

                    F1_m1 == T,
                    F2_m1 == m1 * g,

                    F1_m2 == T,
                    F2_m2 == m2 * g
                    
                    );

                DoubleFloat.tolerance = 0.00001;

                {
                    var vals = new List<Equation>()
                    {
                        m1 == 2.0, m2 == 4.0, g == 9.8
                    };

                    var zeros = vals.Where(eq => eq.b == 0).ToList();

                    // a
                    {
                        eqs
                            .EliminateVariables(F_m1, F_m2, F2_m1, F2_m2, F1_m1, F1_m2, T)
                            .IsolateVariable(a)
                            
                            .AssertEqTo(
                                a == (-1 * g * m1 + g * m2) / (m1 + m2)
                            )

                            .SubstituteEqLs(vals)
                            
                            .AssertEqTo(a == 3.2666666666666666);
                    }

                    // T
                    {
                        eqs
                            .EliminateVariables(F_m1, F_m2, F2_m1, F2_m2, F1_m1, F1_m2, a)
                            .IsolateVariable(T)
                            
                            .AssertEqTo(
                                T == 2 * g * m2 / (1 + m2 / m1)
                            )

                            .SubstituteEqLs(vals)
                            
                            .AssertEqTo(
                                T == 26.133333333333333
                            );
                    }
                    
                }
            }

            #endregion
            
            #region PSE 5E E5.10 - Acceleration of Two Objects Connected by a Cord

            {
                // A ball of mass m1 and a block of mass m2 are attached by a
                // lightweight cord that passes over a frictionless pulley of 
                // negligible mass, as shown in Figure 5.16a. The block lies 
                // on a frictionless incline of angle th. Find the magnitude 
                // of the acceleration of the two objects and the tension in the cord.

                ////////////////////////////////////////////////////////////////////////////////

                var F1_m1 = new Symbol("F1_m1");        // force 1 on mass 1
                var F2_m1 = new Symbol("F2_m1");        // force 2 on mass 1
                var F3_m1 = new Symbol("F3_m1");        // force 3 on mass 1

                var th1_m1 = new Symbol("th1_m1");      // direction of force 1 on mass 1
                var th2_m1 = new Symbol("th2_m1");      // direction of force 2 on mass 1
                var th3_m1 = new Symbol("th3_m1");      // direction of force 3 on mass 1

                var F1x_m1 = new Symbol("F1x_m1");      // x-component of force 1 on mass 1
                var F2x_m1 = new Symbol("F2x_m1");      // x-component of force 2 on mass 1
                var F3x_m1 = new Symbol("F3x_m1");      // x-component of force 3 on mass 1

                var F1y_m1 = new Symbol("F1y_m1");      // y-component of force 1 on mass 1
                var F2y_m1 = new Symbol("F2y_m1");      // y-component of force 2 on mass 1
                var F3y_m1 = new Symbol("F3y_m1");      // y-component of force 3 on mass 1

                var Fx_m1 = new Symbol("Fx_m1");        // x-component of total force on mass 1
                var Fy_m1 = new Symbol("Fy_m1");        // y-component of total force on mass 1

                var ax_m1 = new Symbol("ax_m1");        // x-component of acceleration of mass 1
                var ay_m1 = new Symbol("ay_m1");        // y-component of acceleration of mass 1

                var m1 = new Symbol("m1");

                ////////////////////////////////////////////////////////////////////////////////

                var F1_m2 = new Symbol("F1_m2");        // force 1 on mass 2
                var F2_m2 = new Symbol("F2_m2");        // force 2 on mass 2
                var F3_m2 = new Symbol("F3_m2");        // force 3 on mass 2

                var th1_m2 = new Symbol("th1_m2");      // direction of force 1 on mass 2
                var th2_m2 = new Symbol("th2_m2");      // direction of force 2 on mass 2
                var th3_m2 = new Symbol("th3_m2");      // direction of force 3 on mass 2

                var F1x_m2 = new Symbol("F1x_m2");      // x-component of force 1 on mass 2
                var F2x_m2 = new Symbol("F2x_m2");      // x-component of force 2 on mass 2
                var F3x_m2 = new Symbol("F3x_m2");      // x-component of force 3 on mass 2

                var F1y_m2 = new Symbol("F1y_m2");      // y-component of force 1 on mass 2
                var F2y_m2 = new Symbol("F2y_m2");      // y-component of force 2 on mass 2
                var F3y_m2 = new Symbol("F3y_m2");      // y-component of force 3 on mass 2

                var Fx_m2 = new Symbol("Fx_m2");        // x-component of total force on mass 2
                var Fy_m2 = new Symbol("Fy_m2");        // y-component of total force on mass 2

                var ax_m2 = new Symbol("ax_m2");        // x-component of acceleration of mass 2
                var ay_m2 = new Symbol("ay_m2");        // y-component of acceleration of mass 2

                var m2 = new Symbol("m2");

                ////////////////////////////////////////////////////////////////////////////////

                var incline = new Symbol("incline");

                var T = new Symbol("T");                // tension in cable

                var g = new Symbol("g");                // gravity

                var n = new Symbol("n");                // normal force on block

                var a = new Symbol("a");

                var Pi = new Symbol("Pi");

                var eqs = new And(

                    ax_m2 == ay_m1,                     // the block moves right as the ball moves up

                    ////////////////////////////////////////////////////////////////////////////////

                    F1x_m1 == F1_m1 * cos(th1_m1),
                    F2x_m1 == F2_m1 * cos(th2_m1),
                    F3x_m1 == F3_m1 * cos(th3_m1),

                    F1y_m1 == F1_m1 * sin(th1_m1),
                    F2y_m1 == F2_m1 * sin(th2_m1),
                    F3y_m1 == F3_m1 * sin(th3_m1),

                    Fx_m1 == F1x_m1 + F2x_m1 + F3x_m1,
                    Fy_m1 == F1y_m1 + F2y_m1 + F3y_m1,

                    Fx_m1 == m1 * ax_m1,
                    Fy_m1 == m1 * ay_m1,

                    ////////////////////////////////////////////////////////////////////////////////

                    F1x_m2 == F1_m2 * cos(th1_m2),
                    F2x_m2 == F2_m2 * cos(th2_m2),
                    F3x_m2 == F3_m2 * cos(th3_m2),

                    F1y_m2 == F1_m2 * sin(th1_m2),
                    F2y_m2 == F2_m2 * sin(th2_m2),
                    F3y_m2 == F3_m2 * sin(th3_m2),

                    Fx_m2 == F1x_m2 + F2x_m2 + F3x_m2,
                    Fy_m2 == F1y_m2 + F2y_m2 + F3y_m2,

                    Fx_m2 == m2 * ax_m2,
                    Fy_m2 == m2 * ay_m2,

                    ////////////////////////////////////////////////////////////////////////////////
                    
                    a == ax_m2
                    
                    );

                DoubleFloat.tolerance = 0.00001;

                {
                    var vals = new List<Equation>()
                    {
                        ax_m1 == 0,                         // ball  moves vertically
                        ay_m2 == 0,                         // block moves horizontally

                        F1_m1 == T,
                        F2_m1 == m1 * g,
                        F3_m1 == 0,

                        th1_m1 == 90 * Pi / 180,            // force 1 is straight up
                        th2_m1 == 270 * Pi / 180,           // force 2 is straight down

                        F1_m2 == n,
                        F2_m2 == T,
                        F3_m2 == m2 * g,

                        th1_m2 == 90 * Pi / 180,            // force 1 is straight up
                        th2_m2 == 180 * Pi / 180,           // force 2 is straight down
                        th3_m2 == 270 * Pi / 180 + incline  // force 3 direction

                    };

                    var zeros = vals.Where(eq => eq.b == 0).ToList();
                    
                    // a
                    {
                        eqs
                            .SubstituteEqLs(vals)

                            .EliminateVariables(
                                F1x_m1, F2x_m1, F3x_m1,
                                F1y_m1, F2y_m1, F3y_m1,
                                
                                Fx_m1, Fy_m1,

                                F1x_m2, F2x_m2, F3x_m2,
                                F1y_m2, F2y_m2, F3y_m2,

                                Fx_m2, Fy_m2,                                
                                
                                ax_m2, n, T, ay_m1
                            )
                            
                            .AssertEqTo(
                            
                                a == (g * m1 - g * m2 * sin(incline)) / (-m1 - m2)

                            )

                            .SubstituteEq(m1 == 10.0)
                            .SubstituteEq(m2 == 5.0)
                            .SubstituteEq(incline == 45 * Math.PI / 180)
                            .SubstituteEq(g == 9.8)

                            .AssertEqTo(a == -4.2234511814572784);
                    }
                    
                    // T
                    {
                        eqs
                            .SubstituteEqLs(vals)

                            .EliminateVariables(
                                F1x_m1, F2x_m1, F3x_m1,
                                F1y_m1, F2y_m1, F3y_m1,

                                Fx_m1, Fy_m1,

                                F1x_m2, F2x_m2, F3x_m2,
                                F1y_m2, F2y_m2, F3y_m2,

                                Fx_m2, Fy_m2,

                                ax_m2, n, a, ay_m1
                            )
                            
                            .IsolateVariable(T)
                            .RationalizeExpression()
                            
                            .AssertEqTo(

                                T == m1 * (-g * m2 - g * m2 * sin(incline)) / (-m1 - m2)

                            );
                    }
                }
            }

            #endregion

            #region PSE 5E E5.10 - Acceleration of Two Objects Connected by a Cord - Obj3

            {
                // A ball of mass m1 and a block of mass m2 are attached by a
                // lightweight cord that passes over a frictionless pulley of 
                // negligible mass, as shown in: 
                //
                //      http://i.imgur.com/XMHM6On.png
                //
                // The block lies on a frictionless incline of angle th.
                //
                // Find the magnitude of the acceleration of the two objects
                // and the tension in the cord.

                var bal = new Obj2("bal");
                var blk = new Obj3("blk");

                var th = new Symbol("th");

                var T = new Symbol("T");                // tension in cable
                var g = new Symbol("g");                // gravity
                var n = new Symbol("n");                // normal force on block
                var a = new Symbol("a");

                var m1 = new Symbol("m1");
                var m2 = new Symbol("m2");

                var Pi = new Symbol("Pi");

                var eqs = new And(

                    blk.ax == bal.ay,                   // the block moves right as the ball moves up

                    a == blk.ax,

                    bal.Equations(),
                    blk.Equations()

                    );

                DoubleFloat.tolerance = 0.00001;

                var vals = new List<Equation>
                {
                    bal.ax == 0,

                    bal.m == m1,

                    bal.F1 == T,            bal.th1 == (90).ToRadians(),                // force 1 is straight up
                    bal.F2 == m1 * g,       bal.th2 == (270).ToRadians(),               // force 2 is straight down

                    blk.ay == 0,

                    blk.m == m2,

                    blk.F1 == n,            blk.th1 == (90).ToRadians(),                // force 1 is straight up
                    blk.F2 == T,            blk.th2 == (180).ToRadians(),               // force 2 is straight down
                    blk.F3 == m2 * g,       blk.th3 == (270).ToRadians() + th           // force 3 direction
                };

                // a
                eqs
                    .SubstituteEqLs(vals)

                    .EliminateVariables(

                        bal.ΣFx, bal.F1x, bal.F2x,
                        bal.ΣFy, bal.F1y, bal.F2y,

                        blk.ΣFx, blk.F1x, blk.F2x, blk.F3x,
                        blk.ΣFy, blk.F1y, blk.F2y, blk.F3y,

                        blk.ax, bal.ay,

                        T, n
                    )

                    .IsolateVariable(a)

                    .AssertEqTo(

                        a == (g * m1 - g * m2 * sin(th)) / (-m1 - m2)

                    );

                // T
                eqs
                    .SubstituteEqLs(vals)

                    .EliminateVariables(

                        bal.ΣFx, bal.F1x, bal.F2x,
                        bal.ΣFy, bal.F1y, bal.F2y,

                        blk.ΣFx, blk.F1x, blk.F2x, blk.F3x,
                        blk.ΣFy, blk.F1y, blk.F2y, blk.F3y,

                        blk.ax, bal.ay,

                        a, n
                    )

                .IsolateVariable(T)

                .RationalizeExpression()

                .AssertEqTo(

                    T == m1 * (-g * m2 - g * m2 * sin(th)) / (-m1 - m2)

                );
            }

            #endregion

            #region PSE 5E E5.12 - Experimental Determination of μs and μk

            {
                // The following is a simple method of measuring coefficients of
                // friction: Suppose a block is placed on a rough surface
                // inclined relative to the horizontal, as shown in Figure 5.19. 
                // The incline angle is increased until the block starts to move. 
                // Let us show that by measuring the critical angle θ_c at which this
                // slipping just occurs, we can obtain μs.

                ////////////////////////////////////////////////////////////////////////////////

                var F1_m1 = new Symbol("F1_m1");        // force 1 on mass 1
                var F2_m1 = new Symbol("F2_m1");        // force 2 on mass 1
                var F3_m1 = new Symbol("F3_m1");        // force 3 on mass 1

                var th1_m1 = new Symbol("th1_m1");      // direction of force 1 on mass 1
                var th2_m1 = new Symbol("th2_m1");      // direction of force 2 on mass 1
                var th3_m1 = new Symbol("th3_m1");      // direction of force 3 on mass 1

                var F1x_m1 = new Symbol("F1x_m1");      // x-component of force 1 on mass 1
                var F2x_m1 = new Symbol("F2x_m1");      // x-component of force 2 on mass 1
                var F3x_m1 = new Symbol("F3x_m1");      // x-component of force 3 on mass 1

                var F1y_m1 = new Symbol("F1y_m1");      // y-component of force 1 on mass 1
                var F2y_m1 = new Symbol("F2y_m1");      // y-component of force 2 on mass 1
                var F3y_m1 = new Symbol("F3y_m1");      // y-component of force 3 on mass 1

                var Fx_m1 = new Symbol("Fx_m1");        // x-component of total force on mass 1
                var Fy_m1 = new Symbol("Fy_m1");        // y-component of total force on mass 1

                var ax_m1 = new Symbol("ax_m1");        // x-component of acceleration of mass 1
                var ay_m1 = new Symbol("ay_m1");        // y-component of acceleration of mass 1

                var m1 = new Symbol("m1");

                ////////////////////////////////////////////////////////////////////////////////
                
                var incline = new Symbol("incline");
                
                var f_s = new Symbol("f_s");            // force due to static friction
                
                var g = new Symbol("g");                // gravity

                var n = new Symbol("n");                // normal force on block

                var a = new Symbol("a");

                var Pi = new Symbol("Pi");

                var mu_s = new Symbol("mu_s");          // coefficient of static friction

                var eqs = new And(
                    
                    F1x_m1 == F1_m1 * cos(th1_m1),
                    F2x_m1 == F2_m1 * cos(th2_m1),
                    F3x_m1 == F3_m1 * cos(th3_m1),

                    F1y_m1 == F1_m1 * sin(th1_m1),
                    F2y_m1 == F2_m1 * sin(th2_m1),
                    F3y_m1 == F3_m1 * sin(th3_m1),

                    Fx_m1 == F1x_m1 + F2x_m1 + F3x_m1,
                    Fy_m1 == F1y_m1 + F2y_m1 + F3y_m1,

                    Fx_m1 == m1 * ax_m1,
                    Fy_m1 == m1 * ay_m1,

                    f_s == mu_s * n
                    
                    );

                DoubleFloat.tolerance = 0.00001;

                {
                    var vals = new List<Equation>()
                    {
                        ax_m1 == 0,                         
                        ay_m1 == 0,                         
                        
                        F1_m1 == n,                     
                        F2_m1 == f_s,
                        F3_m1 == m1 * g,

                        th1_m1 == 90 * Pi / 180,            // force 1 is straight up
                        th2_m1 == 180 * Pi / 180,           // force 2 is straight down
                        th3_m1 == 270 * Pi / 180 + incline  // force 3 direction 
                    };

                    var zeros = vals.Where(eq => eq.b == 0).ToList();

                    // mu_s
                    {
                        eqs
                            .SubstituteEqLs(vals)

                            .EliminateVariables(
                                F1x_m1, F2x_m1, F3x_m1,
                                F1y_m1, F2y_m1, F3y_m1,
                                Fx_m1, Fy_m1,
                                f_s, n
                            )
                            .IsolateVariable(mu_s)

                            .DeepSelect(SinCosToTanFunc)

                            .AssertEqTo(mu_s == tan(incline));
                    }
                }
            }

            #endregion
            
            #region PSE 5E E5.13 - The Sliding Hockey Puck

            {
                // A hockey puck on a frozen pond is given an initial speed of
                // 20.0  m/s. If the puck always remains on the ice and slides
                // 115 m before coming to rest, determine the coefficient of
                // kinetic friction between the puck and ice.
                
                ////////////////////////////////////////////////////////////////////////////////

                var s = new Symbol("s");                // displacement
                var u = new Symbol("u");                // initial velocity
                var v = new Symbol("v");                // final velocity
                var a = new Symbol("a");                // acceleration
                var t = new Symbol("t");                // time elapsed
                
                var F1_m1 = new Symbol("F1_m1");        // force 1 on mass 1
                var F2_m1 = new Symbol("F2_m1");        // force 2 on mass 1
                var F3_m1 = new Symbol("F3_m1");        // force 3 on mass 1

                var th1_m1 = new Symbol("th1_m1");      // direction of force 1 on mass 1
                var th2_m1 = new Symbol("th2_m1");      // direction of force 2 on mass 1
                var th3_m1 = new Symbol("th3_m1");      // direction of force 3 on mass 1

                var F1x_m1 = new Symbol("F1x_m1");      // x-component of force 1 on mass 1
                var F2x_m1 = new Symbol("F2x_m1");      // x-component of force 2 on mass 1
                var F3x_m1 = new Symbol("F3x_m1");      // x-component of force 3 on mass 1

                var F1y_m1 = new Symbol("F1y_m1");      // y-component of force 1 on mass 1
                var F2y_m1 = new Symbol("F2y_m1");      // y-component of force 2 on mass 1
                var F3y_m1 = new Symbol("F3y_m1");      // y-component of force 3 on mass 1

                var Fx_m1 = new Symbol("Fx_m1");        // x-component of total force on mass 1
                var Fy_m1 = new Symbol("Fy_m1");        // y-component of total force on mass 1

                var ax_m1 = new Symbol("ax_m1");        // x-component of acceleration of mass 1
                var ay_m1 = new Symbol("ay_m1");        // y-component of acceleration of mass 1

                var m1 = new Symbol("m1");

                ////////////////////////////////////////////////////////////////////////////////

                // var incline = new Symbol("incline");
                
                var f_s = new Symbol("f_s");            // force due to static friction

                var f_k = new Symbol("f_k");            // force due to kinetic friction

                var g = new Symbol("g");                // gravity

                var n = new Symbol("n");                // normal force on block

                // var a = new Symbol("a");

                var Pi = new Symbol("Pi");

                var mu_s = new Symbol("mu_s");          // coefficient of static friction

                var mu_k = new Symbol("mu_k");          // coefficient of kinetic friction
                
                var eqs = new And(

                    a == ax_m1,

                    v == u + a * t,
                    s == (u + v) * t / 2,
                    
                    F1x_m1 == F1_m1 * cos(th1_m1),
                    F2x_m1 == F2_m1 * cos(th2_m1),
                    F3x_m1 == F3_m1 * cos(th3_m1),

                    F1y_m1 == F1_m1 * sin(th1_m1),
                    F2y_m1 == F2_m1 * sin(th2_m1),
                    F3y_m1 == F3_m1 * sin(th3_m1),

                    Fx_m1 == F1x_m1 + F2x_m1 + F3x_m1,
                    Fy_m1 == F1y_m1 + F2y_m1 + F3y_m1,

                    Fx_m1 == m1 * ax_m1,
                    Fy_m1 == m1 * ay_m1,

                    f_s == mu_s * n,
                    f_k == mu_k * n
                    
                    );
                
                DoubleFloat.tolerance = 0.00001;

                {
                    var symbolic_vals = new List<Equation>()
                    {
                        F1_m1 == n,
                        F2_m1 == f_k,
                        F3_m1 == m1 * g,

                        th1_m1 == 90 * Pi / 180,            // force 1 is straight up
                        th2_m1 == 180 * Pi / 180,           // force 2 is left
                        th3_m1 == 270 * Pi / 180            // force 3 is straight down
                    };

                    var vals = new List<Equation>()
                    {
                        //ax_m1 == 0,
                        ay_m1 == 0,

                        s == 115,
                        u == 20,
                        v == 0,

                        g == 9.8
                    };
                    
                    var zeros = vals.Where(eq => eq.b == 0).ToList();

                    // mu_k
                    {
                        eqs
                            .SubstituteEqLs(zeros)
                            .SubstituteEqLs(symbolic_vals)

                            .EliminateVariables(
                                t,
                                F1x_m1, F2x_m1, F3x_m1,
                                F1y_m1, F2y_m1, F3y_m1,

                                Fx_m1, Fy_m1,

                                f_s, f_k,

                                n,

                                ax_m1, a

                                )

                            .IsolateVariable(mu_k)
                            
                            .AssertEqTo(    mu_k == (u ^ 2) / s / g / 2    )

                            .SubstituteEqLs(vals)
                            
                            .AssertEqTo(    mu_k == 0.17746228926353147    );
                    }
                }
            }

            #endregion
            
            #region PSE 5E E5.14 - Acceleration of Two Connected Objects When Friction Is Present

            {
                // A block of mass m1 on a rough, horizontal surface is connected
                // to a ball of mass m2 by a lightweight cord over a lightweight,
                // frictionless pulley, as shown:
                //
                // http://i.imgur.com/0fHOmGJ.png
                //
                // A force of magnitude F at an angle th with the horizontal is
                // applied to the block as shown. The coefficient of kinetic
                // friction between the block and surface is mu_k.
                // 
                // Determine the magnitude of the acceleration of the two objects.
                
                ////////////////////////////////////////////////////////////////////////////////

                var F1_m1 = new Symbol("F1_m1");        // force 1 on mass 1
                var F2_m1 = new Symbol("F2_m1");        // force 2 on mass 1
                var F3_m1 = new Symbol("F3_m1");        // force 3 on mass 1
                var F4_m1 = new Symbol("F4_m1");        // force 4 on mass 1
                var F5_m1 = new Symbol("F5_m1");        // force 5 on mass 1

                var th1_m1 = new Symbol("th1_m1");      // direction of force 1 on mass 1
                var th2_m1 = new Symbol("th2_m1");      // direction of force 2 on mass 1
                var th3_m1 = new Symbol("th3_m1");      // direction of force 3 on mass 1
                var th4_m1 = new Symbol("th4_m1");      // direction of force 4 on mass 1
                var th5_m1 = new Symbol("th5_m1");      // direction of force 5 on mass 1

                var F1x_m1 = new Symbol("F1x_m1");      // x-component of force 1 on mass 1
                var F2x_m1 = new Symbol("F2x_m1");      // x-component of force 2 on mass 1
                var F3x_m1 = new Symbol("F3x_m1");      // x-component of force 3 on mass 1
                var F4x_m1 = new Symbol("F4x_m1");      // x-component of force 4 on mass 1
                var F5x_m1 = new Symbol("F5x_m1");      // x-component of force 5 on mass 1

                var F1y_m1 = new Symbol("F1y_m1");      // y-component of force 1 on mass 1
                var F2y_m1 = new Symbol("F2y_m1");      // y-component of force 2 on mass 1
                var F3y_m1 = new Symbol("F3y_m1");      // y-component of force 3 on mass 1
                var F4y_m1 = new Symbol("F4y_m1");      // y-component of force 4 on mass 1
                var F5y_m1 = new Symbol("F5y_m1");      // y-component of force 5 on mass 1

                var Fx_m1 = new Symbol("Fx_m1");        // x-component of total force on mass 1
                var Fy_m1 = new Symbol("Fy_m1");        // y-component of total force on mass 1

                var ax_m1 = new Symbol("ax_m1");        // x-component of acceleration of mass 1
                var ay_m1 = new Symbol("ay_m1");        // y-component of acceleration of mass 1

                var m1 = new Symbol("m1");

                ////////////////////////////////////////////////////////////////////////////////

                var F1_m2 = new Symbol("F1_m2");        // force 1 on mass 2
                var F2_m2 = new Symbol("F2_m2");        // force 2 on mass 2
                
                var th1_m2 = new Symbol("th1_m2");      // direction of force 1 on mass 2
                var th2_m2 = new Symbol("th2_m2");      // direction of force 2 on mass 2
                
                var F1x_m2 = new Symbol("F1x_m2");      // x-component of force 1 on mass 2
                var F2x_m2 = new Symbol("F2x_m2");      // x-component of force 2 on mass 2
                
                var F1y_m2 = new Symbol("F1y_m2");      // y-component of force 1 on mass 2
                var F2y_m2 = new Symbol("F2y_m2");      // y-component of force 2 on mass 2
                
                var Fx_m2 = new Symbol("Fx_m2");        // x-component of total force on mass 2
                var Fy_m2 = new Symbol("Fy_m2");        // y-component of total force on mass 2

                var ax_m2 = new Symbol("ax_m2");        // x-component of acceleration of mass 2
                var ay_m2 = new Symbol("ay_m2");        // y-component of acceleration of mass 2

                var m2 = new Symbol("m2");

                ////////////////////////////////////////////////////////////////////////////////
                
                var F = new Symbol("F");                // force applied at angle on block
                var th = new Symbol("th");              // angle of force applied on block
                var T = new Symbol("T");                // tension in cable
                var g = new Symbol("g");                // gravity
                var n = new Symbol("n");                // normal force on block

                var a = new Symbol("a");

                var Pi = new Symbol("Pi");
                                
                var f_k = new Symbol("f_k");            // force due to kinetic friction
                
                var mu_k = new Symbol("mu_k");          // coefficient of kinetic friction

                var eqs = new And(
                    
                    ax_m1 == ay_m2,                     // the block moves right as the ball moves up

                    ////////////////////////////////////////////////////////////////////////////////

                    F1x_m1 == F1_m1 * cos(th1_m1),
                    F2x_m1 == F2_m1 * cos(th2_m1),
                    F3x_m1 == F3_m1 * cos(th3_m1),
                    F4x_m1 == F4_m1 * cos(th4_m1),
                    F5x_m1 == F5_m1 * cos(th5_m1),

                    F1y_m1 == F1_m1 * sin(th1_m1),
                    F2y_m1 == F2_m1 * sin(th2_m1),
                    F3y_m1 == F3_m1 * sin(th3_m1),
                    F4y_m1 == F4_m1 * sin(th4_m1),
                    F5y_m1 == F5_m1 * sin(th5_m1),

                    Fx_m1 == F1x_m1 + F2x_m1 + F3x_m1 + F4x_m1 + F5x_m1,
                    Fy_m1 == F1y_m1 + F2y_m1 + F3y_m1 + F4y_m1 + F5y_m1,

                    Fx_m1 == m1 * ax_m1,
                    Fy_m1 == m1 * ay_m1,

                    ////////////////////////////////////////////////////////////////////////////////

                    F1x_m2 == F1_m2 * cos(th1_m2),
                    F2x_m2 == F2_m2 * cos(th2_m2),

                    F1y_m2 == F1_m2 * sin(th1_m2),
                    F2y_m2 == F2_m2 * sin(th2_m2),

                    Fx_m2 == F1x_m2 + F2x_m2,
                    Fy_m2 == F1y_m2 + F2y_m2,

                    Fx_m2 == m2 * ax_m2,
                    Fy_m2 == m2 * ay_m2,

                    ////////////////////////////////////////////////////////////////////////////////

                    f_k == mu_k * n,

                    a == ax_m1

                    );
                
                DoubleFloat.tolerance = 0.00001;

                {
                    var vals = new List<Equation>()
                    {
                        ay_m1 == 0,                                     // block moves horizontally
                        ax_m2 == 0,                                     // ball moves vertically
                        
                        F1_m1 == F,         th1_m1 == th,               // force applied at angle
                        F2_m1 == n,         th2_m1 == 90 * Pi / 180,    // normal force is straight up
                        F3_m1 == T,         th3_m1 == 180 * Pi / 180,   // force due to cord is left
                        F4_m1 == f_k,       th4_m1 == 180 * Pi / 180,   // force due to friction is left
                        F5_m1 == m1 * g,    th5_m1 == 270 * Pi / 180,   // force due to gravity is down
                        
                        F1_m2 == T,         th1_m2 == 90 * Pi / 180,    // force due to cord is up
                        F2_m2 == m2 * g,    th2_m2 == 270 * Pi / 180    // force due to gravity is down                                
                    };

                    var zeros = vals.Where(eq => eq.b == 0).ToList();

                    // a
                    {
                        eqs
                            .SubstituteEqLs(vals)

                            .EliminateVariables(
                                ax_m1,

                                Fx_m1, Fy_m1,
                                Fx_m2, Fy_m2,

                                F1x_m1, F2x_m1, F3x_m1, F4x_m1, F5x_m1,
                                F1y_m1, F2y_m1, F3y_m1, F4y_m1, F5y_m1,

                                F1x_m2, F2x_m2,
                                F1y_m2, F2y_m2,

                                T, f_k, n,

                                ay_m2
                            )
                            
                            .AssertEqTo(

                                a == (g * m2 + g * m1 * mu_k - F * mu_k * sin(th) - cos(th) * F) / (-m1 - m2)

                            );
                    }
                }
            }

            #endregion
                                    
            #region PSE 5E E5.14 - Acceleration of Two Connected Objects When Friction Is Present - Obj5

            {
                // A block of mass m1 on a rough, horizontal surface is connected
                // to a ball of mass m2 by a lightweight cord over a lightweight,
                // frictionless pulley, as shown:
                //
                // http://i.imgur.com/0fHOmGJ.png
                //
                // A force of magnitude F at an angle th with the horizontal is
                // applied to the block as shown. The coefficient of kinetic
                // friction between the block and surface is mu_k.
                // 
                // Determine the magnitude of the acceleration of the two objects.

                var blk = new Obj5("blk");
                var bal = new Obj3("bal");
                
                var F = new Symbol("F");                // force applied at angle on block
                var th = new Symbol("th");              // angle of force applied on block
                var T = new Symbol("T");                // tension in cable
                var g = new Symbol("g");                // gravity
                var n = new Symbol("n");                // normal force on block

                var a = new Symbol("a");

                var Pi = new Symbol("Pi");

                var f_k = new Symbol("f_k");            // force due to kinetic friction

                var mu_k = new Symbol("mu_k");          // coefficient of kinetic friction

                var m1 = new Symbol("m1");
                var m2 = new Symbol("m2");

                var eqs = new And(

                    blk.ax == bal.ay,                   // the block moves right as the ball moves up

                    blk.Equations(),
                    bal.Equations(),

                    f_k == mu_k * n,

                    a == blk.ax

                    );
                
                var vals = new List<Equation>()
                {
                    blk.ay == 0,                                        // block moves horizontally
                        
                    blk.F1 == F,            blk.th1 == th,              // block moves horizontally
                    blk.F2 == n,            blk.th2 == 90 * Pi / 180,   // normal force is straight up
                    blk.F3 == T,            blk.th3 == 180 * Pi / 180,  // force due to cord is left
                    blk.F4 == f_k,          blk.th4 == 180 * Pi / 180,  // force due to friction is left
                    blk.F5 == blk.m * g,    blk.th5 == 270 * Pi / 180,  // force due to gravity is down

                    bal.ax == 0,                                        // ball moves vertically

                    bal.F1 == T,            bal.th1 == 90 * Pi / 180,   // force due to cord is up
                    bal.F2 == bal.m * g,    bal.th2 == 270 * Pi / 180,  // force due to gravity is down
                    bal.F3 == 0,

                    blk.m == m1,
                    bal.m == m2
                };

                // a
                
                eqs
                    .SubstituteEqLs(vals)

                    .EliminateVariables(

                        blk.ax,

                        blk.ΣFx, blk.ΣFy,
                        bal.ΣFx, bal.ΣFy,

                        blk.F1x, blk.F2x, blk.F3x, blk.F4x, blk.F5x,
                        blk.F1y, blk.F2y, blk.F3y, blk.F4y, blk.F5y,

                        bal.F1x, bal.F2x, bal.F3x,
                        bal.F1y, bal.F2y, bal.F3y,

                        T, f_k, n,

                        bal.ay
                    )

                    .AssertEqTo(

                        a == (g * m2 + g * m1 * mu_k - F * mu_k * sin(th) - cos(th) * F) / (-m1 - m2)

                    );
                
            }

            #endregion
            
            #region PSE 5E P5.25

            {
                // A bag of cement of weight F_g hangs from three wires as
                // shown in http://i.imgur.com/f5JpLjY.png
                //  
                // Two of the wires make angles th1 and th2 with the horizontal.
                // If the system is in equilibrium, show that the tension in the
                // left -hand wire is:
                //
                //          T1 == F_g cos(th2) / sin(th1 + th2)
                
                ////////////////////////////////////////////////////////////////////////////////

                var F1_m1 = new Symbol("F1_m1");        // force 1 on mass 1
                var F2_m1 = new Symbol("F2_m1");        // force 2 on mass 1
                var F3_m1 = new Symbol("F3_m1");        // force 3 on mass 1

                var th1_m1 = new Symbol("th1_m1");      // direction of force 1 on mass 1
                var th2_m1 = new Symbol("th2_m1");      // direction of force 2 on mass 1
                var th3_m1 = new Symbol("th3_m1");      // direction of force 3 on mass 1

                var F1x_m1 = new Symbol("F1x_m1");      // x-component of force 1 on mass 1
                var F2x_m1 = new Symbol("F2x_m1");      // x-component of force 2 on mass 1
                var F3x_m1 = new Symbol("F3x_m1");      // x-component of force 3 on mass 1

                var F1y_m1 = new Symbol("F1y_m1");      // y-component of force 1 on mass 1
                var F2y_m1 = new Symbol("F2y_m1");      // y-component of force 2 on mass 1
                var F3y_m1 = new Symbol("F3y_m1");      // y-component of force 3 on mass 1

                var Fx_m1 = new Symbol("Fx_m1");        // x-component of total force on mass 1
                var Fy_m1 = new Symbol("Fy_m1");        // y-component of total force on mass 1

                var ax_m1 = new Symbol("ax_m1");        // x-component of acceleration of mass 1
                var ay_m1 = new Symbol("ay_m1");        // y-component of acceleration of mass 1

                var m1 = new Symbol("m1");

                ////////////////////////////////////////////////////////////////////////////////
                
                var g = new Symbol("g");                // gravity
                
                var a = new Symbol("a");

                var Pi = new Symbol("Pi");

                var T1 = new Symbol("T1");
                var T2 = new Symbol("T2");
                var T3 = new Symbol("T3");

                var th1 = new Symbol("th1");
                var th2 = new Symbol("th2");
                
                var eqs = new And(
                    
                    F1x_m1 == F1_m1 * cos(th1_m1),
                    F2x_m1 == F2_m1 * cos(th2_m1),
                    F3x_m1 == F3_m1 * cos(th3_m1),

                    F1y_m1 == F1_m1 * sin(th1_m1),
                    F2y_m1 == F2_m1 * sin(th2_m1),
                    F3y_m1 == F3_m1 * sin(th3_m1),

                    Fx_m1 == F1x_m1 + F2x_m1 + F3x_m1,
                    Fy_m1 == F1y_m1 + F2y_m1 + F3y_m1,

                    Fx_m1 == m1 * ax_m1,
                    Fy_m1 == m1 * ay_m1
                    
                    );

                DoubleFloat.tolerance = 0.00001;

                {
                    var vals = new List<Equation>()
                    {
                        ax_m1 == 0,
                        ay_m1 == 0,
                        
                        F1_m1 == T2,
                        F2_m1 == T1,
                        F3_m1 == m1 * g,

                        th1_m1 == th2,                              
                        th2_m1 == 180 * Pi / 180 - th1,             
                        th3_m1 == 270 * Pi / 180
                    };

                    var zeros = vals.Where(eq => eq.b == 0).ToList();

                    // T1
                    {
                        eqs
                            .SubstituteEqLs(vals)

                            .EliminateVariables(
                            
                                F1x_m1, F2x_m1, F3x_m1,
                                F1y_m1, F2y_m1, F3y_m1,

                                Fx_m1, Fy_m1,

                                T2
                            )

                            .IsolateVariable(T1)
                            
                            .RationalizeExpression()

                            .DeepSelect(SumDifferenceFormulaAFunc)

                            .AssertEqTo(
                                T1 == cos(th2) * g * m1 / sin(th1 + th2)
                            );
                    }
                }
            }

            #endregion
            
            #region PSE 5E P5.25 Obj

            {
                // A bag of cement of weight F_g hangs from three wires as
                // shown in http://i.imgur.com/f5JpLjY.png
                //  
                // Two of the wires make angles th1 and th2 with the horizontal.
                // If the system is in equilibrium, show that the tension in the
                // left -hand wire is:
                //
                //          T1 == F_g cos(th2) / sin(th1 + th2)
                
                var bag = new Obj3("bag");
                
                var T1 = new Symbol("T1");
                var T2 = new Symbol("T2");
                var T3 = new Symbol("T3");

                var F_g = new Symbol("F_g");

                var th1 = new Symbol("th1");
                var th2 = new Symbol("th2");

                var eqs = bag.Equations();

                var vals = new List<Equation>()
                {
                    bag.ax == 0,
                    bag.ay == 0,

                    bag.F1 == T1,       bag.th1 == (180).ToRadians() - th1,
                    bag.F2 == T2,       bag.th2 == th2,
                    bag.F3 == F_g,      bag.th3 == (270).ToRadians()
                };

                eqs
                    .SubstituteEqLs(vals)

                    .EliminateVariables(

                        bag.ΣFx, bag.F1x, bag.F2x, bag.F3x,
                        bag.ΣFy, bag.F1y, bag.F2y, bag.F3y,

                        T2
                    
                    )

                    .IsolateVariable(T1)

                    .RationalizeExpression()

                    .DeepSelect(SumDifferenceFormulaAFunc)

                    .AssertEqTo(    T1 == cos(th2) * F_g / sin(th1 + th2)   );
                
            }

            #endregion
            
            #region PSE 5E P5.31

            {
                // Two people pull as hard as they can on ropes attached
                // to a boat that has a mass of 200 kg. If they pull in the
                // same direction, the boat has an acceleration of
                // 1.52 m/s^2 to the right. If they pull in opposite directions,
                // the boat has an acceleration of 0.518 m/s^2 to the
                // left.
                // 
                // What is the force exerted by each person on the
                // boat? (Disregard any other forces on the boat.)

                ////////////////////////////////////////////////////////////////////////////////

                var F1_m1 = new Symbol("F1_m1");        // force 1 on mass 1
                var F2_m1 = new Symbol("F2_m1");        // force 2 on mass 1
                
                var th1_m1 = new Symbol("th1_m1");      // direction of force 1 on mass 1
                var th2_m1 = new Symbol("th2_m1");      // direction of force 2 on mass 1
                
                var F1x_m1 = new Symbol("F1x_m1");      // x-component of force 1 on mass 1
                var F2x_m1 = new Symbol("F2x_m1");      // x-component of force 2 on mass 1
                
                var F1y_m1 = new Symbol("F1y_m1");      // y-component of force 1 on mass 1
                var F2y_m1 = new Symbol("F2y_m1");      // y-component of force 2 on mass 1
                
                var Fx_m1 = new Symbol("Fx_m1");        // x-component of total force on mass 1
                var Fy_m1 = new Symbol("Fy_m1");        // y-component of total force on mass 1

                var ax_m1 = new Symbol("ax_m1");        // x-component of acceleration of mass 1
                var ay_m1 = new Symbol("ay_m1");        // y-component of acceleration of mass 1

                var m1 = new Symbol("m1");

                ////////////////////////////////////////////////////////////////////////////////

                var F1_m2 = new Symbol("F1_m2");        // force 1 on mass 2
                var F2_m2 = new Symbol("F2_m2");        // force 2 on mass 2

                var th1_m2 = new Symbol("th1_m2");      // direction of force 1 on mass 2
                var th2_m2 = new Symbol("th2_m2");      // direction of force 2 on mass 2

                var F1x_m2 = new Symbol("F1x_m2");      // x-component of force 1 on mass 2
                var F2x_m2 = new Symbol("F2x_m2");      // x-component of force 2 on mass 2

                var F1y_m2 = new Symbol("F1y_m2");      // y-component of force 1 on mass 2
                var F2y_m2 = new Symbol("F2y_m2");      // y-component of force 2 on mass 2

                var Fx_m2 = new Symbol("Fx_m2");        // x-component of total force on mass 2
                var Fy_m2 = new Symbol("Fy_m2");        // y-component of total force on mass 2

                var ax_m2 = new Symbol("ax_m2");        // x-component of acceleration of mass 2
                var ay_m2 = new Symbol("ay_m2");        // y-component of acceleration of mass 2

                var m2 = new Symbol("m2");

                ////////////////////////////////////////////////////////////////////////////////
                
                var Pi = new Symbol("Pi");
                
                var T1 = new Symbol("T1");
                var T2 = new Symbol("T2");

                var eqs = new And(

                    m1 == m2,
                                        
                    F1x_m1 == F1_m1 * cos(th1_m1),
                    F2x_m1 == F2_m1 * cos(th2_m1),
                    
                    F1y_m1 == F1_m1 * sin(th1_m1),
                    F2y_m1 == F2_m1 * sin(th2_m1),
                    
                    Fx_m1 == F1x_m1 + F2x_m1,
                    Fy_m1 == F1y_m1 + F2y_m1,

                    Fx_m1 == m1 * ax_m1,
                    Fy_m1 == m1 * ay_m1,


                    F1x_m2 == F1_m2 * cos(th1_m2),
                    F2x_m2 == F2_m2 * cos(th2_m2),

                    F1y_m2 == F1_m2 * sin(th1_m2),
                    F2y_m2 == F2_m2 * sin(th2_m2),

                    Fx_m2 == F1x_m2 + F2x_m2,
                    Fy_m2 == F1y_m2 + F2y_m2,

                    Fx_m2 == m2 * ax_m2,
                    Fy_m2 == m2 * ay_m2
                    );

                DoubleFloat.tolerance = 0.00001;

                {
                    var vals = new List<Equation>()
                    {
                        ay_m1 == 0,

                        F1_m1 == T1,    th1_m1 == 0,
                        F2_m1 == T2,    th2_m1 == 0,

                        ay_m2 == 0,

                        F1_m2 == T1,    th1_m2 == 180 * Pi / 180,
                        F2_m2 == T2,    th2_m2 == 0
                    };

                    var zeros = vals.Where(eq => eq.b == 0).ToList();

                    var numerical_vals = new List<Equation>()
                    {
                        m1 == 200,

                        ax_m1 == 1.52,
                        ax_m2 == -0.518
                    };
                    
                    // T1
                    {
                        eqs
                            .SubstituteEqLs(vals)

                            .EliminateVariables(
                            
                                m2,

                                F1x_m1, F2x_m1,
                                F1y_m1, F2y_m1,

                                F1x_m2, F2x_m2,
                                F1y_m2, F2y_m2,

                                Fx_m1, Fy_m1,
                                
                                Fx_m2, Fy_m2,

                                T2
                                
                                )

                            .IsolateVariable(T1)

                            .AssertEqTo(
                            
                                T1 == -(ax_m2 * m1 - ax_m1 * m1) / 2
                                
                            )
                            
                            .SubstituteEqLs(numerical_vals)

                            .AssertEqTo(T1 == 203.8);
                    }

                    // T2
                    {
                        eqs
                            .SubstituteEqLs(vals)

                            .EliminateVariables(

                                m2,

                                F1x_m1, F2x_m1,
                                F1y_m1, F2y_m1,

                                F1x_m2, F2x_m2,
                                F1y_m2, F2y_m2,

                                Fx_m1, Fy_m1,

                                Fx_m2, Fy_m2,

                                T1

                                )

                            .IsolateVariable(T2)

                            .AssertEqTo(
                            
                                T2 == (ax_m1 * m1 + ax_m2 * m1) / 2
                                
                            )
                            
                            .SubstituteEqLs(numerical_vals)

                            .AssertEqTo(T2 == 100.19999999999999);
                    }
                }
            }

            #endregion

            #region PSE 5E P5.31 Obj

            {
                // Two people pull as hard as they can on ropes attached
                // to a boat that has a mass of 200 kg. If they pull in the
                // same direction, the boat has an acceleration of
                // 1.52 m/s^2 to the right. If they pull in opposite directions,
                // the boat has an acceleration of 0.518 m/s^2 to the
                // left.
                // 
                // What is the force exerted by each person on the
                // boat? (Disregard any other forces on the boat.)

                ////////////////////////////////////////////////////////////////////////////////

                var b1 = new Obj2("b1");            // boat in scenario 1 (same direction)
                var b2 = new Obj2("b2");            // boat in scenario 2 (opposite directions)

                var m = new Symbol("m");

                ////////////////////////////////////////////////////////////////////////////////

                var Pi = new Symbol("Pi");

                var T1 = new Symbol("T1");
                var T2 = new Symbol("T2");

                var eqs = new And(

                    b1.Equations(),
                    b2.Equations()

                    );

                DoubleFloat.tolerance = 0.00001;

                var vals = new List<Equation>()
                {
                    b1.m == m,

                    b1.ay == 0,

                    b1.F1 == T1, b1.th1 == 0,
                    b1.F2 == T2, b1.th2 == 0,

                    b2.m == m,

                    b2.ay == 0,

                    b2.F1 == T1, b2.th1 == (180).ToRadians(),
                    b2.F2 == T2, b2.th2 == 0

                };

                var zeros = vals.Where(eq => eq.b == 0).ToList();

                var numerical_vals = new List<Equation>()
                {
                    m == 200,

                    b1.ax == 1.52,
                    b2.ax == -0.518
                };

                // T1
                eqs
                    .SubstituteEqLs(vals)

                    .EliminateVariables(

                        b1.ΣFx, b1.F1x, b1.F2x,
                        b1.ΣFy, b1.F1y, b1.F2y,

                        b2.ΣFx, b2.F1x, b2.F2x,
                        b2.ΣFy, b2.F1y, b2.F2y,

                        T2
                    )

                    .IsolateVariable(T1)

                    .AssertEqTo(

                        T1 == -(b2.ax * m - b1.ax * m) / 2

                    )

                    .SubstituteEqLs(numerical_vals)

                    .AssertEqTo(T1 == 203.8);

                // T2
                eqs
                    .SubstituteEqLs(vals)

                    .EliminateVariables(

                        b1.ΣFx, b1.F1x, b1.F2x,
                        b1.ΣFy, b1.F1y, b1.F2y,

                        b2.ΣFx, b2.F1x, b2.F2x,
                        b2.ΣFy, b2.F1y, b2.F2y,

                        T1
                    )

                    .IsolateVariable(T2)

                    .AssertEqTo(

                        T2 == (b1.ax * m + b2.ax * m) / 2

                    )

                    .SubstituteEqLs(numerical_vals)

                    .AssertEqTo(T2 == 100.19999999999999);

            }

            #endregion
            
            #region PSE 5E P5.55 

            {
                // An inventive child named Pat wants to reach an apple
                // in a tree without climbing the tree. Sitting in a chair
                // connected to a rope that passes over a frictionless pulley
                // Pat pulls on the loose end of the rope with such a force
                // that the spring scale reads 250 N. Pat’s weight is 320 N,
                // and the chair weighs 160 N.
                //
                // http://i.imgur.com/wwlypzB.png
                //
                // (a) Draw free - body diagrams for Pat and the chair considered as
                // separate systems, and draw another diagram for Pat and
                // the chair considered as one system.
                //
                // (b) Show that the acceleration of the system is upward and
                // find its magnitude.
                //
                // (c) Find the force Pat exerts on the chair.

                var b = new Obj3("b");          // boy
                var c = new Obj3("c");          // chair
                var s = new Obj3("s");          // system

                var T = new Symbol("T");        // rope tension
                var n = new Symbol("n");        // normal force

                var Fg_b = new Symbol("Fg_b");  // force due to gravity of the boy
                var Fg_c = new Symbol("Fg_c");  // force due to gravity of the chair
                var Fg_s = new Symbol("Fg_s");  // force due to gravity of the system

                var a = new Symbol("a");        // acceleration

                var Pi = new Symbol("Pi");
                var g = new Symbol("g");

                var eqs = new And(

                    Fg_b == b.m * g,
                    Fg_c == c.m * g,
                    Fg_s == s.m * g,

                    Fg_s == Fg_c + Fg_b,

                    s.Equations(),
                    c.Equations()

                    ).Simplify();

                var vals = new List<Equation>()
                {
                    //b.ax == 0,
                    c.ax == 0,
                    s.ax == 0,

                    //b.F1 == T,          b.th1 == 90 * Pi / 180,
                    //b.F2 == n,          b.th2 == 90 * Pi / 180,
                    //b.F3 == b.m * g,    b.th3 == 270 * Pi / 180,

                    c.F1 == T,          c.th1 == 90 * Pi / 180,
                    c.F2 == n,          c.th2 == 270 * Pi / 180,
                    c.F3 == Fg_c,       c.th3 == 270 * Pi / 180,

                    s.F1 == T,          s.th1 == 90 * Pi / 180,
                    s.F2 == T,          s.th2 == 90 * Pi / 180,
                    s.F3 == Fg_s,       s.th3 == 270 * Pi / 180,

                    //b.ay == a,
                    c.ay == a,
                    s.ay == a
                };

                var numerical_vals = new List<Equation>()
                {
                    T == 250.0,
                    Fg_b == 320,
                    Fg_c == 160,
                    g == 9.8
                };

                DoubleFloat.tolerance = 0.00001;

                // a
                eqs
                    .SubstituteEqLs(vals)

                    .EliminateVariables(

                        s.ΣFx, s.F1x, s.F2x, s.F3x,
                        s.ΣFy, s.F1y, s.F2y, s.F3y,

                        c.ΣFx, c.F1x, c.F2x, c.F3x,
                        c.ΣFy, c.F1y, c.F2y, c.F3y,

                        n,

                        s.m,

                        Fg_s,

                        b.m, c.m

                    )

                    .IsolateVariable(a)

                    .AssertEqTo(

                        a == -g * (Fg_b + Fg_c - 2 * T) / (Fg_b + Fg_c)

                    )

                    .SubstituteEqLs(numerical_vals)

                    .AssertEqTo(a == 0.40833333333333333);

                // n
                eqs
                    .SubstituteEqLs(vals)

                    .EliminateVariables(

                        s.ΣFx, s.F1x, s.F2x, s.F3x,
                        s.ΣFy, s.F1y, s.F2y, s.F3y,

                        c.ΣFx, c.F1x, c.F2x, c.F3x,
                        c.ΣFy, c.F1y, c.F2y, c.F3y,

                        c.m, s.m,

                        Fg_s,

                        b.m,

                        a

                    )

                    .IsolateVariable(n)

                    .AssertEqTo(

                        n == -1 * (Fg_c - T - Fg_c * (Fg_b + Fg_c - 2 * T) / (Fg_b + Fg_c))

                    )

                    .SubstituteEqLs(numerical_vals);

                DoubleFloat.tolerance = null;

            }

            #endregion
            
            #region PSE 5E P5.59

            {
                // A mass M is held in place by an applied force F and a
                // pulley system: 
                //
                //                 http://i.imgur.com/TPAHTlW.png
                //
                // The pulleys are massless and frictionless. Find 
                //                     
                // (a) the tension in each section of rope, T1, T2, T3, T4, and T5
                //                     
                // (b) the magnitude of F. 
                //                     
                // (Hint: Draw a free - body diagram for each pulley.)

                var pul1_F = new Symbol("pul1_F");      // magnitude of total force on pully 1
                var pul1_m = new Symbol("pul1_m");      // mass of pully 1
                var pul1_a = new Symbol("pul1_a");      // acceleration of pully 1

                var pul2_F = new Symbol("pul2_F");      // magnitude of total force on pully 2
                var pul2_m = new Symbol("pul2_m");      // mass of pully 2
                var pul2_a = new Symbol("pul2_a");      // acceleration of pully 2
                
                var T1 = new Symbol("T1");
                var T2 = new Symbol("T2");
                var T3 = new Symbol("T3");
                var T4 = new Symbol("T4");
                var T5 = new Symbol("T5");

                var F = new Symbol("F");

                var M = new Symbol("M");

                var g = new Symbol("g");

                var eqs = new And(

                     T1 == F,
                     T2 == T3,
                     T1 == T3,
                     T5 == M * g,
                     
                     pul1_a == 0,
                     pul1_m == 0,

                     pul1_F == T4 - T1 - T2 - T3,
                     pul1_F == pul1_m * pul1_a,

                     pul2_m == 0,

                     pul2_F == T2 + T3 - T5,
                     pul2_F == pul2_m * pul2_a

                    );

                DoubleFloat.tolerance = 0.00001;

                // T1
                {
                    eqs
                        .EliminateVariables(pul1_F, pul2_F, pul1_m, pul2_m, pul1_a, T2, T3, T4, T5, F)
                        .IsolateVariable(T1)
                        .AssertEqTo(T1 == g * M / 2);
                }
                
                // T2
                {
                    eqs
                        .EliminateVariables(pul1_F, pul2_F, pul1_m, pul2_m, pul1_a, T1, T3, T4, T5, F)
                        .IsolateVariable(T2)
                        .AssertEqTo(T2 == g * M / 2);
                }

                // T3
                {
                    eqs
                        .EliminateVariables(pul1_F, pul2_F, pul1_m, pul2_m, pul1_a, T1, T2, T4, T5, F)
                        .IsolateVariable(T3)
                        .AssertEqTo(T3 == g * M / 2);
                }

                // T4
                {
                    eqs
                        .EliminateVariables(pul1_F, pul2_F, pul1_m, pul2_m, pul1_a, T1, T2, T3, T5, F)
                        .IsolateVariable(T4)
                        .AssertEqTo(T4 == g * M * 3 / 2);
                }

                // T5
                {
                    eqs
                        .EliminateVariables(pul1_F, pul2_F, pul1_m, pul2_m, pul1_a, T1, T2, T3, T4, F)
                        .AssertEqTo(T5 == g * M);
                }

                // F
                {
                    eqs
                        .EliminateVariables(pul1_F, pul2_F, pul1_m, pul2_m, pul1_a, T1, T2, T3, T4, T5)
                        .IsolateVariable(F)
                        .AssertEqTo(F == g * M / 2);
                }
            }

            #endregion
            
            #region PSE 5E P5.69
            {
                // What horizontal force must be applied to the cart shown:

                // http://i.imgur.com/fpkzsYI.png

                // so that the blocks remain stationary relative to the cart?
                // Assume all surfaces, wheels, and pulley are frictionless.
                // (Hint:Note that the force exerted by the string accelerates m1.)

                var blk1 = new Obj3("blk1");
                var blk2 = new Obj3("blk2");

                var sys = new Obj3("sys");
                
                var m1 = new Symbol("m1");
                var m2 = new Symbol("m2");
                
                var T = new Symbol("T");
                var F = new Symbol("F");
                var M = new Symbol("M");
                var g = new Symbol("g");
                var a = new Symbol("a");
                
                var eqs = new And(

                    blk1.Equations(),
                    blk2.Equations(),

                    sys.Equations()
                    
                    );

                var vals = new List<Equation>()
                {
                    blk1.ax == a,
                    blk1.ay == 0,

                    blk1.m == m1,

                    blk1.F1 == T,   blk1.th1 == 0,

                    blk1.th2 == (90).ToRadians(),
                    blk1.th3 == (270).ToRadians(),


                    blk2.ax == a,
                    blk2.ay == 0,

                    blk2.m == m2,

                    blk2.th1 == 0,

                    blk2.F2 == T,       blk2.th2 == (90).ToRadians(),
                    blk2.F3 == m2 * g,  blk2.th3 == (270).ToRadians(),


                    sys.ax == a,
                    sys.ay == 0,

                    sys.m == M + m1 + m2,

                    sys.F1 == F,        sys.th1 == 0,

                    sys.th2 == (90).ToRadians(),
                    sys.th3 == (270).ToRadians()

                };

                eqs
                    .SubstituteEqLs(vals)

                    .EliminateVariables(

                        blk1.ΣFx, blk1.F1x, blk1.F2x, blk1.F3x,
                        blk1.ΣFy, blk1.F1y, blk1.F2y, blk1.F3y,

                        blk1.F2,

                        blk2.ΣFx, blk2.F1x, blk2.F2x, blk2.F3x,
                        blk2.ΣFy, blk2.F1y, blk2.F2y, blk2.F3y,

                        blk2.F1,

                        sys.ΣFx, sys.F1x, sys.F2x, sys.F3x,
                        sys.ΣFy, sys.F1y, sys.F2y, sys.F3y,

                        sys.F2,

                        T, a

                    )

                    .AssertEqTo(   F == g * m2 / m1 * (M + m1 + m2)   );
            }
            #endregion
                        
            #region PSE 5E E7.7
            {
                // A  6.0-kg block initially at rest is pulled to the right along a
                // horizontal, frictionless surface by a constant horizontal force
                // of 12 N. Find the speed of the block after it has moved 3.0 m.
                
                var W = new Symbol("W");
                var F = new Symbol("F");
                var d = new Symbol("d");

                var Kf = new Symbol("Kf");
                var Ki = new Symbol("Ki");

                var m = new Symbol("m");

                var vf = new Symbol("vf");
                var vi = new Symbol("vi");

                var eqs = new And(

                    W == F * d,

                    W == Kf - Ki,

                    Kf == m * (vf ^ 2) / 2,
                    Ki == m * (vi ^ 2) / 2,

                    m != 0

                    );

                var vals = new List<Equation>() { m == 6.0, vi == 0, F == 12, d == 3 };

                // vf
                eqs
                    .EliminateVariables(Kf, Ki, W)
                    .IsolateVariable(vf)
                    .LogicalExpand().CheckVariable(m).SimplifyEquation().SimplifyLogical()

                    .AssertEqTo(

                        new Or(
                            new And(
                                vf == sqrt(-2 * m * (-d * F - m * (vi ^ 2) / 2)) / m,
                                m != 0),
                            new And(
                                vf == -sqrt(-2 * m * (-d * F - m * (vi ^ 2) / 2)) / m,
                                m != 0)))
                                
                    .SubstituteEq(vi == 0)
                                        
                    .AssertEqTo(

                        new Or(
                            new And(
                                vf == sqrt(2 * d * F * m) / m,
                                m != 0),
                            new And(
                                vf == -sqrt(2 * d * F * m) / m,
                                m != 0)))
                    
                    .SubstituteEqLs(vals)

                    .AssertEqTo(
                        new Or(
                            vf == 3.4641016151377544,
                            vf == -3.4641016151377544));

            }
            #endregion
                        
            #region PSE 5E E7.8
            {
                // Find the final speed of the block described in Example 7.7 if
                // the surface is not frictionless but instead has a coefficient of
                // kinetic friction of 0.15.

                var W = new Symbol("W");
                var F = new Symbol("F");
                var d = new Symbol("d");
                var n = new Symbol("n");

                var g = new Symbol("g");

                var Kf = new Symbol("Kf");
                var Ki = new Symbol("Ki");

                var m = new Symbol("m");

                var vf = new Symbol("vf");
                var vi = new Symbol("vi");

                var fk = new Symbol("fk");
                                
                var μk = new Symbol("μk");

                var eqs = new And(

                    Kf == m * (vf ^ 2) / 2,
                    Ki == m * (vi ^ 2) / 2,

                    W == F * d,

                    n == m * g,

                    fk == n * μk,

                    W - fk * d == Kf - Ki,

                    m != 0

                    );

                var vals = new List<Equation>()
                {
                    vi == 0,
                    F == 12.0,
                    d == 3.0,

                    m == 6.0,
                    
                    μk == 0.15,

                    g == 9.8,
                };

                // vf
                eqs
                    .EliminateVariables(Kf, Ki, W, n, fk)
                    .IsolateVariable(vf)
                    .LogicalExpand().SimplifyEquation().SimplifyLogical().CheckVariable(m)
                    .SubstituteEq(vi == 0)
                    .AssertEqTo(
                        new Or(
                            new And(
                                vf == -sqrt(2 * m * (d * F - d * g * m * μk)) / m,
                                m != 0),
                            new And(
                                vf == sqrt(2 * m * (d * F - d * g * m * μk)) / m,
                                m != 0)))

                    .SubstituteEqLs(vals)

                    .AssertEqTo(new Or(vf == -1.7832554500127007, vf == 1.7832554500127007));

            }
            #endregion
            
            #region PSE 5E E7.11
            {
                // A block of mass 1.6 kg is attached to a horizontal spring that
                // has a force constant of 1.0 x 10^3 N/m, as shown in Figure
                // 7.10. The spring is compressed 2.0 cm and is then released
                // from  rest.
                
                // (a) Calculate the  speed of  the block  as it  passes
                // through the equilibrium position x = 0 if the surface is frictionless.

                // (b) Calculate the speed of the block as it passes through
                // the equilibrium position if a constant frictional force of 4.0 N
                // retards its motion from the moment it is released.

                var ΣW = new Symbol("ΣW");
                                
                var Kf = new Symbol("Kf");
                var Ki = new Symbol("Ki");

                var m = new Symbol("m");
                var d = new Symbol("d");
                var k = new Symbol("k");

                var vf = new Symbol("vf");
                var vi = new Symbol("vi");

                var fk = new Symbol("fk");
                
                var W_s = new Symbol("W_s");
                var W_f = new Symbol("W_f");
                
                var x_max = new Symbol("x_max");

                var eqs = new And(

                    W_s == k * (x_max ^ 2) / 2,

                    Kf == m * (vf ^ 2) / 2,
                    Ki == m * (vi ^ 2) / 2,
                    
                    W_f == -fk * d,

                    ΣW == Kf - Ki,

                    ΣW == W_s + W_f,

                    m != 0

                    );

                // vf
                {
                    var vals = new List<Equation>() { m == 1.6, vi == 0, fk == 0, k == 1000, x_max == -0.02 };

                    eqs
                        .EliminateVariables(ΣW, Kf, Ki, W_f, W_s)
                        .IsolateVariable(vf)
                        .LogicalExpand().SimplifyEquation().SimplifyLogical().CheckVariable(m)
                        
                        .AssertEqTo(
                            new Or(
                                new And(
                                    vf == sqrt(-2 * m * (d * fk - m * (vi ^ 2) / 2 - k * (x_max ^ 2) / 2)) / m,
                                    m != 0),
                                new And(
                                    vf == -sqrt(-2 * m * (d * fk - m * (vi ^ 2) / 2 - k * (x_max ^ 2) / 2)) / m,
                                    m != 0)))
                                              
                        .SubstituteEqLs(vals)
                        
                        .AssertEqTo(new Or(vf == 0.5, vf == -0.5));
                }

                // vf
                {
                    var vals = new List<Equation>() { m == 1.6, vi == 0, fk == 4, k == 1000, x_max == -0.02, d == 0.02 };

                    eqs
                        .EliminateVariables(ΣW, Kf, Ki, W_f, W_s)
                        .IsolateVariable(vf)
                        .LogicalExpand().SimplifyEquation().SimplifyLogical().CheckVariable(m)
                        
                        .SubstituteEqLs(vals)
                        
                        .AssertEqTo(new Or(vf == 0.3872983346207417, vf == -0.3872983346207417));
                }
                
            }
            #endregion
                        
            #region PSE 6E P7.3
            {
                // Batman, whose mass is 80.0kg, is dangling on the free end
                // of a 12.0m rope, the other end of which is fixed to a tree
                // limb above. He is able to get the rope in motion as only
                // Batman knows how, eventually getting it to swing enough
                // that he can reach a ledge when the rope makes a 60.0°
                // angle with the vertical. How much work was done by the
                // gravitational force on Batman in this maneuver?

                var m = new Symbol("m");
                var a = new Symbol("a");

                var W = new Symbol("W");
                var F = new Symbol("F");
                var d = new Symbol("d");

                var yA = new Symbol("yA");
                var yB = new Symbol("yB");

                var th = new Symbol("th");

                var len = new Symbol("len");
                
                var eqs = new And(

                    yA == -len,

                    yB == -len * cos(th),

                    d == yB - yA,

                    F == m * a,

                    W == F * d
                              
                    );

                var vals = new List<Equation>()
                { m == 80, len == 12, th == (60).ToRadians(), a == -9.8 };

                eqs
                    .EliminateVariables(F, d, yA, yB)

                    .AssertEqTo(W == a * (len - cos(th) * len) * m)

                    .SubstituteEqLs(vals)
                    
                    .AssertEqTo(W == -4704.0);
            }
            #endregion
                        
            #region PSE 6E P7.23
            {
                // If it takes 4.00J of work to stretch a Hooke’s-law spring
                // 10.0cm from its unstressed length, determine the extra
                // work required to stretch it an additional 10.0cm.

                var WsAB = new Symbol("WsAB");
                
                var WsA = new Symbol("WsA");
                var WsB = new Symbol("WsB");

                var k = new Symbol("k");

                var xA = new Symbol("xA");
                var xB = new Symbol("xB");
                                
                var eqs = new And(
                    
                    WsA == k * (xA ^ 2) / 2,
                    WsB == k * (xB ^ 2) / 2,

                    WsAB == WsB - WsA
                                        
                    );

                var vals = new List<Equation>() { xA == 0.1, xB == 0.2, WsA == 4 };

                eqs

                    .EliminateVariables(WsB, k)

                    .AssertEqTo(
                    
                        WsAB == WsA * (xB ^ 2) / (xA ^ 2) - WsA     
                        
                        )
                        
                    .SubstituteEqLs(vals)
                    
                    .AssertEqTo(WsAB == 12.0);
                
            }
            #endregion
            
            Console.WriteLine("Testing complete");
            
            Console.ReadLine();
        }
    }
}
