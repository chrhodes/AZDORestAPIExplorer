using System.Collections.Generic;
using System.Threading.Tasks;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.DomainServices
{
    public interface IProjectLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetProjectLookupAsync();
    }
}
