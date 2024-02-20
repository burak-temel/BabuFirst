using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class TaxRate : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public ICollection<Product> Products { get; set; }

        public TaxRate()
        {
            Products = new HashSet<Product>();
        }
    }




}
