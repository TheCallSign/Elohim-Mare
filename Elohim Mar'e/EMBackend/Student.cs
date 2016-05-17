using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElohimMare.EMBackend
{
    class Student 
    {
        public string fullName { get; set; } = "";
        public string givenName { get; set; } = "";
        public string surname { get; set; } = "";
        public string preferredName { get; set; } = "";
        public string studentNumber { get; set; } = ""; // pretty much the student number but with the 'g' prefix.
        public string mail { get; set; } = "";
        public string initials { get; set; } = "";
        public string homeDirectory { get; set; } = "";
<<<<<<< HEAD
        public DateTime loginExpiration { get; set; }
=======
        public DateTime loginExpiration { get; set; } 
>>>>>>> fd8c6ba776d20d3585924802cf74d905642f38d6
        public bool loginDisabled { get; set; }
        public int accessCardNumber  { get; set; }
        public bool allowUnlimitedCredit  { get; set; } //idk what this is
        public string adsPath { get; set; } // direct path to student in LDAP
        public string timeTable { get; set; } = ""; //students timetable
        override public string ToString()
        {
            return String.Format("{0}: {1}, {2}, {3}, {4}, {5}", fullName, studentNumber, mail, accessCardNumber, loginDisabled, timeTable);
        }
    } 
}
