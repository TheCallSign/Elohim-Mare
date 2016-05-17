using ElohimMare.EMBackend;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ElohimMare
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = (new LDAPManager()).LoadAllStudents();
            Console.WriteLine(String.Format("There are {0} undergrad and postgrad students loaded.", students.Count));
            Console.WriteLine("Interactive interpreter ready. [h for help]");
            Interpret(students);
            Console.ReadKey();
        }

        static void Interpret(List<Student> s)
        {
            char a = ' ';
            while (a != 'q')
            {
                a = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (a)
                {
                    case 's':
                        Console.WriteLine(SearchStudentNumber(s, Console.ReadLine()));
                        break;
                    case 'f':
                        Console.WriteLine(s.Where(x => x.studentNumber == Console.ReadLine()).FirstOrDefault().ToString());
                        break;
                    case 'h':
                        Console.WriteLine("s for Student number search\nf for First name search");
                        break;
                }
            }
        }

        static string SearchStudentNumber(List<Student> s, string search)
        {
            return s.Where(x => x.studentNumber == search).FirstOrDefault().ToString();
        }
    }

    class EmRPCServer
    {
        public EmRPCServer()
        {

        }

        public int InitServer()
        {
            
            return 0;
        }
    }
}
