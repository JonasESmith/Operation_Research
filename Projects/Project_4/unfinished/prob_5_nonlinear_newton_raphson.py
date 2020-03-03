import sympy
import os
import plotly.graph_objects as go
import numpy as np
from prettytable import PrettyTable
from sympy import symbols, pprint, lambdify, Matrix
from sympy.solvers.solveset import nonlinsolve
from sympy.parsing.sympy_parser import parse_expr
# L = lambda; M = Mu
x, y, z, L, M = symbols('x y z L M', real=True)
sympy.init_printing(use_unicode=False)


def latex_dump_row(params):
    print(" " + " & ".join(params) + " \\\\ \n \\hline")


def newton_raphson_5_dimensions(expr, p0, tol=1e-3, delta=1e-3, small=1e-3):
    table = PrettyTable()
    table.title = f"Extrema of {str(expr)}"
    names = ['Index', 'x', 'y', 'z', 'L', 'M', 'L(x, y, z, L, M)']
    table.field_names = names
    latex_dump_row(names)
    mat = Matrix([expr])
    # Builds a jacobian from the provided matrix
    j = mat.jacobian([x, y, z, L, M])
    pprint(j)
    itr = 0
    while itr < 5:
        itr += 1
        sub_dict = {x: p0[0], y: p0[1], z: p0[2], L: p0[3], M: p0[4]}
        hessian = j.jacobian([x, y, z, L, M])   
        h_inverse = hessian.inv().subs(sub_dict)
        gradient = j.subs(sub_dict)
        grad_and_inverse = (gradient * h_inverse)
        p = p0 - grad_and_inverse.T
        p0 = p
        val = expr.subs(sub_dict).n()
        params = [f"{itr}", f"{p0[0].n():.3f}", f"{p0[1].n():.3f}", f"{p0[2].n():.3f}", f"{p0[3].n():.3f}", f"{p0[4].n():.3f}", f"{val.n():.3f}"]
        latex_dump_row(params)
        table.add_row(params)
    print(str(table))



if __name__ == "__main__":
    # Read in expression
    expr = parse_expr('x*y*z - L * (x**2 + y**2 + z**2 - 12) - M *(x + y + z - 4)', locals())
    p0 = Matrix([0, 1, 1, 1, 1])
    newton_raphson_5_dimensions(expr, p0, 1e-2)