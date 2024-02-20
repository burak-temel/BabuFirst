using Core.Entities;

namespace Entities.Concrete
{
    public class ServiceItem : IEntity
    {
        public int ServiceRecordId { get; set; }
        public ServiceRecord ServiceRecord { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
