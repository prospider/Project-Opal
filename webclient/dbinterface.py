import re
import sqlite3
import constants
import time

class DB:
    def __init__(self):
        print("Initializing DB Connection...")
        try:
            self.conn = sqlite3.connect(constants.DBLOCATION)
        except :
            print("Could not open connection to DB at %s"%constants.DBLOCATION)
            return
        if self.conn:
            print("Connection to DB established.")
            #test

    def createUser(self, fn, ln,ad, sin, wage):
        argumentTuple = (fn + " " + ln, ad, "test11", sin, wage)

        #code to strip spaces and dashes from SIN.
        for ch in [' ', '-']:
            if ch in sin:
                sin = sin.replace(ch, '')

        #This block's purpose is to ensure basic level integrity of the data.
        try:
            float(sin)
            float(wage)
        except:
            raise ValueError

        if len(fn) == 0 or len(ln) == 0 or len(ad) == 0 or len(sin) != 9:
            raise ValueError

        stm = '''INSERT INTO T_USER
                 (name, address,bank_account, sin, wage)
                 VALUES
                 (?,?,?,?,?)
              '''

        print(stm)
        self.executeSql(stm, argumentTuple)



    def grabPW(self, username):
        print("Entering _> GrabPW: For user %s" %username)
        argumentTuple = (username,)
        stm = "SELECT password FROM T_CREDENTIALS WHERE username = ?"

        hashedPW = self.executeSql(stm, argumentTuple)

        if hashedPW:
            return hashedPW[0][0]
        else:
            return None

    def grabUserList(self):
        print("Entering -> GrabUserList")
        stm = "SELECT name, id from T_USER ORDER BY name ASC"
        userList = self.executeSql(stm)

        if userList:
            return userList
        else:
            return None

    def grabShiftCountByUserID(self,userID):
        argumentTuple = (userID,)
        stm = "SELECT COUNT(*) FROM T_SHIFT WHERE employee_id = ?"
        shiftCount = self.executeSql(stm, argumentTuple)
        return shiftCount[0][0]
        #todo This is super patchwork code, should be tightened with try/catch stuff.

    def grabUserNameByUserID(self,userID):
        argumentTuple = (userID,)
        stm = "SELECT Name FROM T_USER WHERE id = ?"
        userName = self.executeSql(stm, argumentTuple)
        return userName[0][0] #as it returns a 2-dimensional Tuple

    def grabStartTimesByUserID(self, userID):
        argumentTuple = (userID,)
        stm = "SELECT start_time FROM T_SHIFT WHERE employee_id = ?"
        shiftStartTimes = self.executeSql(stm, argumentTuple)

        returnArray = [theStartTime[0] for theStartTime in shiftStartTimes]
        print(returnArray)
        return returnArray

    def grabAllShiftsByUserID(self,userID):
        argumentTuple = (userID,)
        stm = "SELECT start_time, end_time FROM T_SHIFT WHERE employee_id = ? ORDER BY start_time DESC"
        shiftStartTimes = self.executeSql(stm, argumentTuple)
        return shiftStartTimes

    def grabAllShiftsForUserWithinDates(self, start_date, end_date, userId):
        print("Entering-> grabAllShiftsForUserWithinDates()")
        argumentTuple = (start_date, end_date, userId)
        print(argumentTuple)
        stm = '''
                SELECT * FROM T_SHIFT WHERE start_time > ? and start_time < ? and employee_id = ? ORDER BY start_time
              '''
        print(stm)
        daShifts = self.executeSql(stm, argumentTuple)
        for item in daShifts:
            print(item)
        print("Leaving -> grabAllShiftsForUserWithinDates()")
        return daShifts

    def grabShiftCountForUserWithinDates(self,start_date, end_date, userId):
        print("Entering-> grabShiftCountForUserWithinDates()")
        argumentTuple = (start_date, end_date, userId)
        print(argumentTuple)
        stm = '''
                SELECT COUNT(*) FROM T_SHIFT WHERE start_time > ? and start_time < ? and employee_id = ?
              '''
        print(stm)
        daCount = self.executeSql(stm, argumentTuple)
        print("Leaving -> grabShiftCountForUserWithinDates()")
        return daCount[0][0]

    def grabAllShiftsByMonthByUserID(self, userID):
        argumentTuple = (userID,)
        stm = "SELECT start_time from T_SHIFT where employee_id = ?"
        allShifts = self.executeSql(stm, argumentTuple)
        returnDict = { 1 : 0, 2 : 0, 3 : 0, 4 : 0, 5 : 0, 6 : 0, 7 : 0, 8 : 0, 9 : 0, 10 : 0, 11 : 0, 12 : 0}
        for item in allShifts:
            if '-01-' in item[0]:
                returnDict[1] += 1
            elif '-02-' in item[0]:
                returnDict[2] += 1
            elif '-03-' in item[0]:
                returnDict[3] += 1
            elif '-04-' in item[0]:
                returnDict[4] += 1
            elif '-05-' in item[0]:
                returnDict[5] += 1
            elif '-06-' in item[0]:
                returnDict[6] += 1
            elif '-07-' in item[0]:
                returnDict[7] += 1
            elif '-08-' in item[0]:
                returnDict[8] += 1
            elif '-09-' in item[0]:
                returnDict[9] += 1
            elif '-10-' in item[0]:
                returnDict[10] += 1
            elif '-11-' in item[0]:
                returnDict[11] += 1
            elif '-12-' in item[0]:
                returnDict[12] += 1


        return returnDict



    def executeSql(self, sql, argumentTuple = None):
        print("Entering-> executeSql()")
        cursor = self.grab_cursor()
        if argumentTuple is not None:
            cursor.execute(sql, argumentTuple)
        else:
            cursor.execute(sql)

        resultSet = cursor.fetchall()
        self.conn.commit()

        #Checking integrity and returning
        if resultSet == None:
            print("No rows returned, empty DB? Wrong Table?")
        else:
            print("Grabbed resultSet list...")

        print("Leaving -> executeSql")

        return resultSet



    ########################
    #NON-LIST Functions#####
    ########################

    def grab_cursor(self):
        try:
            cursor = self.conn.cursor()
        except:
            print("Couldn't grab a cursor. Quitting grab_Cursor")
            return None
        print("Grabbed a cursor...")
        return cursor












