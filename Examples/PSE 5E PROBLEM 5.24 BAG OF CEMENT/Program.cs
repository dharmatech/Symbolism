using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;
using Symbolism.Substitute;
using Physics;
using Utils;

namespace PSE_5E_PROBLEM_5._24_BAG_OF_CEMENT
{
    class Program
    {
        static void Main(string[] args)
        {
            // A bag of cement of weight 325 N hangs from three
            // wires as shown in Figure P5.24. Two of the wires make
            // angles th1 = 60.0° and th2 = 25.0° with the horizontal. If
            // the system is in equilibrium, find the tensions
            // T1, T2, and T3 in the wires.

            // From problem 5.25:

            // If the system is in equilibrium, show that 
            // the tension in the left-hand wire is
            // 
            // T1 = Fg cos(th2) / sin(th1 + th2)

            var F3 = new Symbol("F3");

            var th1 = new Symbol("th1");
            var th2 = new Symbol("th2");
            var th3 = new Symbol("th3");

            var _F1 = new Point() { angle = th1 };
            var _F2 = new Point() { angle = th2 };
            var _F3 = new Point() { magnitude = F3, angle = th3 };

            var m = new Symbol("m");

            var obj = new Obj();

            obj.acceleration.x = 0;
            obj.acceleration.y = 0;

            obj.mass = m;

            obj.forces.Add(_F1);
            obj.forces.Add(_F2);
            obj.forces.Add(_F3);

            "F1 magnitude, symbolic:".Disp(); "".Disp();

            obj.ForceMagnitude(_F1).Disp(); "".Disp();

            "F1 magnitude, numeric:".Disp(); "".Disp();

            obj.ForceMagnitude(_F1)
                .Substitute(F3, 325)
                .Substitute(th1, (180 - 60).ToRadians())
                .Substitute(th2, (25).ToRadians())
                .Substitute(th3, (270).ToRadians())
                .Substitute(Trig.Pi, Math.PI)
                .Disp();

            "".Disp();

            "F3 magnitude, numeric:".Disp(); "".Disp();

            obj.ForceMagnitude(_F2)
                .Substitute(F3, 325)
                .Substitute(th1, (180 - 60).ToRadians())
                .Substitute(th2, (25).ToRadians())
                .Substitute(th3, (270).ToRadians())
                .Substitute(Trig.Pi, Math.PI)
                .Disp();

            Console.ReadLine();
        }
    }
}
