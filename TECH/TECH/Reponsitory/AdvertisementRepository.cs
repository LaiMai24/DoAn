using Microsoft.EntityFrameworkCore;
using System;
using TECH.Data.DatabaseEntity;

namespace TECH.Reponsitory
{
    public interface IAdvertisementRepository : IRepository<Advertisement, int>
    {
       
    }

    public class AdvertisementRepository : EFRepository<Advertisement, int>, IAdvertisementRepository
    {
        public AdvertisementRepository(DataBaseEntityContext context) : base(context)
        {
        }
    }
}
