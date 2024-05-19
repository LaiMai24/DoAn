
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
    public interface ICityService
    {      
        List<CityModelView> GetAll();
        CityModelView GetById(int id);
        CityModelView GetByName(string name);
    }

    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private IUnitOfWork _unitOfWork;
        public CityService(ICityRepository cityRepository,
            IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }
        public List<CityModelView> GetAll()
        {
            var data = _cityRepository.FindAll().Select(c=>new CityModelView()
            {
                id = c.id,
                name = c.name
            }).ToList();

            return data;
        }
        public CityModelView GetById(int id)
        {
            var data = _cityRepository.FindAll(p => p.id == id).FirstOrDefault();
            if (data != null)
            {
                var model = new CityModelView()
                {
                    id = data.id,
                    name = data.name,                    
                };
                return model;
            }
            return null;
        }
        public CityModelView GetByName(string name)
        {
            var data = _cityRepository.FindAll(p => p.name.ToLower().Trim().Contains(name.ToLower().Trim())).FirstOrDefault();
            if (data != null)
            {
                var model = new CityModelView()
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
