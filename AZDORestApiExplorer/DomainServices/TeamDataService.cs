using System;
using System.Data.Entity;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Persistence.Data;

using VNC;
using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.DomainServices
{
    public class TeamDataService : GenericEFRepository<Team, AZDORestApiExplorerDbContext>, ITeamDataService
    {

        #region Constructors, Initialization, and Load

        public TeamDataService(AZDORestApiExplorerDbContext context)
            : base(context)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties


        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods

        public override async Task<Team> FindByIdAsync(int id)
        {
            Int64 startTicks = Log.DOMAINSERVICES("(TeamDataService) Enter", Common.LOG_CATEGORY);

            Team result = null;

            //var result = await Context.TeamsSet
            //    .Include(f => f.PhoneNumbers)
            //    .SingleAsync(f => f.Id == id);

            Log.DOMAINSERVICES("(TeamDataService) Exit", Common.LOG_CATEGORY, startTicks);

            return result;
        }

        //public void RemovePhoneNumber(TeamPhoneNumber model)
        //{
        //    Int64 startTicks = Log.DOMAINSERVICES("Enter", Common.LOG_APPNAME);

        //    Context.TeamPhoneNumbersSet.Remove(model);

        //    Log.DOMAINSERVICES("Exit", Common.LOG_APPNAME, startTicks);
        //}


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods


        #endregion

    }
}
