using Microsoft.EntityFrameworkCore;
using System;
using TECH.Data.DatabaseEntity;

namespace TECH.Reponsitory
{
    public interface ISidersRepository : IRepository<Siders, int>
    {
       
    }

    public class SidersRepository : EFRepository<Siders, int>, ISidersRepository
    {
        public SidersRepository(DataBaseEntityContext context) : base(context)
        {
        }
    }
}
