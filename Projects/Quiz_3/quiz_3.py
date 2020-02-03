import os
from pulp import *
import pandas as pd

# Define the problem
prob = LpProblem("Diet_Optimization", LpMaximize)

# Define the data set to be used
# IMPORTANT 
# You could theoretically import the entire sonic list to an excel sheet 
# with the specified columns and none of code would need changed.
working_directory = os.path.dirname(os.path.normpath(__file__))
df = pd.read_excel(working_directory + "\\nutrition_info.xlsx", nrows=365)

# Create a list of all the food items in the excel sheet
food_items = list(df['Food Items'])

# Create dictionaries for each of the nutrition categories by name
# Could probably have been simplified with a for loop and iterating over
# food_items, but then we wouldn't have nicely named variables.
column_names = df.head()
calories = dict(zip(food_items, df['Calories']))
calories_from_fat = dict(zip(food_items, df['Calories from Fat']))
fat = dict(zip(food_items, df['Fat (g)']))
saturated_fat = dict(zip(food_items, df['Saturated Fat (g)']))
trans_fat = dict(zip(food_items, df['Trans Fat (g)']))
cholesterol = dict(zip(food_items, df['Cholesterol (mg)']))
sodium = dict(zip(food_items, df['Sodium (mg)']))
carbs = dict(zip(food_items, df['Carbs (g)']))
dietary_fiber = dict(zip(food_items, df['Dietary Fiber (g)']))
sugar = dict(zip(food_items, df['Sugar (g)']))
protein = dict(zip(food_items, df['Protein (g)']))
item_count = dict(zip(food_items, df['Item Count']))

# Define the scope of the optimization solution, i.e. the variables will be nonnegative
# in this case.
food_vars = LpVariable.dicts("Food", food_items, lowBound=0, cat=LpInteger)

# The objective function: dietary_fiber was selected for maximization.
prob += lpSum([dietary_fiber[i]*food_vars[i] for i in food_items])

# Calories
prob += lpSum([calories[f] * food_vars[f] for f in food_items]) >= 2000, "CalorieMinimum"

# Calories from fat
prob += lpSum([calories_from_fat[f] * food_vars[f] for f in food_items]) >= 585, "CaloriesFromFatMinimum"

# Fat
prob += lpSum([fat[f] * food_vars[f] for f in food_items]) >= 64.5, "FatMinimum"

# Saturated fat
prob += lpSum([saturated_fat[f] * food_vars[f] for f in food_items]) <= 27, "SaturatedFatMaximum"

# Trans fat
prob += lpSum([trans_fat[f] * food_vars[f] for f in food_items]) <= 27, "TransFatMaximum"

# Cholesterol
prob += lpSum([cholesterol[f] * food_vars[f] for f in food_items]) <= 250, "CholesterolMaximum"

# Sodium
prob += lpSum([sodium[f] * food_vars[f] for f in food_items]) <= 3000, "SodiumMaximum"

# Carbs
prob += lpSum([carbs[f] * food_vars[f] for f in food_items]) >= 130, "CarbsMinimum"

# Sugar
prob += lpSum([sugar[f] * food_vars[f] for f in food_items]) <= 50, "SugarMaximum"

# Protein
prob += lpSum([protein[f] * food_vars[f] for f in food_items]) >= 56, "ProteinMinimum"

# Item Count
prob += lpSum([item_count[f] * food_vars[f] for f in food_items]) == 4, "ItemCountEquals"

prob.solve()

print(f"Status: {LpStatus[prob.status]}\n")

total_fiber = 0
total_calories = 0
total_calories_from_fat = 0
total_fat = 0
total_saturated_fat = 0
total_trans_fat = 0
total_cholesterol = 0
total_sodium = 0
total_carbs = 0
total_sugar = 0
total_protein = 0

for v in prob.variables():
    if v.varValue > 0:
        print(f"{v.name.replace('_', ' ')[5:]} = {int(v.varValue)}")
        total_fiber += (v.varValue * dietary_fiber[v.name.replace('_', ' ')[5:]])
        total_calories += (v.varValue * calories[v.name.replace('_', ' ')[5:]])
        total_calories_from_fat += (v.varValue * calories_from_fat[v.name.replace('_', ' ')[5:]])
        total_fat += (v.varValue * fat[v.name.replace('_', ' ')[5:]])
        total_saturated_fat += (v.varValue * saturated_fat[v.name.replace('_', ' ')[5:]])
        total_trans_fat += (v.varValue * trans_fat[v.name.replace('_', ' ')[5:]])
        total_cholesterol += (v.varValue * cholesterol[v.name.replace('_', ' ')[5:]])
        total_sodium += (v.varValue * sodium[v.name.replace('_', ' ')[5:]])
        total_carbs += (v.varValue * carbs[v.name.replace('_', ' ')[5:]])
        total_sugar += (v.varValue * sugar[v.name.replace('_', ' ')[5:]])
        total_protein += (v.varValue * protein[v.name.replace('_', ' ')[5:]])
    elif v.varValue < 0:
        print("Error: value less than 0. Cannot have negative food quantities.")
if LpStatus[prob.status] == "Optimal":
    print(f"\nMaximized dietary fiber amount: {total_fiber} (g)\n")
    print(f"Other Nutrient Values:")
    print(f"Calories: {total_calories} kCal")
    print(f"Calories from Fat: {total_calories_from_fat} kCal")
    print(f"Fat: {total_fat} (g)")
    print(f"Saturated Fat: {total_saturated_fat} (g)")
    print(f"Trans Fat: {total_trans_fat} (g)")
    print(f"Cholesterol: {total_cholesterol} (mg)")
    print(f"Sodium: {total_sodium} (mg)")
    print(f"Carbs: {total_carbs} (g)")
    print(f"Sugar: {total_sugar} (g)")
    print(f"Protein: {total_protein} (g)")
    
