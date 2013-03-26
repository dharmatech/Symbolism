using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;
using Physics;
using Utils;

namespace PSE_5E_PROBLEM_5._20_Three_Forces_Find_Acceleration_and_Mass
{
    class Program
    {
        static void Main(string[] args)
        {
            // Three forces, given by _F1 = (-2.00i + 2.00j) N, 
            // _F2 = (5.00i - 3.00j) N, and _F3 = (-45.0i) N, 
            // act on an object to give 
            // it an acceleration of magnitude 3.75 m/s^2.
            // (a) What is the direction of the acceleration? (b) What
            // is the mass of the object? (c) If the object is initially at
            // rest, what is its speed after 10.0 s? 
            // (d) What are the velocity components of the object after 10.0 s?

            var F1x = new Symbol("F1x");
            var F1y = new Symbol("F1y");
            var F2x = new Symbol("F2x");
            var F2y = new Symbol("F2y");
            var F3x = new Symbol("F3x");
            var F3y = new Symbol("F3y");

            var a = new Symbol("a");

            var t = new Symbol("t");

            var obj = new Obj()
            {
                forces = new List<Point>()
                {
                    new Point(F1x, F1y),
                    new Point(F2x, F2y),
                    new Point(F3x, F3y)
                }
            };

            "What is the direction of the acceleration?".Disp(); "".Disp();

            "symbolic:".Disp(); "".Disp();

            obj.TotalForce().ToAngle().Disp(); "".Disp();

            "numeric:".Disp(); "".Disp();

            obj.TotalForce().ToAngle()
                .ToDegrees()
                .Substitute(F1x, -2)
                .Substitute(F1y, 2)
                .Substitute(F2x, 5)
                .Substitute(F2y, -3)
                .Substitute(F3x, -45)
                .Substitute(F3y, 0)
                .Substitute(Trig.Pi, Math.PI)
                .Disp();

            "".Disp();

            "What is the mass of the object?".Disp(); "".Disp();

            obj.totalForce = obj.TotalForce();

            obj.acceleration = Point.FromAngle(obj.totalForce.ToAngle(), a);

            obj.mass = obj.Mass();

            "symbolic:".Disp(); "".Disp();

            obj.mass.Disp(); "".Disp();

            "numeric:".Disp(); "".Disp();

            obj.mass
                .Substitute(a, 3.75)
                .Substitute(F1x, -2)
                .Substitute(F1y, 2)
                .Substitute(F2x, 5)
                .Substitute(F2y, -3)
                .Substitute(F3x, -45)
                .Substitute(F3y, 0)
                .Disp();

            "".Disp();

            "If the object is initially at rest, what is its speed after 10.0 s?".Disp();

            "".Disp();

            obj.velocity = new Point(0, 0);

            obj.time = 0;

            obj.VelocityAtTime(t).Norm().Disp();

            "".Disp();

            obj.VelocityAtTime(t).Norm()
                .Substitute(a, 3.75)
                .Substitute(F1x, -2)
                .Substitute(F1y, 2)
                .Substitute(F2x, 5)
                .Substitute(F2y, -3)
                .Substitute(F3x, -45)
                .Substitute(F3y, 0)
                .Substitute(t, 10)
                .Disp();

            "".Disp();

            "What are the velocity components of the object after 10.0 s?".Disp();

            "".Disp();

            "velocity.x: ".Disp();
            obj.VelocityAtTime(t).x
                .Substitute(a, 3.75)
                .Substitute(F1x, -2)
                .Substitute(F1y, 2)
                .Substitute(F2x, 5)
                .Substitute(F2y, -3)
                .Substitute(F3x, -45)
                .Substitute(F3y, 0)
                .Substitute(t, 10)
                .Disp(); "".Disp();

            "velocity.y: ".Disp();
            obj.VelocityAtTime(t).y
                .Substitute(a, 3.75)
                .Substitute(F1x, -2)
                .Substitute(F1y, 2)
                .Substitute(F2x, 5)
                .Substitute(F2y, -3)
                .Substitute(F3x, -45)
                .Substitute(F3y, 0)
                .Substitute(t, 10)
                .Disp(); "".Disp();

            Console.ReadLine();
        }
    }
}
