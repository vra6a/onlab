
using System.Collections.Generic;
using ModelGenerator;
   namespace SampleModel {
    public partial class Husband : IHusband {
        public         string Name { get; set; }

private IWife wife;
public 
 IWife Wife 
 {
                        get {return wife; }set 
                        {
                            if(!object.Equals(wife, value))
                            {
                                wife = (Wife)value;
                                if(!object.Equals(wife, null)) wife.Husband = this;
                            }
                        }}

        
    }
}