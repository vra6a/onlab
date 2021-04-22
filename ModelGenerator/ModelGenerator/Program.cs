using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using ModelGenerator.Standalone;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ModelGenerator
{
    class Program
    {
        static readonly string SourceFilesPath = @"..\..\..\..\SampleModel";
        static readonly string[] SourceFiles = new string[] { "Model.cs", "Implementation.cs" };
        static readonly string GeneratedFilesPath = Path.Combine(SourceFilesPath, "Generated");

        static void Main(string[] args)
        {
            if (Directory.Exists(GeneratedFilesPath)) Directory.Delete(GeneratedFilesPath, true);
            Directory.CreateDirectory(GeneratedFilesPath);

            var syntaxTrees = SourceFiles.Select(fileName => CSharpSyntaxTree.ParseText(File.ReadAllText(Path.Combine(SourceFilesPath, fileName))));
            var options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);
            var preCompilation = CSharpCompilation.Create("GenModel", options: options).AddSyntaxTrees(syntaxTrees).AddReferences(SystemRuntimeAssemblies()).AddReferences(ReferencedAssemblies());
            var formatter = new DiagnosticFormatter();
            Console.WriteLine("Pre-generation diagnostics:");
            foreach (var diag in preCompilation.GetDiagnostics())
            {
                if (diag.Severity != DiagnosticSeverity.Hidden)
                {
                    Console.WriteLine("  " + formatter.Format(diag));
                }
            }
            Console.WriteLine("===============================");

            var generator = new StandaloneModelGenerator();
            var context = StandaloneGeneratorExecutionContext.FromParams(preCompilation);
            generator.Execute(context);
            foreach (var hs in context.GeneratedSources)
            {
                var genFilePath = Path.Combine(GeneratedFilesPath, hs.HintName);
                Console.WriteLine("Generated file: " + genFilePath);
                File.WriteAllText(genFilePath, hs.Source);
            }

            Console.WriteLine("===============================");
            var postCompilation = preCompilation.AddSyntaxTrees(context.GeneratedSources.Select(hs => CSharpSyntaxTree.ParseText(hs.Source, path: hs.HintName)));
            Console.WriteLine("Post-generation diagnostics:");
            foreach (var diag in postCompilation.GetDiagnostics())
            {
                if (diag.Severity != DiagnosticSeverity.Hidden)
                {
                    Console.WriteLine("  " + formatter.Format(diag));
                }
            }
        }

        private static IEnumerable<MetadataReference> SystemRuntimeAssemblies()
        {
            var assemblyNames = new string[] { "mscorlib.dll", "netstandard.dll", "System.dll", "System.Core.dll", "System.Runtime.dll", "System.Collections.dll", "System.Collections.Immutable.dll" };
            var coreTypes = new Type[] { typeof(object) };
            var coreAssembly = typeof(object).Assembly.Location;
            var assemblyPath = Path.GetDirectoryName(coreAssembly);
            var result = new List<MetadataReference>();
            result.AddRange(assemblyNames.Select(assemblyName => MetadataReference.CreateFromFile(Path.Combine(assemblyPath, assemblyName))));
            result.AddRange(coreTypes.Select(type => MetadataReference.CreateFromFile(type.Assembly.Location)));
            return result;
        }

        private static IEnumerable<MetadataReference> ReferencedAssemblies()
        {
            var types = new Type[] { typeof(ModelGenerator.ModelObjectAttribute) };
            var assemblyPath = Path.GetDirectoryName(typeof(object).Assembly.Location);
            return types.Select(type => MetadataReference.CreateFromFile(type.Assembly.Location));
        }

    }
}
