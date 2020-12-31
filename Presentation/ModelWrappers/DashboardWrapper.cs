using System;
using System.Collections.Generic;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ModelWrappers
{
    public class DashboardWrapper : ModelWrapper<Domain.Dashboard>
    {
        public DashboardWrapper(Domain.Dashboard model) : base(model)
        {
        }

    }
}
