
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class VehicleRepository : EfEntityRepositoryBase<Vehicle, ProjectDbContext>, IVehicleRepository
    {
        public VehicleRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
