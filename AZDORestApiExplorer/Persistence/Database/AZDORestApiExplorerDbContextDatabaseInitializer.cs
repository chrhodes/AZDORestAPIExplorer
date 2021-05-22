using System;
using System.Data.Entity;

using VNC;

namespace AZDORestApiExplorer.Persistence.Data
{
    public class AZDORestApiExplorerDbContextDatabaseInitializer : CreateDatabaseIfNotExists<AZDORestApiExplorerDbContext>
    {
        protected override void Seed(AZDORestApiExplorerDbContext context)
        {
            Int64 startTicks = Log.PERSISTENCE("Enter", Common.LOG_CATEGORY);

            base.Seed(context);

            Log.PERSISTENCE("Exit", Common.LOG_CATEGORY, startTicks);
        }
    }
}
