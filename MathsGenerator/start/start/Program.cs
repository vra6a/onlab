using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace start
{
    class Program
    {
        static void Main(string[] args)
        {
            var mathString = System.IO.File.ReadAllText(@"D:\test.txt");
            Console.WriteLine(mathString);
            Console.ReadLine();
            
        }
    }
}
