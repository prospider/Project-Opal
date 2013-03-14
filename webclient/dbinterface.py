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
        stm = '''INSERT INTO T_USER
                 (name, address,bank_account, sin, wage)
                 VALUES
                 ('%s','%s','%s',%s,%s)
              '''%(fn + " " + ln,ad,"test11",sin,wage)
        #todo write some code that cuts spaces out of SIN
        #todo write some code that allows real numbers in the html textbox for wage

        writeCheck = self.executeSql(stm)
        print(writeCheck)


    def grabPW(self, username):
        print("Entering _> GrabPW: For user %s" %username)
        stm = "SELECT password FROM T_CREDENTIALS WHERE username = '%s'" %username

        hashedPW = self.executeSql(stm)

        if hashedPW:
            return hashedPW[0][0]
        else:
            return None

    def grabUserList(self):
        print("Entering -> GrabUserList")
        stm = "SELECT username, user_id from T_CREDENTIALS ORDER BY username ASC"
        userList = self.executeSql(stm)

        if userList:
            return userList
        else:
            return None

    def grabShiftCountByUserID(self,userID):
        stm = "SELECT COUNT(*) FROM T_SHIFT WHERE employee_id = %s" %userID
        shiftCount = self.executeSql(stm)
        return shiftCount[0][0]
        #This is super patchwork code, should be tightened with try/catch stuff.

    def grabStartTimesByUserID(self, userID):
        stm = "SELECT start_time FROM T_SHIFT WHERE employee_id = %s" %userID
        shiftStartTimes = self.executeSql(stm)

        returnArray = [theStartTime[0] for theStartTime in shiftStartTimes]
        print(returnArray)
        return returnArray

    def grabMostRecentShiftByUserID(self,userID):
        stm = "SELECT start_time, end_time FROM T_SHIFT WHERE employee_id = %s ORDER BY start_time DESC" %userID
        shiftStartTimes = self.executeSql(stm)
        for item in shiftStartTimes:
            print(item)
        return shiftStartTimes


    def executeSql(self, sql):
        print("Entering-> executeSql()")
        cursor = self.grab_cursor()
        cursor.execute(sql)
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












