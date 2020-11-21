using System;
using System.Data.Entity;

namespace AZDORestApiExplorer.Persistence.Data
{
    public class AZDORestApiExplorerDbContextDatabaseInitializer : CreateDatabaseIfNotExists<AZDORestApiExplorerDbContext>
    {
        protected override void Seed(AZDORestApiExplorerDbContext context)
        {
            Console.WriteLine("Seed(APPLICATIONDbContext)");
            base.Seed(context);
        }
    }
}
