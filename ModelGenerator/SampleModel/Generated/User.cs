
using System.Collections.Generic;
   namespace SampleModel {
    public partial class User : IUser {
        public         string Name { get; set; }


 ISet<IRole> IUser.Roles { get; }


        
    }
}