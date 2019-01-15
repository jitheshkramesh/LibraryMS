﻿using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryServices
{
    public class LibraryBranchServices : ILibraryBranch
    {
        private readonly LibraryContext _context;

        public LibraryBranchServices(LibraryContext context)
        {
            _context = context;
        }

        public void Add(LibraryBranch newBranch)
        {
            _context.Add(newBranch);
            _context.SaveChanges();
        }

        public LibraryBranch Get(int branchId)
        {
            return _context.LibraryBranchs
                .Include(b => b.Patrons)
                .Include(b => b.LibraryAssets)
                .FirstOrDefault(p => p.Id == branchId);
        }

        public IEnumerable<LibraryBranch> GetAll()
        {
            return _context.LibraryBranchs
                .Include(a => a.Patrons)
                .Include(a => a.LibraryAssets);
        }

        public int GetAssetCount(int branchId)
        {
            return Get(branchId).LibraryAssets.Count();
        }

        public IEnumerable<LibraryAsset> GetAssets(int branchId)
        {
            return _context.LibraryBranchs
                .Include(a => a.LibraryAssets)
                .First(b => b.Id == branchId)
                .LibraryAssets;
        }

        public decimal GetAssetsValue(int branchId)
        {
            var assetsValue = GetAssets(branchId)
                .Select(a => a.Cost);
            return assetsValue.Sum();
        }

        public IEnumerable<string> GetBranchHours(int branchId)
        {
            var hours = _context.BranchHours
                .Where(a => a.Branch.Id == branchId);

            var displayHours =
                DataHelpers.HumanizeBusinessHours(hours);

            return displayHours;
        }

        public int GetPatronCount(int branchId)
        {
            return Get(branchId).Patrons.Count();
        }

        public IEnumerable<Patron> GetPatrons(int branchId)
        {
            return _context.LibraryBranchs
                .Include(a => a.Patrons)
                .First(b => b.Id == branchId)
                .Patrons;
        }

        //TODO: Implement 
        public bool IsBranchOpen(int branchId)
        {
            //var currentTimeHour = DateTime.Now.Hour;
            //var currentDayOfWeek = (int)DateTime.Now.DayOfWeek + 1;
            //var hours = _context.BranchHours.Where(h => h.Branch.Id == branchId);
            //var dayHours = hours.FirstOrDefault(h => h.DayOfWeek == currentDayOfWeek);
            //return currentTimeHour < dayHours.CloseTime && currentTimeHour > dayHours.OpenTime;
            return true;
        }

    }
}

