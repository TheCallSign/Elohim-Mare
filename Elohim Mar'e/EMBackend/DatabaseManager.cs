using System;
using System.Collections.Generic;
using System.Data;
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

        private SQLiteConnection conn;

        public DatabaseManager()
        {
            if (!File.Exists(DATABASE_NAME))
            {
                SQLiteConnection.CreateFile(DATABASE_NAME);
                conn = new SQLiteConnection(CONNECTION_STRING);
                conn.Open();
                conn.ChangePassword(DATABASE_PASSWORD);
                CreateTables();
            }
            else
            {
                conn = new SQLiteConnection(CONNECTION_STRING);
                conn.Open();
            }
        }

        public void Reinit()
        {
            string sql = "DROP TABLE  Students";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            sql = "DROP TABLE Staff";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
            CreateTables();
        }

        public void CreateTables()
        {
            string sql = "CREATE TABLE Students(StudentNumber CHAR(8) NOT NULL PRIMARY KEY, Surname VARCHAR(25) NULL, FullName VARCHAR(40) NULL, Initials VARCHAR(5) NULL, mail VARCHAR(30) NULL, loginExpiration DATETIME NULL, loginDisabled BIT NULL, accessCardNumber INT NULL, allowUnlimitedCredit BIT NULL, timetable VARCHAR(66) )";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE Staff(uid VARCHAR(8) NOT NULL PRIMARY KEY, Surname VARCHAR(25) NULL, FullName VARCHAR(40) NULL, Initials VARCHAR(5) NULL, mail VARCHAR(35) NULL, loginExpiration DATETIME NULL, loginDisabled BIT NULL, accessCardNumber INT NULL, allowUnlimitedCredit BIT NULL, title VARCHAR(50), telephone VARCHAR(25), department VARCHAR(40) )";
            command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public List<Student> SearchStudents(string colName, string search)
        {
            //string sql = "SELECT * FROM Students WHERE ? LIKE ?";
            string sql = "SELECT * FROM Students WHERE studentnumber LIKE @search";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.Parameters.Add(new SQLiteParameter("field", colName));
            command.Parameters.Add(new SQLiteParameter("search", search + "%"));
            SQLiteDataReader reader = command.ExecuteReader();
            List<Student> result = new List<Student>();
            while (reader.Read())
            {
                Student s = new Student()
                {
                    studentNumber = reader.GetFieldValue<string>(0),
                    surname = reader.GetFieldValue<string>(1),
                    fullname = reader.GetFieldValue<string>(2),
                    initials = reader.GetFieldValue<string>(3),
                    mail = reader.GetFieldValue<string>(4),
                    loginExpiration = reader.GetFieldValue<DateTime>(5),
                    loginDisabled = reader.GetFieldValue<bool>(6),
                    accessCardNumber = reader.GetFieldValue<int>(7),
                    allowUnlimitedCredit = reader.GetFieldValue<bool>(8),
                    timeTable = reader.GetFieldValue<string>(9)
                };
                result.Add(s);
            }
            return result;
        }
        // http://stackoverflow.com/questions/418898/sqlite-upsert-not-insert-or-replace/4330694#4330694
        public void AddStudent(Student s)
        {
            SQLiteCommand command = conn.CreateCommand();
            //command.CommandText = "INSERT INTO students (studentnumber, surname, fullname, initials, mail, loginexpiration, logindisabled, accessCardNumber, allowUnlimitedCredit, timetable) VALUES (?,?,?,?,?,?,?,?,?,?)";
            command.CommandText = "INSERT INTO students (studentnumber, surname, fullname, initials, mail, loginexpiration, logindisabled, accessCardNumber, allowUnlimitedCredit, timetable) VALUES (?,?,?,?,?,?,?,?,?,?) ON DUPLICATE KEY UPDATE";
            command.Parameters.Add(new SQLiteParameter("studentnumber", s.studentNumber));
            command.Parameters.Add(new SQLiteParameter("surname", s.surname));
            command.Parameters.Add(new SQLiteParameter("fullname", s.fullname));
            command.Parameters.Add(new SQLiteParameter("initials", s.initials));
            command.Parameters.Add(new SQLiteParameter("mail", s.mail));
            command.Parameters.Add(new SQLiteParameter("loginexpiration", s.loginExpiration));
            command.Parameters.Add(new SQLiteParameter("logindiabled", s.loginDisabled));
            command.Parameters.Add(new SQLiteParameter("accessCardNumber", s.accessCardNumber));
            command.Parameters.Add(new SQLiteParameter("allowUnlimitedCredit", s.allowUnlimitedCredit));
            command.Parameters.Add(new SQLiteParameter("timetable", s.timeTable));
            command.ExecuteNonQuery();
        }
        // http://stackoverflow.com/questions/418898/sqlite-upsert-not-insert-or-replace/4330694#4330694
        public void AddStaff(Staff s)
        {
            SQLiteCommand command = conn.CreateCommand();
            command.CommandText = "INSERT INTO Staff (uid, Surname, FullName, Initials, mail, loginExpiration, loginDisabled, accessCardNumber, allowUnlimitedCredit, title, telephone, department) VALUES (?,?,?,?,?,?,?,?,?,?,?,?)";
            command.Parameters.Add(new SQLiteParameter("uid", s.staffUID));
            command.Parameters.Add(new SQLiteParameter("surname", s.surname));
            command.Parameters.Add(new SQLiteParameter("fullname", s.fullname));
            command.Parameters.Add(new SQLiteParameter("initials", s.initials));
            command.Parameters.Add(new SQLiteParameter("mail", s.mail));
            command.Parameters.Add(new SQLiteParameter("loginexpiration", s.loginExpiration));
            command.Parameters.Add(new SQLiteParameter("logindiabled", s.loginDisabled));
            command.Parameters.Add(new SQLiteParameter("accessCardNumber", s.accessCardNumber));
            command.Parameters.Add(new SQLiteParameter("allowUnlimitedCredit", s.allowUnlimitedCredit));
            command.Parameters.Add(new SQLiteParameter("title", s.title));
            command.Parameters.Add(new SQLiteParameter("telephone", s.telNumber));
            command.Parameters.Add(new SQLiteParameter("department", s.department));
            command.ExecuteNonQuery();
        }
        public void Shutdown()
        {
            conn.Close();
        }
    }
}
