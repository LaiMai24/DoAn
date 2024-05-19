
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECH.Areas.Admin.Models;
using TECH.Areas.Admin.Models.Search;
using TECH.Data.DatabaseEntity;
using TECH.Reponsitory;
using TECH.Utilities;

namespace TECH.Service
{
    public interface IAdvertisementService
    {
        void Add(string image);
        string GetImage();
        void Save();
    }

    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private IUnitOfWork _unitOfWork;
        public AdvertisementService(IAdvertisementRepository advertisementRepository,
            IUnitOfWork unitOfWork)
        {
            _advertisementRepository = advertisementRepository;
            _unitOfWork = unitOfWork;
        }
      
        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Add(string image)
        {
            try
            {
                if (!string.IsNullOrEmpty(image))
                {
                    var advertisement = new Advertisement
                    {
                        image = image,
                    };
                    _advertisementRepository.Add(advertisement);
                }
            }
            catch (Exception ex)
            {
            }

        }
         public string GetImage()
        {
            var data = _advertisementRepository.FindAll().OrderByDescending(p=>p.id).FirstOrDefault();
            if (data != null)
            {
                return data.image;
            }
            return null;
        }

    }
}
