using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    namespace Events
    {
        public class GetTemplatesEvent : PubSubEvent<GetTemplatesEventArgs> { }

        public class GetTemplatesEventArgs
        {
            public Organization Organization;

            public Domain.Core.Project Project;

            public Domain.Core.Team Team;
        }

        public class SelectedTemplateChangedEvent : PubSubEvent<Template> { }
    }


    public class TemplatesRoot
    {
    }

    public class Template
    {

        public RESTResult<Template> Results { get; set; } = new RESTResult<Template>();
    }

}
