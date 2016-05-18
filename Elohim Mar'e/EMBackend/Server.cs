using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElohimMare.EMBackend
{
    class Server
    {
        private DatabaseManager dm;
        private LDAPManager ldapm;
        private List<Student> studentList = new List<Student>();
        private List<Staff> staffList = new List<Staff>();

        public Server()
        {
            dm = new DatabaseManager();
            ldapm = new LDAPManager();
        }

        public void RefreshStudentList()
        {
            studentList.Clear();
            studentList = (new LDAPManager()).LoadAllStudents();
        }

        public void RefreshStaffList()
        {
            staffList.Clear();
            staffList = (new LDAPManager()).LoadAllStaff();
        }

        public void StartConsole()
        {
            //if(studentList.Count() <= 0)
            //{
            //    Console.WriteLine("Refreshing Student List with LDAP Server");
            //    RefreshStudentList();
            //}
            //Console.WriteLine(String.Format("There are {0} undergrad and postgrad students loaded.", studentList.Count));
            Console.WriteLine("Interactive interpreter ready. [h for help]");

            char a = ' ';
            try
            {
                while (a != 'q')
                {
                    Console.Write("? ");
                    a = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    switch (a)
                    {
                        case 's':
                            Console.WriteLine(SearchStudentNumber(studentList, Console.ReadLine()));
                            break;
                        case 'S':
                            Console.WriteLine(SearchStaffNumber(staffList, Console.ReadLine()));
                            break;
                        case 'f':
                            Console.WriteLine(SearchStudentName(studentList, Console.ReadLine()));
                            break;
                        case 'F':
                            Console.WriteLine(SearchStaffName(staffList, Console.ReadLine()));
                            break;
                        case 'h':
                        case '?':
                            Console.WriteLine("s : Student number search\nS : Staff number search\nf : Student name Search \nF : Staff name search \nR : Reinit the database\nl : Reload in-memory list of students/staff");
                            break;
                        case 'R':
                            Console.WriteLine("Reinitialize the database? Only use this if you have deleted the file! [N/y]?");
                            char yn = 'n';
                            yn = Console.ReadKey().KeyChar;
                            if (yn == 'y' || yn == 'Y')
                            {
                                dm.Reinit();
                                dm = new DatabaseManager();
                            }
                            else
                            {
                                break;
                            }
                            goto case 'r'; 
                        case 'r':
                            Console.WriteLine("\nRefreshing Student List with LDAP Server");
                            RefreshStudentList();
                            RefreshStaffList();
                            foreach (Student s in studentList)
                            {
                                dm.AddStudent(s);
                            }
                            foreach (Staff s in staffList)
                            {
                                dm.AddStaff(s);
                            }
                            Console.WriteLine(String.Format("There are {0} undergrad and postgrad students and {1} staff loaded.", studentList.Count, staffList.Count));
                            break;
                        case 'l':
                            Console.WriteLine("Load database from LDAP? [N/y]?");
                            char _yn = 'n';
                            _yn = Console.ReadKey().KeyChar;
                            if (_yn == 'y' || _yn == 'Y')
                            {
                                Console.WriteLine("Refreshing Student/Staff List with LDAP Server");
                                RefreshStudentList();
                                RefreshStaffList();
                                Console.WriteLine(String.Format("There are {0} undergrad and postgrad students and {1} staff loaded.", studentList.Count, staffList.Count));
                            }
                            else
                            {
                                break;
                            }
                            continue;
                    }
                }
            }
            finally
            {
                Shutdown();
            }
        }

        private string SearchStudentNumber(List<Student> s, string search)
        {
            return (s.Where(x => x.studentNumber == search).FirstOrDefault() ?? new Student()).ToString();
        }
        private string SearchStudentName(List<Student> s, string search)
        {
            return (s.Where(x => x.fullname.StartsWith(search)).FirstOrDefault() ?? new Student()).ToString();
        }
        private string SearchStaffNumber(List<Staff> s, string search)
        {
            return (s.Where(x => x.staffUID == search).FirstOrDefault() ?? new Staff()).ToString();
        }
        private string SearchStaffName(List<Staff> s, string search)
        {
            return (s.Where(x => x.fullname.StartsWith(search)).FirstOrDefault() ?? new Staff()).ToString();
        }

        public void Shutdown()
        {
            dm.Shutdown();
        }
    }
}

