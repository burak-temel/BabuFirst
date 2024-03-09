using Core.Entities;
using System.Collections.Generic;

namespace Entities.Concrete
{
public class Customer : IEntity
{
    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }
    public ICollection<Vehicle> Vehicles { get; set; }
    public ICollection<Invoice> Invoices { get; set; }

    //public Customer()
    //{
    //    Vehicles = new HashSet<Vehicle>();
    //    Invoices = new HashSet<Invoice>();
    //}
}

}
