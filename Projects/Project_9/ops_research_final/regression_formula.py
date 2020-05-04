import os
import plotly.graph_objects as go
import pandas
import random


# basic statistic class
class Stat:
    index = 0
    date = ""
    newCases = 0

    def __init__(self, index, date, newCases):
        self.index = index
        self.date = date
        self.newCases = newCases

# returns the forecast based on the stats passed
def getForecast(Stats, count): 
    n = count - 1
    x_sum = 0
    y_sum = 0
    x_square = 0
    y_square = 0
    x_y = 0

    for stats in Stats:
        x_sum += stats.index
        y_sum += stats.newCases

        x_square += stats.index * stats.index
        y_square += stats.newCases * stats.newCases

        x_y += stats.index * stats.newCases

    a = ((x_square) * (y_sum) - (x_sum)*(x_y)) / ((n)*(x_square) - (x_sum)*(x_sum))

    b = ((n) * (x_y) - (x_sum)*(y_sum)) / ((n)*(x_square) - (x_sum)*(x_sum))

    regression = a + b * count

    return(regression)

# sumsCases
def sumCases(Stats):
    cases = 0
    for stat in Stats:
        cases += stat.newCases
    return(cases)

Stats = []
count = 0

# Loads stats from text file
with open("data.txt", "r") as filestream:
    for line in filestream:
        stats = line.split("*")
        stat_cases = stats[1].replace(',','')
        Stats.append(Stat(count ,stats[0], int(stat_cases)))
        count += 1

print("day {0} : cases {1}".format(count, getForecast(Stats, count)))
print()
print("quarantine")

numDays = 7

# continues to forcast a week ahead of time, with current trends of staying home.
for x in range(numDays):
    forValue = getForecast(Stats, count)
    forValue = forValue * random.uniform(2.4, 2.9) * 0.2143
    print("{0} : {1}".format(count, forValue))
    Stats.append(Stat(count, "new date", int(forValue)))
    count+=1

socialDistance = sumCases(Stats)

print("{0} total cases for 05/10/2020".format(socialDistance))
print()

# copy Stats with quarantine
StatZero = Stats.copy()

# remove changes made with the most recent predictions.
for x in range(7):
    Stats.pop()
    count += (-1)

print("non-quarantine")

for x in range(numDays):
    forValue = getForecast(Stats, count)
    forValue = forValue * random.uniform(2.4, 2.9) * 0.7857
    print("{0} : {1}".format(count, forValue))
    Stats.append(Stat(count, "new date", int(forValue)))
    count+=1

# copy stats for non-quarantine
StatOne = Stats.copy()

noSociDistance = sumCases(Stats)
print("{0} total cases for 05/10/2020".format(noSociDistance))
newCases = noSociDistance - socialDistance

print("new cases = {0}, without quarantine".format(newCases))
print()
input("press any key to graph results")

index = 0
days = []
caseNon = []
caseSoc = []

for x in StatZero:
    days.append(x.index)
    caseNon.append(x.newCases)
    caseSoc.append(StatOne[index].newCases)
    index += 1
# https://plotly.com/python/line-charts/
# adding multiple different lines to this polyLineChart

fig = go.Figure()
fig.add_trace(go.Scatter(x=days, y=caseSoc, name='Forcasting without social distancing',
                         line=dict(color='firebrick', width=4)))
fig.add_trace(go.Scatter(x=days, y=caseNon, name='Forcasting with social distancing',
                         line=dict(color='green', width=4)))

fig.show()

# cases numbers 
# https://www.cdc.gov/coronavirus/2019-ncov/cases-updates/cases-in-us.html
#
# https://www.psychologytoday.com/us/blog/laugh-cry-live/202004/the-shocking-numbers-behind-the-novel-coronavirus-pandemic
# reproduction of covid https://www.ncbi.nlm.nih.gov/pmc/articles/PMC7074654/

