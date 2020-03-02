'''three_point_method.py: Using the three point interval sequential search method, determine
the local maxima/minima along an initial interval that MUST be unimodal. '''

import os
from prettytable import PrettyTable


def function(x):
    '''Defines the function to be evaluated for.'''
    return x**3 - x + 1


def write_table(table, file_location):
    with open(file_location, 'w') as file:
        file.write(str(table))

def divide_interval(a, b, count=4):
    '''Calculate the new three point interval given [a, b], returns [x1, x2, x3]'''
    distance = b - a
    dx = distance / count
    out = [a + dx]
    for i in range(1, count - 1):
        out.append(out[i - 1] + dx)
    return out


def three_point_search(itr, func, a, b, table, tol=1e-2, verbose=False):
    itr += 1
    '''Implements a system of three point interval search through successive segment elimination.'''    
    three_points = divide_interval(a, b)
    five_points = three_points.copy()
    five_points.insert(0, a)
    five_points.append(b)
    f_vals = [func(three_points[i]) for i in range(0, len(three_points))]
    index_of_min = f_vals.index(min(f_vals)) + 1
    t.add_row([
        f"{itr}", f"{a:.3f}", f"{five_points[1]:.3f}", f"{five_points[2]:.3f}", f"{five_points[3]:.3f}", f"{b:.3f}", 
        f"{func(a):.3f}", f"{f_vals[0]:.3f}", f"{f_vals[1]:.3f}", f"{f_vals[2]:.3f}", f"{func(b):.3f}",
        f"{five_points[index_of_min]:.3f}", f"{f_vals[index_of_min - 1]:.3f}"
        ])
    if (b - a < tol):
        if verbose:
            working_directory = os.path.dirname(os.path.normpath(__file__))
            write_table(table, working_directory + "\\three_point_table.txt")
        return five_points[index_of_min]
    else:
        a = five_points[index_of_min - 1]
        b = five_points[index_of_min + 1]   
        return three_point_search(itr, func, a, b, table, verbose=verbose)
    

if __name__ == "__main__":
    t = PrettyTable()
    t.title = f"Minima of x**3 - x + 1 on the Interval [0, 1.28]"
    t.field_names = ['Iteration', 'a', 'x1', 'x2', 'x3', 'b', 'f(a)', 'f(x1)', 'f(x2)', 'f(x3)', 'f(b)', 'Min Point', 'Min Value']
    three_point_search(0, function, 0, 1.28, t, verbose=True)

