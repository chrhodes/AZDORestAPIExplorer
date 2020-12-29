using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.Domain
{
    public class ProjectsRoot
    {
        public int count { get; set; }
        public Project[] value { get; set; }
    }

    public class Project
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string state { get; set; }
        public int revision { get; set; }
        public string visibility { get; set; }
        public DateTime lastUpdateTime { get; set; }
        public string description { get; set; }
    }

    public class ProjectX : IEntity<int>, IModificationHistory, IOptimistic
    {

        #region IEntity<int>

        public int Id { get; set; }

        #endregion

    #region IModificationHistory

    public DateTime? DateModified { get; set; }

    public DateTime? DateCreated { get; set; }

    public Boolean? IsDirty { get; set; }

    #endregion

    #region IOptimistic

    // Need to have data annotation here.
    // Presence in interface ignored.
    [Timestamp]
    public byte[] RowVersion { get; set; }

    #endregion
}
}
