using LibraryData;
using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryServices
{
    public class PatronServices : IPatron
    {
        private LibraryContext _context;
        public PatronServices(LibraryContext context)
        {
            _context = context;
        }

        public void Add(Patron newPatron)
        {
            _context.Add(newPatron);
            _context.SaveChanges();
        }

        public Patron Get(int Id)
        {
            return GetAll()
                .FirstOrDefault(p => p.Id == Id);
        }

        public IEnumerable<Patron> GetAll()
        {
            return _context.Patrons
                 .Include(h => h.LibraryCard)
                  .Include(p => p.HomeLibraryBranch);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int PatronId)
        {
            var cardId = Get(PatronId)
                 .LibraryCard.Id;

            return _context.CheckoutHistories
                .Include(co => co.LibraryCard)
                .Include(c => c.LibraryAsset)
                .Where(co => co.LibraryCard.Id == cardId)
                .OrderByDescending(co => co.CheckedOut);
        }

        public IEnumerable<Checkout> GetCheckouts(int PatronId)
        {
            var cardId = Get(PatronId)
                 .LibraryCard.Id;

            return _context.Checkouts
                .Include(co => co.LibraryCard)
                .Include(c => c.LibraryAsset)
                .Where(c => c.LibraryCard.Id == cardId);
        }

        public IEnumerable<Holds> GetHolds(int PatronId)
        {
            var cardId = Get(PatronId)
                 .LibraryCard.Id;
            return _context.Holds
                .Include(h => h.LibraryCard)
                .Include(h => h.LibraryAsset)
                .Where(h => h.LibraryCard.Id == cardId)
                .OrderByDescending(h => h.HoldPlaced);
        }
    }
}
