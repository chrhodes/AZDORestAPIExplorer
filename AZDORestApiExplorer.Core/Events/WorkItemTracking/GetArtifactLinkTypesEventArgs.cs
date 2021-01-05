using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events.WorkItemTracking
{
    public class GetArtifactLinkTypesEventArgs
    {
        public Organization Organization;

        public Domain.Core.Project Project;
    }
}