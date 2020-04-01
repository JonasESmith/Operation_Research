
import os
import numpy as np
from prettytable import PrettyTable

#Array of paths.
#The main diagonal is ignored.
#All valid paths should be non-negative numbers.
#Any path that does not exist should be indiated by a negative number (-1).
paths = np.array([[ 0, 12, -1, 16, -1, 11, -1, -1, -1, -1],
                  [12,  0,  8,  6, -1, -1, -1, 25, -1, -1],
                  [-1,  8,  0,  4, 14, -1, -1, -1, -1, -1],
                  [16,  6,  4,  0, -1, 15,  7, -1, 20, -1],
                  [-1, -1, 14, -1,  0, -1, 17, 10, -1, -1],
                  [11, -1, -1, 15, -1,  0, -1, -1,  3, -1],
                  [-1, -1, -1,  7, 17, -1,  0, -1, -1,  5],
                  [-1, 25, -1, -1, 10, -1, -1,  0, 18, 13],
                  [-1, -1, -1, 20, -1,  3, -1, 18,  0,  9],
                  [-1, -1, -1, -1, -1, -1,  5, 13,  9,  0]])

#paths = np.array([[ 0, 54, 29, 78, -1, -1, -1, -1, -1],
#                  [54,  0, -1, 36, -1, -1, 53, -1, -1],
#                  [29, -1,  0, 55, -1, 64, -1, -1, -1],
#                  [78, 36, 55,  0, 23, -1, -1, -1, 50],
#                  [-1, -1, -1, 23,  0, 44, -1, -1, 51],
#                  [-1, -1, 64, -1, 44,  0, -1, -1, 75],
#                  [-1, 53, -1, -1, -1, -1,  0, 40, 57],
#                  [-1, -1, -1, -1, -1, -1, 40,  0, 24],
#                  [-1, -1, -1, 50, 51, 75, 57, 24,  0]])
#The name of each node. This is only used for display purposes. The index of the node is used for all other purposes.
nodeNames = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J"]

#Class to store inforation about each node.
class Node:
    index = 0
    #Is this node starred?
    starred = False
    #Which node did the shortest path come from?
    nodeFrom = -1
    #What is the length of the path up to this node?
    lengthFrom = -1
    #What branches are available from this node?
    paths = []

    def __init__(self, index):
        self.index = index
        self.paths = []

    def remainingPaths(self):
        remaining = []
        for path in self.paths:
            if not path.circled:
                remaining.append(path)
        return remaining

#Class to store information about each path.
class Path:
    #Index of the starting node
    start = 0
    #Index of the ending node
    end = 0
    #Length of the path
    length = 0
    #Is this path circled?
    circled = False

    def __init__(self, start, end, length):
        self.start = start
        self.end = end
        self.length = length

    def __str__(self):
        return f"({self.start}, {self.end}, {self.length})"

    def __repr__(self):
        return self.__str__()


#Function to print the node tables
def printNodes():
    #Find the max number of paths for a single node.
    maxPaths = 0
    for node in nodes:
        if len(node.paths) > maxPaths:
            maxPaths = len(node.paths)
    
    cols = ["Node", "Starred", "Total Cost"]
    for i in range(0, maxPaths):
        cols.append(f"Branch {i}")
    table = PrettyTable(cols)
    table.title = "Current State"

    for node in nodes:
        if node.index == sink:
            continue
        row = []
        row.append(f"{nodeNames[node.index]}") #Node name
        row.append("*" if node.starred else "") #Star
        row.append(f"{node.lengthFrom}" if node.lengthFrom >= 0 else "") #Total cost
        for i in range(0, maxPaths):
            if i < len(node.paths):
                if node.paths[i].circled:
                    row.append("(" + nodeNames[node.index] + nodeNames[node.paths[i].end] + f"  {node.paths[i].length})")
                else:
                    row.append(nodeNames[node.index] + nodeNames[node.paths[i].end] + f"  {node.paths[i].length}")
            else:
                row.append("")
        table.add_row(row)


    print(table)
    print()


#Make sure the path matrix is square.
if paths.shape[0] != paths.shape[1]:
    print("The path matrix must be square!")
    exit

#Get the number of nodes
size = paths.shape[0]

#Ask the user to input these values. Will need to lookup in nodeNames.
source = 0
sink = 7
debugMode = True

#List to keep track of info about each node.
nodes = [Node(i) for i in range(0, size)]
nodes[source].starred = True
nodes[source].lengthFrom = 0

for row in range(0, size):
    for col in range(0, size):
        #Add a new path if row is not equal to the column, the value in the matrix is non-zero, the path is not returning to the source, and the row is not the sink.
        if row != col and paths[row][col] >= 0 and col != source and row != sink:
            nodes[row].paths.append(Path(row, col, paths[row][col]))

#Sort the paths within each node by length.
for node in nodes:
    node.paths.sort(key=lambda path: path.length)


printNodes()

#Main program loop. Iterate until we reach the sink.
while True:
    #If debug mode is on, pause until the user moves to the next step.
    if debugMode:
        input("Press enter to run the next iteration")
        print()
        print()

    #Look for the shortest total length after travelling a path from all starred nodes.
    #Note: The shortest path from a node will always be first in the list of remaining nodes since the lists are sorted and we are removing them as we go.
    shortestPath = None
    shortestLength = -1
    for node in nodes:
        remainingPaths = node.remainingPaths()
        #Check if the node is starred and has at least one path remaining.
        if node.starred and len(remainingPaths) > 0:
            #Check if the total length so far plus the length of the next path is less than our current minimum.
            #If the shorestLength is -1 we havn't found any node yet, so it will become our current min.
            if node.lengthFrom + remainingPaths[0].length < shortestLength or shortestLength == -1:
                #Update the shorest path and shorest length.
                shortestPath = remainingPaths[0]
                shortestLength = node.lengthFrom + remainingPaths[0].length
    
    #Determine the new node based on where the shorest path ends.
    newNode = nodes[shortestPath.end]
    #Circle the new path
    shortestPath.circled = True
    #Star the new node, update where it came from, and what the total length to the node is.
    newNode.starred = True
    newNode.nodeFrom = shortestPath.start
    newNode.lengthFrom = shortestLength

    #Remove all paths ending at the new node, except for the circled path.
    for node in nodes:
        for path in node.paths:
            if path.end == newNode.index and path != shortestPath:
                del node.paths[node.paths.index(path)]
                break
    
    #Print the current state of the nodes.
    printNodes()

    #If the sink node has been starred, break from the loop.
    if nodes[sink].starred:
        break

#Start at the sink and work backward to find the best path.
outPath = nodeNames[sink]
curNode = sink
while True:
    outPath = nodeNames[nodes[curNode].nodeFrom] + " -> " + outPath
    curNode = nodes[curNode].nodeFrom
    if curNode == source:
        break

#Print the final output
print("Path: " + outPath)
print(f"Total Length: {nodes[sink].lengthFrom}")