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
        public const string DATABASE_NAME = "SightBase_data.db";
        public const string CONNECTION_STRING = "Data Source ="+ DATABASE_NAME + ";Version=3;";

        SQLiteConnection db;
        public DatabaseManager()
        {
            if (!File.Exists(DATABASE_NAME))
            {
                SQLiteConnection.CreateFile(DATABASE_NAME);
            }
            else
            {
                db = new SQLiteConnection(CONNECTION_STRING);
                db.Open();
            }
        }

        public void Shutdown()
        {
            db.Close();
        }
    }
}
