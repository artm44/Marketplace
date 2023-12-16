using Marketplace.DAL.Interfaces;
using Marketplace.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Repositories
{
    public class ProductRepositoryDB : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepositoryDB(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(Product entity)
        {
            _db.Products.Add(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Delete(Product entity)
        {
            _db.Products.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public Product Get(int id)
        {
            return _db.Products.FirstOrDefault(x => x.Id == id);
        }

        public Product GetByName(string name)
        {
            return _db.Products.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<Product> Select()
        {
            return _db.Products.ToList();
        }
    }
}
