using System;
using System.Data.Entity;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Persistence.Data;

using VNC;
using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.DomainServices
{
    public class Core_Project_DataService : GenericEFRepository<Domain.Core.Project, AZDORestApiExplorerDbContext>, ICore_Project_DataService
    {

        #region Constructors, Initialization, and Load

        public Core_Project_DataService(AZDORestApiExplorerDbContext context)
            : base(context)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
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

        public override async Task<Domain.Core.Project> FindByIdAsync(int id)
        {
            Int64 startTicks = Log.DOMAINSERVICES("(ProjectDataService) Enter", Common.LOG_APPNAME);

            Domain.Core.Project result = null;

            //var result = await Context.ProjectsSet
            //    .Include(f => f.PhoneNumbers)
            //    .SingleAsync(f => f.Id == id);

            Log.DOMAINSERVICES("(ProjectDataService) Exit", Common.LOG_APPNAME, startTicks);

            return result;
        }

        //public void RemovePhoneNumber(ProjectPhoneNumber model)
        //{
        //    Int64 startTicks = Log.DOMAINSERVICES("Enter", Common.LOG_APPNAME);

        //    Context.ProjectPhoneNumbersSet.Remove(model);

        //    Log.DOMAINSERVICES("Exit", Common.LOG_APPNAME, startTicks);
        //}


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods


        #endregion

    }
}
