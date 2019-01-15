using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryData;
using LibraryData.Models;
using LibraryMS.Models.patron;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMS.Controllers
{
    public class PatronController : Controller
    {
        private IPatron _patron;
        public PatronController(IPatron patron)
        {
            _patron = patron;
        }

        public IActionResult Index()
        {
            var allPatrons = _patron.GetAll();
            var patronModels = allPatrons.Select(p => new PatronDetalModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                LibraryCardId = p.LibraryCard.Id,
                OverDueFees = p.LibraryCard.Fees,
                HomeLibraryBranch=p.HomeLibraryBranch.Name,
            }).ToList();

            var model = new PatronIndexModel()
            {
                Patrons = patronModels
            };

            return View(model);
        }

        public IActionResult Detail(int Id) {
            var patron = _patron.Get(Id);

            var model = new PatronDetalModel
            {
                LastName = patron.LastName,
                FirstName = patron.FirstName,
                Address = patron.Address,
                HomeLibraryBranch = patron.HomeLibraryBranch.Name,
                MemberSince = patron.LibraryCard.Created,
                OverDueFees = patron.LibraryCard.Fees,
                LibraryCardId = patron.LibraryCard.Id,
                Telephone = patron.TelephoneNumber,
                AssetsCheckout = _patron.GetCheckouts(Id).ToList() ?? new List<Checkout>(),
                AssetsCheckoutHistory = _patron.GetCheckoutHistory(Id),
                Holds = _patron.GetHolds(Id)
            };
            return View(model);
        }
    }
}