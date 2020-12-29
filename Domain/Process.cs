using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.Domain
{
    public class ProcessesRoot
    {
        public int count { get; set; }
        public Process[] value { get; set; }
    }

    public class Process
    {
        public string typeId { get; set; }
        public string name { get; set; }
        public string referenceName { get; set; }
        public string description { get; set; }
        public string parentProcessTypeId { get; set; }
        public bool isEnabled { get; set; }
        public bool isDefault { get; set; }
        public string customizationType { get; set; }
    }

    public class ProcessX : IEntity<int>, IModificationHistory, IOptimistic
    {

        public int Id { get; set; }
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
