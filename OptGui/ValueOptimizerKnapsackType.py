# Solve type 1918
from prettytable import PrettyTable


def solve(names, values, weights, inclusive_value_limit, show_tables=True):
    x = PrettyTable()
    x.title = "First Stage"
    steps = range(0, inclusive_value_limit + 1)
    fields = [str(i) for i in range(0, inclusive_value_limit + 1)]
    fields.insert(0, 'f \ x')
    x.field_names = fields.copy()

    n_rows = []
    for i in range(0, len(names)):
        name, weight, value = (names[i], values[i], weights[i])
        divisible = 0
        row = [f'{name}']
        n_row = []
        for j in steps:
            if j != 0 and j % value == 0:
                divisible += 1
            row.append(weight*divisible)
            n_row.append(weight*divisible)
        x.add_row(row)
        n_rows.append(n_row)
    if show_tables:
        print(x)

    u = PrettyTable()
    u.title = "u"
    fields[0] = ''
    u.field_names = fields.copy()

    m_rows = []
    d_rows = []
    f_index = len(n_rows) - 1
    m_index = -1
    maxes, decisions = recurse(n_rows[len(n_rows) - 1], [0 for i in steps], max([i for i in steps]), [], [])
    m_rows.append(maxes)
    d_rows.append(decisions)
    while f_index > 0:
        f_index -= 1
        m_index += 1
        maxes, decisions = recurse(n_rows[f_index], m_rows[m_index], max([i for i in steps]), [], [])
        m_rows.append(maxes)
        d_rows.append(decisions)
    
    if show_tables:
        for i in range(0, len(m_rows)):
            index = len(m_rows) - i
            m_row = m_rows[i].copy()
            d_row = d_rows[i].copy()
            m_row.insert(0, f'm:{index} (u)')
            d_row.insert(0, f"d:{index} (u)")
            u.add_row(m_row)
            u.add_row(d_row)
        print(u)
    optimal_choice(m_rows, d_rows, names, values, weights, inclusive_value_limit)

    
def recurse(f, m, index, maxes, decisions):
    if index > -1:
        values = []
        for i in range(0, index + 1):
            values.append(f[i] + m[index - i])
        max_index = values.index(max(values))
        maxes.append(values[max_index])
        decisions.append(max_index)
        return recurse(f, m, index - 1, maxes, decisions)
    else:
        maxes.reverse()
        decisions.reverse()
        return maxes, decisions


def optimal_choice(m_rows, d_rows, names, values, weights, inclusive_value_limit):
    m_rows.reverse()
    d_rows.reverse()
    optimal_value = m_rows[0][len(m_rows[0]) - 1]
    items_count = []  
    for i in range(0, len(m_rows)):
        weight = d_rows[i][inclusive_value_limit]
        inclusive_value_limit -= weight
        if i == 0:
            items_count.append(weight // weights[i])
        else:
            items_count.append(weight // weights[i])

    print(f"The optimal Value is: {optimal_value}")
    print(f"The item choices are as follows: ")
    for i in range(0, len(items_count)):
        print(f"{items_count[i]} of item {names[i]} ")
    return optimal_value, items_count
        


if __name__ == "__main__":
    names = ['I', 'II', 'III', 'IV']
    weights = [10, 25, 45, 60]
    values = [1, 2, 3, 4]
    solve(names, weights, values, 10, show_tables=True)

