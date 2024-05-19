
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
    public interface IColorsService
    {
        PagedResult<ColorsModelView> GetAllPaging(CategoryViewModelSearch ColorsModelViewSearch);
        ColorsModelView GetByid(int id);
        void Add(ColorsModelView view);
        bool Update(ColorsModelView view);
        bool Deleted(int id);
        void Save();
        bool UpdateStatus(int id, int status);
        List<ColorsModelView> GetAll();
        bool IsExist(string name);
    }

    public class ColorsService : IColorsService
    {
        private readonly IColorsRepository _colorsRepository;
        private IUnitOfWork _unitOfWork;
        public ColorsService(IColorsRepository colorsRepository,
            IUnitOfWork unitOfWork)
        {
            _colorsRepository = colorsRepository;
            _unitOfWork = unitOfWork;
        }
        public List<ColorsModelView> GetAll()
        {
            var data = _colorsRepository.FindAll().Select(c=>new ColorsModelView()
            {
                id = c.id,
                name = c.name
            }).ToList();

            return data;
        }
        public bool IsExist(string name)
        {
            var data = _colorsRepository.FindAll().Any(p => p.name == name || p.code == name);
            return data;
        }

        public bool IsCategoryNameExist(string name)
        {
            var data = _colorsRepository.FindAll().Any(p => p.name == name);
            return data;
        }

        public ColorsModelView GetByid(int id)
        {
            var data = _colorsRepository.FindAll(p => p.id == id).FirstOrDefault();
            if (data != null)
            {                
                var model = new ColorsModelView()
                {
                    id = data.id,
                    name = data.name,
                    code =data.code
                };
                return model;
            }
            return null;
        }       
        public void Add(ColorsModelView view)
        {
            try
            {
                if (view != null)
                {
                    var category = new Colors
                    {                      
                        name = view.name,
                        code = view.code,
                        status = 0,
                    };
                    _colorsRepository.Add(category);                  
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
        public bool Update(ColorsModelView view)
        {
            try
            {
                var dataServer = _colorsRepository.FindById(view.id);
                if (dataServer != null)
                {
                    dataServer.name = view.name;
                    dataServer.code = view.code;
                    _colorsRepository.Update(dataServer);                                        
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
                var dataServer = _colorsRepository.FindById(id);
                if (dataServer != null)
                {
                    dataServer.status = status;
                    _colorsRepository.Update(dataServer);
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
                var dataServer = _colorsRepository.FindById(id);
                if (dataServer != null)
                {
                    _colorsRepository.Remove(dataServer);
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return false;
        }
        public PagedResult<ColorsModelView> GetAllPaging(CategoryViewModelSearch ColorsModelViewSearch)
        {
            try
            {
                var query = _colorsRepository.FindAll();               
                
                if (!string.IsNullOrEmpty(ColorsModelViewSearch.name))
                {
                    query = query.Where(c => c.name.ToLower().Trim().Contains(ColorsModelViewSearch.name.ToLower().Trim()));
                }

                int totalRow = query.Count();
                query = query.Skip((ColorsModelViewSearch.PageIndex - 1) * ColorsModelViewSearch.PageSize).Take(ColorsModelViewSearch.PageSize);
                var data = query.Select(c => new ColorsModelView()
                {
                    name = (!string.IsNullOrEmpty(c.name) ? c.name : ""),
                    code = (!string.IsNullOrEmpty(c.code) ? c.code : ""),
                    id = c.id,                  
                    status = c.status,
                }).ToList();
              
                var pagingData = new PagedResult<ColorsModelView>
                {
                    Results = data,
                    CurrentPage = ColorsModelViewSearch.PageIndex,
                    PageSize = ColorsModelViewSearch.PageSize,
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
