'''three_point_method.py: Using the three point interval sequential search method, determine
the local maxima/minima along an initial interval that MUST be unimodal. '''

def function(x):
    '''Defines the function to be evaluated for.'''
    return x**3 - x + 1


def divide_interval(a, b, count=4):
    '''Calculate the new three point interval given [a, b], returns [x1, x2, x3]'''
    distance = b - a
    dx = distance / count
    out = [a + dx]
    for i in range(1, count - 1):
        out.append(out[i - 1] + dx)
    return out


def three_point_search(func, a, b, tol=1e-2):
    '''Implements a system of three point interval search through successive segment elimination.'''
    f_vals = []
    three_points = divide_interval(a, b)
    five_points = three_points.copy()
    five_points.insert(0, a)
    five_points.append(b)
    for i in range(0, len(three_points)):
        f_vals.append(function(three_points[i]))
    index_of_min = f_vals.index(min(f_vals)) + 1
    if (b - a < tol):
        return five_points[index_of_min]
    else:
        a = five_points[index_of_min - 1]
        b = five_points[index_of_min + 1]
        return three_point_search(func, a, b)
    

if __name__ == "__main__":
    print(three_point_search(function, 0, 1.28))

