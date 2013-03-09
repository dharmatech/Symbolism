using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;

namespace Physics
{
    public static class Misc
    {
        public static MathObject QuadraticEquation(MathObject a, MathObject b, MathObject c, int solution=0)
        {
            if (a == new Integer(0) || a == new DoubleFloat(0.0))
                throw new Exception("a is zero. Equation is not quadratic.");

            var discriminant = b * b - 4 * a * c;

            var half = new Integer(1) / 2;

            if (solution == 0)
                return (-b + (discriminant ^ half)) / (2 * a);

            if (solution == 1)
                return (-b - (discriminant ^ half)) / (2 * a);

            throw new Exception();
        }
    }

    public static class Trig
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

    public class Point
    {
        public MathObject x;
        public MathObject y;

        public Point() { }

        public Point(MathObject x_val, MathObject y_val)
        { x = x_val; y = y_val; }

        //////////////////////////////////////////////////////////////////////
        // overloads for 'int'
        public Point(int x_val, int y_val)
        { x = new Integer(x_val); y = new Integer(y_val); }

        public Point(int x_val, MathObject y_val)
        { x = new Integer(x_val); y = y_val; }

        public Point(MathObject x_val, int y_val)
        { x = x_val; y = new Integer(y_val); }
        //////////////////////////////////////////////////////////////////////

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

        public MathObject Norm()
        { return (x * x + y * y) ^ (new Integer(1) / 2); }
    }

    public class Obj
    {
        public Point position = new Point();
        public Point velocity = new Point();
        public Point acceleration = new Point();

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
    
    public static class Calc
    {
        public static MathObject Time(Obj a, Obj b, int solution = 0)
        {
            if (a.velocity.x != null &&
                b.velocity.x != null &&
                a.acceleration.x != null &&
                a.acceleration.x != new DoubleFloat(0.0) &&
                a.acceleration.x != new Integer(0))
                return (b.velocity.x - a.velocity.x) / a.acceleration.x;

            if (a.velocity.y != null &&
                b.velocity.y != null &&
                a.acceleration.y != null &&
                a.acceleration.y != new DoubleFloat(0.0) &&
                a.acceleration.y != new Integer(0))
                return (b.velocity.y - a.velocity.y) / a.acceleration.y;

            // yf = yi + vyi * t + 1/2 * ay * t^2
            // 0 = 1/2 * ay * t^2 + vyi * t + yi - yf
            // apply quadratic equation to find t
            
            if (a.position.y != null &&
                b.position.y != null &&
                a.velocity.y != null &&
                a.acceleration.y != null &&
                a.acceleration.y != new Integer(0) &&
                a.acceleration.y != new DoubleFloat(0.0))
            {
                var half = new Integer(1) / 2;

                return
                    Misc.QuadraticEquation(
                        half * a.acceleration.y,
                        a.velocity.y,
                        a.position.y - b.position.y,
                        solution);
            }

            throw new Exception();
        }
    }
}
