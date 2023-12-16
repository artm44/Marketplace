using CsvHelper.Configuration;
using CsvHelper;
using Marketplace.DAL.Interfaces;
using Marketplace.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Repositories
{
    public class ProductInStoreRepositoryCSV : IProductInStoreRepository
    {
        private readonly string _filePath = "D:\\Учёба\\ИТМО\\ООП\\Marketplace\\Marketplace\\DataBaseCSV\\Products_Store.csv";

        private readonly CsvConfiguration _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
        };

        public List<ProductInStore> _productInStore;
        public ProductInStoreRepositoryCSV()
        {
            try
            {
                using (var reader = new StreamReader(_filePath))
                using (var csv = new CsvReader(reader, _csvConfiguration))
                {
                    _productInStore = csv.GetRecords<ProductInStore>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading products in stores: {ex.Message}");
            }
        }

        public void SaveProductsInStores()
        {
            try
            {
                using (var writer = new StreamWriter(_filePath))
                using (var csv = new CsvWriter(writer, _csvConfiguration))
                {
                    csv.WriteRecords(_productInStore);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving products in stores: {ex.Message}");
            }
        }
        public bool Create(ProductInStore entity)
        {
            entity.Id = _productInStore.Count > 0 ? _productInStore.Max(s => s.Id) + 1 : 1;
            _productInStore.Add(entity);
            SaveProductsInStores();
            return true;
        }

        public bool CreateRange(IEnumerable<ProductInStore> entities)
        {
            int id = _productInStore.Count > 0 ? _productInStore.Max(s => s.Id) + 1 : 1;
            foreach (var entity in entities)
            {
                entity.Id = id;
                id++;
            }
            _productInStore.AddRange(entities);
            SaveProductsInStores();
            return true;
        }

        public bool Delete(ProductInStore entity)
        {
            _productInStore.Remove(entity);
            SaveProductsInStores();
            return true;
        }

        public ProductInStore FindBestStoreForProduct(int id)
        {
            return _productInStore.Where(x => x.Id_product == id).OrderBy(x => x.Price).FirstOrDefault();
        }

        public ProductInStore Get(int id)
        {
            return _productInStore.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ProductInStore> GetByProductId(int id)
        {
            return _productInStore.Where(x => x.Id_product == id).ToList();
        }

        public IEnumerable<ProductInStore> GetByStoreId(int id)
        {
            return _productInStore.Where(x => x.Id_store == id).ToList();
        }

        public ProductInStore GetProductInStore(int storeId, int productId)
        {
            return _productInStore.FirstOrDefault(x => x.Id_store == storeId && x.Id_product == productId);
        }

        public IEnumerable<ProductInStore> Select()
        {
            return _productInStore.ToList();
        }

        public bool Update(ProductInStore entity)
        {
            //_productInStore.Update(entity);
            SaveProductsInStores();

            return true;
        }

        public bool UpdateRange(IEnumerable<ProductInStore> entities)
        {
            //_productInStore.UpdateRange(entities);
            SaveProductsInStores();

            return true;
        }
    }
}
