using System;
using System.Collections.Generic;
using System.Text;

namespace codewriter
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
