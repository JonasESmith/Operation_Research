import os
from tkinter import filedialog, messagebox, Tk

import numpy as np
import pandas as pd
from prettytable import PrettyTable
from pulp import *


cwd = os.getcwd()
solverdir = "cbc-2.7.5-win64\\bin\\cbc.exe"
solverdir = os.path.join(cwd, solverdir)
solver = COIN_CMD(path=solverdir)


def dominance(game, rows, cols):
    # Loop through each row, looking for another row that is always worse.
    remRows = []
    for i in range(0, game.shape[0]):
        for j in range(0, game.shape[0]):
            if i == j or j in remRows:
                continue
            rem = True
            for k in range(0, game.shape[1]):
                if game[j][k] > game[i][k]:
                    rem = False
                    break
            if rem:
                remRows.append(j)
    # Loop through each column, looking for another row that is always worse.
    remCols = []
    for i in range(0, game.shape[1]):
        for j in range(0, game.shape[1]):
            if i == j or j in remCols:
                continue
            rem = True
            for k in range(0, game.shape[0]):
                if game[k][j] < game[k][i]:
                    rem = False
                    break
            if rem:
                remCols.append(j)
    # Reverse sort the rows and columns to delete.
    remRows.sort(reverse=True)
    remCols.sort(reverse=True)
    # Remove the rows.
    for i in remRows:
        game = np.delete(game, i, 0)
        rows = np.delete(rows, i, 0)
    # Remove the columns.
    for i in remCols:
        game = np.delete(game, i, 1)
        cols = np.delete(cols, i, 0)
    return (game, rows, cols)


def optimizePlayer1(game):
    # Define the problem
    prob = LpProblem("Problem", LpMaximize)

    # Create the variables for each option.
    vars = []
    for i in range(0, game.shape[0]):
        # Arbitrary name, low bound, high bound, LpInteger or LpContinuous
        vars.append(LpVariable(f"x{i}", 0, None, LpContinuous))

    payoff = LpVariable(f"x{game.shape[0]}", None, None, LpContinuous)

    # Add the objective function
    prob += payoff

    # Add the constraints for each col in the matrix
    for c in range(0, game.shape[1]):
        prob += (
            lpSum([game[r][c] * vars[r] for r in range(0, game.shape[0])]) >= payoff,
            f"Payoff to player 1 if player 2 picks {c}",
        )

    # Add the constraint that all probabilities must add to 1.
    prob += lpSum([vars[i] for i in range(0, game.shape[0])]) == 1

    prob.solve(solver)

    if LpStatus[prob.status] != "Optimal":
        raise ValueError("Player 1 solution returned a non-optimal status")

    return [i.varValue for i in vars]


def optimizePlayer2(game):
    # Define the problem
    prob = LpProblem("Problem", LpMaximize)

    # Create the variables for each option.
    vars = []
    for i in range(0, game.shape[1]):
        # Arbitrary name, low bound, high bound, LpInteger or LpContinuous
        vars.append(LpVariable(f"x{i}", 0, None, LpContinuous))

    payoff = LpVariable(f"x{game.shape[1]}", None, None, LpContinuous)

    # Add the objective function
    prob += -payoff

    # Add the constraints for each row in the matrix
    for r in range(0, game.shape[0]):
        prob += (
            lpSum([game[r][c] * vars[c] for c in range(0, game.shape[1])]) <= payoff,
            f"Payoff to player 2 if player 1 picks {r}",
        )

    # Add the constraint that all probabilities must add to 1.
    prob += lpSum([vars[i] for i in range(0, game.shape[1])]) == 1

    prob.solve(solver)

    if LpStatus[prob.status] != "Optimal":
        raise ValueError("Player 2 solution returned a non-optimal status")

    return [i.varValue for i in vars]


def ask_for_file():
    # Get the current working directory
    # No longer works correctly if not a script
    # cwd = os.path.dirname(os.path.normpath(__file__))
    cwd_exe = os.getcwd()
    # Ask for file with current working directory as starting point
    file = filedialog.askopenfilename(
        initialdir=cwd_exe,
        filetypes=([("Excel Sheet", "*.xlsx")]),
        title="Please Open Your Game's Excel Spreadsheet",
    )

    if os.path.isfile(file):
        return file
    else:
        raise FileNotFoundError("The file specified does not exist! Exiting.")


""" Insertion point for any python module ran as the target
 Keeps things in code blocks when the python script is no longer procedural
 and uses functions and/or classes"""
if __name__ == "__main__":
    # Two armys game define:
    try:
        root = Tk()
        root.withdraw()
        if not os.path.exists(solverdir):
            raise Exception(
                "The solver could not be located! Please keep the exe in the same folder as the CBC folder!"
            )
        # Read information from user defined excel sheet
        excel_file = ask_for_file()
        game_info = pd.read_excel(excel_file)

        # Game Definition Section
        rows = np.array(game_info.iloc[:, 0])
        game_info.drop(game_info.columns[0], axis=1, inplace=True)
        cols = np.array(game_info.columns)
        game = np.asarray(game_info)
        # End Game Definition Section

        # Run dominance on the game until no changes occur.
        newGame, rows, cols = dominance(game, rows, cols)
        while not np.array_equal(game, newGame):
            game = newGame
            newGame, rows, cols = dominance(game, rows, cols)

        # Format columns for table output
        cols = cols.tolist()
        rows = rows.tolist()

        initial_cols = cols.copy()
        initial_cols.insert(0, " ")
        table = PrettyTable(initial_cols)
        table.title = "Simplified Matrix"
        game_list = game.tolist()

        # Insert the appropriate row header for each row
        for i in range(0, len(rows)):
            game_list[i].insert(0, rows[i])
            table.add_row(game_list[i])

        print(table)
        print()

        p1 = PrettyTable(rows)
        p1.title = "Player 1"
        choices = optimizePlayer1(game)

        for i in range(0, len(choices)):
            choices[i] = f"{choices[i]:.2%}"

        p1.add_row(choices)
        p2 = PrettyTable(cols)
        p2.title = "Player 2"
        choices = optimizePlayer2(game)

        for i in range(0, len(choices)):
            choices[i] = f"{choices[i]:.2%}"

        p2.add_row(choices)
        print(p1)
        print()
        print(p2)
        input("Press enter to continue...")
    except FileNotFoundError as fnfe:
        messagebox.showerror("Invalid File", fnfe)
    except ValueError as ve:
        messagebox.showerror("Could Not Optimize", ve)
        # To show the table that was already printed
        input("Press enter to continue...")
    except Exception as e:
        # Catch unexpected errors in a messagebox instead of to the console
        messagebox.showerror("Solution Failed", e)      
