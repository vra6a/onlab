using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ModelGenerator.Standalone;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelGenerator
{
    class InterfaceHolder
    {
        public List<InterfaceElement> elements = new List<InterfaceElement>();

        public void addElement(INamedTypeSymbol intf, StandaloneGeneratorExecutionContext context) {
            if(getElementByName(intf.Name) == null)
            {
                var arr = context.Compilation.SyntaxTrees.ToArray();
               
                List<string> funcNames = getFuncNames(intf.Name, arr[0].GetRoot().DescendantNodes().ToArray());
                List<string> funcBodies = getFuncBody(funcNames, arr[1].GetRoot().DescendantNodes().ToArray());
                InterfaceElement e = new InterfaceElement(intf.Name);
                foreach(string fb in funcBodies)
                {
                    e.addImplementation(fb);
                }
                
                elements.Add(e);
            }
        }

        public InterfaceElement getElementByName(string interfaceName)
        {
            foreach(InterfaceElement e in elements)
            {
                if(e.Name == interfaceName)
                {
                    return e;
                }
            }
            return null;
        }

        private List<string> getFuncBody(List<string> funcNames, SyntaxNode[] nodes)
        {
            bool isFirst = true;
            List<string> bodies = new List<string>();

            foreach(string name in funcNames)
            {
                string funcBody = "";
                foreach (SyntaxNode n in nodes)
                {
                    if (n.ToFullString().Contains(name))
                    {
                        if (isFirst)
                        {
                            isFirst = false;
                        }
                        else
                        {
                            funcBody = n.ToFullString();
                        }
                    }
                }
                bodies.Add(funcBody);
            }

            
            return bodies;
        }


        private List<string> getFuncNames(string intfName, SyntaxNode[] nodes)
        {
            bool isFirst = true;
            List<string> funcNames = new List<string>();
            foreach(SyntaxNode n in nodes)
            {
                if(n.ToFullString().Contains("public interface " + intfName))
                {
                    if(isFirst)
                    {
                        isFirst = false;
                    }else
                    {
                        var desNodes = n.DescendantNodes().ToList();
                        foreach(var a in desNodes)
                        {
                            if(a is PropertyDeclarationSyntax || a is MethodDeclarationSyntax)
                            {
                                funcNames.Add(a.ToString().Substring(0, a.ToString().Length - 1));
                            }
                            if(a is SimpleBaseTypeSyntax)
                            {
                                var otherNames = getFuncNames(a.ToString(), nodes);
                                foreach(var o in otherNames)
                                {
                                    funcNames.Add(o);
                                }

                            }
                        }   
                    }
                }   
            }
            return funcNames;
        }
    }
}
