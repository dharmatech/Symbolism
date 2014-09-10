using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;
using Physics;
using Utils;

namespace PSE_5E_PROBLEM_5._28_FIRE_HELICOPTER
{
    class Program
    {
        static void Main(string[] args)
        {
            // A fire helicopter carries a 620-kg bucket of water at the
            // end of a cable 20.0 m long. As the aircraft flies back
            // from a fire at a constant speed of 40.0 m/s, the cable
            // makes an angle of 40.0° with respect to the vertical. 
            // (a) Determine the force of air resistance on the bucket.
            // (b) After filling the bucket with sea water, the pilot re-
            // turns to the fire at the same speed with the bucket now
            // making an angle of 7.00° with the vertical. What is the
            // mass of the water in the bucket?

            var F1 = new Symbol("F1"); // force of air resistance
            var F2 = new Symbol("F2"); // force of cable
            var F3 = new Symbol("F3"); // force of gravity

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

            "Force of air resistance on the bucket:".Disp(); "".Disp();

            var FAir =
                obj.ForceMagnitude(_F1)
                    .Substitute(F3, 620 * 9.8)
                    .Substitute(th1, (180).ToRadians())
                    .Substitute(th2, (90-40).ToRadians())
                    .Substitute(th3, (270).ToRadians())
                    .Substitute(Trig.Pi, Math.PI)
                    .Disp();

            "".Disp();

            _F1.magnitude = FAir;

            _F3.magnitude = null;

            var FBucketWithWater =
                obj.ForceMagnitude(_F3)
                    .Substitute(th1, (180).ToRadians())
                    .Substitute(th2, (90 - 7).ToRadians())
                    .Substitute(th3, (270).ToRadians())
                    .Substitute(Trig.Pi, Math.PI);

            "What is the mass of the water in the bucket?".Disp(); "".Disp();

            (FBucketWithWater / 9.8 - 620).Disp();

            Console.ReadLine();
        }
    }
}
