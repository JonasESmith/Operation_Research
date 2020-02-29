import sympy
import numpy as np
from sympy import symbols, Eq, pprint
from sympy.solvers.solveset import nonlinsolve, solveset
from sympy.parsing.sympy_parser import parse_expr
x, y, z, _n = symbols("x y z _n", real=True)
sympy.init_printing(use_unicode=True)


def part_derive(expr):
    part_dx = expr.diff(x)
    part_dy = expr.diff(y)
    return part_dx, part_dy


def critical_points(expr):
    # Take the first order partial derivatives with respect to x and y.
    fx, fy = part_derive(expr)
    system = [fx, fy]
    solved_symbols = nonlinsolve(system, [x, y])
    solved_system = convert_to_usable(solved_symbols)
    # Set the partial derivatives to 0 and solve each independently for their variable
    return solved_system


def get_maxima_minima(expr, points):
    for i in range(0, len(points)):
        pprint(expr.subs({x: points[i][0], y: points[i][1]}))


def convert_to_usable(solved_system):
    usable = []
    for i in range(0, len(solved_system.args)):
        usable.append([
            solved_system.args[i].args[0].lamda(0),
            solved_system.args[i].args[1].lamda(0)
            ])
    return usable

# Not used for this problem but I made it anyway.
def classify(expr, points):
    fx, fy = part_derive(expr)
    fxx, fyy = part_derive(fx)[0], part_derive(fy)[1]
    fxy = part_derive(fx)[1]
    classifications = {}
    for i in range(0, len(points)):
        fxx_val = fxx.subs({x:points[i][0], y:points[i][1]})
        fyy_val = fyy.subs({x:points[i][0], y:points[i][1]})
        fxy_val = fxy.subs({x:points[i][0], y:points[i][1]})
        D = (fxx_val * fyy_val) - (fxy_val)**2      
        if D > 0 and fxx_val > 0:
            classifications[(points[i][0], points[i][1])] = "relative minimum"
        elif D > 0 and fxx_val < 0: 
            classifications[(points[i][0], points[i][1])] = "relative maximum"
        elif D < 0:
            classifications[(points[i][0], points[i][1])] = "saddle point"
        else:
            classifications[(points[i][0], points[i][1])] = "no conclusion"
    return classifications

    
if __name__ == "__main__":
    expr = parse_expr("sin(x) + cos(x) + sin(y) - cos(y)", locals())
    points = critical_points(expr)
    print(classify(expr, points))
    get_maxima_minima(expr, points)