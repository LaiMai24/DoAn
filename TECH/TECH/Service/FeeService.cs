
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
    public interface IFeeService
    {
        PagedResult<FeeModelView> GetAllPaging(FeeViewModelSearch FeeModelViewSearch);
        FeeModelView GetByid(int id);
        void Add(FeeModelView view);
        bool Update(FeeModelView view);
        bool Deleted(int id);
        FeeModelView GetDetailFee(int cityId, int districtId, int wardId);
        void Save();
    }

    public class FeeService : IFeeService
    {
        private readonly IFeeRepository _feeRepository;
        private IUnitOfWork _unitOfWork;
        public FeeService(IFeeRepository feeRepository,
            IUnitOfWork unitOfWork)
        {
            _feeRepository = feeRepository;
            _unitOfWork = unitOfWork;
        }
        //public List<FeeModelView> GetAll()
        //{
        //    var data = _feeRepository.FindAll(c=>c.status == 0).Select(c=>new FeeModelView()
        //    {
        //        id = c.id,
        //        name = c.name
        //    }).ToList();

        //    return data;
        //}

        //public bool IsCategoryNameExist(string name)
        //{
        //    var data = _feeRepository.FindAll().Any(p => p.name == name);
        //    return data;
        //}

        public FeeModelView GetByid(int id)
        {
            var data = _feeRepository.FindAll(p => p.id == id).FirstOrDefault();
            if (data != null)
            {                
                var model = new FeeModelView()
                {
                    id = data.id,
                    city_id = data.city_id,
                    district_id = data.district_id,
                    ward_id = data.ward_id,
                    fee = data.fee
                };
                return model;
            }
            return null;
        }
        public void Add(FeeModelView view)
        {
            try
            {
                if (view != null)
                {
                    var fees = new Fees
                    {                      
                        city_id = view.city_id,
                        district_id = view.district_id,
                        ward_id = view.ward_id,
                        fee = view.fee,
                    };
                    _feeRepository.Add(fees);                  
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
        public bool Update(FeeModelView view)
        {
            try
            {
                var dataServer = _feeRepository.FindById(view.id);
                if (dataServer != null)
                {
                    dataServer.city_id = view.city_id;
                    dataServer.district_id = view.district_id;
                    dataServer.ward_id = view.ward_id;
                    dataServer.fee = view.fee;
                    _feeRepository.Update(dataServer);                                        
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

      //public  bool UpdateStatus(int id, int status)
      //  {
      //      try
      //      {
      //          var dataServer = _feeRepository.FindById(id);
      //          if (dataServer != null)
      //          {
      //              dataServer.status = status;
      //              _feeRepository.Update(dataServer);
      //              return true;
      //          }
      //      }
      //      catch (Exception ex)
      //      {
      //          return false;
      //      }

      //      return false;
      //  }

        public bool Deleted(int id)
        {
            try
            {
                var dataServer = _feeRepository.FindById(id);
                if (dataServer != null)
                {
                    _feeRepository.Remove(dataServer);
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return false;
        }
        public FeeModelView GetDetailFee(int cityId, int districtId, int wardId)
        {
            var dataServer = _feeRepository.FindAll().Where(p=>p.city_id == cityId && p.district_id == districtId && p.ward_id == wardId).Select(c => new FeeModelView()
            {
                id = c.id,
                city_id = c.city_id,
                district_id = c.district_id,
                ward_id = c.ward_id,
                fee = c.fee
            }).FirstOrDefault();
            return dataServer;
        }
        public PagedResult<FeeModelView> GetAllPaging(FeeViewModelSearch FeeModelViewSearch)
        {
            try
            {
                var query = _feeRepository.FindAll();
                if (FeeModelViewSearch.city_id == 0 &&
                    FeeModelViewSearch.district_id == 0 &&
                    FeeModelViewSearch.ward_id == 0 && !string.IsNullOrEmpty(FeeModelViewSearch.name))
                {
                    query = query.Where(c => c.city_id == FeeModelViewSearch.city_id &&
                    c.district_id == FeeModelViewSearch.district_id &&
                    c.ward_id == FeeModelViewSearch.ward_id);
                }
                else
                {
                    if (FeeModelViewSearch.city_id > 0)
                    {
                        query = query.Where(c => c.city_id == FeeModelViewSearch.city_id);
                    }
                    if (FeeModelViewSearch.district_id > 0)
                    {
                        query = query.Where(c => c.district_id == FeeModelViewSearch.district_id);
                    }
                    if (FeeModelViewSearch.ward_id > 0)
                    {
                        query = query.Where(c => c.ward_id == FeeModelViewSearch.ward_id);
                    }
                }

                int totalRow = query.Count();
                query = query.Skip((FeeModelViewSearch.PageIndex - 1) * FeeModelViewSearch.PageSize).Take(FeeModelViewSearch.PageSize);
                var data = query.Select(c => new FeeModelView()
                {                   
                    id = c.id,
                    city_id = c.city_id,
                    district_id = c.district_id,
                    ward_id = c.ward_id,
                    fee = c.fee
                }).ToList();
              
                var pagingData = new PagedResult<FeeModelView>
                {
                    Results = data,
                    CurrentPage = FeeModelViewSearch.PageIndex,
                    PageSize = FeeModelViewSearch.PageSize,
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
