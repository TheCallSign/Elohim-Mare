using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElohimMare.EMBackend
{
    class DatabaseManager
    {
        private const string DATABASE_PASSWORD = "AzGaGg6LXUyKGtrFACSp";
        public const string DATABASE_NAME = "SightBase_data.db";
        public const string CONNECTION_STRING = "Data Source ="+ DATABASE_NAME + ";Version=3;Password="+DATABASE_PASSWORD;

        SQLiteConnection conn;
        public DatabaseManager()
        {
            if (!File.Exists(DATABASE_NAME))
            {
                SQLiteConnection.CreateFile(DATABASE_NAME);
                conn = new SQLiteConnection(CONNECTION_STRING);
                conn.Open();
                conn.ChangePassword(DATABASE_PASSWORD);
            }
            else
            {
                conn = new SQLiteConnection(CONNECTION_STRING);
                conn.Open();
            }
        }

        private void CreateTables()
        {
            string sql = "CREATE TABLE Students(StudentNumber CHAR(8) NOT NULL PRIMARY KEY, Surname VARCHAR(25) NULL, FullName VARCHAR(40) NULL, Initials VARCHAR(5) NULL, CardNumber INT NULL, workForceID CHAR(7) NULL, mail VARCHAR(30) NULL, loginExpiration DATETIME NULL, loginDisabled BIT NULL, accessCardNumber INT NULL, allowUnlimitedCredit BIT NULL)";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public void AddStudent(Student s)
        {
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = "INSERT INTO students (studentnumber, surname, fullname, initials, cardnumber, workforceid, mail, homedirectory, loginexpiration, logindiabled, accessCardNumber,allowUnlimitedCredit";
            
        }

        public void Shutdown()
        {
            conn.Close();
        }
    }
}
