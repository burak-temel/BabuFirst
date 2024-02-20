﻿
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class PersonRepository : EfEntityRepositoryBase<Person, ProjectDbContext>, IPersonRepository
    {
        public PersonRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
