using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace codewriter
{
    class Template
    {
        private ArrayList _lines = new ArrayList();
        public string Name;
        public ArrayList Parameters = new ArrayList();
        public ArrayList Lines = new ArrayList();

        public Template()
        {
        }

        public void Addline(string line)
        {
            _lines.Add(line);
        }

        public void Finished()
        {
            getNameAndParameters();
            generateLines();
           
        }

        private void generateLines()
        {
            bool isFirst = true;
            string tmp = "";
            foreach (string s in _lines)
            {
                if(isFirst)
                {
                    isFirst = false;
                }else
                {
                    if(s.Contains("["))
                    {
                        tmp = "Console.WriteLine(\"";
                        int index = 0;
                        while(index < s.Length-1)
                        {
                            if(s[index] == '[')
                            {
                                index++;
                                tmp += "\" + ";
                            }else if(s[index] == ']')
                            {
                                tmp += " + \"";
                                index++;
                            }else
                            {
                                tmp += s[index];
                                index++;
                            }
                            
                        }
                        tmp += "\");";
                    }
                    else
                    {
                        tmp = s;
                    }
                }
                //Console.WriteLine(tmp);
                Lines.Add(tmp);
            }
        }

        public void debug()
        {
            foreach(string s in _lines)
            {
                //Console.WriteLine(s);
            }
        }

        private void getNameAndParameters()
        {
            string firstLine = _lines[0].ToString();
            Name = firstLine.Substring(9, firstLine.IndexOf("(") - firstLine.IndexOf(" ")-1);
            //Console.WriteLine(Name);

            string pmeters = firstLine.Substring(firstLine.IndexOf("(") + 1, firstLine.IndexOf(")") - firstLine.IndexOf("(")-1);

            string[] pm = pmeters.Split(",");

            foreach (string p in pm)
            {
                string[] l = p.Split(" ");
                Parameter tmp = new Parameter(l[0], l[1]);
                Parameters.Add(tmp);
            }
        }

        private void getParameters()
        {

        }

    }
}
