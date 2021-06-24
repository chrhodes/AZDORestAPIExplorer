using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using AZDORestApiExplorer.Domain.Core;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.Domain.Git
{
    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Rename classes to use TYPEsRoot and TYPE classes

    public class RepositoriesRoot
    {
        public int count { get; set; }
        public Repository[] value { get; set; }
    }

    public class Repository
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public Project project { get; set; }
        public string defaultBranch { get; set; }
        public int size { get; set; }
        public string remoteUrl { get; set; }
        public string sshUrl { get; set; }
        public string webUrl { get; set; }
        public bool isDisabled { get; set; }
        public bool isFork { get; set; }
    }
}
