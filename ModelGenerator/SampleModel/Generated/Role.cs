using System.Collections.Generic;

namespace SampleModel {
    public partial class Role : IRole {
        public         string Name { get; set; }

        public ISet<IUser> Users { get; }



        
    }
}