using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;
using Symbolism.Substitute;
using Physics;
using Utils;

namespace PSE_5E_Problem_5._21_Block_Resting_on_Floor
{
    class Program
    {
        static void Main(string[] args)
        {
            // A 15.0-lb block rests on the floor. (a) What force does
            // the floor exert on the block? (b) If a rope is tied to the
            // block and run vertically over a pulley, and the other end
            // is attached to a free-hanging 10.0-lb weight, what is the
            // force exerted by the floor on the 15.0-lb block? (c) If we
            // replace the 10.0-lb weight in part (b) with a 20.0-lb
            // weight, what is the force exerted by the floor on the
            // 15.0-lb block?

            var thn = new Symbol("thn"); // angle of normal force
            var thg = new Symbol("thg"); // angle of gravity force
            var th3 = new Symbol("th3");

            var Fn = new Symbol("Fn"); // normal force
            var Fg = new Symbol("Fg"); // gravity force
            var F3 = new Symbol("F3"); // rope force

            var m = new Symbol("m");
            var ay = new Symbol("ay");

            var g = new Symbol("g");

            {
                var obj = new Obj() { mass = m };

                obj.acceleration.y = ay;

                obj.forces.Add(new Point() { angle = thn });
                obj.forces.Add(new Point() { angle = thg, magnitude = Fg });

                "Without rope, normal force magnitude:".Disp(); "".Disp();

                obj.ForceMagnitude(obj.forces[0])
                    .Substitute(thn, (90).ToRadians())
                    .Substitute(thg, (270).ToRadians())
                    .Substitute(Trig.Pi, Math.PI)
                    .Substitute(Fg, 15)
                    .Substitute(ay, 0)
                    .Disp();

                "".Disp();
            }

            {
                var obj = new Obj() { mass = m };

                obj.acceleration.y = ay;

                obj.forces.Add(new Point() { angle = thn });
                obj.forces.Add(new Point() { angle = thg, magnitude = Fg });
                obj.forces.Add(new Point() { angle = th3, magnitude = F3 });

                "Rope & 10 lb weight, normal force magnitude:".Disp(); "".Disp();

                obj.ForceMagnitude(obj.forces[0])
                    .Substitute(thn, (90).ToRadians())
                    .Substitute(thg, (270).ToRadians())
                    .Substitute(th3, (90).ToRadians())
                    .Substitute(Trig.Pi, Math.PI)
                    .Substitute(Fg, 15)
                    .Substitute(F3, 10)
                    .Substitute(ay, 0)
                    .Disp();
            }

            Console.ReadLine();
        }
    }
}
