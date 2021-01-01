using AZDORestApiExplorer.Domain;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.DomainServices
{
    public interface ICatDataService : IGenericRepository<Cat>
    {
        void RemovePhoneNumber(CatPhoneNumber model);
    }
}
