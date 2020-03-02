import sympy as sym
import numpy as np
from scipy.optimize import fmin

# w = lambda
x,y,z,w = sym.symbols('x y z w', real=True)

# the starting function
f = (y**2 * z**2 + z**2 * x**2 + x**2 * y**2) + (x**2 + y**2 +z**2) - 2 * x*y*z - 2*(z + x -y) - 2*(y*z + z*x - x*y)

# find the gradient of F
# find the Hessian matrix of F
# starting with x_0 = [0, 0, 0]^T use steepest descent to approximate a minimum of F. continue through x_5
# starting with X_0 = [2, -1, 2], apply the newton-raphson method to approximate a minimum of F. continue through x_7.

# (A) 
gradient = [0,0,0]

gradient[0] = sym.diff(f,x)
gradient[1] = sym.diff(f,y)
gradient[2] = sym.diff(f,z)

# for i in gradient: 
#     print(i)

# (B)
hessian = [[0,0,0],
           [0,0,0],
           [0,0,0]]

i = 0

for out_partial in gradient:
    hessian[i][0] = sym.diff(out_partial,x)
    hessian[i][1] = sym.diff(out_partial,y)
    hessian[i][2] = sym.diff(out_partial,z)
    i += 1
