using AZDORestApiExplorer.Domain;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.DomainServices
{
    public interface ITeamDataService : IGenericRepository<Team>
    {
        //void RemovePhoneNumber(TeamPhoneNumber model);
    }
}
