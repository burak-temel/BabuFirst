using System;

namespace Entities.Concrete

{
    public class Employee : Person
    {
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public decimal Salary { get; set; }
    }
}
