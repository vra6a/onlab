using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using ModelGenerator.Standalone;

namespace ModelGenerator.SourceGenerator
{
    [Generator]
    public class ModelSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var generator = new StandaloneModelGenerator();
            generator.Execute(StandaloneGeneratorExecutionContext.FromCSharp(context));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }

    }
}
