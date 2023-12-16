using Marketplace.Domain.Entity;
using Marketplace.Domain.Response;
using Marketplace.Domain.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service.Interfaces
{
    public interface IProductService
    {
        IBaseResponse<IEnumerable<Product>> GetProducts();

        IBaseResponse<Product> GetProduct(int id);

        IBaseResponse<Product> GetProductByName(string name);

        IBaseResponse<bool> DeleteProduct(int id);

        IBaseResponse<bool> CreateProduct(ProductViewModel ProductViewModel);

    }
}
