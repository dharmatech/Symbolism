using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;
using Symbolism.Substitute;
using Physics;
using Utils;

namespace PSE_5E_PRO_5._30_SIMPLE_ACCELEROMETER
{
    class Program
    {
        static void Main(string[] args)
        {
            // A simple accelerometer is constructed by suspending a
            // mass m from a string of length L that is tied to the top
            // of a cart. As the cart is accelerated the string-mass 
            // system makes a constant angle th with the vertical. 
            // (a) Assuming that the string mass is negligible compared 
            // with m, derive an expression for the cart’s acceleration
            // in terms of and show that it is independent of
            // the mass mand the length L. 
            // (b) Determine the acceleration of the cart when th = 23.0°.

            var F1 = new Symbol("F1"); // force of string
            var F2 = new Symbol("F2"); // force of gravity

            var th1 = new Symbol("th1");
            var th2 = new Symbol("th2");;

            var _F1 = new Point() { angle = th1 };
            var _F2 = new Point() { angle = th2, magnitude = F2 };

            var m = new Symbol("m");

            var g = new Symbol("g");

            var obj = new Obj() { mass = m };

            obj.acceleration.y = 0;

            obj.forces.Add(_F1);
            obj.forces.Add(_F2);

            _F1.magnitude = obj.ForceMagnitude(_F1);

            ("Derive an expression for the cart’s acceleration in terms " +
            "of and show that it is independent of the mass mand the length L:").Disp();

            "".Disp();

            obj.AccelerationX()
                .Substitute(F2, m * g)
                .Substitute(Trig.Cos(th2), 0)
                .Substitute(Trig.Sin(th2), -1)
                .Disp();

            "".Disp();

            "Determine the acceleration of the cart when th = 23.0°".Disp(); "".Disp();

            obj.AccelerationX()
                .Substitute(F2, m * g)
                .Substitute(Trig.Cos(th2), 0)
                .Substitute(Trig.Sin(th2), -1)
                .Substitute(th1, (90 - 23).ToRadians())
                .Substitute(Trig.Pi, Math.PI)
                .Substitute(g, 9.8)
                .Disp();

            Console.ReadLine();
        }
    }
}
