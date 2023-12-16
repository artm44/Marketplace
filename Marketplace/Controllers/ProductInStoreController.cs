using Marketplace.Domain.ViewModels.Product;
using Marketplace.Domain.ViewModels.ProductInStore;
using Marketplace.Service.Implementations;
using Marketplace.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Marketplace.Controllers
{
    public class ProductInStoreController : Controller
    {
        private readonly IProductInStoreService _productInStoreService;

        public ProductInStoreController(IProductInStoreService productInStoreService)
        {
            _productInStoreService = productInStoreService;
        }

        [HttpPost]
        public ActionResult AddProductsInStore(List<ProductInStoreViewModel> productInStoreViewModel)
        {
            var response = _productInStoreService.AddProductsInStore(productInStoreViewModel);
            ViewBag.Result = response.Data;
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View();
            }
            return RedirectToAction("Error");
        }

        public ActionResult AddProductsInStore()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BuyProductsInStore(string storeName, List<SetProductsViewModel> products)
        {
            var response = _productInStoreService.BuyProductsInStore(storeName, products);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                ViewBag.Status = true;
                ViewBag.Result = response.Data;
                return View("BuyProductsInStore");
            }
            return RedirectToAction("Error");
        }

        public ActionResult BuyProductsInStore()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetProductsInStoreByName(string name)
        {
            var response = _productInStoreService.GetProductsInStoreByStoreName(name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public ActionResult GetProductInStoresByName(string name)
        {
            var response = _productInStoreService.GetProductInStoresByProductName(name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        public ActionResult BestStoreForProduct() { return View(); }

        public ActionResult GetBestStoreForProduct() { return View(); }

        [HttpPost]
        public ActionResult GetBestStoreForProduct(string name)
        {
            var response = _productInStoreService.FindBestStoreForProduct(name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                ViewBag.Status = true;
                ViewBag.Store = response.Data.Name;
                ViewBag.Price = response.Data.Price;
                return View();
            }
            return RedirectToAction("Error");
        }

        public ActionResult GetBestStoreForProducts() { return View(); }

        [HttpPost]
        public ActionResult GetBestStoreForProducts(List<SetProductsViewModel> products)
        {
            var response = _productInStoreService.FindBestStoreForProducts(products);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                ViewBag.Status = true;
                ViewBag.Store = response.Data.Name;
                ViewBag.Price = response.Data.Price;
                return View();
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public ActionResult GetProductsForAmount(string nameStore, decimal amount )
        {
            var response = _productInStoreService.ProductsForAmount(nameStore, amount);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                ViewBag.Status = true;
                ViewBag.Data = response.Data;
                return View();
            }
            return RedirectToAction("Error");
        }

        public ActionResult GetProductsForAmount() { return View(); }
    }
}
