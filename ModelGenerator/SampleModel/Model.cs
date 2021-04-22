using ModelGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleModel
{
    [ModelObject]
    public interface INamedElement
    {
        string Name { get; set; }
    }

    public interface IHusband : INamedElement
    {
        [Opposite(Type = typeof(IWife), Name = "Husband")]
        IWife Wife { get; set; }
    }

    public interface IWife : INamedElement
    {
        [Opposite(Type = typeof(IHusband), Name = "Wife")]
        IHusband Husband { get; set; }
    }

    public interface IUser : INamedElement
    {
        [Opposite(Type = typeof(IRole), Name = "Users")]
        ISet<IRole> Roles { get; }
    }

    public interface IRole : INamedElement
    {
        [Opposite(Type = typeof(IUser), Name = "Roles")]
        ISet<IUser> Users { get; }
    }

    [ModelObject]
    public interface IA
    {
        string FooA(string name);
    }

    [ModelObject]
    public interface IB : IA
    {
        string FooB(string name);
    }

    [ModelObject]
    public interface IC : IA
    {
        string FooC(string name);
    }

    [ModelObject]
    public interface ID : IB, IC
    {
        string FooD(string name);
    }
}
