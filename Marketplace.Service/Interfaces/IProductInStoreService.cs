using Marketplace.Domain.Entity;
using Marketplace.Domain.Response;
using Marketplace.Domain.ViewModels.Product;
using Marketplace.Domain.ViewModels.ProductInStore;
using Marketplace.Domain.ViewModels.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service.Interfaces
{
    public interface IProductInStoreService
    {
        IBaseResponse<IEnumerable<ProductInStore>> GetProductsInStore();

        IBaseResponse<ProductInStore> GetProductInStore(int id);

        IBaseResponse<IEnumerable<ProductInStore>> GetProductsInStoreByStoreName(string name);

        IBaseResponse<IEnumerable<ProductInStore>> GetProductInStoresByProductName(string name);

        IBaseResponse<bool> DeleteProductInStore(int id);

        IBaseResponse<bool> AddProductsInStore(List<ProductInStoreViewModel> productInStoreViewModel);

        IBaseResponse<StorePurchasePriceViewModel> FindBestStoreForProduct(string productName);

        IBaseResponse<StorePurchasePriceViewModel> FindBestStoreForProducts(List<SetProductsViewModel> products);

        IBaseResponse<IEnumerable<ProductInStoreViewModel>> ProductsForAmount(string storeName, decimal amount);

        IBaseResponse<decimal> BuyProductsInStore(string storeName, List<SetProductsViewModel> products);


    }
}
