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
            Console.WriteLine(String.Format("There are {0} undergrad and postgrad students (and/or possible currently staff) at Rhodes.", students.Count));
            Console.WriteLine("Here is a random one: ");
            int p = (new Random()).Next(0, students.Count - 1);
            Console.WriteLine(students[p]);
            Console.WriteLine(students[p].stuff);
            Console.ReadKey();
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
