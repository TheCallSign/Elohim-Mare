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
            studentList = (new LDAPManager()).LoadAllStudents();
        }

        public void StartConsole()
        {
            if(studentList.Count() <= 0)
            {
                Console.WriteLine("Refreshing Student List with LDAP Server");
                RefreshStudentList();
            }
            Console.WriteLine(String.Format("There are {0} undergrad and postgrad students loaded.", studentList.Count));
            Console.WriteLine("Interactive interpreter ready. [h for help]");

            char a = ' ';

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
                        Console.WriteLine(studentList.Where(x => {
                            string input = Console.ReadLine().ToLower();
                            return x.fullName.ToLower().StartsWith(input) || x.surname.ToLower().StartsWith(input);
                        }).FirstOrDefault().ToString());
                        break;
                    case 'h':
                    case '?':
                        Console.WriteLine("s : Student number search\nf : Name (First or last) search\nR : Reinit the database");
                        break;
                    case 'R':
                        Console.WriteLine("Reinitialize the database? [N/y]?");
                        char yn = 'n';
                        yn = Console.ReadKey().KeyChar;
                        if (yn != 'n' || yn != 'N')
                        {
                            dm.Reinit();
                        }
                        break;
                }
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

