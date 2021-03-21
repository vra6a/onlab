using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGen
{
    class Parameter
    {
        public string _Name;
        public string _Type;

        public Parameter(string type, string name)
        {
            _Type = type;
            _Name = name;
        }

    }
}
