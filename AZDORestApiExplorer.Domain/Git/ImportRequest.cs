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
        public class GetImportRequestsEvent : PubSubEvent<GetImportRequestsEventArgs> { }

        public class GetImportRequestsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Domain.Core.Project Project;

            public Domain.Git.Repository Repository;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedImportRequestChangedEvent : PubSubEvent<ImportRequest> { }
    }

    public class ImportRequestsRoot
    {
        public int count { get; set; }
        public ImportRequest[] value { get; set; }
    }

    public class ImportRequest
    {
        public int importRequestId { get; set; }
        public Repository repository { get; set; }
        public Parameters parameters { get; set; }
        public string status { get; set; }
        public Detailedstatus detailedStatus { get; set; }
        public _Links _links { get; set; }
        public string url { get; set; }

        public class Parameters
        {
            public object tfvcSource { get; set; }
            public Gitsource gitSource { get; set; }
            public bool deleteServiceEndpointAfterImportIsDone { get; set; }
        }

        public class Gitsource
        {
            public string url { get; set; }
            public bool overwrite { get; set; }
        }

        public class Detailedstatus
        {
            public int currentStep { get; set; }
            public string[] allSteps { get; set; }
        }

        public class _Links
        {
            public Self self { get; set; }
            public Repository1 repository { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

        public class Repository1
        {
            public string href { get; set; }
        }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class ImportRequest

}
