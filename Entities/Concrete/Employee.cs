using Core.Entities;
using System;

namespace Entities.Concrete

{
    public class Employee : IEntity
    {
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public decimal Salary { get; set; }
    }
}
