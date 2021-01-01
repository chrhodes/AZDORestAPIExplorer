using System;
using System.Collections.Generic;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ModelWrappers
{
    public class ListWrapper : ModelWrapper<Domain.List>
    {
        public ListWrapper(Domain.List model) : base(model)
        {
        }

    }
}
