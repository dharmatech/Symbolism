using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;

namespace Physics
{
    static class Trig
    {
        public static Symbol Pi = new Symbol("Pi");

        public static MathObject ToRadians(this MathObject n) { return n * Pi / 180; }

        public static MathObject ToDegrees(this MathObject n) { return 180 * n / Pi; }

        public static MathObject ToRadians(this int n) { return new Integer(n) * Pi / 180; }

        public static MathObject ToDegrees(this int n) { return 180 * new Integer(n) / Pi; }

        public static MathObject Sin(MathObject arg)
        { return new Symbolism.Sin(arg).Simplify(); }

        public static MathObject Cos(MathObject arg)
        { return new Symbolism.Cos(arg).Simplify(); }        
    }

    class Point
    {
        public MathObject x;
        public MathObject y;

        public Point() { }

        public Point(int x_val, int y_val)
        { x = new Integer(x_val); y = new Integer(y_val); }

        public Point(MathObject x_val, MathObject y_val)
        { x = x_val; y = y_val; }

        public static Point FromAngle(MathObject angle, MathObject mag)
        { return new Point(Trig.Cos(angle) * mag, Trig.Sin(angle) * mag); }

        public override string ToString()
        { return "Point(" + x + ", " + y + ")"; }

        public static Point operator +(Point a, Point b)
        { return new Point(a.x + b.x, a.y + b.y); }

        public static Point operator *(Point a, MathObject b)
        { return new Point(a.x * b, a.y * b); }

        public static Point operator /(Point a, MathObject b)
        { return new Point(a.x / b, a.y / b); }
    }

    class Obj
    {
        public Point position;
        public Point velocity;
        public Point acceleration;

        public MathObject time;

        public void Print()
        {
            Console.WriteLine(
                "time: " + time + "\n" +
                "position.x: " + position.x + "\n" +
                "position.y: " + position.y + "\n" +
                "velocity.x: " + velocity.x + "\n" +
                "velocity.y: " + velocity.y + "\n" +
                "acceleration.x: " + acceleration.x + "\n" +
                "acceleration.y: " + acceleration.y + "\n");
        }

        public Obj AtTime(MathObject t)
        {
            var dt = t - time;

            return
                new Obj()
                {
                    time = t,
                    acceleration = acceleration,
                    velocity = velocity + acceleration * dt,
                    position = position + velocity * dt + acceleration * dt * dt / new Integer(2)
                };
        }
    }

    static class Calc
    {
        public static MathObject Time(Obj a, Obj b)
        {
            if (!(a.velocity.x == null) &&
                !(b.velocity.x == null) &&
                !(a.acceleration.x == null) &&
                !(a.acceleration.x == new DoubleFloat(0.0)) &&
                !(a.acceleration.x == new Integer(0)))
                return (b.velocity.x - a.velocity.x) / a.acceleration.x;

            if (!(a.velocity.y == null) &&
                !(b.velocity.y == null) &&
                !(a.acceleration.y == null) &&
                !(a.acceleration.y == new DoubleFloat(0.0)) &&
                !(a.acceleration.y == new Integer(0)))
                return (b.velocity.y - a.velocity.y) / a.acceleration.y;

            throw new Exception();
        }
    }

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

            // var g = new DoubleFloat(9.8);

            var g = new Symbol("g");

            var _g = new Point(new Integer(0), -g);

            // An Obj representing the object at A:

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
                    position = new Point(),
                    velocity = new Point(objA.velocity.x, new Integer(0)),
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
                .Substitute(g, new DoubleFloat(9.8))
                .Substitute(Trig.Pi, new DoubleFloat(3.14159))
                .Substitute(vA, new Integer(11))
                );

            Console.WriteLine();

            Console.WriteLine("Maximum height reached: ");

            Console.WriteLine(
                objB.position.y
                .Substitute(g, new DoubleFloat(9.8))
                .Substitute(thA, Trig.ToRadians(20))
                .Substitute(Trig.Pi, new DoubleFloat(3.14159))
                .Substitute(vA, new Integer(11))
                );
            
            Console.ReadLine();
        }
    }
}
