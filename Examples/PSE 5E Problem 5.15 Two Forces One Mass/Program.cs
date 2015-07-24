using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;
using Symbolism.Substitute;
using Physics;
using Utils;

namespace PSE_5E_Problem_5._15_Two_Forces_One_Mass
{
    class Program
    {
        static void Main(string[] args)
        {
            // Two forces _F1 and _F2 act on a 5.00-kg mass. 
            // If F1 = 20.0 N and F2 = 15.0 N, find the accelerations in 
            // (a) and (b) of Figure P5.15.

            var F1 = new Symbol("F1");
            var F2 = new Symbol("F2");

            var th1 = new Symbol("th1");
            var th2 = new Symbol("th2");

            var m = new Symbol("m");

            var obj = new Obj() { mass = m };

            obj.forces.Add(Point.FromAngle(th1, F1));
            obj.forces.Add(Point.FromAngle(th2, F2));

            "symbolic:".Disp(); "".Disp();

            "acceleration.x:".Disp(); "".Disp();

            obj.Acceleration().x.Disp(); "".Disp();

            "acceleration.y:".Disp(); "".Disp();

            obj.Acceleration().y.Disp(); "".Disp();

            "numeric (th2 = 90 deg):".Disp(); "".Disp();

            Console.Write("acceleration is ");

            Console.Write(
                obj.Acceleration().Norm()
                    .Substitute(th1, 0.0)
                    .Substitute(F1, 20)
                    .Substitute(th2, (90).ToRadians())
                    .Substitute(F2, 15)
                    .Substitute(m, 5)
                    .Substitute(Trig.Pi, Math.PI));

            Console.Write(" at ");

            Console.Write(
                obj.Acceleration().ToAngle().ToDegrees()
                    .Substitute(th1, 0.0)
                    .Substitute(F1, 20)
                    .Substitute(th2, (90).ToRadians())
                    .Substitute(F2, 15)
                    .Substitute(m, 5)
                    .Substitute(Trig.Pi, Math.PI));

            Console.Write(" degrees"); "".Disp(); "".Disp();

            "numeric (th2 = 60 deg):".Disp(); "".Disp();

            Console.Write("acceleration is ");

            Console.Write(
                obj.Acceleration().Norm()
                    .Substitute(th1, 0.0)
                    .Substitute(F1, 20)
                    .Substitute(th2, (60).ToRadians())
                    .Substitute(F2, 15)
                    .Substitute(m, 5)
                    .Substitute(Trig.Pi, Math.PI));

            Console.Write(" at ");

            Console.Write(
                obj.Acceleration().ToAngle().ToDegrees()
                    .Substitute(th1, 0.0)
                    .Substitute(F1, 20)
                    .Substitute(th2, (60).ToRadians())
                    .Substitute(F2, 15)
                    .Substitute(m, 5)
                    .Substitute(Trig.Pi, Math.PI));

            Console.Write(" degrees"); Console.WriteLine();
            
            Console.ReadLine();
        }
    }
}
