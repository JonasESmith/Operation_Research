import sympy
import os
import plotly.graph_objects as go
import numpy as np
from prettytable import PrettyTable
from sympy import symbols, pprint, lambdify
from sympy.solvers.solveset import nonlinsolve
from sympy.parsing.sympy_parser import parse_expr
x, y, z = symbols("x y z", real=True)
sympy.init_printing(use_unicode=True)


def part_derive(expr):
    '''Get the partial derivatives of the passed expression.'''
    part_dx = expr.diff(x)
    part_dy = expr.diff(y)
    return part_dx, part_dy


def critical_points(expr):
    '''Find the critical points of the expression.'''
    # Take the first order partial derivatives with respect to x and y.
    fx, fy = part_derive(expr)
    system = [fx, fy]
    solved_symbols = nonlinsolve(system, [x, y])
    solved_system = convert_to_usable(solved_symbols)
    # Set the partial derivatives to 0 and solve each independently for their variable
    return solved_system


def get_values(expr, points):
    '''Plug critical points into the expression and evaluate.'''
    values = []
    for i in range(0, len(points)):
        values.append(expr.subs({x: points[i][0], y: points[i][1]}))
    return values


def convert_to_usable(solved_system):
    '''This is for navigating the horrors that are the nested arguments of a sympy expression.'''
    usable = []
    for i in range(0, len(solved_system.args)):
        usable.append([
            solved_system.args[i].args[0].lamda(0),
            solved_system.args[i].args[1].lamda(0)
            ])
    return usable


def classify(expr, points):
    fx, fy = part_derive(expr)
    fxx, fyy = part_derive(fx)[0], part_derive(fy)[1]
    fxy = part_derive(fx)[1]
    classifications = []
    for i in range(0, len(points)):
        fxx_val = fxx.subs({x:points[i][0], y:points[i][1]})
        fyy_val = fyy.subs({x:points[i][0], y:points[i][1]})
        fxy_val = fxy.subs({x:points[i][0], y:points[i][1]})
        D = (fxx_val * fyy_val) - (fxy_val)**2      
        if D > 0 and fxx_val > 0:
            classifications.append("relative minimum")
        elif D > 0 and fxx_val < 0: 
            classifications.append("relative maximum")
        elif D < 0:
            classifications.append("saddle point")
        else:
            classifications.append("no conclusion")
    return classifications


def plot_stuff(z_data):
    working_directory = os.path.dirname(os.path.normpath(__file__))
    x_data = np.linspace(0, 2*np.pi)
    y_data = np.linspace(0, 2*np.pi)
    
    fig = go.Figure(go.Surface(
        contours = {
            "x": {"show": True, "start": 1.5, "end": 2, "size": 0.04, "color":"white"},
            "z": {"show": True, "start": 0.5, "end": 0.8, "size": 0.05}
        },
        x = x_data,
        y = y_data,
        z = z_data
    ))
    fig.update_layout(
        scene = {
            "xaxis": {"nticks": 20},
            "zaxis": {"nticks": 4},
            'camera_eye': {"x": 0, "y": -1, "z": 0.5},
            "aspectratio": {"x": 1, "y": 1, "z": 0.2}
        })
    fig.write_html(working_directory + '\plot\plot.html', auto_open=True)


if __name__ == "__main__":
    # The main portion of the program, determines and classifies
    # all major points of the function.
    expr = parse_expr("sin(x) + cos(x) + sin(y) - cos(y)", locals())
    points = critical_points(expr)
    classes = (classify(expr, points))
    func_values = get_values(expr, points)

    # Displays the information in tabular format
    t = PrettyTable()
    t.title = f"g(x, y) = {expr}"
    t.field_names = ["Points", "Values", "Classification"]
    for i in range(0, len(func_values)):
        t.add_row([f"{points[i]}", f"{func_values[i]}", classes[i]])
    print(t)

    # Plots the function over a [0, 2*pi] x [0, 2*pi] space.
    func = lambdify(symbols('x y'), expr)
    x_vals = np.linspace(0, 2*np.pi)
    y_vals = np.linspace(0, 2*np.pi)
    X_vals, Y_vals = np.meshgrid(x_vals, y_vals)
    Z = func(X_vals, Y_vals)
    plot_stuff(Z)
'''
Output:

+--------------------------------------------------+
|   g(x, y) = sin(x) + sin(y) + cos(x) - cos(y)    |
+------------------+------------+------------------+
|      Points      |   Values   |  Classification  |
+------------------+------------+------------------+
| [5*pi/4, 3*pi/4] |     0      |   saddle point   |
| [5*pi/4, 7*pi/4] | -2*sqrt(2) | relative minimum |
|  [pi/4, 3*pi/4]  | 2*sqrt(2)  | relative maximum |
|  [pi/4, 7*pi/4]  |     0      |   saddle point   |
+------------------+------------+------------------+
'''