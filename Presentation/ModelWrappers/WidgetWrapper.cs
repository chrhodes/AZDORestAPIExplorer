using System;
using System.Collections.Generic;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ModelWrappers
{
    public class WidgetWrapper : ModelWrapper<Domain.Widget>
    {
        public WidgetWrapper(Domain.Widget model) : base(model)
        {
        }

    }
}
