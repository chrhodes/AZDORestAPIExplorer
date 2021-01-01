using System;
using System.Collections.Generic;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ModelWrappers
{
    public class SystemControlWrapper : ModelWrapper<Domain.SystemControl>
    {
        public SystemControlWrapper(Domain.SystemControl model) : base(model)
        {
        }

    }
}
