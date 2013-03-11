using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;
using Physics;
using Utils;

namespace PSE_5E_Problem_4._9_Projectile_Motion
{
    class Program
    {
        static void Main(string[] args)
        {
            // In a local bar, a customer slides an empty beer mug
            // down the counter for a refill. The bartender is 
            // momentarily distracted and does not see the mug, which slides
            // off the counter and strikes the floor 1.40 m from the
            // base of the counter. If the height of the counter is 
            // 0.860 m, (a) with what velocity did the mug leave the
            // counter and (b) what was the direction of the mug’s 
            // velocity just before it hit the floor?

            var xA = new Symbol("xA");      // position.x at point A
            var yA = new Symbol("yA");      // position.y at point A
            var thA = new Symbol("thA");    // angle at point A
            var vA = new Symbol("vA");      // velocity at point A

            var xB = new Symbol("xB");

            var g = new Symbol("g");        // magnitude of gravity
            var _g = new Point(0, -g);      // gravity vector

            Func<MathObject, MathObject> numeric = obj =>
                obj
                    .Substitute(xA, 0)
                    .Substitute(xB, 1.4)
                    .Substitute(yA, 0.86)
                    .Substitute(g, 9.8)
                    .Substitute(Trig.Pi, 3.14159);

            var objA = new Obj()
            {
                position = new Point(xA, yA),
                velocity = new Point(null, 0),
                acceleration = _g,
                time = 0
            };

            var objB = new Obj()
            {
                position = new Point(xB, 0),
                velocity = new Point(null, null),
                acceleration = _g
            };

            var timeB = Calc.Time(objA, objB, 1);

            objB.time = timeB;

            var _vA = Calc.InitialVelocity(objA, objB);

            objA.velocity = _vA;

            "With what velocity did the mug leave the counter?".Disp(); "".Disp();

            "symbolic:".Disp();

            "x:".Disp(); _vA.x.Disp();

            "y:".Disp(); _vA.y.Disp();

            "".Disp();

            "numeric:".Disp();

            "x:".Disp(); numeric(_vA.x).Disp();

            "y:".Disp(); numeric(_vA.y).Disp();

            "".Disp();

            "What was the direction of the mug’s velocity just before it hit the floor?".Disp(); "".Disp();

            objB = objA.AtTime(timeB);

            "symbolic:".Disp();

            objB.velocity.ToAngle().Disp(); "".Disp();

            "numeric:".Disp();

            numeric(objB.velocity.ToAngle().ToDegrees()).Disp();

            Console.ReadLine();
        }
    }
}
