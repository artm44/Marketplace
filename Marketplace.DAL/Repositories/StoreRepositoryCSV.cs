using CsvHelper;
using CsvHelper.Configuration;
using Marketplace.DAL.Interfaces;
using Marketplace.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Marketplace.DAL.Repositories
{
    public class StoreRepositoryCSV : IStoreRepository
    {
        private readonly string _filePath = "D:\\Учёба\\ИТМО\\ООП\\Marketplace\\Marketplace\\DataBaseCSV\\Stores.csv";

        private readonly CsvConfiguration _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
        };

        public List<Store> _stores;
        public StoreRepositoryCSV() {
            try
            {
                using (var reader = new StreamReader(_filePath))
                using (var csv = new CsvReader(reader, _csvConfiguration))
                {
                    _stores = csv.GetRecords<Store>().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading stores: {ex.Message}");
            }
        }

        public void SaveStores()
        {
            try
            {
                using (var writer = new StreamWriter(_filePath))
                using (var csv = new CsvWriter(writer, _csvConfiguration))
                {
                    csv.WriteRecords(_stores);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving stores: {ex.Message}");
            }
        }
        public bool Create(Store entity)
        {
            entity.Id = _stores.Count > 0 ? _stores.Max(s => s.Id) + 1 : 1;
            _stores.Add(entity);
            SaveStores();
            return true;
        }

        public bool Delete(Store entity)
        {
            _stores.Remove(entity);
            SaveStores();
            return true;
        }

        public Store Get(int id)
        {
            return _stores.FirstOrDefault(x => x.Id == id);
        }

        public Store GetByName(string name)
        {
            return _stores.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<Store> Select()
        {
            return _stores;
        }
    }
}
