using Microsoft.CodeAnalysis;
using ModelGenerator.Standalone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelGenerator
{
    public class StandaloneModelGenerator : IStandaloneSourceGenerator
    {
        InterfaceHolder holder = new InterfaceHolder();

        public void Execute(StandaloneGeneratorExecutionContext context)
        {
            foreach (var symbol in GeneratorUtils.GetNamedTypeSymbols(context.Compilation))
            {
               
                if(symbol.DeclaringSyntaxReferences.Length > 0)
                {
                    if (GeneratorUtils.IsModelObject(symbol))
                    {
                        GenerateImplementation(context, symbol);
                    }else if(GeneratorUtils.HasOppositeAttribute(symbol))
                    {
                        Console.WriteLine(symbol);
                    }
                }
                
                
            }
            
        }

        private void GenerateImplementation(StandaloneGeneratorExecutionContext context, INamedTypeSymbol intf)
        {
            
            var ns = intf.ContainingNamespace;
            var nsName = ns.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Substring(8);
            var clsName = intf.Name.Substring(1);
            var cls = ns.GetMembers(clsName).OfType<INamedTypeSymbol>().Where(nts => nts.TypeKind == TypeKind.Class).FirstOrDefault();

            
            holder.addElement(intf, context);
            var element = holder.getElementByName(intf.Name);

            var firstLine = "public partial class " + clsName;
            var source = 
$@"namespace {nsName} {{
    public partial class {clsName} : {intf.Name} {{
        {element.getImplementations()}
        
    }}
}}";
            context.AddSource(clsName + ".cs", source);
            //getSource(context, intf, firstLine);

        }
    }
}
