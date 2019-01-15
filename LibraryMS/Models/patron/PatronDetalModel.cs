using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryData.Models;

namespace LibraryMS.Models.patron
{
    public class PatronDetalModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }             

        public string FullName {
            get {
                return FirstName + " " + LastName;
            }
        }

        public int LibraryCardId { get; set; }
        public string Address { get; set; }
        public DateTime MemberSince { get; set; }
        public string Telephone { get; set; }
        public string HomeLibraryBranch { get; set; }
        public decimal OverDueFees { get; set; }
        public IEnumerable<Checkout> AssetsCheckout { get; set; }
        public IEnumerable<CheckoutHistory> AssetsCheckoutHistory { get; set; }
        public IEnumerable<Holds> Holds { get; set; }
    }
}
