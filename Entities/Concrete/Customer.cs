﻿using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Customer : Person
    {
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }

        public Customer()
        {
            Vehicles = new HashSet<Vehicle>();
        }
    }
}