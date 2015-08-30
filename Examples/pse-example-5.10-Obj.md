
# Classes for representing forces on objects 

In this [walkthrough](https://gist.github.com/dharmatech/a14d1a29a7d4c0728d37), one thing you may notice is the large number of `Symbol` objects that are defined. For example, for just the ball, the following are defined:

```C#
var F1_m1 = new Symbol("F1_m1");        // force 1 on mass 1
var F2_m1 = new Symbol("F2_m1");        // force 2 on mass 1

var th1_m1 = new Symbol("th1_m1");      // direction of force 1 on mass 1
var th2_m1 = new Symbol("th2_m1");      // direction of force 2 on mass 1

var F1x_m1 = new Symbol("F1x_m1");      // x-component of force 1 on mass 1
var F2x_m1 = new Symbol("F2x_m1");      // x-component of force 2 on mass 1

var F1y_m1 = new Symbol("F1y_m1");      // y-component of force 1 on mass 1
var F2y_m1 = new Symbol("F2y_m1");      // y-component of force 2 on mass 1

var Fx_m1 = new Symbol("Fx_m1");        // x-component of total force on mass 1
var Fy_m1 = new Symbol("Fy_m1");        // y-component of total force on mass 1

var ax_m1 = new Symbol("ax_m1");        // x-component of acceleration of mass 1
var ay_m1 = new Symbol("ay_m1");        // y-component of acceleration of mass 1
```

Then we have the many equations for the ball:

```C#
F1x_m1 == F1_m1 * cos(th1_m1),
F2x_m1 == F2_m1 * cos(th2_m1),

F1y_m1 == F1_m1 * sin(th1_m1),
F2y_m1 == F2_m1 * sin(th2_m1),

Fx_m1 == F1x_m1 + F2x_m1,
Fy_m1 == F1y_m1 + F2y_m1,

Fx_m1 == m1 * ax_m1,
Fy_m1 == m1 * ay_m1,
```

That's just for an object with two forces acting on it! The block, which has three forces acting on it, has more even more `Symbol`s and more equations.

I've defined a class [`Obj2`](https://github.com/dharmatech/Symbolism/blob/ffa322d81cc0e8de932b3ae2d3dcf7b90b7c5bfd/Tests/Tests.cs#L112) which represents an object with two forces acting on it. So now, instead of all those symbols and equations for the ball, we write:

```C#
var bal = new Obj2("bal");
```

A nice benefit is that now the ball's associated symbols are object members and can be accessed easily via intellisense:

![](http://i.imgur.com/cNVIAsW.png)

Moreover, the laws of motion equations can be generated with the `Equations` method:

```C#
bal.Equations().DispLong()
```

displays the following at the console:

![](http://i.imgur.com/lCgEJec.png)

The block, which has three forces acting on it is similarly represented with an `Obj3`:

```C#
var blk = new Obj3("blk");
```

Now the definition of the equations, which [took up about 40 lines](https://github.com/dharmatech/Symbolism/blob/ffa322d81cc0e8de932b3ae2d3dcf7b90b7c5bfd/Tests/Tests.cs#L3595-L3635) in the original example, is simply:

```C#
var eqs = new And(

    blk.ax == bal.ay,                   // the block moves right as the ball moves up

    a == blk.ax,

    bal.Equations(),
    blk.Equations()

    );
```

Let's display the resulting set of equations:

```C#
eqs.DispLong()
```

![](http://i.imgur.com/1OaYQhW.png)

The symbolic values are also specified in terms of the members of `bal` and `blk`:

```C#
var vals = new List<Equation>
{
    bal.ax == 0,

    bal.m == m1,

    bal.F1 == T,            bal.th1 == (90).ToRadians(),                // force 1 is straight up
    bal.F2 == m1 * g,       bal.th2 == (270).ToRadians(),               // force 2 is straight down

    blk.ay == 0,

    blk.m == m2,

    blk.F1 == n,            blk.th1 == (90).ToRadians(),                // force 1 is straight up
    blk.F2 == T,            blk.th2 == (180).ToRadians(),               // force 2 is straight down
    blk.F3 == m2 * g,       blk.th3 == (270).ToRadians() + th           // force 3 direction
};
```

If we substitute those values:

```C#
eqs.SubstituteEqLs(vals).DispLong()
```

the equations become:

![](http://i.imgur.com/tE1EuQp.png)

OK, let's find the acceleration. We eliminate unknown values and isolate `a`:

```C#
eqs
    .SubstituteEqLs(vals)

    .EliminateVariables(

        bal.ΣFx, bal.F1x, bal.F2x,
        bal.ΣFy, bal.F1y, bal.F2y,

        blk.ΣFx, blk.F1x, blk.F2x, blk.F3x,
        blk.ΣFy, blk.F1y, blk.F2y, blk.F3y,

        blk.ax, bal.ay,

        T, n
    )

    .IsolateVariable(a)

    .DispLong()
```

to get:

![](http://i.imgur.com/9NjZZ7b.png)


This version of the example in terms of `Obj2` and `Obj3` is checked in as a [test](https://github.com/dharmatech/Symbolism/blob/ffa322d81cc0e8de932b3ae2d3dcf7b90b7c5bfd/Tests/Tests.cs#L3730).
