
Library that implements automatic simplification of algebraic expressions in C#.

To get an idea of the kinds of expressions it handles, see the [tests in this file](https://github.com/dharmatech/Symbolism/blob/master/Tests/Tests.cs).

The automatic simplification algorithm comes from the book "Computer Algebra and Symbolic Computation: Mathematical Methods" by Joel S. Cohen.

The core of the system is in [Symbolism.cs](https://github.com/dharmatech/Symbolism/blob/master/Symbolism/Symbolism.cs).

Symbolism began as a port of the Scheme [MPL library](https://github.com/dharmatech/mpl) to C#.

[GiNaC](http://www.ginac.de/) and [SymbolicC++](http://issc.uj.ac.za/symbolic/symbolic.html) are of course very inspirational.

The "PSE 5E" examples and problems in the unit tests are from the textbook "Physics for Scientists and Engineers, 5th Edition" by Serway and Jewett.

[Here's a walk-through of solving a physics problem](https://gist.github.com/dharmatech/d6d499f14c808b159689).

A slightly more complex [walk-through](https://gist.github.com/dharmatech/a5e74ef03d98b3ff1c45).

[Walk-through](https://gist.github.com/dharmatech/a14d1a29a7d4c0728d37) of solving a laws of motion problem.

In action:

![](http://i.imgur.com/7FH36o1.png)

[Unit test](https://github.com/dharmatech/Symbolism/blob/ff09e2c20e026091225f4f303bbb06487a08f58d/Tests/Tests.cs#L2732) for that example.

# Physics Examples

## Motion in Two Dimensions

[PSE 5E E4.3](https://github.com/dharmatech/Symbolism/blob/e6cf94395a92127b7b25c97748ced065433f00b1/Tests/Tests.cs#L1450)
[PSE 5E P4.9](https://github.com/dharmatech/Symbolism/blob/e6cf94395a92127b7b25c97748ced065433f00b1/Tests/Tests.cs#L5686)
[PSE 5E P4.11](https://github.com/dharmatech/Symbolism/blob/e6cf94395a92127b7b25c97748ced065433f00b1/Tests/Tests.cs#L5686)

## The Laws of Motion

[PSE 5E E5.1](https://github.com/dharmatech/Symbolism/blob/e6cf94395a92127b7b25c97748ced065433f00b1/Tests/Tests.cs#L3124)
[PSE 5E E5.4](https://github.com/dharmatech/Symbolism/blob/450ece65ce4ca4f196b512cba0f5ddeabd024690/Tests/Tests.cs#L3308)
[PSE 5E E5.6](https://github.com/dharmatech/Symbolism/blob/450ece65ce4ca4f196b512cba0f5ddeabd024690/Tests/Tests.cs#L3424)

## Work and Kinetic Energy

[PSE 5E E7.8](https://github.com/dharmatech/Symbolism/blob/e6cf94395a92127b7b25c97748ced065433f00b1/Tests/Tests.cs#L5519)
[PSE 5E E7.11](https://github.com/dharmatech/Symbolism/blob/e6cf94395a92127b7b25c97748ced065433f00b1/Tests/Tests.cs#L5596)
[PSE 6E P7.3](https://github.com/dharmatech/Symbolism/blob/e6cf94395a92127b7b25c97748ced065433f00b1/Tests/Tests.cs#L5686)

# References

Computer Algebra and Symbolic Computation: Elementary Algorithms 
by Joel S. Cohen

Computer Algebra and Symbolic Computation: Mathematical Methods 
by Joel S. Cohen 
