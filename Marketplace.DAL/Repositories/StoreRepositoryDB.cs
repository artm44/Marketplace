using Marketplace.DAL.Interfaces;
using Marketplace.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Repositories
{
    public class StoreRepositoryDB : IStoreRepository
    {
        private readonly ApplicationDbContext _db;

        public StoreRepositoryDB(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(Store entity)
        {
            _db.Stores.Add(entity);
            _db.SaveChanges();

            return true;
        }

        public bool Delete(Store entity)
        {
            _db.Stores.Remove(entity);
            _db.SaveChanges();

            return true;
        }

        public Store Get(int id)
        {
            return _db.Stores.FirstOrDefault(x => x.Id == id);
        }

        public Store GetByName(string name)
        {
            return _db.Stores.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<Store> Select()
        {
            return _db.Stores.ToList();
        }
    }
}
