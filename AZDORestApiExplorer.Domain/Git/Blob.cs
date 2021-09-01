using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Git.Events;

using Newtonsoft.Json;

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

            public Domain.Git.Repository Repository;

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

    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class Blob

}
