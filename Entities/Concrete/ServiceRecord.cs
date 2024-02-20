using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class ServiceRecord : BaseEntity, IEntity
    {
        public int Id { get; set; }
        public DateTime ServiceDate { get; set; }
        public string Description { get; set; }
        public decimal LaborCost { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public ICollection<ServiceItem> ServiceItems { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public ServiceRecord()
        {
            ServiceItems = new HashSet<ServiceItem>();
        }
    }




}
