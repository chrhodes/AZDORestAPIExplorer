using System;
using System.Collections.Generic;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ModelWrappers.Accounts
{
    public class AccountWrapper : ModelWrapper<Domain.Accounts.Account>
    {
        public AccountWrapper(Domain.Accounts.Account model) : base(model)
        {
        }

    }
}
