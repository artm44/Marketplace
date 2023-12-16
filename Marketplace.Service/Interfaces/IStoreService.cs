using Marketplace.Domain.Entity;
using Marketplace.Domain.Response;
using Marketplace.Domain.ViewModels.ProductInStore;
using Marketplace.Domain.ViewModels.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service.Interfaces
{
    public interface IStoreService
    {
        IBaseResponse<IEnumerable<Store>> GetStores();

        IBaseResponse<Store> GetStore(int id);

        IBaseResponse<Store> GetStoreByName(string name);

        IBaseResponse<bool> DeleteStore(int id);

        IBaseResponse<bool> CreateStore(StoreViewModel storeViewModel);

    }
}
