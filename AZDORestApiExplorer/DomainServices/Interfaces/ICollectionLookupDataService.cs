using System.Collections.Generic;
using System.Threading.Tasks;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.DomainServices
{
    public interface ICollectionLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetCollectionLookupAsync();
    }
}
