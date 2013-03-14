using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;
using Physics;
using Utils;

namespace PSE_5E_Problem_4._11_Snowball_Fight
{
    class Program
    {
        static void Main(string[] args)
        {
            // One strategy in a snowball fight is to throw a first snowball at a 
            // high angle over level ground. While your opponent is watching the
            // first one, you throw a second one at a low angle and timed to arrive 
            // at your opponent before or at the same time as the first one. Assume 
            // both snowballs are thrown with a speed of 25.0 m/s. The first one is 
            // thrown at an angle of 70.0° with respect to the horizontal.
            // 
            // (a) At what angle should the second (low-angle) snowball be thrown 
            // if it is to land at the same point as the first?
            //
            // (b) How many seconds later should the second snowball be thrown if it 
            // is to land at the same time as the first?

            var xA = new Symbol("xA");      // position.x at point A
            var yA = new Symbol("yA");      // position.y at point A
            var th1A = new Symbol("th1A");  // angle of snowball 1 at point A
            var vA = new Symbol("vA");      // velocity at point A

            var g = new Symbol("g");        // magnitude of gravity
            var _g = new Point(0, -g);      // gravity vector

            //Func<MathObject, MathObject> numeric = obj =>
            //    obj
            //        .Substitute(xA, 0)
            //        .Substitute(xB, 1.4)
            //        .Substitute(yA, 0.86)
            //        .Substitute(g, 9.8)
            //        .Substitute(Trig.Pi, 3.14159);

            var obj1A = new Obj()           // snowball 1 at initial point
            {
                position = new Point(xA, yA),
                velocity = Point.FromAngle(th1A, vA),
                acceleration = _g,
                time = 0
            };

            var obj1B = new Obj()            // snowball 1 at final point
            {
                position = new Point(null, 0),
                velocity = new Point(obj1A.velocity.x, null),
                acceleration = _g
            };

            var time1B = Calc.Time(obj1A, obj1B, 1);

            obj1B = obj1A.AtTime(time1B);

            var obj2A = new Obj()           // snowball 2 at initial point
            {
                position = obj1A.position,
                speed = vA,
                acceleration = _g
            };

            var obj2B = new Obj()           // snowball 2 at final point
            {
                position = obj1B.position,
                acceleration = _g
            };

            //Calc.InitialAngle(obj2A, obj2B, 1, 0)
            //    .Substitute(yA, 0)
            //    .Substitute(th1A, (70).ToRadians())
            //    .Substitute(vA, 25)
            //    .Substitute(Trig.Pi, 3.14159)
            //    .Substitute(g, 9.8)
            //    .ToDegrees()
            //    .Substitute(Trig.Pi, 3.14159)
            //    .Disp();

            var th2 = Calc.InitialAngle(obj2A, obj2B, 1, 0);

            obj2A.velocity = Point.FromAngle(th2, vA);

            var time2B = Calc.Time(obj2A, obj2B, 1);

            (time1B - time2B)
                .Substitute(yA, 0)
                .Substitute(th1A, (70).ToRadians())
                .Substitute(vA, 25)
                .Substitute(Trig.Pi, 3.14159)
                .Substitute(g, 9.8)
                .Disp();

            Console.ReadLine();
        }
    }
}
