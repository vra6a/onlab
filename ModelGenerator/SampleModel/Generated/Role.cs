
using System.Collections.Generic;
using ModelGenerator;
   namespace SampleModel {
    public partial class Role : IRole {
        public         string Name { get; set; }

private ISet<IUser> users;
public 
 ISet<IUser> Users 
 {
                        get {return users; }}

        
    }
}