using Microsoft.EntityFrameworkCore;
using System;
using TECH.Data.DatabaseEntity;

namespace TECH.Reponsitory
{
    public interface IWardsRepository : IRepository<Wards, int>
    {
       
    }

    public class WardsRepository : EFRepository<Wards, int>, IWardsRepository
    {
        public WardsRepository(DataBaseEntityContext context) : base(context)
        {
        }
    }
}
