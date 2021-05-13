using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGenerator
{
    class customProperty
    {
        public string body;
        public bool isPrivate;

        public customProperty(string _body, bool _isPrivate)
        {
            this.body = _body;
            this.isPrivate = _isPrivate;
        }
    }
}
