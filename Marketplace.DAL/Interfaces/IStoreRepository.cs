using Marketplace.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.DAL.Interfaces
{
    public interface IStoreRepository : IBaseRepository<Store>
    {
        Store GetByName(string name);
    }
}
