namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    namespace Events
    {

    }
    public class ArtifactLinkTypesRoot
    {
        public int count { get; set; }
        public ArtifactLinkType[] value { get; set; }
    }

    public class ArtifactLinkType
    {
        public string linkType { get; set; }
        public string artifactType { get; set; }
        public string toolType { get; set; }
    }
}
