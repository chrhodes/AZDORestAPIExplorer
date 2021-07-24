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
    public class ProjectDataService : GenericEFRepository<Project, AZDORestApiExplorerDbContext>, IProjectDataService
    {

        #region Constructors, Initialization, and Load

        public ProjectDataService(AZDORestApiExplorerDbContext context)
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

        public override async Task<Project> FindByIdAsync(int id)
        {
            Int64 startTicks = Log.DOMAINSERVICES("(ProjectDataService) Enter", Common.LOG_CATEGORY);

            Project result = null;

            //var result = await Context.ProjectsSet
            //    .Include(f => f.PhoneNumbers)
            //    .SingleAsync(f => f.Id == id);

            Log.DOMAINSERVICES("(ProjectDataService) Exit", Common.LOG_CATEGORY, startTicks);

            return result;
        }

        //public void RemovePhoneNumber(ProjectPhoneNumber model)
        //{
        //    Int64 startTicks = Log.DOMAINSERVICES("Enter", Common.LOG_CATEGORY);

        //    Context.ProjectPhoneNumbersSet.Remove(model);

        //    Log.DOMAINSERVICES("Exit", Common.LOG_CATEGORY, startTicks);
        //}


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods


        #endregion

    }
}
