using Marketplace.DAL.Interfaces;
using Marketplace.Domain.Entity;
using Marketplace.Domain.Response;
using Marketplace.Domain.ViewModels.ProductInStore;
using Marketplace.Domain.ViewModels.Store;
using Marketplace.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Marketplace.Service.Implementations
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public IBaseResponse<bool> CreateStore(StoreViewModel storeViewModel)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var store = new Store()
                {
                    Name = storeViewModel.Name,
                    Address = storeViewModel.Address
                };

                _storeRepository.Create(store);
                baseResponse.Data = true;
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateStore] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                    Data = false
                };
            }
        }

        public IBaseResponse<bool> DeleteStore(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var store = _storeRepository.Get(id);
                if (store == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                    return baseResponse;
                }

                _storeRepository.Delete(store);
                baseResponse.Data = true;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteStore] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                    Data = false
                };
            }
        }

        public IBaseResponse<Store> GetStore(int id)
        {
            var baseResponse = new BaseResponse<Store>();
            try
            {
                var store = _storeRepository.Get(id);
                if (store == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                    return baseResponse;
                }

                baseResponse.Data = store;
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Store>()
                {
                    Description = $"[GetStore] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<Store> GetStoreByName(string name)
        {
            var baseResponse = new BaseResponse<Store>();
            try
            {
                var store = _storeRepository.GetByName(name);
                if (store == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                    return baseResponse;
                }

                baseResponse.Data = store;
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Store>()
                {
                    Description = $"[GetStoreByName] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<IEnumerable<Store>> GetStores()
        {
            var baseResponse = new BaseResponse<IEnumerable<Store>>();
            try
            {
                var stores = _storeRepository.Select();
                if (stores.Count() == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.OK; 
                    return baseResponse;
                }

                baseResponse.Data = stores;
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Store>>()
                {
                    Description = $"[GetStores] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

    }
}
