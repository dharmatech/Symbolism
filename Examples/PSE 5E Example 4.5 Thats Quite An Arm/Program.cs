using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;
using Symbolism.Substitute;
using Physics;
using Utils;

namespace PSE_5E_Example_4._5_Thats_Quite_An_Arm
{
    class Program
    {
        static void Main(string[] args)
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
                time = 0
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

            "How long is it before the stone hits the ground?".Disp();

            "".Disp();
            
            "Symbolic answer:".Disp();

            timeAD.Disp();

            "".Disp();

            "Numeric answer:".Disp();

            timeAD
                .Substitute(g, 9.8)
                .Substitute(thA, (30).ToRadians())
                .Substitute(Trig.Pi, 3.14159)
                .Substitute(vA, 20)
                .Substitute(yD, -45)
                .Disp();

            "".Disp();

            "What is the speed of the stone just before it strikes the ground?".Disp();
            
            "".Disp();

            "Symbolic answer:".Disp();

            objD.velocity.Norm().Disp();

            "".Disp();

            "Numeric answer:".Disp();

            objD.velocity.Norm()
                .Substitute(g, 9.8)
                .Substitute(thA, (30).ToRadians())
                .Substitute(Trig.Pi, 3.14159)
                .Substitute(vA, 20)
                .Substitute(yD, -45)
                .Disp();

            Console.ReadKey();
        }
    }
}
