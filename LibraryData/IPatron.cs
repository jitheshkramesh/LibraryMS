using LibraryData.Models;
using System.Collections.Generic;

namespace LibraryData
{
    public interface IPatron
    {
        Patron Get(int Id);
        IEnumerable<Patron> GetAll();
        void Add(Patron newPatron);

        IEnumerable<CheckoutHistory> GetCheckoutHistory(int PatronId);
        IEnumerable<Holds> GetHolds(int PatronId);
        IEnumerable<Checkout> GetCheckouts(int PatronId);

    }
}
