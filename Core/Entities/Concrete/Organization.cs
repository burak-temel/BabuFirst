using Core.Entities;
using System;
using System.Collections.Generic;

namespace Core.Entities.Concrete
{
    public class Organization : BaseEntity, IEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        //user
        public ICollection<User> User { get; set; }

        public Organization()
        {
            User = new HashSet<User>();
        }
    }




}
