
using System.Collections.Generic;
using ModelGenerator;
   namespace SampleModel {
    public partial class C : IC {
        
                public string FooA(string name)
        {
            return "FooA: " + name;
        }
        public string FooC(string name)
        {
            return "FooC: " + name;
        }

    }
}