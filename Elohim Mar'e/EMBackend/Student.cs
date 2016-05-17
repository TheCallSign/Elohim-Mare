using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElohimMare.EMBackend
{
    class Student 
    {
        public string fullname { get; set; } = "";
        public string firstnames { get; set; } = "";
        public string surname { get; set; } = "";
        public string studentNumber { get; set; } = ""; // pretty much the student number but with the 'g' prefix.
        public string mail { get; set; } = "";
        public string initials { get; set; } = "";
        public string homeDirectory { get; set; } = "";
        public DateTime loginExpiration { get; set; }
        public bool loginDisabled { get; set; }
        public int accessCardNumber  { get; set; }
        public bool allowUnlimitedCredit  { get; set; } //idk what this is
        public string timeTable { get; set; } //students timetable
        override public string ToString()
        {
            return String.Format("Fullname: {0} \nStudentNumber: {1} \nMail: {2} \nAccessCardNumber: {3} \nLoginDisabled: {4} \nTimeTable: {5}", fullname, studentNumber, mail, accessCardNumber, loginDisabled, timeTable);
        }
    } 
}
