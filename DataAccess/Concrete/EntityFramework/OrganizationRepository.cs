
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using Core.Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class OrganizationRepository : EfEntityRepositoryBase<Organization, ProjectDbContext>, IOrganizationRepository
    {
        public OrganizationRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
