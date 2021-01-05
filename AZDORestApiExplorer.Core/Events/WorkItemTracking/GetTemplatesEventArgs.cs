
using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.WorkItemTracking.Core.Events
{
    public class GetTemplatesEventArgs
    {
        public Organization Organization;

        public Domain.Core.Project Project;

        public Domain.Core.Team Team;
    }
}