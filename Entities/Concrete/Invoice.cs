using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Invoice : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime InvoiceDate { get; set; }
        public ICollection<ServiceRecord> ServiceRecords { get; set; }
        public decimal TotalAmount { get; set; }

        public Invoice()
        {
            ServiceRecords = new HashSet<ServiceRecord>();
        }
    }




}
