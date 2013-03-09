using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;
using Physics;

namespace PSE_Example_4._3_The_Long_Jump
{
    class Program
    {
        static void Main(string[] args)
        {
            // A long-jumper leaves the ground at an angle of 20.0° above
            // the horizontal and at a speed of 11.0 m/s. 
            // (a) How far does he jump in the horizontal direction? 
            // (Assume his motion is equivalent to that of a particle.)
            // (b) What is the maximum height reached?

            // For the purposes of solving the problem, let's take 
            //   point A as the initial position
            //   point B as the peak position
            //   point C as the final position.

            // First we'll get the answer in a symbolic form.

            var thA = new Symbol("thA"); // angle at point A
            var vA = new Symbol("vA"); // velocity at point A

            var g = new Symbol("g"); // magnitude of gravity

            var _g = new Point(0, -g); // gravity vector

            // An Obj representing the object at A:

            var objA =
                new Obj()
                {
                    position = new Point(0, 0),
                    velocity = Point.FromAngle(thA, vA),
                    acceleration = _g,
                    time = 0
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

            Console.WriteLine("How far does he dump in the horizontal direction?");
            Console.WriteLine(objC.position.x);

            Console.WriteLine();

            Console.WriteLine("What is the maximum height reached?");
            Console.WriteLine(objB.position.y);

            Console.WriteLine();

            Console.WriteLine("Now for the numerical solutions:");

            Console.WriteLine();

            Console.WriteLine("Distance jumped: ");

            Console.WriteLine(
                objC.position.x
                .Substitute(thA, Trig.ToRadians(20))
                .Substitute(g, 9.8)
                .Substitute(Trig.Pi, 3.14159)
                .Substitute(vA, 11)
                );

            Console.WriteLine();

            Console.WriteLine("Maximum height reached: ");

            Console.WriteLine(
                objB.position.y
                .Substitute(g, 9.8)
                .Substitute(thA, Trig.ToRadians(20))
                .Substitute(Trig.Pi, 3.14159)
                .Substitute(vA, 11)
                );

            Console.ReadLine();

        }
    }
}
