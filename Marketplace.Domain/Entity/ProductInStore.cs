using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.Entity
{
    public class ProductInStore
    {
        public int Id { get; set; }
        public int Id_store { get; set; }
        public int Id_product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
