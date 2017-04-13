//=================================================================================================
// Class VehiclesController
// Vehicles controller
// Implements presentation logic for the list of vehicles.
//=================================================================================================
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using vms;
using Vms.Views;

namespace Vms.ViewModels
{
   /// <summary>
   /// Vehicles controller
   /// Implements presentation logic for the list of vehicles.
   /// </summary>
   public class VehiclesController : ListController<VehiclePropertiesView,VehicleViewModel,OwnedVehicle>
   {
      #region Constructors

      /// <summary>
      /// Default constructor.
      /// </summary>
      public VehiclesController()
      {
         NewRequestCommand = new DelegateCommand(args => CreateRequest(), args => CanCreateRequest);
      }

      #endregion

      #region WPF commands

      /// <summary>
      /// Create a new request for the selected vehicle.
      /// </summary>
      public ICommand NewRequestCommand { get; private set; }

      #endregion

      #region Protected properties.

      /// <summary>
      /// Determines whether a new vehicle can be registered.
      /// </summary>
      protected override bool CanCreateItem => base.CanCreateItem && ClientSecurityContext.CanManageVehicles;

      /// <summary>
      /// Determines whether the selected vehicle can be edited.
      /// </summary>
      protected override bool CanEditItem => base.CanEditItem && ClientSecurityContext.CanManageVehicles;

      /// <summary>
      /// Determines whether the selected trip can be deleted.
      /// </summary>
      protected override bool CanDeleteItem => base.CanDeleteItem && ClientSecurityContext.CanDeleteVehicles;

      #endregion

      #region Protected methods

      /// <summary>
      /// Creates and returns a collection of vehicle view model items.
      /// </summary>
      /// <returns>A collection of vehicle view models.</returns>
      protected override IEnumerable<VehicleViewModel> GetItems() => 
         from vehicle in AppServices.Get<OwnedVehicle>() select new VehicleViewModel(vehicle);

      #endregion

      #region Private Properties

      /// <summary>
      /// Determines whether the new request can be created for the selected vehicle.
      /// </summary>
      private bool CanCreateRequest => SelectedItem != null;

      #endregion

      #region Private methods

      /// <summary>
      /// Creates a new request.
      /// </summary>
      private void CreateRequest()
      {
         if (!SelectedItem.IsAvailable)
            MessageBox.Show("Selected vehicle is not available for requests.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
         else
         {
            var request = new RequestViewModel { VinNumber = SelectedItem.VinNumber };
            var view = new RequestView
            {
               Owner = Application.Current.MainWindow,
               DataContext = request
            };
            view.ShowDialog();
         }
      }

      #endregion
   }
}
