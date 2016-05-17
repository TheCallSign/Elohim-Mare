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
                        case 'f':
                            Console.WriteLine(studentList.Where(x =>
                            {
                                string input = Console.ReadLine().ToLower();
                                return x.fullName.ToLower().StartsWith(input) || x.surname.ToLower().StartsWith(input);
                            }).FirstOrDefault().ToString());
                            break;
                        case 'h':
                        case '?':
                            Console.WriteLine("s : Student number search\nf : Name (First or last) search\nR : Reinit the database\nl : Reload in-memory list of students");
                            break;
                        case 'R':
                            Console.WriteLine("Reinitialize the database? [N/y]?");
                            char yn = 'n';
                            yn = Console.ReadKey().KeyChar;
                            if (yn != 'n' || yn != 'N')
                            {
                                dm.Reinit();
                            }
                            continue;
                        case 'r':
                            Console.WriteLine("Refreshing Student List with LDAP Server, and writing to database");
                            RefreshStudentList();
                            foreach (Student s in studentList)
                            {
                                dm.AddStudent(s);
                            }
                            Console.WriteLine(String.Format("There are {0} undergrad and postgrad students loaded.", studentList.Count));
                            break;
                        case 'l':
                            Console.WriteLine("Refreshing Student List with LDAP Server");
                            RefreshStudentList();
                            Console.WriteLine(String.Format("There are {0} undergrad and postgrad students loaded.", studentList.Count));
                            break;

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
            return s.Where(x => x.studentNumber == search).FirstOrDefault().ToString();
        }

        public void Shutdown()
        {
            dm.Shutdown();
        }
    }
}

