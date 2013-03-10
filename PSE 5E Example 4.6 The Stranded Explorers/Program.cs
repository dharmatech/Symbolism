using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;
using Physics;
using Utils;

namespace PSE_5E_Example_4._6_The_Stranded_Explorers
{
    class Program
    {
        static void Main(string[] args)
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

            "Where does the package strike the ground relative to the point at which it was released?".Disp(); "".Disp();

            "symbolic:".Disp();

            objB.position.x.Disp(); "".Disp();

            "numeric:".Disp();

            objB.position.x
                .Substitute(xA, 0)
                .Substitute(yA, 100)
                .Substitute(vA, 40)
                .Substitute(thA, 0.0)
                .Substitute(g, 9.8)
                .Disp();

            ("What are the horizontal and vertical components " +
             "of the velocity of the package just before it hits the ground?").Disp(); "".Disp();

            "symbolic velocity.x:".Disp();

            objB.velocity.x.Disp(); "".Disp();

            "symbolic velocity.y:".Disp();

            objB.velocity.y.Disp(); "".Disp();

            "numeric velocity.x:".Disp();

            objB.velocity.x
                .Substitute(xA, 0)
                .Substitute(yA, 100)
                .Substitute(vA, 40)
                .Substitute(thA, 0.0)
                .Substitute(g, 9.8)
                .Disp(); "".Disp();

            "symbolic velocity.y:".Disp();

            objB.velocity.y
                .Substitute(xA, 0)
                .Substitute(yA, 100)
                .Substitute(vA, 40)
                .Substitute(thA, 0.0)
                .Substitute(g, 9.8)
                .Disp(); "".Disp();

            Console.ReadLine();
        }
    }
}
