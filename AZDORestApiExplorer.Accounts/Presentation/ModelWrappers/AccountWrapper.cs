using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Accounts.Presentation.ModelWrappers
{
    public class AccountWrapper : ModelWrapper<Domain.Accounts.Account>
    {
        public AccountWrapper(Domain.Accounts.Account model) : base(model)
        {
        }
    }
}
