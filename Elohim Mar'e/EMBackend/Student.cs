using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElohimMare.EMBackend
{
    class Student 
    {
        public string fullName { get; set; } 
        public string givenName { get; set; }
        public string surname { get; set; }
        public string preferredName { get; set; } 
        public string studentNumber { get; set; } // pretty much the student number but with the 'g' prefix.
        public string workForceID { get; set; }  //workdForceID
        public string mail { get; set; } 
        public string initials { get; set; }
        public string homeDirectory { get; set; }
        public DateTime loginExpiration { get; set; }
        public bool loginDisabled { get; set; }
        public int accessCardNumber  { get; set; }
        public bool allowUnlimitedCredit  { get; set; } //idk what this is
        public string stuff { get; set; }
        public string adsPath { get; set; }
        override public string ToString()
        {
            return String.Format("{0}: {1}, {2}, {3}, {4}", fullName, studentNumber, mail, accessCardNumber, loginDisabled);
        }
    } 
}
