using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData
{
   public interface ICheckout
    {
        void Add(Checkout newCheckout);
        IEnumerable<Checkout> GetAll();
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int Id);
        IEnumerable<Holds> getCurrentHold(int id);

        Checkout GetById(int checkoutId);
        Checkout GetLatestCheckout(int checkoutId);
        string GetCurrentCheckoutPatron(int assetId);
        string GetCurrentHoldPatronName(int id);
        DateTime GetCurrentHoldPlaced(int id);
        bool IsCheckedout(int id);

        void CheckOutItem(int assetId, int LibraryCardId);
        void CheckInItem(int assetId);
        void PlaceHold(int assetId, int LibraryCardId);
        void MarkLost(int assetId);
        void MarkFound(int assetId);

        
        

             
    }
}
