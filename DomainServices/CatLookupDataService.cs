using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using AZDORestApiExplorer.Persistence.Data;
//using AZDORestApiExplorer.Persistence.LookupData;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.DomainServices
{
    public class LookupDataService : ICatLookupDataService
    {
        private Func<AZDORestApiExplorerDbContext> _contextCreator;

        public LookupDataService(Func<AZDORestApiExplorerDbContext> context)
        {
            _contextCreator = context;
        }

        public async Task<IEnumerable<LookupItem>> GetTYPELookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.TYPESet.AsNoTracking()
                  .Select(f =>
                  new LookupItem
                  {
                      Id = f.Id,
                      DisplayMember = f.FieldString
                  })
                  .ToListAsync();
            }
        }
    }
}
