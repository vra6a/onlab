
using System.Collections.Generic;
using ModelGenerator;
   namespace SampleModel {
    public partial class Wife : IWife {
        public         string Name { get; set; }

private IHusband husband;
public 
 IHusband Husband 
 {
                        get {return husband; }set 
                        {
                            if(!object.Equals(husband, value))
                            {
                                husband = (Husband)value;
                                if(!object.Equals(husband, null)) husband.Wife = this;
                            }
                        }}

        
    }
}