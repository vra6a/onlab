namespace SampleModel {
    public partial class Husband : IHusband {
        public         string Name { get; set; }


 IWife IHusband.Wife { get; set; }


        
    }
}