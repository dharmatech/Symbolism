using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;
using Physics;
using Utils;

namespace PSE_5E_Problem_4._17_Cannon_Avalanche
{
    class Program
    {
        static void Main(string[] args)
        {
            // A cannon with a muzzle speed of 1 000 m/s is used to
            // start an avalanche on a mountain slope. The target is 
            // 2 000 m from the cannon horizontally and 800 m above
            // the cannon. At what angle, above the horizontal, should
            // the cannon be fired?

            var xA = new Symbol("xA");      // position.x at point A
            var yA = new Symbol("yA");      // position.y at point A
            var thA = new Symbol("thA");  // angle of snowball 1 at point A
            var vA = new Symbol("vA");      // velocity at point A

            var xB = new Symbol("xB");      // position.x at point A
            var yB = new Symbol("yB");      // position.y at point A

            var g = new Symbol("g");        // magnitude of gravity
            var _g = new Point(0, -g);      // gravity vector

            var objA = new Obj()
            {
                position = new Point(xA, yA),
                speed = vA,
                acceleration = _g,
                time = 0
            };

            var objB = new Obj()
            {
                position = new Point(xB, yB),
                acceleration = _g
            };

            "At what angle, above the horizontal, should the cannon be fired?".Disp();

            Calc.InitialAngle(objA, objB)
                .ToDegrees()
                .Substitute(xA, 0)
                .Substitute(yA, 0)
                .Substitute(xB, 2000.0)
                .Substitute(yB, 800)
                .Substitute(vA, 1000)
                .Substitute(g, 9.8)
                .Substitute(Trig.Pi, Math.PI)
                .Disp();

            Console.ReadLine();
        }
    }
}
