
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
    public interface ISidersService
    {
        void Add(string image);
        List<SidersModelView> GetImage();
        bool Deleted(int id);
        void Save();
    }

    public class SidersService : ISidersService
    {
        private readonly ISidersRepository _sidersRepository;
        private IUnitOfWork _unitOfWork;
        public SidersService(ISidersRepository sidersRepository,
            IUnitOfWork unitOfWork)
        {
            _sidersRepository = sidersRepository;
            _unitOfWork = unitOfWork;
        }
      
        public void Save()
        {
            _unitOfWork.Commit();
        }

        public bool Deleted(int id)
        {
            try
            {
                var dataServer = _sidersRepository.FindById(id);
                if (dataServer != null)
                {
                    _sidersRepository.Remove(dataServer);
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return false;
        }

        public void Add(string image)
        {
            try
            {
                if (!string.IsNullOrEmpty(image))
                {
                    var advertisement = new Siders
                    {
                        image = image,
                        create_at = DateTime.Now,
                    };
                    _sidersRepository.Add(advertisement);
                }
            }
            catch (Exception ex)
            {
            }

        }
         public List<SidersModelView> GetImage()
        {
            var data = _sidersRepository.FindAll().Select(i=>new SidersModelView()
            {
                id= i.id,
                image = i.image,
                create_at = i.create_at
            }).ToList();
            if (data != null && data.Count > 0)
            {
                return data;
            }
            return null;
        }

    }
}
