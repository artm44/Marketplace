using Marketplace.DAL.Interfaces;
using Marketplace.Domain.Entity;
using Marketplace.Domain.Response;
using Marketplace.Domain.ViewModels.Product;
using Marketplace.Domain.ViewModels.ProductInStore;
using Marketplace.Domain.ViewModels.Store;
using Marketplace.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Marketplace.Service.Implementations
{
    public class ProductInStoreService : IProductInStoreService
    {
        private readonly IProductInStoreRepository _ProductInStoreRepository;

        private readonly IStoreService _StoreService;

        private readonly IProductService _ProductService;

        public ProductInStoreService(IProductInStoreRepository ProductInStoreRepository, IStoreService storeService, IProductService productService)
        {
            _ProductInStoreRepository = ProductInStoreRepository;
            _StoreService = storeService;
            _ProductService = productService;
        }

        public IBaseResponse<bool> AddProductsInStore(List<ProductInStoreViewModel> productInStoreViewModel)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                foreach (var product in productInStoreViewModel)
                {
                    if(product.Quantity <= 0)
                    {
                        baseResponse.Description = "Impossible quantity";
                        baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                        return baseResponse;
                    }
                }
                var store = _StoreService.GetStoreByName(productInStoreViewModel[0].Name_store);
                if (store == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                    return baseResponse;
                }

                int id_store = store.Data.Id;

                var addProducts = new List<ProductInStore>(); //Новые состояния продуктов в магазине

                var newProducts = new List<ProductInStore>(); //Новые продукты в магазине

                foreach (var item in productInStoreViewModel)
                {
                    var response = _ProductService.GetProductByName(item.Name_product);
                    if (response.StatusCode != Domain.Enum.StatusCode.OK)
                    {
                        baseResponse.Description = "User not found";
                        baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                        return baseResponse;
                    }
                    int id_product = response.Data.Id;

                    var productInStore = _ProductInStoreRepository.GetProductInStore(id_store, id_product);
                    if (productInStore == null) //Проверка был ли продукт в магазине
                    {
                        if(item.Price <=  0)
                        {
                            baseResponse.Description = "Impossible price";
                            baseResponse.StatusCode = Domain.Enum.StatusCode.InternalServerError;
                            return baseResponse;
                        }

                        newProducts.Add(new ProductInStore()
                        {
                            Id_product = id_product,
                            Id_store = id_store,
                            Quantity = item.Quantity,
                            Price = item.Price
                        });
                    } else
                    {
                        if (item.Price > 0) { productInStore.Price = item.Price; }
                        productInStore.Quantity += item.Quantity;
                        addProducts.Add(productInStore);
                    }
                }
                
                _ProductInStoreRepository.CreateRange(newProducts);
                _ProductInStoreRepository.UpdateRange(addProducts);

                baseResponse.Data = true;
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[AddProductsInStore] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                    Data = false
                };
            }
        }

        public IBaseResponse<bool> DeleteProductInStore(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var ProductInStore = _ProductInStoreRepository.Get(id);
                if (ProductInStore == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                    return baseResponse;
                }

                _ProductInStoreRepository.Delete(ProductInStore);
                baseResponse.Data = true;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteProductInStore] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                    Data = false
                };
            }
        }

        public IBaseResponse<ProductInStore> GetProductInStore(int id)
        {
            var baseResponse = new BaseResponse<ProductInStore>();
            try
            {
                var Product = _ProductInStoreRepository.Get(id);
                if (Product == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                    return baseResponse;
                }

                baseResponse.Data = Product;
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductInStore>()
                {
                    Description = $"[GetProductInStore] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<IEnumerable<ProductInStore>> GetProductInStoresByProductName(string name)
        {
            var baseResponse = new BaseResponse<IEnumerable<ProductInStore>>();
            try
            {
                var product = _ProductService.GetProductByName(name);
                if (product == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                    return baseResponse;
                }

                int id = product.Data.Id;

                var ProductsInStore = _ProductInStoreRepository.GetByProductId(id);
                if (ProductsInStore.Count() == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = ProductsInStore;
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ProductInStore>>()
                {
                    Description = $"[GetProductInStoresByProductName] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<IEnumerable<ProductInStore>> GetProductsInStoreByStoreName(string name)
        {
            var baseResponse = new BaseResponse<IEnumerable<ProductInStore>>();
            try
            {
                var store = _StoreService.GetStoreByName(name);
                if (store == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                    return baseResponse;
                }

                int id = store.Data.Id;

                var ProductsInStore = _ProductInStoreRepository.GetByStoreId(id);
                if (ProductsInStore.Count() == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                    return baseResponse;
                }

                baseResponse.Data = ProductsInStore;
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ProductInStore>>()
                {
                    Description = $"[GetProductsInStoreByStoreName] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<IEnumerable<ProductInStore>> GetProductsInStore()
        {
            var baseResponse = new BaseResponse<IEnumerable<ProductInStore>>();
            try
            {
                var ProductsInStore = _ProductInStoreRepository.Select();
                if (ProductsInStore.Count() == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = ProductsInStore;
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ProductInStore>>()
                {
                    Description = $"[GetProductsInStore] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<IEnumerable<ProductInStoreViewModel>> ProductsForAmount(string storeName, decimal amount)
        {
            var baseResponse = new BaseResponse<IEnumerable<ProductInStoreViewModel>>();
            try
            {
                var response = GetProductsInStoreByStoreName(storeName);
                if (response.StatusCode != Domain.Enum.StatusCode.OK)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                    return baseResponse;
                }

                var products = new List<ProductInStoreViewModel>();

                foreach (var product in response.Data)
                {
                    if(product.Price <=  amount && product.Quantity > 0)
                    {
                        string productName = _ProductService.GetProduct(product.Id_product).Data.Name;
                        int productQuantity = (int)(amount / product.Price) > product.Quantity 
                            ? product.Quantity : (int)(amount / product.Price);
                        products.Add(new ProductInStoreViewModel()
                        {
                            Name_store = storeName,
                            Name_product = productName,
                            Price = product.Price,
                            Quantity = productQuantity
                        });
                    }
                }

                baseResponse.Data = products;

                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<ProductInStoreViewModel>>()
                {
                    Description = $"[ProductsForAmount] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<decimal> BuyProductsInStore(string storeName, List<SetProductsViewModel> products)
        {
            var baseResponse = new BaseResponse<decimal>();
            try
            {
                //Получение списка товаров
                var response = GetProductsInStoreByStoreName(storeName);
                if (response.StatusCode != Domain.Enum.StatusCode.OK)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                    return baseResponse;
                }

                decimal sum = 0;

                var newProducts = new List<ProductInStore>(); //Новые состояния продуктов в магазине
                //Проверка доступности товаров в магазине
                foreach (var product in products)
                {
                    bool find = false;

                    var idProduct = _ProductService.GetProductByName(product.Name);
                    if (idProduct.StatusCode != Domain.Enum.StatusCode.OK) {
                        baseResponse.Description = "User not found";
                        baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                        return baseResponse;
                    }

                    foreach (var item in response.Data)
                    {
                        if(item.Id_product == idProduct.Data.Id)
                        {
                            if (item.Quantity < product.Quantity) 
                            {
                                baseResponse.Description = "Not enough products";
                                baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                                return baseResponse;
                            }
                            item.Quantity -= product.Quantity;
                            newProducts.Add(item);
                            sum += product.Quantity * item.Price;
                            find = true;
                            break;
                        }
                    }

                    if (!find) {
                        baseResponse.Description = "Not enough products";
                        baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                        return baseResponse;
                    }
                }

                int idStore = _StoreService.GetStoreByName(storeName).Data.Id;
                //Изменение значений в базе данных
                _ProductInStoreRepository.UpdateRange(newProducts);

                baseResponse.Data = sum;
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<decimal>()
                {
                    Description = $"[BuyProductsInStore] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<StorePurchasePriceViewModel> FindBestStoreForProduct(string productName)
        {
            var baseResponse = new BaseResponse<StorePurchasePriceViewModel>();
            try
            {
                var product = _ProductService.GetProductByName(productName);
                if (product == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                    return baseResponse;
                }

                int id = product.Data.Id;

                var productInStore = _ProductInStoreRepository.FindBestStoreForProduct(id);

                if (productInStore == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                    return baseResponse;
                }

                string storeName = _StoreService.GetStore(productInStore.Id_store).Data.Name;

                baseResponse.Data = new StorePurchasePriceViewModel()
                {
                    Name = storeName,
                    Price = productInStore.Price,
                };

                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<StorePurchasePriceViewModel>()
                {
                    Description = $"[FindBestStoreForProduct] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<StorePurchasePriceViewModel> FindBestStoreForProducts(List<SetProductsViewModel> products)
        {
            var baseResponse = new BaseResponse<StorePurchasePriceViewModel>();
            try
            {
                var stores = _StoreService.GetStores();
                var productsId = new List<int>(); //id запрашиваемых продуктов

                foreach (var product in products)
                {
                    var response = _ProductService.GetProductByName(product.Name);
                    if(response.StatusCode != Domain.Enum.StatusCode.OK)
                    {
                        baseResponse.Description = "User not found";
                        baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                        return baseResponse;
                    }
                    else { productsId.Add(response.Data.Id); }
                }

                string? storeName = null;
                decimal lowest_sum = -1;

                foreach (var store in stores.Data)
                {      
                    decimal sum = 0;
                    bool possible = true;
                    for (int i = 0; i < products.Count; i++)
                    {
                        var productInStore = _ProductInStoreRepository.GetProductInStore(store.Id, productsId[i]);
                        if (productInStore == null || productInStore.Quantity < products[i].Quantity) { possible = false; break; }
                        else sum += productInStore.Price * products[i].Quantity;
                    }

                    if(possible && (sum < lowest_sum || lowest_sum == -1)) { lowest_sum = sum; storeName = store.Name; }
                }

                if(storeName == null) {
                    baseResponse.Description = "Not found store";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                    return baseResponse;
                }
                

                baseResponse.Data = new StorePurchasePriceViewModel()
                {
                    Name = storeName,
                    Price = lowest_sum
                };

                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<StorePurchasePriceViewModel>()
                {
                    Description = $"[FindBestStoreForProducts] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }
    }
}
