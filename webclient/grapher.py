import matplotlib.pyplot as plt
from matplotlib.dates import date2num
import random
import dbinterface
import datetime
#NOTE. YOU NEED TO INSTALL MATPLOTLIB THROUGH PIP/PYPM.

db = dbinterface.DB()
startTimeData = db.grabStartTimesByUserID(1)
print(startTimeData)
#woop woop got it formatted ncicely.

data = [(datetime.datetime.strptime('2010-02-05', "%Y-%m-%d"), 12312), #here is where we need to pull the data from DB and insert.
        (datetime.datetime.strptime('2010-02-19', "%Y-%m-%d"), 123123),
        (datetime.datetime.strptime('2010-03-05', "%Y-%m-%d"), 987),
        (datetime.datetime.strptime('2010-03-19', "%Y-%m-%d"), 34555)]


x = [date2num(date) for (date, value) in data]
y = [value for (date, value) in data]

fig = plt.figure()
graph = fig.add_subplot(111)
graph.bar(x,y)

# Set the xtick locations to correspond to dates
graph.set_xticks(x)

# Set the xtick labels to correspond to just the dates
graph.set_xticklabels([date.strftime("%B %d") for (date, value) in data]) #strftime converts datetime objects to readable strings.

plt.show()