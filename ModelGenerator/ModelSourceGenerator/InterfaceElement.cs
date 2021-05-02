using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGenerator
{
    class InterfaceElement
    {
        public string Name;

        public List<string> Implementations = new List<string>();
        public List<string> Properties = new List<string>();

        public InterfaceElement(string name)
        {     
            Name = name;
        }

        public void addProperty(string property)
        {
            Properties.Add(property);
        }

        public void addImplementation(string imp)
        {
            if(!Implementations.Contains(imp)) {
                Implementations.Add(imp);
            }
            
        }

        public string getProperties()
        {
            string tmp = "";
            foreach (string prop in Properties)
            {
                tmp += "public " + prop + "\n";
            }
            return tmp;
        }

        public string getImplementations()
        {
            string tmp = "";
            for (int i=0; i< Implementations.Count-1; i++)
            {
                tmp += Implementations[i];
            }
            return tmp;
        }
    }

}
