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


    def grabPW(self, username):
        print("Entering _> GrabPW: For user %s" %username)
        stm = "SELECT password FROM T_CREDENTIALS WHERE username = '%s'" %username

        hashedPW = self.executeSql(stm)

        if hashedPW:
            return hashedPW[0][0]
        else:
            return None



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
            for item in resultSet:
                print(item)


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












