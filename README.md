# Optimization Demos
A series of demos on combinatorial optimization, written in C#, designed to help software 
developers learn the basics of Constraint Programming, along with Linear and 
Mixed-Integer Programming.

**Note: Portions of these demos make use of the Gurobi solver which requires a license.
Details on getting an evaluation license to run the full samples can 
be found on [Gurobi.com](http://www.gurobi.com/downloads/evaluation-request).


## Bss.Optimization.Appetizers.sln
An implementation of a very simple, knapsack-style solution to the problem described
in [XKCD 287](http://xkcd.com/287) using the Gurobi solver. 

 
## Bss.Optimization.Pottery.sln
Two solution implementations for the Pete's Pottery Paradise problem described in the
presentation, [A Developer's Guide to Finding Optimal Solutions](http://1drv.ms/1QqBcbh). 
Both solutions exist within the confines of a single Visual Studio solution file.

**Note: To switch between the naive and LP optimizers, uncomment the appropriate 
implementation in the Create method found in the 
Bss.Optimization.Pottery.Test.Extensions.cs file.**

###Naive Solution
Traverses the entire possible search-space attempting to locate feasible solutions, 
then searches all feasible solutions to determine the optimal solution.  This implementation
fails when the search space is very large but does not require a separate solver.

###LP Optimization using the Gurobi Solver
Uses a Linear Program with linear constraints and a linear objective along with integer 
variables to determine the optimal solution. This implementation, which uses the Gurobi solver,
works well even with an extremely large search space. 