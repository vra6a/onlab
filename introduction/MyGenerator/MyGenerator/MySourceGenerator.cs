using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SourceGeneratorSamples
{
    [Generator]
    public class HelloWorldGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // begin creating the source we'll inject into the users compilation
            StringBuilder sourceBuilder =
                new StringBuilder(@"
                                    using System;
                                    using System.Threading;
                                    using System.Threading.Tasks;
                                    namespace HelloWorldGenerated
                                    {
                                        public static class HelloWorld
                                        {
                                            public static void SayHello() 
                                            {
                                                Console.WriteLine(""Hello from generated code!"");
                                                for(int i=0; i<30; i++) {
                                                        Console.WriteLine(i);
                                                        Thread.Sleep(10);
                                                }
                                    ");

            

            // finish creating the source to inject
            sourceBuilder.Append(@"
        }
    }
}");

            // inject the created source into the users compilation
            context.AddSource("helloWorldGenerated", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // No initialization required
        }
    }
}
