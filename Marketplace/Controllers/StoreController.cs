using Marketplace.DAL.Interfaces;
using Marketplace.Service.Interfaces;
using Marketplace.Domain.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Marketplace.Domain.ViewModels.Store;
using Marketplace.Service.Implementations;
using Marketplace.Domain.ViewModels.ProductInStore;


namespace Marketplace.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        // GET: StoreController
        [HttpGet]
        public ActionResult GetStores()
        {
            var response = _storeService.GetStores();
            if(response.StatusCode == Domain.Enum.StatusCode.OK) {
                return View(response.Data.ToList());
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public ActionResult GetStore(int id)
        {
            var response = _storeService.GetStore(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public ActionResult GetStoreByName(string name)
        {
            var response = _storeService.GetStoreByName(name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpDelete]
        public ActionResult DeleteStore(int id)
        {
            var response = _storeService.DeleteStore(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetStores");
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public ActionResult CreateStore()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStore(StoreViewModel model)
        {
            var response = _storeService.CreateStore(model);
            ViewBag.Result = response.Data;
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View();
            }
            return RedirectToAction("Error");
        }
    }
}
