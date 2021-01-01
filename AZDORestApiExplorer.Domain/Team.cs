using System;
using System.ComponentModel.DataAnnotations;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.Domain
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

public class TeamX : IEntity<int>, IModificationHistory, IOptimistic
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
