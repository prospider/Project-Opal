import matplotlib.pyplot as plt
from matplotlib.dates import date2num
import random
import dbinterface
import datetime
#NOTE. YOU NEED TO INSTALL MATPLOTLIB THROUGH PIP/PYPM.

db = dbinterface.DB()
startTimeData = db.grabStartTimesByUserID(1)
print(startTimeData)
#woop woop got it formatted nicely.

data = [(datetime.datetime.strptime('2010-02-01', "%Y-%m-%d"), 6),
        (datetime.datetime.strptime('2010-02-02', "%Y-%m-%d"), 3),
        (datetime.datetime.strptime('2010-02-03', "%Y-%m-%d"), 3),
        (datetime.datetime.strptime('2010-02-04', "%Y-%m-%d"), 8),
        (datetime.datetime.strptime('2010-02-05', "%Y-%m-%d"), 10), #here is where we need to pull the data from DB and insert.
        (datetime.datetime.strptime('2010-02-06', "%Y-%m-%d"), 8),
        (datetime.datetime.strptime('2010-02-07', "%Y-%m-%d"), 9),
        (datetime.datetime.strptime('2010-02-08', "%Y-%m-%d"), 5),
        (datetime.datetime.strptime('2010-02-09', "%Y-%m-%d"), 2), #here is where we need to pull the data from DB and insert.
        (datetime.datetime.strptime('2010-02-10', "%Y-%m-%d"), 2),
        (datetime.datetime.strptime('2010-02-11', "%Y-%m-%d"), 7),
        (datetime.datetime.strptime('2010-02-12', "%Y-%m-%d"), 5),
        (datetime.datetime.strptime('2010-02-13', "%Y-%m-%d"), 5), #here is where we need to pull the data from DB and insert.
        (datetime.datetime.strptime('2010-02-14', "%Y-%m-%d"), 12),
        (datetime.datetime.strptime('2010-02-15', "%Y-%m-%d"), 5)]


possibleShiftLengths= [0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16]

x = [date2num(date) for (date, value) in data]
y = [value for (date, value) in data]
fig = plt.figure()
graph = fig.add_subplot(111)

graph.bar(x,y)
plt.ylabel("Hours Worked")

# Set the xtick locations to correspond to dates
graph.set_xticks(x)
graph.set_yticks(possibleShiftLengths)
# Set the xtick labels to correspond to just the dates
graph.set_xticklabels([date.strftime("%B %d") for (date, value) in data]) #strftime converts datetime objects to readable strings.
labels = graph.get_xticklabels()
for label in labels:
    label.set_rotation(30)#wonky xlabel rotation code
plt.show()