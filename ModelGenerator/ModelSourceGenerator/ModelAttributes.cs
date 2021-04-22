using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelGenerator
{
    [AttributeUsage(AttributeTargets.Interface, Inherited = true)]
    public class ModelObjectAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class OppositeAttribute : Attribute
    {
        public Type Type { get; set; }
        public string Name { get; set; }
    }

}
