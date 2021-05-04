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
                InterfaceElement e = new InterfaceElement(intf.Name);

                elements.Add(e);
            }
        }

        public void addImplementations(INamedTypeSymbol intf, StandaloneGeneratorExecutionContext context)
        {
            foreach (var e in elements)
            {
                if (e.Name == intf.Name)
                {
                    var arr = context.Compilation.SyntaxTrees.ToArray();
                    List<string> funcNames = getFuncNames(intf.Name, arr[0].GetRoot().DescendantNodes().ToArray());
                    List<string> funcBodies = getFuncBody(funcNames, arr[1].GetRoot().DescendantNodes().ToArray());

                    foreach (string fb in funcBodies)
                    {
                        e.addImplementation(fb);
                    }
                }
            }
        }

        public void addProperty(INamedTypeSymbol intf, StandaloneGeneratorExecutionContext context) {
            foreach(var e in elements)
            {
                if(e.Name == intf.Name)
                {
                    var arr = context.Compilation.SyntaxTrees.ToArray();
                    List<customProperty> funcProperties = getFuncProperties(intf.Name, arr[0].GetRoot().DescendantNodes().ToArray());

                    foreach (customProperty fp in funcProperties)
                    {
                        e.addProperty(fp);
                    }
                }
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

        private List<customProperty> getFuncProperties(string intfName, SyntaxNode[] nodes)
        {
            bool isFirst = true;
            List<customProperty> properties = new List<customProperty>();
            foreach (SyntaxNode n in nodes)
            {
                

                if (n.ToFullString().Contains("public interface " + intfName))
                {
                    if (isFirst)
                    {
                        isFirst = false;
                    }
                    else
                    {
                        var desNodes = n.DescendantNodes().ToList();
                        foreach (var a in desNodes)
                        {
                            

                            if (a is PropertyDeclarationSyntax)
                            {
                                
                                bool onlySetter = false;
                                foreach (var b in a.DescendantNodes())
                                {
                                    if(b is AccessorListSyntax)
                                    {

                                        if (b.DescendantNodes().Count() < 2)
                                        {
                                            onlySetter = true;
                                        }
                                    }
                                }
                                if (a.ToFullString().Contains("]"))
                                {
                                    var tmp = a.ToFullString().Split(']');
                                    var interfaceadder = tmp[1].ToString().Split(' ');
                                    interfaceadder = interfaceadder.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                                    interfaceadder[2] = intfName + "." + interfaceadder[2];
                                    var final = String.Join(" ", interfaceadder);
                                    properties.Add(new customProperty(final, true));

                                }
                                else
                                {
                                    properties.Add(new customProperty(a.ToFullString(), false));
                                }
                                
                            }
                            if (a is SimpleBaseTypeSyntax)
                            {
                                var otherProperties = getFuncProperties(a.ToString(), nodes);
                                foreach (var o in otherProperties)
                                {
                                    properties.Add(o);
                                }
                            }
                        }
                    }
                }
            }
            return properties;
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
                            if(a is MethodDeclarationSyntax)
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
