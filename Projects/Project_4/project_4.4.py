import sympy as sym
import numpy as np
import matplotlib.pyplot as plt
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

# (C)

# g(x,y,z) = hessian[i], for i >= n
# so to find w.
# h_x = x_0 + w*g(x,y,z)

hessian_row = [0,0,0]
x_0 = [0,0,0]
i = 0

hessian_row[0] = hessian[0][0].subs({x:x_0[0], y:x_0[1], z:x_0[2]})
hessian_row[1] = hessian[0][1].subs({x:x_0[0], y:x_0[1], z:x_0[2]})
hessian_row[2] = hessian[0][2].subs({x:x_0[0], y:x_0[1], z:x_0[2]})

h_x = x_0 + w*np.array(hessian_row)
g = f.subs({x:h_x[0], y:h_x[1], z:h_x[2]})

# -0.0572, -0.0284

print(g)
