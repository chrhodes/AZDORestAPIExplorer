using System.Collections.Generic;
using System.Threading.Tasks;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.DomainServices
{
    public interface IFoodLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetFoodLookupAsync();
    }
}
