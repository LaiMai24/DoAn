using Microsoft.EntityFrameworkCore;
using System;
using TECH.Data.DatabaseEntity;

namespace TECH.Reponsitory
{
    public interface IDistrictsRepository : IRepository<Districts, int>
    {
       
    }

    public class DistrictsRepository : EFRepository<Districts, int>, IDistrictsRepository
    {
        public DistrictsRepository(DataBaseEntityContext context) : base(context)
        {
        }
    }
}
