using Microsoft.EntityFrameworkCore;
using System;
using TECH.Data.DatabaseEntity;

namespace TECH.Reponsitory
{
    public interface ISizesRepository : IRepository<Size, int>
    {
       
    }

    public class SizesRepository : EFRepository<Size, int>, ISizesRepository
    {
        public SizesRepository(DataBaseEntityContext context) : base(context)
        {
        }
    }
}
