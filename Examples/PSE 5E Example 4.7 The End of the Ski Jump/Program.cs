using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;
using Physics;
using Utils;

namespace PSE_5E_Example_4._7_The_End_of_the_Ski_Jump
{
    class Program
    {
        static void Main(string[] args)
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

            "Where does he land on the incline?".Disp(); nl();

            "x position (symbolic):".Disp();

            intersection.x.Disp(); nl();

            "y position (symbolic):".Disp();

            intersection.y.Disp(); nl();

            "x position (numeric):".Disp();

            numeric(intersection.x).Disp(); nl();

            "y position (numeric):".Disp();

            numeric(intersection.y).Disp(); nl();

            var objB = new Obj()
            {
                position = intersection,
                acceleration = _g
            };

            "Determine how long the jumper is airborne".Disp(); nl();

            "symbolic:".Disp();

            var timeB = Calc.Time(objA, objB, 1);

            timeB.Disp(); nl();

            "numeric:".Disp();

            numeric(timeB).Disp(); nl();

            objB = objA.AtTime(timeB);

            "Determine his vertical component of velocity just before he lands".Disp();
            nl();

            "symbolic:".Disp();

            objB.velocity.y.Disp(); nl();

            "numeric:".Disp();

            numeric(objB.velocity.y).Disp();

            Console.ReadLine();
        }
    }
}
