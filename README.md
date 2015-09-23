
Library that implements automatic simplification of algebraic expressions in C#.

To get an idea of the kinds of expressions it handles, see the [tests in this file](https://github.com/dharmatech/Symbolism/blob/master/Tests/Tests.cs).

The automatic simplification algorithm comes from the book "Computer Algebra and Symbolic Computation: Mathematical Methods" by Joel S. Cohen.

The core of the system is in [Symbolism.cs](https://github.com/dharmatech/Symbolism/blob/master/Symbolism/Symbolism.cs).

Symbolism began as a port of the Scheme [MPL library](https://github.com/dharmatech/mpl) to C#.

[GiNaC](http://www.ginac.de/) and [SymbolicC++](http://issc.uj.ac.za/symbolic/symbolic.html) are of course very inspirational.

The "PSE 5E" examples and problems in the unit tests are from the textbook "Physics for Scientists and Engineers, 5th Edition" by Serway and Jewett. Here's an [index](https://github.com/dharmatech/Symbolism/blob/master/Examples/unit-test-index.md) of some of the examples.

[Here's a walk-through of solving a physics problem](https://gist.github.com/dharmatech/d6d499f14c808b159689).

A slightly more complex [walk-through](https://gist.github.com/dharmatech/a5e74ef03d98b3ff1c45).

[Walk-through](https://gist.github.com/dharmatech/a14d1a29a7d4c0728d37) of solving a laws of motion problem.

In action:

![](http://i.imgur.com/7FH36o1.png)

[Unit test](https://github.com/dharmatech/Symbolism/blob/ff09e2c20e026091225f4f303bbb06487a08f58d/Tests/Tests.cs#L2732) for that example.

References:

Computer Algebra and Symbolic Computation: Elementary Algorithms 
by Joel S. Cohen

Computer Algebra and Symbolic Computation: Mathematical Methods 
by Joel S. Cohen 
