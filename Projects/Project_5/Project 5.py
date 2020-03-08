
import os
from pulp import *
import numpy as np

#Two farmers:
#rows = np.array([0, 1, 2, 3, 4, 5, 6])
#cols = np.array([0, 1, 2, 3, 4, 5, 6])
#game = np.array([[3, 1, 2, 3, 4, 5, 6],
#                 [5, 3, 2, 3, 4, 5, 6],
#                 [4, 4, 3, 3, 4, 5, 6],
#                 [3, 3, 3, 3, 4, 5, 6],
#                 [2, 2, 2, 2, 3, 5, 6],
#                 [1, 1, 1, 1, 1, 3, 6],
#                 [0, 0, 0, 0, 0, 0, 4]])

#Two armys:
rows = np.array(["1 Only", "Half/Half", "2 Only"])
cols = np.array(["1 Only", "Half/Half", "2 Only"])
game = np.array([[5, 10, 20],
                 [8, 2.8, 20],
                 [8, 4, 2]])


def dominance(game, rows, cols):
    #Loop through each row, looking for another row that is always worse.
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
    #Loop through each column, looking for another row that is always worse.
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
    #Reverse sort the rows and columns to delete.
    remRows.sort(reverse=True)
    remCols.sort(reverse=True)
    #Remove the rows.
    for i in remRows:
        game = np.delete(game, i, 0)
        rows = np.delete(rows, i, 0)
    #Remove the columns.
    for i in remCols:
        game = np.delete(game, i, 1)
        cols = np.delete(cols, i, 0)
    return (game, rows, cols)

def optimizePlayer1(game):
    # Define the problem
    prob = LpProblem("Problem", LpMaximize)

    #Create the variables for each option.
    vars = []
    for i in range(0, game.shape[0]):
        #Arbitrary name, low bound, high bound, LpInteger or LpContinuous
        vars.append(LpVariable(f"x{i}", 0, None, LpContinuous))
    
    payoff = LpVariable(f"x{game.shape[0]}", 0, None, LpContinuous)

    #Add the objective function
    prob += payoff

    #Add the constraints for each col in the matrix
    for c in range(0, game.shape[1]):
        prob += lpSum([game[r][c] * vars[r] for r in range(0, game.shape[0])]) >= payoff, f"Payoff to player 1 if player 2 picks {c}"
    
    #Add the constraint that all probabilities must add to 1.
    prob += lpSum([vars[i] for i in range(0, game.shape[0])]) == 1

    prob.solve()
    
    if LpStatus[prob.status] != "Optimal":
        print("Error: Optimization not optimal!")
        return [0 for i in vars]

    return [i.varValue for i in vars];

def optimizePlayer2(game):
    # Define the problem
    prob = LpProblem("Problem", LpMaximize)

    #Create the variables for each option.
    vars = []
    for i in range(0, game.shape[1]):
        #Arbitrary name, low bound, high bound, LpInteger or LpContinuous
        vars.append(LpVariable(f"x{i}", 0, None, LpContinuous))
    
    payoff = LpVariable(f"x{game.shape[0]}", 0, None, LpContinuous)

    #Add the objective function
    prob += -payoff

    #Add the constraints for each row in the matrix
    for r in range(0, game.shape[1]):
        prob += lpSum([game[r][c] * vars[c] for c in range(0, game.shape[1])]) <= payoff, f"Payoff to player 2 if player 1 picks {r}"
    
    #Add the constraint that all probabilities must add to 1.
    prob += lpSum([vars[i] for i in range(0, game.shape[1])]) == 1

    prob.solve()
    
    if LpStatus[prob.status] != "Optimal":
        print("Error: Optimization not optimal!")
        return [0 for i in vars]

    return [i.varValue for i in vars];
    


#Run dominance on the game until no changes occur.
newGame, rows, cols = dominance(game, rows, cols)
while not np.array_equal(game, newGame):
    game = newGame
    newGame, rows, cols = dominance(game, rows, cols)
   
print(f"Simplified Game:");
print(game);
print(f"Player 1: {optimizePlayer1(game)}");
print(f"Player 2: {optimizePlayer2(game)}");
