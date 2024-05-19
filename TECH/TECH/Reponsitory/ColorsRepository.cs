using Microsoft.EntityFrameworkCore;
using System;
using TECH.Data.DatabaseEntity;

namespace TECH.Reponsitory
{
    public interface IColorsRepository : IRepository<Colors, int>
    {
       
    }

    public class ColorsRepository : EFRepository<Colors, int>, IColorsRepository
    {
        public ColorsRepository(DataBaseEntityContext context) : base(context)
        {
        }
    }
}
