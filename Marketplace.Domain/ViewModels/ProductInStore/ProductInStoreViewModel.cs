using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Domain.ViewModels.ProductInStore
{
    public class ProductInStoreViewModel
    {
        public string Name_store { get; set; }
        public string Name_product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
