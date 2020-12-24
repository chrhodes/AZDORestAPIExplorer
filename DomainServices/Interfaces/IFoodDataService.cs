using System.Threading.Tasks;

using AZDORestApiExplorer.Domain;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.DomainServices
{
    public interface IFoodDataService : IGenericRepository<Food>
    {
        Task<bool> IsReferencedByCatAsync(int id);
    }
}
