# Optimization Demos
A series of demos on combinatorial optimization, written in C#, designed to help software 
developers learn the basics of Constraint Programming, along with Linear and 
Mixed-Integer Programming.

**Note: Portions of these demos make use of the Gurobi solver which requires a license.
Details on getting an evaluation license to run the full samples can 
be found on [Gurobi.com](http://www.gurobi.com/downloads/evaluation-request).**


## Bss.Optimization.Sudoku.sln (Google CP & Microsoft Solver Foundation)

### Constraint Solver
Uses constraint solvers to find all valid solutions to a sudoku 
puzzle based on the input "hints". Project is limited, by code, to puzzles with less than 100000 possible solutions in order to protect system memory and CPU.

**Note: To switch between the various solvers, uncomment the appropriate 
model in the Main method found in the 
Bss.Optimization.Sudoku.Program.cs file. To change puzzles, uncomment the appropriate set of puzzle hints in the same file.** 

## Bss.Optimization.Appetizers.sln (all solvers) & Bss.Optimization.Appetizers.Glop.sln (excludes Gurobi)
Multiple solution implementations to the very simple, knapsack-style problem described
in [XKCD 287](http://xkcd.com/287). 

**Note: To switch between the various optimizers, uncomment the appropriate 
implementation in the GetOptimizer method found in the 
Bss.Optimization.Appetizers.Program.cs file.**

### Naive Solution
Traverses the entire possible search-space attempting to locate feasible solutions, 
then searches all feasible solutions to determine the optimal solution.  This implementation
fails when the search space is very large.

### LP Optimization using the Gurobi Solver
Uses a Linear Program with integer constraints and a linear objective along with integer 
variables to determine the optimal solution. This implementation works well even with an
extremely large search space.  The Gurobi solver requires a license, details of which can 
be found on [Gurobi.com](http://www.gurobi.com/).

### LP Optimization using the Google Solver
Uses a Linear Program with integer constraints and a linear objective along with integer 
variables to determine the optimal solution. This implementation works well even with an
extremely large search space.  The GLOP solver is Google's open-source linear programming system, 
details of which can be found on [developers.google.com](https://developers.google.com/optimization/lp/glop).


 
## Bss.Optimization.Pottery.sln (all solvers) & Bss.Optimization.Pottery.Glop.sln (excludes Gurobi)
Three solution implementations for the Pete's Pottery Paradise problem described in the
presentation, [A Developer's Guide to Finding Optimal Solutions](http://1drv.ms/1QqBcbh) and others. 
All solutions exist within the confines of a single Visual Studio solution file.

**Note: To switch between the various optimizers, uncomment the appropriate 
implementation in the Create method found in the 
Bss.Optimization.Pottery.Test.Extensions.cs file.**

### Naive Solution
Traverses the entire possible search-space attempting to locate feasible solutions, 
then searches all feasible solutions to determine the optimal solution.  This implementation
fails when the search space is very large.

### LP Optimization using the Gurobi Solver
Uses a Linear Program with linear constraints and a linear objective along with integer 
variables to determine the optimal solution. This implementation works well even with an
extremely large search space.  The Gurobi solver requires a license, details of which can 
be found on [Gurobi.com](http://www.gurobi.com/).

### LP Optimization using the Google Solver
Uses a Linear Program with linear constraints and a linear objective along with integer 
variables to determine the optimal solution. This implementation works well even with an
extremely large search space.  The GLOP solver is Google's open-source linear programming system, 
details of which can be found on [developers.google.com](https://developers.google.com/optimization/lp/glop).
