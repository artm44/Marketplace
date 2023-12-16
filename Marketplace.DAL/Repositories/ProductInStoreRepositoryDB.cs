using Marketplace.DAL.Interfaces;
using Marketplace.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Repositories
{
    public class ProductInStoreRepositoryDB : IProductInStoreRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductInStoreRepositoryDB(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(ProductInStore entity)
        {
            _db.Products_Stores.Add(entity);
            _db.SaveChanges();

            return true;
        }

        public bool CreateRange(IEnumerable<ProductInStore> entities)
        {
            _db.Products_Stores.AddRange(entities);
            _db.SaveChanges();

            return true;
        }

        public bool Delete(ProductInStore entity)
        {
            _db.Products_Stores.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public ProductInStore FindBestStoreForProduct(int id)
        {
            return _db.Products_Stores.Where(x => x.Id_product == id).OrderBy(x => x.Price).FirstOrDefault();
        }

        public ProductInStore Get(int id)
        {
            return _db.Products_Stores.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ProductInStore> GetByProductId(int id)
        {
            return _db.Products_Stores.Where(x => x.Id_product == id).ToList();
        }

        public IEnumerable<ProductInStore> GetByStoreId(int id)
        {
            return _db.Products_Stores.Where(x => x.Id_store == id).ToList();
        }

        public ProductInStore GetProductInStore(int storeId, int productId)
        {
            return _db.Products_Stores.FirstOrDefault(x => x.Id_store == storeId && x.Id_product == productId);
        }

        public IEnumerable<ProductInStore> Select()
        {
            return _db.Products_Stores.ToList();
        }

        public bool Update(ProductInStore entity)
        {
            _db.Products_Stores.Update(entity);
            _db.SaveChanges();

            return true;
        }

        public bool UpdateRange(IEnumerable<ProductInStore> entities)
        {
            _db.Products_Stores.UpdateRange(entities);
            _db.SaveChanges();

            return true;
        }
    }
}
