__author__ = 'Gary.Graham'

import random
import dbinterface
import datetime
import time
db = dbinterface.DB()


stm = '''
        INSERT INTO T_SHIFT (employee_id, vehicle_number, start_time, end_time)
        VALUES(?,?,?,?)
      '''

def getRandomDateIn2013():
    month = random.randint(1,12)
    day = random.randint(1,30)
    hour = random.randint(8,20)
    minute = 0
    second = 0
    try:
        dateTimeStamp = datetime.datetime(2013, month, day, hour, minute)
    except ValueError:
        dateTimeStamp = datetime.datetime(2013, month, 28, hour, minute)
    return dateTimeStamp

def getRandomShiftLength():
    hours = 3600
    totalSeconds = random.randint(4,12) * hours

    totalTimeDelta = datetime.timedelta(seconds=totalSeconds)

    return totalTimeDelta


def addRecord():
    driver = random.randint(1,3)
    startDate = getRandomDateIn2013()
    shiftLength = getRandomShiftLength()
    vehicle = random.randint(10,30)
    endDate = startDate + shiftLength
    args = [driver, vehicle, startDate, endDate ]
    print(args)
    db.executeSql(stm, args)
    db.conn.commit()

index = 0
while index < 300:
    addRecord()
    index += 1
    time.sleep(0.5)



rows = db.executeSql("Select * from T_SHIFT")

for item in rows:
    print(item)

