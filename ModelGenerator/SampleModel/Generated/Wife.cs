
using System.Collections.Generic;
   namespace SampleModel {
    public partial class Wife : IWife {
        public         string Name { get; set; }


 IHusband IWife.Husband { get; set; }


        
    }
}