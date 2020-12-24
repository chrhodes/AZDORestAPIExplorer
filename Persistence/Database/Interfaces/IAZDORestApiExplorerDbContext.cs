using System.Data.Entity;

using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Persistence.Data
{
    public interface IAZDORestApiExplorerDbContext
    {
        int SaveChanges();

        DbSet<Cat> CatsSet { get; set; }
    }
}
