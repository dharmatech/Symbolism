using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;
using Symbolism.Substitute;
using Physics;
using Utils;

namespace PSE_5E_PROBLEM_5._31_TWO_PEOPLE_PULLING_BOAT
{
    class Program
    {
        static void Main(string[] args)
        {
            // Two people pull as hard as they can on ropes attached
            // to a boat that has a mass of 200 kg. If they pull in the
            // same direction, the boat has an acceleration of 
            // 1.52 m/s^2 to the right. If they pull in opposite directions, 
            // the boat has an acceleration of 0.518 m/s^2
            // to the left. What is the force exerted by each person on the
            // boat? (Disregard any other forces on the boat.)

            // Trig.Cos(new DoubleFloat(Math.PI)).Disp();

            var m = new Symbol("m");

            var aAx = new Symbol("aAx");
            var aBx = new Symbol("aBx");

            var objA = new Obj() { mass = m };

            objA.acceleration.x = aAx;

            var _F1A = new Point() { angle = 0 };
            var _F2A = new Point() { angle = 0 };

            objA.forces.Add(_F1A);
            objA.forces.Add(_F2A);

            
            var objB = new Obj() { mass = m };

            objB.acceleration.x = aBx;

            var _F1B = new Point() { angle = 0 };
            var _F2B = new Point() { angle = new DoubleFloat(Math.PI) };

            objB.forces.Add(_F1B);
            objB.forces.Add(_F2B);

            "force 1 magnitude (symbolic):".Disp(); "".Disp();

            Calc.ForceMagnitude(objA, objB, _F1A, _F1B)
                .Substitute(Trig.Cos(0), 1)
                .Disp();

            "force 1 magnitude (numeric):".Disp(); "".Disp();

            Calc.ForceMagnitude(objA, objB, _F1A, _F1B)
                .Substitute(Trig.Cos(0), 1)
                .Substitute(m, 200)
                .Substitute(aAx, 1.52)
                .Substitute(aBx, -0.518)
                .Disp();

            "".Disp();

            "force 2 magnitude (symbolic):".Disp(); "".Disp();

            Calc.ForceMagnitude(objA, objB, _F2A, _F2B)
                .Substitute(Trig.Cos(0), 1)
                .Disp();

            "force 2 magnitude (numeric):".Disp(); "".Disp();

            Calc.ForceMagnitude(objA, objB, _F2A, _F2B)
                .Substitute(Trig.Cos(0), 1)
                .Substitute(m, 200)
                .Substitute(aAx, 1.52)
                .Substitute(aBx, -0.518)
                .Disp();

            "".Disp();



            Console.ReadLine();
        }
    }
}
