

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace start
{
    class Program
    {
        static void Main(string[] args)
        {
            //HelloWorldGenerated.HelloWorld.SayHello();
            //Thread.Sleep(10);

            MGenSample.HelloWorld.Hello("asd");
        }
    }
}


namespace MGenSample
{
    public static class HelloWorld
    {
        public static void Hello(string name)
        {
            Console.WriteLine("Hello, " + name + "");
            Console.ReadLine();
        }
    }
}


