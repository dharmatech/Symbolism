using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;
using Physics;
using Utils;

namespace Tests
{
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

        static void Main(string[] args)
        {
            Action<Equation> AssertIsTrue = (eq) =>
            {
                if (!eq) Console.WriteLine(eq.ToString());
            };

            Func<MathObject, MathObject> sin = obj => Trig.Sin(obj);
            Func<MathObject, MathObject> cos = obj => Trig.Cos(obj);

            {
                var x = new Symbol("x");
                var y = new Symbol("y");
                var z = new Symbol("z");

                Func<int, Integer> Int = (n) => new Integer(n);

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

                // Power.Simplify

                AssertIsTrue((0 ^ x) == 0);
                AssertIsTrue((1 ^ x) == 1);
                AssertIsTrue((x ^ 0) == 1);
                AssertIsTrue((x ^ 1) == x);

                // Product.Simplify

                AssertIsTrue(x * 0 == 0);

                // Difference

                AssertIsTrue(-x == -1 * x);

                AssertIsTrue(x - y == x + -1 * y);


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

                // Console.WriteLine((x + x + x + x) / x);
            }

            #region PSE 5E Example 4.3
            {
                var thA = new Symbol("thA"); // angle at point A
                var vA = new Symbol("vA"); // velocity at point A

                var g = new Symbol("g"); // magnitude of gravity

                var _g = new Point(0, -g); // gravity vector

                var objA =
                    new Obj()
                    {
                        position = new Point(0, 0),
                        velocity = Point.FromAngle(thA, vA),
                        acceleration = _g,
                        time = new Integer(0)
                    };

                var objB =
                    new Obj()
                    {
                        velocity = new Point(objA.velocity.x, 0),
                        acceleration = _g
                    };

                var timeB = Calc.Time(objA, objB);
                var timeC = timeB * 2;

                objB = objA.AtTime(timeB);
                var objC = objA.AtTime(timeC);

                //Console.WriteLine("How far does he dump in the horizontal direction?");

                AssertIsTrue(objC.position.x == 2 * Trig.Cos(thA) * Trig.Sin(thA) * (vA ^ 2) / g);

                //Console.WriteLine("What is the maximum height reached?");

                AssertIsTrue(objB.position.y == (Trig.Sin(thA) ^ 2) * (vA ^ 2) / 2 / g);

                // Console.WriteLine("Distance jumped: ");

                var deg = 3.14159 / 180.0;

                AssertIsTrue(
                    objC.position.x
                    // .Substitute(thA, Trig.ToRadians(20))
                    .Substitute(thA, 20 * deg)
                    .Substitute(g, 9.8)
                    .Substitute(Trig.Pi, 3.14159)
                    .Substitute(vA, 11)
                    ==
                    7.9364536850196412);

                //Console.WriteLine("Maximum height reached: ");

                AssertIsTrue(
                    objB.position.y
                    .Substitute(g, 9.8)
                    .Substitute(thA, Trig.ToRadians(20))
                    .Substitute(Trig.Pi, 3.14159)
                    .Substitute(vA, 11)
                    ==
                    0.72215756424454336);
            }
            #endregion

            #region PSE 5E EXAMPLE 4.5
            {
                // A stone is thrown from the top of a building upward at an
                // angle of 30.0° to the horizontal and with an initial speed of
                // 20.0 m/s, as shown in Figure 4.12. If the height of the building 
                // is 45.0 m, (a) how long is it before the stone hits the ground?
                // (b) What is the speed of the stone just before it strikes the
                // ground?

                var thA = new Symbol("thA"); // angle at point A
                var vA = new Symbol("vA"); // velocity at point A

                var g = new Symbol("g"); // magnitude of gravity

                var _g = new Point(0, -g); // gravity vector

                var objA = new Obj()
                {
                    position = new Point(0, 0),
                    velocity = Point.FromAngle(thA, vA),
                    acceleration = _g,
                    time = new Integer(0)
                };

                var objB = new Obj()
                {
                    velocity = new Point(objA.velocity.x, 0),
                    acceleration = _g,
                };

                var timeB = Calc.Time(objA, objB);

                objB = objA.AtTime(timeB);

                var timeC = timeB * 2;

                var objC = objA.AtTime(timeC);

                var yD = new Symbol("yD");

                var objD = new Obj()
                {
                    position = new Point(null, yD),
                    velocity = new Point(objA.velocity.x, null),
                    acceleration = _g
                };

                var timeAD = Calc.Time(objA, objD, 1);

                objD = objA.AtTime(timeAD);

                // "How long is it before the stone hits the ground?".Disp();

                // "Symbolic answer:".Disp();

                AssertIsTrue(
                    timeAD
                    ==
                    -1 * (g ^ -1) * (-1 * Trig.Sin(thA) * vA + -1 * (((Trig.Sin(thA) ^ 2) * (vA ^ 2) + -2 * g * yD) ^ (new Integer(1) / 2))));

                // "Numeric answer:".Disp();

                AssertEqual(
                    (DoubleFloat)
                    timeAD
                        .Substitute(g, 9.8)
                        .Substitute(thA, (30).ToRadians())
                        .Substitute(Trig.Pi, 3.14159)
                        .Substitute(vA, 20)
                        .Substitute(yD, -45),
                    new DoubleFloat(4.21804787012706),
                    0.0001);

                // "What is the speed of the stone just before it strikes the ground?".Disp();

                // "Symbolic answer:".Disp();

                AssertIsTrue(
                    objD.velocity.Norm()
                    ==
                    (((Trig.Cos(thA) ^ 2) * (vA ^ 2) + (Trig.Sin(thA) ^ 2) * (vA ^ 2) + -2 * g * yD) ^ (new Integer(1) / 2)));

                // "Numeric answer:".Disp();

                AssertEqual(
                    (DoubleFloat)
                    objD.velocity.Norm()
                        .Substitute(g, 9.8)
                        .Substitute(thA, (30).ToRadians())
                        .Substitute(Trig.Pi, 3.14159)
                        .Substitute(vA, 20)
                        .Substitute(yD, -45),
                    new DoubleFloat(35.805027579936315),
                    0.1);
            }
            #endregion

            #region PSE 5E EXAMPLE 4.6
            {
                // An Alaskan rescue plane drops a package of emergency rations 
                // to a stranded party of explorers, as shown in Figure
                // 4.13. If the plane is traveling horizontally at 40.0 m/s and is
                // 100 m above the ground, where does the package strike the
                // ground relative to the point at which it was released?

                var xA = new Symbol("xA");      // position.x at point A

                var yA = new Symbol("yA");      // position.y at point A

                var thA = new Symbol("thA");    // angle at point A

                var vA = new Symbol("vA");      // velocity at point A

                var g = new Symbol("g");        // magnitude of gravity

                var _g = new Point(0, -g);      // gravity vector

                var objA = new Obj()            // obj at the initial position
                {
                    position = new Point(xA, yA),
                    velocity = Point.FromAngle(thA, vA),
                    acceleration = _g,
                    time = 0
                };

                var objB = new Obj()            // obj at the final position
                {
                    position = new Point(null, 0),
                    velocity = new Point(objA.velocity.x, null),
                    acceleration = _g
                };

                var timeB = Calc.Time(objA, objB, 1);

                objB = objA.AtTime(timeB);

                //"Where does the package strike the ground relative to the point at which it was released?".Disp(); "".Disp();

                //"symbolic:".Disp();

                //objB.position.x.Disp(); "".Disp();

                AssertIsTrue(
                    objB.position.x
                    ==
                    xA - cos(thA) / g * vA * (-sin(thA) * vA - (((sin(thA) ^ 2) * (vA ^ 2) + 2 * g * yA) ^ new Integer(1) / 2)));

                //"numeric:".Disp();

                //objB.position.x
                //    .Substitute(xA, 0)
                //    .Substitute(yA, 100)
                //    .Substitute(vA, 40)
                //    .Substitute(thA, 0.0)
                //    .Substitute(g, 9.8)
                //    .Disp();

                AssertEqual(
                    objB.position.x
                        .Substitute(xA, 0)
                        .Substitute(yA, 100)
                        .Substitute(vA, 40)
                        .Substitute(thA, 0.0)
                        .Substitute(g, 9.8),
                    180.70158058105025);

                //"".Disp();

                //("What are the horizontal and vertical components " +
                // "of the velocity of the package just before it hits the ground?").Disp(); "".Disp();

                //"symbolic velocity.x:".Disp();

                //objB.velocity.x.Disp(); "".Disp();

                AssertIsTrue(objB.velocity.x == cos(thA) * vA);

                //"symbolic velocity.y:".Disp();

                //objB.velocity.y.Disp(); "".Disp();

                AssertIsTrue(
                    objB.velocity.y
                    ==
                    -1 * (((sin(thA) ^ 2) * (vA ^ 2) + 2 * g * yA) ^ (new Integer(1) / 2)));

                //"numeric velocity.x:".Disp();

                //objB.velocity.x
                //    .Substitute(xA, 0)
                //    .Substitute(yA, 100)
                //    .Substitute(vA, 40)
                //    .Substitute(thA, 0.0)
                //    .Substitute(g, 9.8)
                //    .Disp(); "".Disp();

                AssertEqual(
                    objB.velocity.x
                        .Substitute(xA, 0)
                        .Substitute(yA, 100)
                        .Substitute(vA, 40)
                        .Substitute(thA, 0.0)
                        .Substitute(g, 9.8),
                    40);

                //"numeric velocity.y:".Disp();

                //objB.velocity.y
                //    .Substitute(xA, 0)
                //    .Substitute(yA, 100)
                //    .Substitute(vA, 40)
                //    .Substitute(thA, 0.0)
                //    .Substitute(g, 9.8)
                //    .Disp(); "".Disp();

                AssertEqual(
                    objB.velocity.y
                        .Substitute(xA, 0)
                        .Substitute(yA, 100)
                        .Substitute(vA, 40)
                        .Substitute(thA, 0.0)
                        .Substitute(g, 9.8),
                    -44.271887242357316);
            }
            #endregion

            #region PSE 5E EXAMPLE 4.7

            {
                // A ski jumper leaves the ski track moving in the horizontal 
                // direction with a speed of 25.0 m/s, as shown in Figure 4.14.
                // The landing incline below him falls off with a slope of 35.0°. 
                // Where does he land on the incline?

                var thA = new Symbol("thA");    // angle at point A

                var vA = new Symbol("vA");      // velocity at point A

                var g = new Symbol("g");        // magnitude of gravity

                var _g = new Point(0, -g);      // gravity vector

                var th = new Symbol("th");      // angle of incline

                var objA = new Obj()
                {
                    position = new Point(0, 0),
                    velocity = Point.FromAngle(thA, vA),
                    acceleration = _g,
                    time = 0
                };

                Func<MathObject, MathObject> numeric = obj =>
                    obj
                        .Substitute(vA, 25)
                        .Substitute(thA, 0.0)
                        .Substitute(th, (-35).ToRadians())
                        .Substitute(Trig.Pi, 3.14159)
                        .Substitute(g, 9.8);

                var intersection = objA.ProjectileInclineIntersection(th);

                Action nl = () => "".Disp();

                // "Where does he land on the incline?".Disp(); nl();

                // "x position (symbolic):".Disp();

                // intersection.x.Disp(); nl();

                AssertIsTrue(
                    intersection.x
                    ==
                    -2 * (cos(th) ^ -1) * (cos(thA) ^ 2) * (g ^ -1) * (sin(th) + -1 * cos(th) * (cos(thA) ^ -1) * sin(thA)) * (vA ^ 2));


                //"y position (symbolic):".Disp();

                //intersection.y.Disp(); nl();

                AssertIsTrue(
                    intersection.y
                    ==
                    -2 * (cos(th) ^ -2) * (cos(thA) ^ 2) / g * sin(th) * (sin(th) + -1 * cos(th) * (cos(thA) ^ -1) * sin(thA)) * (vA ^ 2));

                //"x position (numeric):".Disp();

                //numeric(intersection.x).Disp(); nl();

                AssertEqual(numeric(intersection.x), 89.3120879153208);

                //"y position (numeric):".Disp();

                //numeric(intersection.y).Disp(); nl();

                AssertEqual(numeric(intersection.y), -62.536928534704884);

                var objB = new Obj()
                {
                    position = intersection,
                    acceleration = _g
                };

                //"Determine how long the jumper is airborne".Disp(); nl();

                //"symbolic:".Disp();

                var timeB = Calc.Time(objA, objB, 1);

                // timeB.Disp(); nl();

                Func<MathObject, MathObject> sqrt = obj => obj ^ (new Integer(1) / 2);

                AssertIsTrue(
                    timeB
                    ==
                    -1 / g * 
                    (-sin(thA) * vA - 
                        sqrt(
                            (sin(thA) ^ 2) * (vA ^ 2) + 4 * (cos(th) ^ -2) * (cos(thA) ^ 2) * sin(th) * 
                            (sin(th) - cos(th) / cos(thA) * sin(thA)) * 
                            (vA ^ 2))));
                
                //"numeric:".Disp();

                //numeric(timeB).Disp(); nl();

                AssertEqual(numeric(timeB), 3.5724835166128317);

                objB = objA.AtTime(timeB);

                //"Determine his vertical component of velocity just before he lands".Disp();
                //nl();

                //"symbolic:".Disp();

                //objB.velocity.y.Disp(); nl();

                AssertIsTrue(
                    objB.velocity.y
                    ==
                    -sqrt(
                        (sin(thA) ^ 2) * (vA ^ 2) 
                        + 
                        4 * (cos(th) ^ -2) * (cos(thA) ^ 2) * sin(th) * 
                        (sin(th) - cos(th) * (cos(thA) ^ -1) * sin(thA)) * 
                        (vA ^ 2)));

                //"numeric:".Disp();

                //numeric(objB.velocity.y).Disp();

                AssertEqual(
                    numeric(objB.velocity.y),
                    -35.010338462805755);
            }

            #endregion

            Console.WriteLine("Testing complete");

            Console.ReadLine();
        }
    }
}
