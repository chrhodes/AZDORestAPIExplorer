using System;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.WorkItemTrackingProcess;
using AZDORestApiExplorer.Persistence.Data;

using VNC;
using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.DomainServices
{
    public class ProcessDataService : GenericEFRepository<Process, AZDORestApiExplorerDbContext>, IProcessDataService
    {

        #region Constructors, Initialization, and Load

        public ProcessDataService(AZDORestApiExplorerDbContext context)
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

        public override async Task<Process> FindByIdAsync(int id)
        {
            Int64 startTicks = Log.DOMAINSERVICES("(ProcessDataService) Enter", Common.LOG_APPNAME);

            Process result = null;

            //var result = await Context.ProcesssSet
            //    .Include(f => f.PhoneNumbers)
            //    .SingleAsync(f => f.Id == id);

            Log.DOMAINSERVICES("(ProcessDataService) Exit", Common.LOG_APPNAME, startTicks);

            return result;
        }

        //public void RemovePhoneNumber(ProcessPhoneNumber model)
        //{
        //    Int64 startTicks = Log.DOMAINSERVICES("Enter", Common.LOG_APPNAME);

        //    Context.ProcessPhoneNumbersSet.Remove(model);

        //    Log.DOMAINSERVICES("Exit", Common.LOG_APPNAME, startTicks);
        //}


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods


        #endregion

    }
}
