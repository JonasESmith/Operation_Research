
def function(x):
    return x**3 - x + 1

def divide_interval(interval, count=4):
    distance = interval[1] - interval[0]
    dx = distance / count
    out = [interval[0] + dx]
    for i in range(1, count - 1):
        out.append(out[i - 1] + dx)
    return out



def three_point_search(func, interval, prob_type, tol=1e-2):
    if prob_type == "maximize":
        pass
    else:
        func_vals = []
        inner_interval = divide_interval(interval)
        full_interval = inner_interval.copy()
        full_interval.insert(0, interval[0])
        full_interval.append(interval[1])
        for i in range(0, len(inner_interval)):
            func_vals.append(function(inner_interval[i]))
        index_of_min = func_vals.index(min(func_vals)) + 1
        if (interval[1] - interval[0] < tol):
            return full_interval[index_of_min]
        else:
            new_interval = []
            new_interval.append(full_interval[index_of_min - 1])
            new_interval.append(full_interval[index_of_min + 1])
            return three_point_search(func, new_interval, prob_type)
    




print(three_point_search(function, [0, 1.28], "minimize"))

