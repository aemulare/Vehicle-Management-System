//=================================================================================================
// Class TripsController
// Trips controller
// Implements presentation logic for the list of trips.
//=================================================================================================
using System.Collections.Generic;
using System.Linq;
using vms;
using Vms.Views;

namespace Vms.ViewModels
{
   /// <summary>
   /// Trips controller
   /// Implements presentation logic for the list of trips.
   /// </summary>
   public class TripsController : ListController<TripView,TripViewModel,Trip>
   {
      #region Protected properties

      /// <summary>
      /// Determines whether the trip can be checked out (created).
      /// </summary>
      protected override bool CanCreateItem => ClientSecurityContext.CanCheckout;

      /// <summary>
      /// Determines whether the selected trip can be checked in (edited).
      /// </summary>
      protected override bool CanEditItem => base.CanEditItem &&
         ClientSecurityContext.CanCheckin && SelectedItem.IsInProgress;

      /// <summary>
      /// Determines whether the selected trip can be deleted.
      /// </summary>
      protected override bool CanDeleteItem => base.CanDeleteItem && ClientSecurityContext.CanDeleteTrip;

      #endregion

      #region Protected methods

      /// <summary>
      /// Creates and returns a collection of trip view model items.
      /// </summary>
      /// <returns>A collection of trip view models.</returns>
      protected override IEnumerable<TripViewModel> GetItems() =>
         from trip in AppServices.Get<Trip>() select new TripViewModel(trip);

      #endregion
   }
}
