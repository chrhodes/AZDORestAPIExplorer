using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Git.Events;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Git
{
    namespace Events
    {
        public class GetBlobsEvent : PubSubEvent<GetBlobsEventArgs> { }

        public class GetBlobsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Domain.Core.Project Project;

            public Domain.Git.GitRepository Repository;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedBlobChangedEvent : PubSubEvent<Blob> { }
    }

    public class BlobsRoot
    {
        public int count { get; set; }
        public Blob[] value { get; set; }
    }

    public class Blob
    {

        public RESTResult<Blob> Results { get; set; } = new RESTResult<Blob>();

        public async Task<RESTResult<Blob>> GetList(GetBlobsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Project)", Common.LOG_CATEGORY);



            Log.DOMAIN("Exit(Project)", Common.LOG_CATEGORY, startTicks);

            return Results;
        }
    }
}
