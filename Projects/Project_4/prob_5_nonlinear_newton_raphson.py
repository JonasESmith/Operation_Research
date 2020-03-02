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

