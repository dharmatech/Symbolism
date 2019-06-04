
This is a tutorial on how to build and run a simple Symbolism program on Linux.

The following instructions have been tested on Ubuntu 16.04.3.

Install [.NET Core SDK](https://dotnet.microsoft.com/download).

Install [Visual Studio Code](https://code.visualstudio.com/).

Install the [C# for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp) extension.

Create a new console project:

    $ mkdir symbolism-example
    $ cd symbolism-example
    $ dotnet new console

Add the Symbolism nuget package:

    $ dotnet add package Symbolism

Start Visual Studio Code.

    $ code

Select "File -> Open Folder" from the vscode menu. Select the *symbolism-example* folder.

Select `Program.cs` in the Explorer pane. Replace the code with the following which solves a simple physics exercise:

``` csharp
using System;
using System.Collections.Generic;
using System.Linq;

using Symbolism;
using Symbolism.Substitute;
using Symbolism.EliminateVariable;
using Symbolism.IsolateVariable;
using Symbolism.LogicalExpand;
using Symbolism.SimplifyLogical;

using Symbolism.Utils;

using static Symbolism.Constructors;
using static Symbolism.Trigonometric.Constructors;

namespace symbolism_example
{
    class Program
    {
        static void Main(string[] args)
        {
            // In a local bar, a customer slides an empty beer mug
            // down the counter for a refill. The bartender is momentarily 
            // distracted and does not see the mug, which slides
            // off the counter and strikes the floor 1.40 m from the
            // base of the counter. If the height of the counter is 
            // 0.860 m, (a) with what velocity did the mug leave the
            // counter and (b) what was the direction of the mug’s 
            // velocity just before it hit the floor?

            var xA = new Symbol("xA");
            var yA = new Symbol("yA");

            var xB = new Symbol("xB");
            var yB = new Symbol("yB");

            var vxA = new Symbol("vxA");
            var vyA = new Symbol("vyA");

            var vxB = new Symbol("vxB");
            var vyB = new Symbol("vyB");

            var tAB = new Symbol("tAB");

            var ax = new Symbol("ax");
            var ay = new Symbol("ay");

            var thB = new Symbol("thB");
            var vB = new Symbol("vB");

            var eqs = and(

                vxB == vxA + ax * tAB,
                vyB == vyA + ay * tAB,

                tan(thB) == vyB / vxB,

                xB == xA + vxA * tAB + ax * (tAB ^ 2) / 2,
                yB == yA + vyA * tAB + ay * (tAB ^ 2) / 2,

                xB != 0
            );

            var vals = new List<Equation>() { xA == 0, yA == 0.86, /* vxA */ vyA == 0, xB == 1.4, yB == 0, /* vxB vyB vB thB */ /* tAB */ ax == 0, ay == -9.8 };

            var zeros = vals.Where(eq => eq.b == 0).ToList();

            DoubleFloat.tolerance = 0.00001;

            eqs
                .SubstituteEqLs(zeros)
                .EliminateVariables(thB, vxB, vyB, tAB)
                .IsolateVariable(vxA)
                .LogicalExpand()
                .DispLong()
                .SubstituteEqLs(vals)
                .Disp();

            eqs
                .SubstituteEqLs(zeros)
                .EliminateVariables(vxB, vyB, tAB, vxA)
                .LogicalExpand()
                .CheckVariable(xB)
                .SimplifyLogical()
                .IsolateVariable(thB)
                .DispLong()
                .SubstituteEqLs(vals)
                .Disp();

            DoubleFloat.tolerance = null;
        }
    }
}

```

Press `ctrl+f5` to run the program.

The symbolic and numeric answers will be displayed in the console:

![](https://i.imgur.com/GIlICgo.png)
