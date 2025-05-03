﻿using Tweeter.Core.Domain.Contracts.Common;

namespace Tweeter.Core.Domain.Common
{
    public abstract class BaseAuditableEntity<TKey> : BaseEntity<TKey>, IBaseAuditableEntity where TKey : IEquatable<TKey>
    {
        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; } = null!;


        public DateTime LastModifiedOn { get; set; }

        public DateTime? JoinDate { get; set; }

    }
}
