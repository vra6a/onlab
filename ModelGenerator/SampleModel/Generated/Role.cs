namespace SampleModel {
    public partial class Role : IRole {
        public         string Name { get; set; }


 ISet<IUser> IRole.Users { get; }


        
    }
}