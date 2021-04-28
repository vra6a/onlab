using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGenerator
{
    internal class GeneratorUtils
    {
        public static IEnumerable<INamedTypeSymbol> GetNamedTypeSymbols(Compilation compilation)
        {
            var stack = new Stack<INamespaceSymbol>();
            stack.Push(compilation.GlobalNamespace);

            while (stack.Count > 0)
            {
                var @namespace = stack.Pop();

                foreach (var member in @namespace.GetMembers())
                {
                    if (member is INamespaceSymbol memberAsNamespace)
                    {
                        stack.Push(memberAsNamespace);
                    }
                    else if (member is INamedTypeSymbol memberAsNamedTypeSymbol)
                    {
                        yield return memberAsNamedTypeSymbol;
                    }
                }
            }
        }

        public static bool IsModelObject(INamedTypeSymbol namedType)
        {
            if (namedType.TypeKind != TypeKind.Interface) return false;
            foreach (var attr in namedType.GetAttributes())
            {
                if (attr.AttributeClass.Name == "ModelObjectAttribute") return true;
            }
            return false;
        }

        public static bool HasOppositeAttribute(INamedTypeSymbol namedType)
        {
            foreach (var member in namedType.GetMembers())
            {
                foreach (var attr in member.GetAttributes())
                    if (attr.AttributeClass.Name == "OppositeAttribute")
                        return true;
            }
            return false;
        }
    }
}
