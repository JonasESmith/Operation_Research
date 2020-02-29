import numpy as np 


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


def fibonacci_search(a, b, func, epsilon=1e-2):
    '''Searches for a maximum/minimum over a unimodal interval by progressively
    eliminating portions of the interval.'''
    smallest = (b - a) / epsilon
    fib = fibonacci_numbers([1, 1], int(smallest))
    for N in range(0, len(fib)):
        if (fib[N] >= smallest):
            n_of_smallest = N
            break
    epsil_prime = (b - a) / fib[N] 
    for N in range(n_of_smallest, 2, -1):   
        dx = fib[N-1]*epsil_prime
        x1 = a + dx
        x2 = b - dx
        fx1, fx2 = func(x1), func(x2)        
        if fx1 > fx2:
            a = x2
        else:
            b = x1
    return ((x1 + x2) / 2), func((x1 + x2) / 2)
    

if __name__ == "__main__":
    print(fibonacci_search(0, 1.28, function, epsilon=1e-2))