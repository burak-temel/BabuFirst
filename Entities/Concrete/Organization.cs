using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Organization : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Customer> Customers { get; set; }

        public Organization()
        {
            Employees = new HashSet<Employee>();
            Customers = new HashSet<Customer>();
        }
    }




}
