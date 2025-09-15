using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Abstractions
{
    public abstract class Entity<T> : IEntity<T>
    {
        public abstract T Id { get; set; }
        public abstract DateTime? CreatedAt { get; set; }
        public abstract string? CreatedBy { get; set; }
        public abstract DateTime? LastModified { get; set; }
        public abstract string? LastModifiedBy { get; set; }
    }
}
