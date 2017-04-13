using System;

namespace Vms.Views
{
   public partial class LandingView
   {
      #region Constructors

      public LandingView()
      {
         InitializeComponent();
         this.btRequests.MouseLeftButtonUp += (sender, e) => NavigateToRequestsPage();
         this.btVehicles.MouseLeftButtonUp += (sender, e) => NavigateToVehiclesPage();
         this.btUsers.MouseLeftButtonUp += (sender, e) => NavigateToUsersPage();
         this.btTrips.MouseLeftButtonUp += (sender, e) => NavigateToTripsPage();
      }

      #endregion

      #region Private methods

      private void NavigateToVehiclesPage()
      {
         NavigationService?.Navigate(new Uri("/Views/Pages/VehiclesView.xaml", UriKind.RelativeOrAbsolute));
      }



      private void NavigateToUsersPage()
      {
         NavigationService?.Navigate(new Uri("/Views/Pages/UsersView.xaml", UriKind.RelativeOrAbsolute));
      }



      private void NavigateToRequestsPage()
      {
         NavigationService?.Navigate(new Uri("/Views/Pages/RequestsView.xaml", UriKind.RelativeOrAbsolute));
      }



      private void NavigateToTripsPage()
      {
         NavigationService?.Navigate(new Uri("/Views/Pages/TripsView.xaml", UriKind.RelativeOrAbsolute));
      }

      #endregion
   }
}
