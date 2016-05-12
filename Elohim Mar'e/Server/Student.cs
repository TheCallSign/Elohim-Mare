using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElohimMare.Server
{
    class Student 
    {
        public string fullName { get; set; } 
        public string surname { get; set; }
        public string preferredName { get; set; } 
        public string cn { get; set; } // pretty much the student number but with the 'g' prefix.
        public string studentNumber { get; set; }  //workdForceID
        public string mail { get; set; } 
        public string initials { get; set; }
        public string homeDirectory { get; set; }
        public DateTime loginExpiration { get; set; }
        public bool loginDisabled { get; set; }
        public int accessCardNumber  { get; set; }
        public bool allowUnlimitedCredit  { get; set; } //idk what this is
    } 
}
