using Core.Entities;
using System;

namespace Entities.Concrete
{
    public class Person : BaseEntity, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
