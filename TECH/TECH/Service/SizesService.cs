
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
    public interface ISizesService
    {
        PagedResult<SizesModelView> GetAllPaging(CategoryViewModelSearch SizesModelViewSearch);
        SizesModelView GetByid(int id);
        void Add(SizesModelView view);
        bool Update(SizesModelView view);
        bool Deleted(int id);
        void Save();
        bool UpdateStatus(int id, int status);
        List<SizesModelView> GetAll();
        bool IsExist(string name);
    }

    public class SizesService : ISizesService
    {
        private readonly ISizesRepository _sizesRepository;
        private IUnitOfWork _unitOfWork;
        public SizesService(ISizesRepository sizesRepository,
            IUnitOfWork unitOfWork)
        {
            _sizesRepository = sizesRepository;
            _unitOfWork = unitOfWork;
        }
        public List<SizesModelView> GetAll()
        {
            var data = _sizesRepository.FindAll().Select(c=>new SizesModelView()
            {
                id = c.id,
                name = c.name
            }).ToList();

            return data;
        }
        public bool IsExist(string name)
        {
            var data = _sizesRepository.FindAll().Any(p => p.name == name);
            return data;
        }

        public bool IsCategoryNameExist(string name)
        {
            var data = _sizesRepository.FindAll().Any(p => p.name == name);
            return data;
        }

        public SizesModelView GetByid(int id)
        {
            var data = _sizesRepository.FindAll(p => p.id == id).FirstOrDefault();
            if (data != null)
            {                
                var model = new SizesModelView()
                {
                    id = data.id,
                    name = data.name
                };
                return model;
            }
            return null;
        }       
        public void Add(SizesModelView view)
        {
            try
            {
                if (view != null)
                {
                    var size = new Size
                    {                      
                        name = view.name,
                        status = 0,
                    };
                    _sizesRepository.Add(size);                  
                }
            }
            catch (Exception ex)
            {
            }

        }
        public void Save()
        {
            _unitOfWork.Commit();
        }
        public bool Update(SizesModelView view)
        {
            try
            {
                var dataServer = _sizesRepository.FindById(view.id);
                if (dataServer != null)
                {
                    dataServer.name = view.name;
                    _sizesRepository.Update(dataServer);                                        
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

      public  bool UpdateStatus(int id, int status)
        {
            try
            {
                var dataServer = _sizesRepository.FindById(id);
                if (dataServer != null)
                {
                    dataServer.status = status;
                    _sizesRepository.Update(dataServer);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public bool Deleted(int id)
        {
            try
            {
                var dataServer = _sizesRepository.FindById(id);
                if (dataServer != null)
                {
                    _sizesRepository.Remove(dataServer);
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return false;
        }
        public PagedResult<SizesModelView> GetAllPaging(CategoryViewModelSearch SizesModelViewSearch)
        {
            try
            {
                var query = _sizesRepository.FindAll();               
                
                if (!string.IsNullOrEmpty(SizesModelViewSearch.name))
                {
                    query = query.Where(c => c.name.ToLower().Trim().Contains(SizesModelViewSearch.name.ToLower().Trim()));
                }

                int totalRow = query.Count();
                query = query.Skip((SizesModelViewSearch.PageIndex - 1) * SizesModelViewSearch.PageSize).Take(SizesModelViewSearch.PageSize);
                var data = query.Select(c => new SizesModelView()
                {
                    name = (!string.IsNullOrEmpty(c.name) ? c.name : ""),
                    id = c.id,                  
                    status = c.status,
                }).ToList();
              
                var pagingData = new PagedResult<SizesModelView>
                {
                    Results = data,
                    CurrentPage = SizesModelViewSearch.PageIndex,
                    PageSize = SizesModelViewSearch.PageSize,
                    RowCount = totalRow,
                };
                return pagingData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }      
    }
}
