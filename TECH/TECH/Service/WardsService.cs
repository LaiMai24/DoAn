
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
    public interface IWardsService
    {      
        List<WardsModelView> GetAll();

        List<WardsModelView> GetWardsForDistrictId(int districtId);
        WardsModelView GetById(int id);
        WardsModelView GetByName(string name);
    }

    public class WardsService : IWardsService
    {
        private readonly IWardsRepository _wardsRepository;
        private IUnitOfWork _unitOfWork;
        public WardsService(IWardsRepository wardsRepository,
            IUnitOfWork unitOfWork)
        {
            _wardsRepository = wardsRepository;
            _unitOfWork = unitOfWork;
        }
        public List<WardsModelView> GetAll()
        {
            var data = _wardsRepository.FindAll().Select(c=>new WardsModelView()
            {
                id = c.id,
                name = c.name
            }).ToList();

            return data;
        }
        public WardsModelView GetById(int id)
        {
            var data = _wardsRepository.FindAll(p => p.id == id).FirstOrDefault();
            if (data != null)
            {
                var model = new WardsModelView()
                {
                    id = data.id,
                    name = data.name,
                };
                return model;
            }
            return null;
        }
        public List<WardsModelView> GetWardsForDistrictId(int districtId)
        {
            var data = _wardsRepository.FindAll().Where(d=>d.district_id == districtId).Select(c => new WardsModelView()
            {
                id = c.id,
                name = c.name
            }).ToList();

            return data;
        }
        public WardsModelView GetByName(string name)
        {
            var data = _wardsRepository.FindAll(p => p.name.ToLower().Trim().Contains(name.ToLower().Trim())).FirstOrDefault();
            if (data != null)
            {
                var model = new WardsModelView()
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
