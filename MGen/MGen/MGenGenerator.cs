using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGen
{
    [Generator]
    class MGenGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            ArrayList lines = ReadIn("../../../HelloWorld.mgen");
            string NameSpaceName = FindNameSpaceName(lines);
            string ClassName = FindClassName(lines);
            ArrayList templates = FindTemplates(lines);
            context.AddSource(NameSpaceName, SourceText.From(appender(NameSpaceName, ClassName, templates), Encoding.UTF8));

        }

        static private string appender(string NameSpaceNames, string ClassName, ArrayList templates)
        {
            StringBuilder sourcebuilder = new StringBuilder(
                "using System;\n");
            sourcebuilder.Append(
                "namespace " + NameSpaceNames + "\n");

            sourcebuilder.Append(
                "{\n");
            sourcebuilder.Append(
                "   public static class " + ClassName + "\n"
                    + "   {\n");
            foreach (Template t in templates)
            {
                sourcebuilder.Append(
                    "       public static void " + t.Name + "(");

                int index = 1;
                foreach (Parameter p in t.Parameters)
                {
                    if (index == t.Parameters.Count)
                    {
                        sourcebuilder.Append(p._Type + " " + p._Name + ") {\n");
                    }
                    else
                    {
                        sourcebuilder.Append(p._Type + " " + p._Name + ", ");
                    }
                    index++;
                }

                foreach (string s in t.Lines)
                {
                    sourcebuilder.Append(
                        "      " + s);
                }

            }

            sourcebuilder.Append(
                    "\n       }");

            sourcebuilder.Append(
                    "\n   }");

            sourcebuilder.Append(
                 "\n}");

            return sourcebuilder.ToString();
        }

        private static ArrayList FindTemplates(ArrayList lines)
        {
            ArrayList templates = new ArrayList();
            bool inTemplate = false;
            Template tmp = new Template();
            foreach (string l in lines)
            {
                if (l.Contains("template"))
                {
                    inTemplate = true;
                    if (l.Contains("end template"))
                    {
                        inTemplate = false;
                        tmp.Finished();
                        templates.Add(tmp);
                        tmp = new Template();
                    }
                }
                if (inTemplate)
                {
                    tmp.Addline(l);
                }
            }

            return templates;
        }

        private static string FindClassName(ArrayList lines)
        {
            foreach (string l in lines)
            {
                if (l.Contains("generator"))
                {
                    int startindex = l.IndexOf("generator");
                    Console.WriteLine();
                    return l.Substring(startindex + 10, l.Length - l.IndexOf(" ") - 2);
                }
            }
            return null;
        }

        private static string FindNameSpaceName(ArrayList lines)
        {
            foreach (string l in lines)
            {
                if (l.Contains("namespace"))
                {
                    int startindex = l.IndexOf("namespace");
                    return l.Substring(startindex + 10, l.Length - 11);
                }
            }
            return null;
        }

        private static ArrayList ReadIn(string path)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@path);
            ArrayList lines = new ArrayList();

            string line;
            while ((line = file.ReadLine()) != null)
            {
                //System.Console.WriteLine(line);
                lines.Add(line);
            }
            file.Close();
            return lines;
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
