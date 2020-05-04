import os
import plotly.graph_objects as go
import pandas
import random

class Stat:
    index = 0
    date = ""
    newCases = 0

    def __init__(self, index, date, newCases):
        self.index = index
        self.date = date
        self.newCases = newCases

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
        #print("{0} | {1} | {2} | {3} | {4}".format(stats.index, stats.newCases, stats.index * stats.index, stats.newCases * stats.newCases, stats.index * stats.newCases))
    
    # print("{0} | {1} | {2} | {3} | {4}".format(x_sum, y_sum, x_square, y_square, x_y))

    a = ((x_square) * (y_sum) - (x_sum)*(x_y)) / ((n)*(x_square) - (x_sum)*(x_sum))

    b = ((n) * (x_y) - (x_sum)*(y_sum)) / ((n)*(x_square) - (x_sum)*(x_sum))
    # print("a = {0} ; b = {1}".format(a,b))

    regression = a + b * count

    return(regression)

def sumCases(Stats):
    cases = 0
    for stat in Stats:
        cases += stat.newCases
    return(cases)


Stats = []

count = 0


with open("data.txt", "r") as filestream:
    for line in filestream:
        stats = line.split("*")
        stat_cases = stats[1].replace(',','')
        Stats.append(Stat(count ,stats[0], int(stat_cases)))
        count += 1

print(getForecast(Stats, count))

# print(count)

print("With current social distancing methods")

# continues to forcast a week ahead of time, with current trends of staying home.
for x in range(7):
    forValue = getForecast(Stats, count)
    forValue = forValue * random.uniform(2.4, 2.9) * 0.2143
    print("{0} : {1}".format(count, forValue))
    Stats.append(Stat(count, "new date", int(forValue)))
    count+=1

print(getForecast(Stats, count))

socialDistance = sumCases(Stats)

print(socialDistance)
print()

StatZero = Stats.copy()

# remove changes made with the most recent predictions.
for x in range(7):
    Stats.pop()
    count += (-1)

# now we will add an increase in the amount of nubmers people are seeing each other
# so using the number given from the Journal of Travel Medicine we can see that from many different
# countries the travel rate of covide ranges from 1.4 to 6.49 we will use the median which is 2.79.
# For our purposes we will lower this number as hopefully people will be more careful we will be usin 2.
# now let us forecast an additional week into the future. For the seasonal flue that has a reproduction rate of
# 1.28 this means that for every 100 people that have it they will pass it on to n additional 128 people.

print(getForecast(Stats, count))

print("Removing social distancing methods and 'reopening' states")

for x in range(7):
    forValue = getForecast(Stats, count)
    forValue = forValue * random.uniform(2.4, 2.9) * 0.7857
    print("{0} : {1}".format(count, forValue))
    Stats.append(Stat(count, "new date", int(forValue)))
    count+=1

StatOne = Stats.copy()

noSociDistance = sumCases(Stats)

print(getForecast(Stats, count))
print(noSociDistance)

newCases = noSociDistance - socialDistance

print("One week difference of social distancing vs non social distancing is an additional {0} # of cases".format(newCases))

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

