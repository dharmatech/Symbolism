using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Symbolism;
using Physics;
using Utils;

namespace PSE_5E_PROBLEM_5._26_KITE_FLYING_CONTEST
{
    class Program
    {
        static void Main(string[] args)
        {
            // You are a judge in a children’s kite-flying contest, and
            // two children will win prizes for the kites that pull most
            // strongly and least strongly on their strings. To measure
            // string tensions, you borrow a weight hanger, some slotted
            // weights, and a protractor from your physics teacher
            // and use the following protocol, illustrated in Figure
            // P5.26: Wait for a child to get her kite well-controlled,
            // hook the hanger onto the kite string about 30 cm from
            // her hand, pile on weights until that section of string is
            // horizontal, record the mass required, and record the
            // angle between the horizontal and the string running up
            // to the kite. (a) Explain how this method works. As you
            // construct your explanation, imagine that the children’s
            // parents ask you about your method, that they might
            // make false assumptions about your ability without concrete
            // evidence, and that your explanation is an opportunity to 
            // give them confidence in your evaluation tech-nique. 
            // (b) Find the string tension if the mass required
            // to make the string horizontal is 132 g and the angle of
            // the kite string is 46.3°.

            var F1 = new Symbol("F1");
            var F2 = new Symbol("F2");
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

            "Tension in line to kite:".Disp(); "".Disp();
            
            obj.ForceMagnitude(_F2)
                .Substitute(th1, (180).ToRadians())
                .Substitute(th2, (46.3 * Math.PI / 180))
                .Substitute(th3, (270).ToRadians())
                .Substitute(F3, 0.132 * 9.8)
                .Substitute(Trig.Pi, Math.PI)
                .Disp();

            Console.ReadLine();
        }
    }
}
