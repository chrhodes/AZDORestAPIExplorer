using System;
using System.Data.Entity;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Persistence.Data;

using VNC;
using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.DomainServices
{
    public class CollectionDataService : GenericEFRepository<Collection, AZDORestApiExplorerDbContext>, ICollectionDataService
    {

        #region Constructors, Initialization, and Load

        public CollectionDataService(AZDORestApiExplorerDbContext context)
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

        public override async Task<Collection> FindByIdAsync(int id)
        {
            Int64 startTicks = Log.DOMAINSERVICES("(CollectionDataService) Enter", Common.LOG_CATEGORY);

            Collection result = null;

            //var result = await Context.CollectionsSet
            //    .Include(f => f.PhoneNumbers)
            //    .SingleAsync(f => f.Id == id);

            Log.DOMAINSERVICES("(CollectionDataService) Exit", Common.LOG_CATEGORY, startTicks);

            return result;
        }

        //public void RemovePhoneNumber(CollectionPhoneNumber model)
        //{
        //    Int64 startTicks = Log.DOMAINSERVICES("Enter", Common.LOG_APPNAME);

        //    Context.CollectionPhoneNumbersSet.Remove(model);

        //    Log.DOMAINSERVICES("Exit", Common.LOG_APPNAME, startTicks);
        //}


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods


        #endregion

    }
}
