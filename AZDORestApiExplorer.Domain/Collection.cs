﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.Domain
{
    public class Collection : IEntity<int>, IModificationHistory, IOptimistic
    {
        public Collection()
        {
            //PhoneNumbers = new Collection<CollectionPhoneNumber>();
            ////Meetings = new Collection<Meeting>();
        }

        #region IEntity<int>

        public int Id { get; set; }

        #endregion

        // TODO(crhodes)
        // Examples with each different datatype

        [StringLength(50), Required]
        public string FieldString { get; set; }

        public int? FieldInt { get; set; }

        public Boolean? FieldBoolean { get; set; }

        public double? FieldDouble { get; set; }

        public Single? FieldSingle { get; set; }

        public DateTime? FieldDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? FieldDate2 { get; set; }

        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        //public ICollection<CollectionPhoneNumber> PhoneNumbers { get; set; }

        //public int? FavoriteId { get; set; }

        //public Favorite { get; set; }

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
