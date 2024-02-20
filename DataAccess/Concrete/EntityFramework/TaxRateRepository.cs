
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class TaxRateRepository : EfEntityRepositoryBase<TaxRate, ProjectDbContext>, ITaxRateRepository
    {
        public TaxRateRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
