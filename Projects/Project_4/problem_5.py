import sympy
import os
import plotly.graph_objects as go
import numpy as np
from prettytable import PrettyTable
from sympy import symbols, pprint, lambdify
from sympy.solvers.solveset import nonlinsolve
from sympy.parsing.sympy_parser import parse_expr
x, y, z, L, M = symbols('x y z L M', real=True)
sympy.init_printing(use_unicode=True)


def part_derive(expr):
    '''Get the partial derivatives of the passed expression.'''
    part_dx = expr.diff(x)
    part_dy = expr.diff(y)
    part_dz = expr.diff(z)
    part_dL = expr.diff(L)
    part_dM = expr.diff(M)
    return part_dx, part_dy, part_dz, part_dL, part_dM


def critical_points(expr):
    '''Find the critical points of the expression.'''
    # Take the first order partial derivatives with respect to x and y.
    fx, fy, fz, fL, fM = part_derive(expr)
    system = [fx, fy, fz, fL, fM]
    solved_system = nonlinsolve(system, [x, y, z, L, M])
    #solved_system = convert_to_usable(solved_symbols)
    # Set the partial derivatives to 0 and solve each independently for their variable
    return solved_system.args


def get_values(expr, points):
    '''Plug critical points into the expression and evaluate.'''
    values = []
    for i in range(0, len(points)):
        values.append(expr.subs({x: points[i][0], y: points[i][1], z: points[i][2]}))
    return values


def convert_to_usable(solved_system):
    '''This is for navigating the horrors that are the nested arguments of a sympy expression.'''
    usable = []
    for i in range(0, len(solved_system.args)):
        usable.append([
            solved_system.args[i].args[0].lamda(0),
            solved_system.args[i].args[1].lamda(0),
            solved_system.args[i].args[2].lamda(0),
            solved_system.args[i].args[3].lamda(0),
            solved_system.args[i].args[4].lamda(0)
            ])
    return usable


if __name__ == "__main__":
    expr = parse_expr("x*y*z + L * (x**2 + y**2 + z**2 - 12) + M *(x + y + z - 4)", locals())
    points = critical_points(expr)
    values = get_values(parse_expr('x*y*z', locals()), points)

    working_directory = os.path.dirname(os.path.normpath(__file__))
    with open(working_directory + "\problem_5_data.txt", 'w') as file:
        for point in points:
            file.write(f"x: Decimal: {point[0].n()} --- Exact: {point[0]}\n")
            file.write(f"y: Decimal: {point[1].n()} --- Exact: {point[1]}\n")
            file.write(f"z: Decimal: {point[2].n()} --- Exact: {point[2]}\n")
            file.write(f"L: Decimal: {point[3].n()} --- Exact: {point[3]}\n")
            file.write(f"M: Decimal: {point[4].n()} --- Exact: {point[4]}\n")
            file.write("\n")
        file.write("Determining min and max values for f(x, y, z)...\n")
        file.write(f"Max Value: {max(values).n()}\n")
        file.write(f"Min Value: {min(values).n()}\n")
    

    
'''
Output: current_directory\problem_5_data.txt
'''