using AZDORestApiExplorer.Domain;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.DomainServices
{
    public interface IHTTPExchangeDataService : IGenericRepository<HTTPExchange>
    {
        void RemovePhoneNumber(HTTPExchangePhoneNumber model);
    }
}
