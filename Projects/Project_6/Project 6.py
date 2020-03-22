
import os
import numpy as np

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
#The name of each node. This is only used for display purposes. The index of the node is used for all other purposes.
nodeNames = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J"]

#Class to store inforation about each node.
class Node:
    #Is this node starred?
    starred = False
    #Which node did the shortest path come from?
    nodeFrom = -1
    #What is the length of the path up to this node?
    lengthFrom = -1


#Class to store information about each path.
class Path:
    #Index of the starting node
    start = 0
    #Index of the ending node
    end = 0
    #Length of the path
    length = 0

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
    space = '   '
    #Print the headers
    for node in range(0, size):
        print("*" if nodes[node].starred else " ", end='')
        print(f"{nodeNames[node]}".center(5), end='')
        print(f"{nodes[node].lengthFrom}".rjust(2) if nodes[node].starred else "  ", end=space)
    print()
    for node in range(0, size):
        print("--------", end=space)
    print()

    #Find the max number of paths for a single node.
    maxPaths = 0
    for node in range(0, size):
        if len(nodeList[node]) > maxPaths:
            maxPaths = len(nodeList[node])

    #Print each row of paths
    for i in range(0, maxPaths):
        for node in range(0, size):
            if len(nodeList[node]) >= i+1:
                print(f"{nodeNames[node]}{nodeNames[nodeList[node][i].end]}".ljust(4) + f"{nodeList[node][i].length}".rjust(4), end=space)
            else:
                print("".ljust(8), end=space)
        print()
    print()
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

#List to keep track of info about each node.
nodes = [Node() for i in range(0, size)]
nodes[source].starred = True
nodes[source].lengthFrom = 0

#Construct a list of nodes, each containing paths to its connected nodes.
nodeList = [[] for row in paths]
for row in range(0, size):
    for col in range(0, size):
        #Add a new path if row is not equal to the column, the value in the matrix is non-zero, the path is not returning to the source, and the row is not the sink.
        if row != col and paths[row][col] >= 0 and col != source and row != sink:
            nodeList[row].append(Path(row, col, paths[row][col]))


#Sort the paths within each node by length.
for node in range(0, size):
    nodeList[node].sort(key=lambda path: path.length)


printNodes()

#Main program loop. Iterate until we reach the sink.
while True:
    #Look for the lowest total length from all starred nodes.
    #Note: The shortest path from a node will always be first since the lists are sorted and we are removing them as we go.
    closestNode = -1;
    closestLength = -1;
    for node in range(0, size):
        #Check if the node is starred and has at least one path remaining.
        if nodes[node].starred and len(nodeList[node]) > 0:
            #Check if the total length so far plus the length of the next path is lest than our current min.
            #If the closestLength is -1 we havn't found any node yet, so it will become our current min.
            if nodes[node].lengthFrom + nodeList[node][0].length < closestLength or closestLength == -1:
                #Update the closest node and closest length.
                closestNode = node
                closestLength = nodes[node].lengthFrom + nodeList[node][0].length
    
    #Note: closestNode stores the node containing the shortest path. The new node will be where that path leads.
    newNode = nodeList[closestNode][0].end
    #Star the new node, update where it came from, and what the total length to the node is.
    nodes[newNode].starred = True
    nodes[newNode].nodeFrom = closestNode
    nodes[newNode].lengthFrom = closestLength

    #Remove all paths ending at the new node.
    for node in range(0, size):
        for path in range(0, len(nodeList[node])):
            if nodeList[node][path].end == newNode:
                del nodeList[node][path]
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