
using System.Collections.Generic;
using ModelGenerator;
   namespace SampleModel {
    public partial class User : IUser {
        public         string Name { get; set; }

private ISet<IRole> roles;
public 
 ISet<IRole> Roles 
 {
                        get {return roles; }}

        
    }
}