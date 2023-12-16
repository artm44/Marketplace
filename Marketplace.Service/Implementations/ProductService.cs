using Marketplace.DAL.Interfaces;
using Marketplace.Domain.Entity;
using Marketplace.Domain.Response;
using Marketplace.Domain.ViewModels.Product;
using Marketplace.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _ProductRepository;

        public ProductService(IProductRepository ProductRepository)
        {
            _ProductRepository = ProductRepository;
        }

        public IBaseResponse<bool> CreateProduct(ProductViewModel ProductViewModel)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var Product = new Product()
                {
                    Name = ProductViewModel.Name
                };

                _ProductRepository.Create(Product);
                baseResponse.Data = true;
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateProduct] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                    Data = false
                };
            }
        }

        public IBaseResponse<bool> DeleteProduct(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var Product = _ProductRepository.Get(id);
                if (Product == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.NotFound;
                    return baseResponse;
                }

                _ProductRepository.Delete(Product);
                baseResponse.Data = true;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteProduct] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError,
                    Data = false
                };
            }
        }

        public IBaseResponse<Product> GetProduct(int id)
        {
            var baseResponse = new BaseResponse<Product>();
            try
            {
                var Product = _ProductRepository.Get(id);
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
                return new BaseResponse<Product>()
                {
                    Description = $"[GetProduct] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<Product> GetProductByName(string name)
        {
            var baseResponse = new BaseResponse<Product>();
            try
            {
                var Product = _ProductRepository.GetByName(name);
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
                return new BaseResponse<Product>()
                {
                    Description = $"[GetProductByName] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<IEnumerable<Product>> GetProducts()
        {
            var baseResponse = new BaseResponse<IEnumerable<Product>>();
            try
            {
                var Products = _ProductRepository.Select();
                if (Products.Count() == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = Domain.Enum.StatusCode.OK;
                    return baseResponse;
                }

                baseResponse.Data = Products;
                baseResponse.StatusCode = Domain.Enum.StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Product>>()
                {
                    Description = $"[GetProducts] : {ex.Message}",
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }

    }
}
