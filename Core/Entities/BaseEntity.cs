using System;

namespace Core.Entities
{
    public class BaseEntity : BaseEntity<int> { }
    public class BaseEntity<T> : IEntity
    {
        public virtual T Id { get; set; }
        public DateTime CreatedAt { get ; set ; }
        public int CreatedBy { get ; set ; }
        public DateTime? UpdatedAt { get ; set ; }
        public int? UpdatedBy { get ; set ; }
        public bool IsDeleted { get ; set ; }
    }
}
