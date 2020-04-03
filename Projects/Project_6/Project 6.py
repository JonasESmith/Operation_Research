import os
import zipfile
from tkinter import Tk, filedialog, messagebox

import numpy as np
import pandas as pd
from prettytable import PrettyTable

# Array of paths.
# The main diagonal is ignored.
# All valid paths should be non-negative numbers.
# Any path that does not exist should be indiated by a negative number (-1).

# Class to store inforation about each node.
class Node:
    index = 0
    # Is this node starred?
    starred = False
    # Which node did the shortest path come from?
    nodeFrom = -1
    # What is the length of the path up to this node?
    lengthFrom = -1
    # What branches are available from this node?
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


# Class to store information about each path.
class Path:
    # Index of the starting node
    start = 0
    # Index of the ending node
    end = 0
    # Length of the path
    length = 0
    # Is this path circled?
    circled = False

    def __init__(self, start, end, length):
        self.start = start
        self.end = end
        self.length = length

    def __str__(self):
        return f"({self.start}, {self.end}, {self.length})"

    def __repr__(self):
        return self.__str__()


# Function to print the node tables
def printNodes():
    # Find the max number of paths for a single node.
    maxPaths = 0
    for node in nodes:
        if len(node.paths) > maxPaths:
            maxPaths = len(node.paths)

    output = []

    output.append(["Node", "Starred", "Total Cost"])
    for i in range(0, maxPaths):
        output[0].append(f"Branch {i}")
        output[0].append(f"Cost")
    for node in nodes:
        if node.index == sink:
            continue
        row = []
        row.append(nodeNames[node.index])
        row.append("*" if node.starred else "")
        row.append(node.lengthFrom if node.lengthFrom > 0 else "")
        for path in node.paths:
            if path.circled:
                row.append("(" + nodeNames[path.end] + ")")
            else:
                row.append(nodeNames[path.end])
            row.append(path.length)
        output.append(row)

    dataframes.append(pd.DataFrame(output))


def ask_for_file():
    # Get the current working directory
    # No longer works correctly if not a script
    # cwd = os.path.dirname(os.path.normpath(__file__))
    # Ask for file with current working directory as starting point
    file = filedialog.askopenfilename(
        initialdir=os.getcwd(),
        filetypes=([("Excel Sheet", "*.xlsx")]),
        title="Please Open Your Game's Excel Spreadsheet",
    )

    if os.path.isfile(file):
        return file
    else:
        raise FileNotFoundError("The file specified does not exist! Exiting.")


def to_filename(s):
    return "".join(x if x.isalnum() else "_" for x in s)


if __name__ == "__main__":
    try:
        root = Tk()
        root.withdraw()

        # Read in data frame from a selected file.
        df = pd.read_excel(ask_for_file(), verbose=True)
        # Little bit of regex magic to find all empty cells and set them to 0.
        df.replace(r"^\s*$", 0, regex=True)
        # Store column headers to ask the user for source and sink.
        nodeNames = df.columns.tolist()[1:]
        # Drop column headers.
        df.drop(df.columns[0], axis=1, inplace=True)
        # Convert data frame to useable numpy array.
        paths = np.asarray(df)
        # List data file with indices for user choices.
        for i in range(0, len(nodeNames)):
            print(f"{i}: {nodeNames[i]}")
        # Prompt user for source and sink indices from list.
        source = int(
            input(
                "\nFrom the list above, please set the source by its number: "
            )
        )
        sink = int(
            input(
                "\nFrom the list above, please set the sink by its number: "
            )
        )

        print()
        print()

        # Make sure the path matrix is square.
        if paths.shape[0] != paths.shape[1]:
            raise ValueError("The path matrix must be square!")

        # Get the number of nodes
        size = paths.shape[0]

        # List to keep track of info about each node.
        nodes = [Node(i) for i in range(0, size)]
        nodes[source].starred = True
        nodes[source].lengthFrom = 0

        for row in range(0, size):
            for col in range(0, size):
                # Add a new path if row is not equal to the column, the value in the matrix is non-zero, the path is not returning to the source, and the row is not the sink.
                if (
                    row != col
                    and paths[row][col] >= 0
                    and col != source
                    and row != sink
                ):
                    nodes[row].paths.append(Path(row, col, paths[row][col]))

        # Sort the paths within each node by length.
        for node in nodes:
            node.paths.sort(key=lambda path: path.length)

        dataframes = []
        # Main program loop. Iterate until we reach the sink.
        while True:
            printNodes()
            # Look for the shortest total length after travelling a path from all starred nodes.
            # Note: The shortest path from a node will always be first in the list of remaining nodes since the lists are sorted and we are removing them as we go.
            shortestPath = None
            shortestLength = -1
            for node in nodes:
                remainingPaths = node.remainingPaths()
                # Check if the node is starred and has at least one path remaining.
                if node.starred and len(remainingPaths) > 0:
                    # Check if the total length so far plus the length of the next path is less than our current minimum.
                    # If the shorestLength is -1 we havn't found any node yet, so it will become our current min.
                    if (
                        node.lengthFrom + remainingPaths[0].length
                        < shortestLength
                        or shortestLength == -1
                    ):
                        # Update the shorest path and shorest length.
                        shortestPath = remainingPaths[0]
                        shortestLength = (
                            node.lengthFrom + remainingPaths[0].length
                        )

            # Determine the new node based on where the shorest path ends.
            newNode = nodes[shortestPath.end]
            # Circle the new path
            shortestPath.circled = True
            # Star the new node, update where it came from, and what the total length to the node is.
            newNode.starred = True
            newNode.nodeFrom = shortestPath.start
            newNode.lengthFrom = shortestLength

            # Remove all paths ending at the new node, except for the circled path.
            for node in nodes:
                for path in node.paths:
                    if path.end == newNode.index and path != shortestPath:
                        del node.paths[node.paths.index(path)]
                        break

            # If the sink node has been starred, break from the loop.
            if nodes[sink].starred:
                break

        # Start at the sink and work backward to find the best path.
        outPath = nodeNames[sink]
        curNode = sink
        while True:
            outPath = nodeNames[nodes[curNode].nodeFrom] + " -> " + outPath
            curNode = nodes[curNode].nodeFrom
            if curNode == source:
                break

        # Print the final output
        output = f"Path: {outPath}\n"
        output += f"Total Length: {nodes[sink].lengthFrom}"
        print(output)

        print()
        print("Done.")
        print(f"Exporting {len(dataframes)} iterations to output file.")

        zip_name = f"{to_filename(nodeNames[source])}_to_{to_filename(nodeNames[sink])}.zip"

        with zipfile.ZipFile(zip_name, "w", zipfile.ZIP_DEFLATED) as zipfile:
            with open("final_results.txt", 'w') as file:
                file.write(output)
            zipfile.write("final_results.txt", "final_results.txt")
            os.remove("final_results.txt")
            for i in range(0, len(dataframes)):
                print(f'Exporting "Iteration {i}.csv"')
                dataframes[i].to_csv(f"Iteration_{i}.csv")
                zipfile.write(f"Iteration_{i}.csv", f"Iteration_{i}.csv")
                # Since the zipefile class copies a file and does not move
                # we must tell the os to remove the file manually each iteration.
                os.remove(f"Iteration_{i}.csv")

        print("Done.")
        print(f"Files exported to '{zip_name}'")
    except FileNotFoundError as fnfe:
        messagebox.showerror("File Not Found", fnfe)
    except ValueError as ve:
        messagebox.showerror("Invalid Input", ve)
