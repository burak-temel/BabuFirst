using Core.Entities;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Vehicle : BaseEntity, IEntity
    {
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string VIN { get; set; }
        public double Mileage { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<ServiceRecord> ServiceRecords { get; set; }

        public Vehicle()
        {
            ServiceRecords = new HashSet<ServiceRecord>();
        }
    }




}
