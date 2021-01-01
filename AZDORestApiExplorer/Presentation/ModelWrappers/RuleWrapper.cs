using System;
using System.Collections.Generic;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ModelWrappers
{
    public class RuleWrapper : ModelWrapper<Domain.Rule>
    {
        public RuleWrapper(Domain.Rule model) : base(model)
        {
        }

    }
}
