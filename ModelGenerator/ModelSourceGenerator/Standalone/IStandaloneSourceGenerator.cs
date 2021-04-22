using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGenerator.Standalone
{
    public interface IStandaloneSourceGenerator
    {
        void Execute(StandaloneGeneratorExecutionContext context);
    }
}
