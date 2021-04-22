using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModel
{
    public abstract partial class NamedElement
    {

    }

    public partial class A
    {
        public string FooA(string name)
        {
            return "FooA: " + name;
        }
    }

    public partial class B
    {
        public string FooB(string name)
        {
            return "FooB: " + name;
        }
    }

    public partial class C
    {
        public string FooC(string name)
        {
            return "FooC: " + name;
        }
    }

    public partial class D
    {
        public string FooD(string name)
        {
            return "FooD: " + name;
        }
    }

}
