using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Product :BaseEntity, IEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int TaxRateId { get; set; }
        public TaxRate TaxRate { get; set; }
        public ICollection<ServiceItem> ServiceItems { get; set; }

        public Product()
        {
            ServiceItems = new HashSet<ServiceItem>();
        }
    }




}
