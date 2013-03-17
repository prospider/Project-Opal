import re
import sqlite3
import constants


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
        stm = '''INSERT INTO T_USER
                 (name, address,bank_account, sin, wage)
                 VALUES
                 (?,?,?,?,?)
              '''
        #todo write some code that cuts spaces out of SIN
        #todo write some code that allows real numbers in the html textbox for wage
        print(stm)
        writeCheck = self.executeSql(stm, argumentTuple)
        print(writeCheck)


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

    def grabStartTimesByUserID(self, userID):
        argumentTuple = (userID,)
        stm = "SELECT start_time FROM T_SHIFT WHERE employee_id = ?"
        shiftStartTimes = self.executeSql(stm, argumentTuple)

        returnArray = [theStartTime[0] for theStartTime in shiftStartTimes]
        print(returnArray)
        return returnArray

    def grabMostRecentShiftByUserID(self,userID):
        argumentTuple = (userID,)
        stm = "SELECT start_time, end_time FROM T_SHIFT WHERE employee_id = ? ORDER BY start_time DESC"
        shiftStartTimes = self.executeSql(stm, argumentTuple)
        for item in shiftStartTimes:
            print(item)
        return shiftStartTimes


    def executeSql(self, sql, argumentTuple = None):
        print("Entering-> executeSql()")
        cursor = self.grab_cursor()
        if argumentTuple is not None:
            cursor.execute(sql, argumentTuple)
        else:
            cursor.execute(sql)
        self.conn.commit()
        resultSet = cursor.fetchall()

        #Checking integrity and returning
        if resultSet == None:
            print("No rows returned, empty DB? Wrong Table?")
        else:
            print("Grabbed resultSet list...")
            #for item in resultSet:
            #    print(item)


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












