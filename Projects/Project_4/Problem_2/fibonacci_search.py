import numpy as np 
from prettytable import PrettyTable
import os


def latex_dump_row(params):
    print(" " + " & ".join(params) + " \\\\ \n \\hline")


def function(x):
    '''Defines a function to evaluate for given a unimodal interval.
    Note: To search for a minimum, use the negative form of the equation.'''
    return -(x**3 - x + 1)


def fibonacci_numbers(signature, n):
    '''Returns a sequence of fibonacci numbers out to a specified count.'''
    n += 1
    for _ in range(0, n - len(signature)):
        signature.append(sum(signature[-2:]))
    return signature[:n]


def fibonacci_search(a, b, func, epsilon=1e-2, verbose=False):
    '''Searches for a maximum/minimum over a unimodal interval by progressively
    eliminating portions of the interval.'''
    smallest = (b - a) / epsilon
    fib = fibonacci_numbers([1, 1], int(smallest))

    for N in range(0, len(fib)):
        if (fib[N] >= smallest):
            n_of_smallest = N
            break

    epsil_prime = (b - a) / fib[N] 
    if verbose:
        path = os.path.dirname(os.path.normpath(__file__)) + "\\output\\"
        if not os.path.exists(path):
            os.makedirs(path)

        itr = 0
        table = PrettyTable()
        table.title = f"Minima of x**3 - x + 1 on the Interval [0, 1.28]"
        table.field_names = ['Iteration', 'a', 'b', 'fib[N]', 'x1', 'x2', 'f(x1)', 'f(x2)', 'Mid-Point ((x1 + x2) / 2)', 'Midpoint Value']     

        with open(path + "fibonacci_search_table.txt", 'w') as file:
            file.write(f"Smallest Fibonacci Number >= {smallest}: {fib[N]}\n")
            file.write(f"Tolerance/Epsilon: {epsilon}\n")
            file.write(f"(b - a) / Fib[N] = ({b} - {a}) / {fib[N]}\n")
            file.write(f"Epsilon Prime: {epsil_prime}\n\n")    

        table.add_row([f"{itr}", f"{a:.3f}", f"{b:.3f}", f"{fib[N]}", f"N/A", f"N/A", f"N/A", f"N/A", f"N/A", f"N/A"])   

    for N in range(n_of_smallest, 1, -1):       
        dx = fib[N-1]*epsil_prime
        x1 = a + dx
        x2 = b - dx
        fx1, fx2 = func(x1), func(x2)      
        if fx1 > fx2:
            a = x2
        else:
            b = x1
        mid_point, mid_val = ((x1 + x2) / 2), -func((x1 + x2) / 2)
        if verbose:
            itr += 1
            params = [
                f"{itr}", f"{a:.3f}", f"{b:.3f}", 
                f"{fib[N]}", f"{x1:.3f}", f"{x2:.3f}", 
                f"{-fx1:.3f}", f"{-fx2:.3f}", f"{mid_point:.3f}", 
                f"{mid_val:.3f}"
                ]         
            latex_dump_row(params)
            table.add_row(params)   

    if verbose:
        with open(path + "fibonacci_search_table.txt", 'a') as file:
            file.write(str(table))
            file.write(f"\nMinimum Point: {mid_point:.3f}\n")
            file.write(f"Minimum Value: {mid_val:.3f}")

    return mid_point, mid_val
    

if __name__ == "__main__":
    print(fibonacci_search(0, 1.28, function, epsilon=1e-2, verbose=True))