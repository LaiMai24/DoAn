using Microsoft.EntityFrameworkCore;
using System;
using TECH.Data.DatabaseEntity;

namespace TECH.Reponsitory
{
    public interface ICityRepository : IRepository<City, int>
    {
       
    }

    public class CityRepository : EFRepository<City, int>, ICityRepository
    {
        public CityRepository(DataBaseEntityContext context) : base(context)
        {
        }
    }
}
