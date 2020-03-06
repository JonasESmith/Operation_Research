from pulp import *

# this is the quiz3 script I used...
# could be a jumping off point for the project_5

# maximize :
# z = 2x1 + 10x2 + x3
#subject to :
# 5x + 2y +  z <= 15
# 2x +  y + 7z <= 20
#  x + 3y + 2z <= 25

# Setup the problem to be a pure integer program
x = LpVariable("x", 0, None, cat='Integer')
y = LpVariable("y", 0, None, cat='Integer')
z = LpVariable("z", 0, None, cat='Integer')

# init problem
prob = LpProblem("LPP", LpMaximize)

# constraint functions
prob += 5*x + 2*y  + 1*z <= 15
prob += 2*x + 1*y  + 7*z <= 20
prob += 1*x + 3*y  + 2*z <= 25

# objective function
prob += 2*x + 10*y + 1*z 

# solve the IPP
prob.solve()

# Show solution
print(f"Status: {LpStatus[prob.status]}")
print( '{0}  {1}  {2}'
        .format(' x ', ' y ', ' z ') )
print( '{0}, {1}, {2}'
        .format(value(x), value(y), value(z)))