using System;
using System.ComponentModel.DataAnnotations;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.Domain.Core
{
    public class TeamsRoot
    {    
        public int count { get; set; }
        public Team[] value { get; set; }
    }

    public class Team
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string identityUrl { get; set; }
        public string projectName { get; set; }
        public string projectId { get; set; }
    }
}
