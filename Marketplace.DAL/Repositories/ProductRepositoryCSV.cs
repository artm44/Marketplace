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
    public class ProductRepositoryCSV : IProductRepository
    {
        private readonly string _filePath = "D:\\Учёба\\ИТМО\\ООП\\Marketplace\\Marketplace\\DataBaseCSV\\Products.csv";

        private readonly CsvConfiguration _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
        };

        public List<Product> _products;
        public ProductRepositoryCSV()
        {
            try
            {
                using (var reader = new StreamReader(_filePath))
                using (var csv = new CsvReader(reader, _csvConfiguration))
                {
                    _products = csv.GetRecords<Product>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading stores: {ex.Message}");
            }
        }

        public void SaveProducts()
        {
            try
            {
                using (var writer = new StreamWriter(_filePath))
                using (var csv = new CsvWriter(writer, _csvConfiguration))
                {
                    csv.WriteRecords(_products);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving products: {ex.Message}");
            }
        }
        public bool Create(Product entity)
        {
            entity.Id = _products.Count > 0 ? _products.Max(s => s.Id) + 1 : 1;
            _products.Add(entity);
            SaveProducts();
            return true;
        }

        public bool Delete(Product entity)
        {
            _products.Remove(entity);
            SaveProducts();
            return true;
        }

        public Product Get(int id)
        {
            return _products.FirstOrDefault(x => x.Id == id);
        }

        public Product GetByName(string name)
        {
            return _products.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<Product> Select()
        {
            return _products;
        }
    }
}
