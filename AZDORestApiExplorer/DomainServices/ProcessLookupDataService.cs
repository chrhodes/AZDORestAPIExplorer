using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using AZDORestApiExplorer.Persistence.Data;

using VNC;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.DomainServices
{
    public class ProcessLookupDataService : IProcessLookupDataService
    {

        #region Constructors, Initialization, and Load

        public ProcessLookupDataService(Func<AZDORestApiExplorerDbContext> context)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            _contextCreator = context;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        private Func<AZDORestApiExplorerDbContext> _contextCreator;

        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods

        public async Task<IEnumerable<LookupItem>> GetProcessLookupAsync()
        {
            Int64 startTicks = Log.DOMAINSERVICES("(ProcessLookupDataService) Enter", Common.LOG_CATEGORY);

            IEnumerable<LookupItem> result = null;

            //using (var ctx = _contextCreator())
            //{
            //    result = await ctx.ProcesssSet.AsNoTracking()
            //      .Select(f =>
            //      new LookupItem
            //      {
            //          Id = f.Id,
            //          DisplayMember = f.FieldString
            //      })
            //      .ToListAsync();
            //}

            Log.DOMAINSERVICES("(ProcessLookupDataService) Exit", Common.LOG_CATEGORY, startTicks);

            return result;
        }

        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods


        #endregion

    }
}
