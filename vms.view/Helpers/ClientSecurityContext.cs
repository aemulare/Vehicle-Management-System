//=================================================================================================
// Class ClientSecurityContext
// Client security context
// Performs security related operations and provides entitlements values for the current user.
//=================================================================================================
using vms;

namespace Vms.ViewModels
{
   /// <summary>
   /// Client security context
   /// Performs security related operations and provides entitlements values for the current user.
   /// </summary>
   public static class ClientSecurityContext
   {
      #region Public properties

      /// <summary>
      /// The user who currently is being logged in.
      /// </summary>
      public static UserAccount CurrentUser { get; private set; }

      /// <summary>
      /// Determines whether the current user can manage (create or edit) other user profiles.
      /// </summary>
      public static bool CanManageUsers => CurrentUser.getRole().canManageUsers();

      /// <summary>
      /// Determines whether the current user can delete user profiles.
      /// </summary>
      public static bool CanDeleteUsers => CurrentUser.getRole().canDeleteUsers();

      /// <summary>
      /// Determines whether the current user can manage (create or delete) vehicles.
      /// </summary>
      public static bool CanManageVehicles => CurrentUser.getRole().canManageVehicles();

      /// <summary>
      /// Determines whether the current user can delete vehicles.
      /// </summary>
      public static bool CanDeleteVehicles => CurrentUser.getRole().canDeleteVehicles();

      /// <summary>
      /// Determines whether the current user can perform checkout trip operation.
      /// </summary>
      public static bool CanCheckout => CanCheckin;

      /// <summary>
      /// Determines whether the current user can perform checkin trip operation.
      /// </summary>
      public static bool CanCheckin => CurrentUser.getRole().canCheckInVehicles();

      /// <summary>
      /// Determines whether the current user can delete the selected trip.
      /// </summary>
      public static bool CanDeleteTrip => CurrentUser.getRole().canDeleteRequests();

      #endregion

      #region Public methods

      /// <summary>
      /// Performs sign in operation based on provided user credentials.
      /// </summary>
      /// <param name="login">User login.</param>
      /// <param name="password">Password.</param>
      /// <returns>True, if the user with specified credential exists and authenticated;
      /// otherwise, false.</returns>
      public static bool SignIn(string login, string password)
      {
         CurrentUser = AppServices.GetUser(login);
         return CurrentUser != null && !CurrentUser.isLocked() && CurrentUser.authenticate(password);
      }

      #endregion
   }
}
