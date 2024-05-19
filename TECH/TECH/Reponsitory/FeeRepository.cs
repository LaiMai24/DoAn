using Microsoft.EntityFrameworkCore;
using System;
using TECH.Data.DatabaseEntity;

namespace TECH.Reponsitory
{
    public interface IFeeRepository : IRepository<Fees, int>
    {
       
    }

    public class FeeRepository : EFRepository<Fees, int>, IFeeRepository
    {
        public FeeRepository(DataBaseEntityContext context) : base(context)
        {
        }
    }
}
