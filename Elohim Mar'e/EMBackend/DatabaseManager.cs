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
        public void Reinit()
        {
            string sql = "DROP TABLE Students";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            CreateTables();
        }
        private void CreateTables()
        {
            string sql = "CREATE TABLE Students(StudentNumber CHAR(8) NOT NULL PRIMARY KEY, Surname VARCHAR(25) NULL, FullName VARCHAR(40) NULL, Initials VARCHAR(5) NULL, mail VARCHAR(30) NULL, loginExpiration DATETIME NULL, loginDisabled BIT NULL, accessCardNumber INT NULL, allowUnlimitedCredit BIT NULL, timetable VARCHAR(66) )";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public void AddStudent(Student s)
        {
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = "INSERT INTO students (studentnumber, surname, fullname, initials, mail, loginexpiration, logindisabled, accessCardNumber, allowUnlimitedCredit, timetable) VALUES (?,?,?,?,?,?,?,?,?,?)";
            command.Parameters.Add(new SQLiteParameter("studentnumber", s.studentNumber));
            command.Parameters.Add(new SQLiteParameter("surname", s.surname));
            command.Parameters.Add(new SQLiteParameter("fullname", s.fullName));
            command.Parameters.Add(new SQLiteParameter("initials", s.initials));
            command.Parameters.Add(new SQLiteParameter("mail", s.mail));
            command.Parameters.Add(new SQLiteParameter("loginexpiration", s.loginExpiration));
            command.Parameters.Add(new SQLiteParameter("logindiabled", s.loginDisabled));
            command.Parameters.Add(new SQLiteParameter("accessCardNumber", s.accessCardNumber));
            command.Parameters.Add(new SQLiteParameter("allowUnlimitedCredit", s.allowUnlimitedCredit));
            command.Parameters.Add(new SQLiteParameter("timetable", s.timeTable));
            command.ExecuteNonQuery();
        }

        public void Shutdown()
        {
            conn.Close();
        }
    }
}
