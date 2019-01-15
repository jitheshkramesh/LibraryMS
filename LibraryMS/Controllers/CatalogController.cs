using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryData;
using LibraryMS.Models;
using LibraryMS.Models.checkout;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMS.Controllers
{
    public class CatalogController : Controller
    {
        private ILibraryAsset _assets;
        private ICheckout _checkouts;
        public CatalogController(ILibraryAsset assets,ICheckout checkouts) {
            _assets = assets;
            _checkouts = checkouts;
        }   
        public IActionResult Index()
        {
            var assetModels = _assets.GetAll();

            var listingResult = assetModels
                .Select(result => new AssetIndexListingModel
                {
                    Id = result.Id,
                    ImageUrl = result.ImageUrl,
                    AuthorOrDirector = _assets.GetAuthororDirector(result.Id),
                    DeweyCallNumber = _assets.GetDeweyIndex(result.Id),
                    Title = result.Title,
                    Type = _assets.GetType(result.Id)
                });
            var model = new AssetIndexModel()
            {
                Assets = listingResult
            };
            return View(model);
        }

        public IActionResult Detail(int Id)
        {
            var asset = _assets.GetById(Id);
            var currentHolds = _checkouts.getCurrentHold(Id)
                .Select(a => new AssetHoldModel
                {
                    HoldPlaced = _checkouts.GetCurrentHoldPlaced(a.Id),
                    PatronName = _checkouts.GetCurrentHoldPatronName(a.Id)
                });

            var model = new AssetDetailModel
            {
                Id = Id,
                Title = asset.Title,
                Year = asset.Year,
                Cost = asset.Cost,
                Status = asset.Status.Name,
                ImageUrl = asset.ImageUrl,
                AuthorOrDirector = _assets.GetAuthororDirector(Id),
                CurrentLocation = _assets.GetCurrentLocation(Id).Name,
                DeweyCallNumber = _assets.GetDeweyIndex(Id),
                ISBN = _assets.GetIsbn(Id),
                LatestCheckout = _checkouts.GetLatestCheckout(Id),
                PatronName = _checkouts.GetCurrentCheckoutPatron(Id),
                CurrentHolds = currentHolds,
                CheckoutHistory=_checkouts.GetCheckoutHistory(Id)
            };
            return View(model);
        }

        public IActionResult Checkout(int Id)
        {
            var asset = _assets.GetById(Id);
            var model = new CheckoutModel
            {
                AssetId = Id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedout = _checkouts.IsCheckedout(Id)
            };
            return View(model);
        }

        public IActionResult CheckIn(int Id)
        {
            _checkouts.CheckInItem(Id);
            return RedirectToAction("Detail", new { id = Id });
        }

        public IActionResult Hold(int Id)
        {
            var asset = _assets.GetById(Id);
            var model = new CheckoutModel
            {
                AssetId = Id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedout = _checkouts.IsCheckedout(Id),
                HoldCount = _checkouts.getCurrentHold(Id).Count()
            };
            return View(model);
        }

        public IActionResult MarkLost(int assetId)
        {
            _checkouts.MarkLost(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        public IActionResult MarkFound(int assetId)
        {
            _checkouts.MarkFound(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceCheckout(int assetId,int libraryCardId)
        {
            _checkouts.CheckOutItem(assetId, libraryCardId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceHold(int assetId, int libraryCardId)
        {
            _checkouts.PlaceHold(assetId, libraryCardId);
            return RedirectToAction("Detail", new { id = assetId });
        }
    }
}