﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryData;
using LibraryMS.Models.branch;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMS.Controllers
{
    public class BranchController : Controller
    {
        private ILibraryBranch _branchService;
        public BranchController(ILibraryBranch branch)
        {
            _branchService = branch;
        }
        public IActionResult Index()
        {
            var branchModels = _branchService.GetAll()
                .Select(br => new BranchDetailModel
                {
                    Id = br.Id,
                    BranchName = br.Name,
                    NumberOfAssets = _branchService.GetAssetCount(br.Id),
                    NumberOfPatrons = _branchService.GetPatronCount(br.Id),
                    IsOpen = _branchService.IsBranchOpen(br.Id)
                }).ToList();

            var model = new BranchIndexModel
            {
                Branches = branchModels
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var branch = _branchService.Get(id);
            var model = new BranchDetailModel
            {
                BranchName = branch.Name,
                Description = branch.Description,
                Address = branch.Address,
                Telephone = branch.Telephone,
                BranchOpenedDate = branch.OpenDate.ToString("yyyy-MM-dd"),
                NumberOfPatrons = _branchService.GetPatronCount(id),
                NumberOfAssets = _branchService.GetAssetCount(id),
                TotalAssetValue = _branchService.GetAssetsValue(id),
                ImageUrl = branch.ImageUrl,
                HoursOpen = _branchService.GetBranchHours(id)
            };

            return View(model);
        }
    }
}