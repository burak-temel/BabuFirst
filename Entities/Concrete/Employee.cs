using Core.Entities;
using Core.Entities.Concrete;
using System;

namespace Entities.Concrete

{
    public class Employee : Person, IEntity
    {
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public decimal? Salary { get; set; }
    }
}
