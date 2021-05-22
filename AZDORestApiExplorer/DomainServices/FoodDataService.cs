using System;
using System.Data.Entity;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Persistence.Data;

using VNC;
using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.DomainServices
{
    public class FoodDataService : GenericEFRepository<Food, AZDORestApiExplorerDbContext>, IFoodDataService
    {

        #region Constructors, Initialization, and Load

        public FoodDataService(AZDORestApiExplorerDbContext context)
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

        public async Task<bool> IsReferencedByCatAsync(int id)
        {
            Int64 startTicks = Log.DOMAINSERVICES("(FoodDataService) Enter", Common.LOG_CATEGORY);

            var result = await Context.CatsSet.AsNoTracking()
                .AnyAsync(f => f.FavoriteFoodId == id);

            Log.DOMAINSERVICES("(FoodDataService) Exit", Common.LOG_CATEGORY, startTicks);

            return result;
        }

        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods


        #endregion


    }
}
