
using System.Collections.Generic;
using ModelGenerator;
   namespace SampleModel {
    public partial class B : IB {
        
                public string FooA(string name)
        {
            return "FooA: " + name;
        }
        public string FooB(string name)
        {
            return "FooB: " + name;
        }

    }
}