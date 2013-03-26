using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;

namespace Physics
{
    public static class Misc
    {
        public static MathObject Sqrt(MathObject a)
        { return (a ^ (new Integer(1) / 2)); }

        public static MathObject Sq(MathObject a)
        { return (a ^ 2); }

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

        public static bool NotNull(params MathObject[] objs)
        { return Array.TrueForAll(objs, elt => elt != null); }
    }

    public static class Trig
    {
        public static Symbol Pi = new Symbol("Pi");

        public static MathObject ToRadians(this MathObject n) { return n * Pi / 180; }

        public static MathObject ToDegrees(this MathObject n) { return 180 * n / Pi; }

        public static MathObject ToRadians(this int n) { return new Integer(n) * Pi / 180; }

        public static MathObject ToDegrees(this int n) { return 180 * new Integer(n) / Pi; }

        public static MathObject Sin(MathObject arg)
        { return new Sin(arg).Simplify(); }

        public static MathObject Cos(MathObject arg)
        { return new Cos(arg).Simplify(); }

        public static MathObject Asin(MathObject arg)
        { return new Asin(arg).Simplify(); }

        public static MathObject Atan2(MathObject a, MathObject b)
        { return new Symbolism.Atan2(a, b).Simplify(); }
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

        public static Point operator -(Point a, Point b)
        { return new Point(a.x - b.x, a.y - b.y); }

        public static Point operator *(Point a, MathObject b)
        { return new Point(a.x * b, a.y * b); }

        public static Point operator *(MathObject a, Point b)
        { return b * a; }

        public static Point operator /(Point a, MathObject b)
        { return new Point(a.x / b, a.y / b); }

        public static Point operator /(MathObject a, Point b)
        { return new Point(a / b.x, a / b.y); }
        

        public MathObject Norm()
        { return (x * x + y * y) ^ (new Integer(1) / 2); }

        public MathObject ToAngle() { return Trig.Atan2(y, x); }
    }

    public class Obj
    {
        public MathObject mass;

        public Point position = new Point();
        public Point velocity = new Point();
        public Point acceleration = new Point();

        public MathObject time;

        public MathObject angle;
        public MathObject speed;

        // public Point[] forces;

        public List<Point> forces;

        public MathObject[] forceMagnitudes;

        public MathObject[] forceAngles;

