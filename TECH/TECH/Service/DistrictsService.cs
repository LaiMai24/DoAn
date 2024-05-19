
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
    public interface IDistrictsService
    {      
        List<DistrictsModelView> GetAll();
        DistrictsModelView GetById(int id);
        List<DistrictsModelView> GetDistrictForCityId(int cityId);
        DistrictsModelView GetByName(string name);
    }

    public class DistrictsService : IDistrictsService
    {
        private readonly IDistrictsRepository _districtsRepository;
        private IUnitOfWork _unitOfWork;
        public DistrictsService(IDistrictsRepository districtsRepository,
            IUnitOfWork unitOfWork)
        {
            _districtsRepository = districtsRepository;
            _unitOfWork = unitOfWork;
        }
        public List<DistrictsModelView> GetAll()
        {
            var data = _districtsRepository.FindAll().Select(c=>new DistrictsModelView()
            {
                id = c.id,
                name = c.name
            }).ToList();

            return data;
        }

        public List<DistrictsModelView> GetDistrictForCityId(int cityId)
        {
            var data = _districtsRepository.FindAll().Where(c=>c.city_id == cityId).Select(c => new DistrictsModelView()
            {
                id = c.id,
                name = c.name
            }).ToList();

            return data;
        }

        public DistrictsModelView GetById(int id)
        {
            var data = _districtsRepository.FindAll(p => p.id == id).FirstOrDefault();
            if (data != null)
            {
                var model = new DistrictsModelView()
                {
                    id = data.id,
                    name = data.name,
                };
                return model;
            }
            return null;
        }
        public DistrictsModelView GetByName(string name)
        {
            var data = _districtsRepository.FindAll(p => p.name.ToLower().Contains(name.ToLower())).FirstOrDefault();
            if (data != null)
            {
                var model = new DistrictsModelView()
                {
                    id = data.id,
                    name = data.name,
                };
                return model;
            }
            return null;
        }
    }
}
