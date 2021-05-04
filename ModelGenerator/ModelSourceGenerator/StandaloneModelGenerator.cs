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

                if (symbol.DeclaringSyntaxReferences.Length > 0)
                {
                    if (GeneratorUtils.IsModelObject(symbol) || GeneratorUtils.HasOppositeAttribute(symbol))
                    {
                        GenerateImplementation(context, symbol);
                    }
                }
                
            }
            
        }

        private void GenerateOpposite(StandaloneGeneratorExecutionContext context, INamedTypeSymbol intf)
        {
            var ns = intf.ContainingNamespace;
            var nsName = ns.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Substring(8);
            var clsName = intf.Name.Substring(1);
            var cls = ns.GetMembers(clsName).OfType<INamedTypeSymbol>().Where(nts => nts.TypeKind == TypeKind.Class).FirstOrDefault();

            holder.addElement(intf, context);
            holder.addProperty(intf, context); ;
            var element = holder.getElementByName(intf.Name);

            var source =
$@"namespace {nsName} {{
    public partial class {clsName} : {intf.Name} {{
        {element.getProperties()}
        {element.getImplementations()}
    }}
}}";
            context.AddSource(clsName + ".cs", source);
        }

        private void GenerateImplementation(StandaloneGeneratorExecutionContext context, INamedTypeSymbol intf)
        {
            
            var ns = intf.ContainingNamespace;
            var nsName = ns.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Substring(8);
            var clsName = intf.Name.Substring(1);
            var cls = ns.GetMembers(clsName).OfType<INamedTypeSymbol>().Where(nts => nts.TypeKind == TypeKind.Class).FirstOrDefault();


            holder.addElement(intf, context);
            holder.addProperty(intf, context);
            holder.addImplementations(intf, context);

            var element = holder.getElementByName(intf.Name);

            var source = 
$@"namespace {nsName} {{
    public partial class {clsName} : {intf.Name} {{
        {element.getProperties()}
        {element.getImplementations()}
    }}
}}";
            context.AddSource(clsName + ".cs", source);
            //getSource(context, intf, firstLine);

        }
    }
}
