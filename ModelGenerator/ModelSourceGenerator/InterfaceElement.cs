using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGenerator
{
    class InterfaceElement
    {
        public string Name;

        public List<string> Implementations = new List<string>();
        public List<customProperty> Properties = new List<customProperty>();

        public InterfaceElement(string name)
        {     
            Name = name;
        }

        public void addProperty(customProperty property)
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
            foreach (customProperty prop in Properties)
            {
                if(prop.isOpposite)
                {
                    tmp += prop.body + "\n";
                }
                else
                {
                    tmp += "public " + prop.body + "\n";
                }
                
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