        public Point totalForce;

        

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
                    position = position + velocity * dt + acceleration * dt * dt / 2
                };
        }


        #region ProjectileInclineIntersection derivation

        // xB = xA + vAx t + 1/2 ax t^2 					(9)

        // yB = yA + vAy t + 1/2 ay t^2 					(10)

        // xB - xA = d cos th								(13)

        // yB - yA = d sin th								(14)

        // ax = 0											(11)

        // vAx = vA cos(thA)								(6)

        // vAy = vA sin(thA)								(7)


        // (9):	xB = xA + vAx t + 1/2 ax t^2

        //         xB - xA = vAx t + 1/2 ax t^2 			(9.1)

        // (10):	yB = yA + vAy t + 1/2 ay t^2

        //         yB - yA = vAy t + 1/2 ay t^2 			(10.1)


        // (13):		xB - xA = d cos th

        // /. (9.1)	vAx t + 1/2 ax t^2 = d cos th

        // /. (11)		vAx t = d cos th

        // t 			t = d cos(th) / vAx 				(13.1)


        // (14):		yB - yA = d sin th

        // /. (10.1)	vAy t + 1/2 ay t^2 = d sin th

        // /. (13.1)	vAy [d cos(th) / vAx] + 1/2 ay [d cos(th) / vAx]^2 = d sin th

        //             vAy / vAx d cos(th) + 1/2 ay [d cos(th) / vAx]^2 = d sin th

        //             1/2 ay [d cos(th) / vAx]^2 = d sin th - vAy / vAx d cos(th)

        //             1/2 ay [d cos(th) / vAx]^2 = d [sin(th) - vAy / vAx cos(th)]

        //             1/2 ay d^2 [cos(th) / vAx]^2 = d [sin(th) - vAy / vAx cos(th)]

        //             1/2 ay d [cos(th) / vAx]^2 = [sin(th) - vAy / vAx cos(th)]

        //             d = 2 [sin(th) - vAy / vAx cos(th)] [vAx / cos(th)]^2 / ay 

        // if vAy = 0 then it simplifies to:

        //             d = 2 sin(th) [vAx / cos(th)]^2 / ay 

        #endregion

        public Point ProjectileInclineIntersection(MathObject theta)
        {
            if (theta != null &&
                velocity.x != null &&
                velocity.y != null &&
                acceleration.y != null &&
                acceleration.y != 0 &&
                acceleration.y != 0.0)
            {
                var d =
                    2 * (Trig.Sin(theta) - velocity.y / velocity.x * Trig.Cos(theta))
                    * ((velocity.x / Trig.Cos(theta)) ^ 2)
                    / acceleration.y;

                return
                    new Point(
                        position.x + d * Trig.Cos(theta),
                        position.y + d * Trig.Sin(theta));
            }

            throw new Exception();
        }

        public MathObject TotalForceX()
        {
            MathObject Fx = 0;

            forces.ForEach(elt =>
                {
                    if (elt.x == null)
                        throw new Exception();
                    Fx += elt.x;
                });

            return Fx;
        }

        public MathObject TotalForceY()
        {
            MathObject Fy = 0;

            forces.ForEach(elt =>
            {
                if (elt.y == null)
                    throw new Exception();
                Fy += elt.y;
            });

            return Fy;
        }

        public Point TotalForce()
        { return new Point(TotalForceX(), TotalForceY()); }

        public MathObject AccelerationX()
        {
            if (mass != null) return TotalForceX() / mass;

            throw new Exception();
        }

        public MathObject AccelerationY()
        {
            if (mass != null) return TotalForceY() / mass;

            throw new Exception();
        }

        public Point VelocityAtTime(MathObject t)
        {
            var dt = t - time;

            if (Misc.NotNull(velocity.x, velocity.y, acceleration.x, acceleration.y))
                return velocity + acceleration * dt;

            throw new Exception();
        }

        public MathObject Mass()
        {
            if (Misc.NotNull(totalForce.x, acceleration.x))
                return totalForce.x / acceleration.x;

            if (Misc.NotNull(totalForce.y, acceleration.y))
                return totalForce.y / acceleration.y;

            throw new Exception();
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

        #region InitialAngle notes

        // (1): 			xf = xi + vi cos(th) t + 1/2 ax t^2
        //          		xf - xi = vi cos(th) t
        // t        		t = (xf - xi) / (vi cos(th))       (1.1)

        // (2):     		yf = yi + vi sin(th) t + 1/2 ay t^2
        //              	yf - yi = vi sin(th) t + 1/2 ay t^2
        // /. (1.1)			yf - yi = vi sin(th) {(xf - xi) / (vi cos(th))} + 1/2 ay {(xf - xi) / (vi cos(th))}^2
        //              	yf - yi = sin(th) (xf - xi) / cos(th) + 1/2 ay (xf - xi)^2 / (vi^2 cos^2(th))
        // * 2 cos^2(th)    2 cos^2(th) (yf - yi) = 2 cos^2(th) sin(th) (xf - xi) / cos(th) + 2 cos^2(th) 1/2 ay (xf - xi)^2 / (vi^2 cos^2(th))
        //                  2 cos^2(th) (yf - yi) = 2 cos(th) sin(th) (xf - xi) + ay (xf - xi)^2 / vi^2
        // power-reducing / half angle identity     cos^2(x) = [1 + cos(2x)]/2 :
        //                  2 [1 + cos(2 th)]/2 yf = 2 cos(th) sin(th) xf + ay xf^2 / vi^2
        //                  [1 + cos(2 th)] yf = 2 cos(th) sin(th) xf + ay xf^2 / vi^2
        // double angle formula 2 sin(x) cos(x) = sin(2x) :
        //                  [1 + cos(2 th)] yf = sin(2 th) xf + ay xf^2 / vi^2
        //                  yf + yf cos(2 th) = sin(2 th) xf + ay xf^2 / vi^2
        //                  sin(2 th) xf - yf cos(2 th) = yf - ay xf^2 / vi^2
        // xf = r cos(phi)
        // yf = r sin(phi)
        //                  sin(2 th) r cos(phi) - r sin(phi) cos(2 th) = yf - ay xf^2 / vi^2
        //                  r [ sin(2 th) cos(phi) - sin(phi) cos(2 th) ] = yf - ay xf^2 / vi^2
        // sum/difference identity  sin(x) cos(y) - sin(y) cos(x) = sin(x - y) :
        //                  r sin(2 th - phi) = yf - ay xf^2 / vi^2
        // r = sqrt(xf^2 + yf^2)
        //                  sqrt(xf^2 + yf^2) sin(2 th - phi) = yf - ay xf^2 / vi^2
        // tan(phi) = yf / xf       phi = arctan(yf / xf):
        //                  sqrt(xf^2 + yf^2) sin(2 th - arctan(yf / xf)) = yf - ay xf^2 / vi^2
        // sin(2 th - arctan(yf / xf)) = [yf - ay xf^2 / vi^2] / sqrt(xf^2 + yf^2)
        //
        // arcsin 1: 2 th - arctan(yf / xf) = arcsin{[yf - ay xf^2 / vi^2] / sqrt(xf^2 + yf^2)}         (arcsin1)
        //
        // and
        //
        // arcsin 2: 2 th - arctan(yf / xf) = PI - arcsin{[yf - ay xf^2 / vi^2] / sqrt(xf^2 + yf^2)}    (arcsin2)

        // (arcsin1):
        //
        // 2 th - arctan(yf / xf) = arcsin{[yf - ay xf^2 / vi^2] / sqrt(xf^2 + yf^2)}
        // 2 th = arcsin{[yf - ay xf^2 / vi^2] / sqrt(xf^2 + yf^2)} + arctan(yf / xf)
        // th = {arcsin{[yf - ay xf^2 / vi^2] / sqrt(xf^2 + yf^2)} + arctan(yf / xf)} / 2

        // (arcsin2):
        //
        // 2 th - arctan(yf / xf) = PI - arcsin{[yf - ay xf^2 / vi^2] / sqrt(xf^2 + yf^2)}
        // 2 th = PI - arcsin{[yf - ay xf^2 / vi^2] / sqrt(xf^2 + yf^2)} + arctan(yf / xf)
        // th = [PI - arcsin{[yf - ay xf^2 / vi^2] / sqrt(xf^2 + yf^2)} + arctan(yf / xf)] / 2

        #endregion

        public static MathObject InitialAngle(Obj a, Obj b, int solution = 0, int n = 0)
        {
            if (Misc.NotNull(
                    a.position.x,
                    b.position.x,
                    a.position.y,
                    b.position.y,
                    a.speed,
                    a.acceleration.y)
                &&
                a.speed != 0 &&
                a.speed != 0.0)
            {
                var xf = b.position.x - a.position.x;
                var yf = b.position.y - a.position.y;
                var vi = a.speed;
                var ay = a.acceleration.y;

                if (solution == 0)
                    return
                        (Trig.Asin((yf - ay * (xf ^ 2) / (vi ^ 2)) / Misc.Sqrt((xf ^ 2) + (yf ^ 2))) + Trig.Atan2(yf, xf))
                        /
                        2;
                else if (solution == 1)
                    return
                        (Trig.Pi - Trig.Asin((yf - ay * (xf ^ 2) / (vi ^ 2)) / Misc.Sqrt((xf ^ 2) + (yf ^ 2))) + Trig.Atan2(yf, xf))
                        /
                        2;
            }

            throw new Exception();
        }

        public static Point InitialVelocity(Obj a, Obj b)
        {
            if (a.time != null && b.time != null)
            {
                var dt = b.time - a.time;

                if (Misc.NotNull(
                    a.position.x,
                    a.position.y,
                    b.position.x,
                    b.position.y,
                    a.acceleration.x,
                    a.acceleration.y))
                {
                    var half = new Integer(1) / 2;

                    return
                        (b.position - a.position - half * a.acceleration * (dt ^ 2))
                        /
                        dt;
                }
            }

            throw new Exception();
        }

        //public static MathObject TotalForceX(Obj a)
        //{

        //}

        //public static MathObject AccelerationX(Obj a)
        //{
        //    if (a.forces.All(elt => elt.x != null) && a.mass != null)
        //    {
        //        MathObject Fx = 0;

        //        a.forces.ForEach(elt => Fx = Fx + elt.x);

        //        return Fx / a.mass;
        //    }

        //    throw new Exception();
        //}

        //public static MathObject AccelerationY(Obj a)
        //{
        //    if (a.forces.All(elt => elt.y != null) && a.mass != null)
        //    {
        //        MathObject Fy = 0;

        //        a.forces.ForEach(elt => Fy = Fy + elt.y);

        //        return Fy / a.mass;
        //    }

        //    throw new Exception();
        //}

        //public static MathObject Angle(Obj a)
        //{
        //    a.totalForce.ToAngle()

        //    if (Misc.NotNull(a.totalForce.x, a.totalForce.y))
        //    {
        //        Trig.Atan2(a.totalForce.y, a.totalForce.x)
        //    }

        //    throw new Exception();
        //}
    }
}
