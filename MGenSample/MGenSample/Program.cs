using System;

namespace MGenSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Compile mgen to C#:
            var generator = new MetaGenerator(@"..\..\..\HelloWorld.mgen");
            generator.Compile();

            // Use the generated code:
            var hw = new HelloWorld();
            Console.WriteLine(hw.Hello("me"));
        }
    }
}
