using Marketplace.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Interfaces
{
    public interface IProductInStoreRepository : IBaseRepository<ProductInStore>
    {
        IEnumerable<ProductInStore> GetByStoreId(int id);
        IEnumerable<ProductInStore> GetByProductId(int id);

        ProductInStore GetProductInStore(int storeId, int productId);
        ProductInStore FindBestStoreForProduct(int id);
        bool CreateRange(IEnumerable<ProductInStore> entities);
        bool Update(ProductInStore entity);
        bool UpdateRange(IEnumerable<ProductInStore> entities);
    }
}
