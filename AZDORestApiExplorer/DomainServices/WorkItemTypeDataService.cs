using System;
using System.Data.Entity;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.WorkItemTrackingProcess;
using AZDORestApiExplorer.Persistence.Data;

using VNC;
using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.DomainServices
{
    public class WorkItemTypeDataService : GenericEFRepository<WorkItemType, AZDORestApiExplorerDbContext>, IWorkItemTypeDataService
    {

        #region Constructors, Initialization, and Load

        public WorkItemTypeDataService(AZDORestApiExplorerDbContext context)
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

        //public override async Task<WorkItemType> FindByIdAsync(int id)
        //{
        //    Int64 startTicks = Log.DOMAINSERVICES("(WorkItemTypeDataService) Enter", Common.LOG_APPNAME);

        //    var result = await Context.WorkItemTypesSet
        //        .Include(f => f.PhoneNumbers)
        //        .SingleAsync(f => f.Id == id);

        //    Log.DOMAINSERVICES("(WorkItemTypeDataService) Exit", Common.LOG_APPNAME, startTicks);

        //    return result;
        //}

        //public void RemovePhoneNumber(WorkItemTypePhoneNumber model)
        //{
        //    Int64 startTicks = Log.DOMAINSERVICES("Enter", Common.LOG_APPNAME);

        //    Context.WorkItemTypePhoneNumbersSet.Remove(model);

        //    Log.DOMAINSERVICES("Exit", Common.LOG_APPNAME, startTicks);
        //}


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods


        #endregion

    }
}
