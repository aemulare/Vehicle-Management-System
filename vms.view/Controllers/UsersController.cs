//=================================================================================================
// Class UsersController
// Users controller.
// Represents controller for users and their personal info.
//=================================================================================================
using System.Collections.Generic;
using System.Linq;
using vms;
using Vms.Views;

namespace Vms.ViewModels
{
   /// <summary>
   /// Users controller.
   /// Represents controller for users and their personal info.
   /// </summary>
   public class UsersController : ListController<UserInfoView,UserViewModel,UserAccount>
   {
      #region Protected properties

      /// <summary>
      /// Determines whether a new user profile can be created.
      /// </summary>
      protected override bool CanCreateItem => base.CanCreateItem && ClientSecurityContext.CanManageUsers;

      /// <summary>
      /// Determines whether the selected user can be edited.
      /// </summary>
      protected override bool CanEditItem => base.CanEditItem && ClientSecurityContext.CanManageUsers;

      /// <summary>
      /// Determines whether the selected user can be deleted.
      /// </summary>
      protected override bool CanDeleteItem => base.CanDeleteItem && ClientSecurityContext.CanDeleteUsers;

      #endregion

      #region Protected methods

      /// <summary>
      /// Creates and returns a collection of user view model items.
      /// </summary>
      /// <returns>A collection of user view models.</returns>
      protected override IEnumerable<UserViewModel> GetItems()
      {
         var role = ClientSecurityContext.CurrentUser.getRole();
         var viewAll = role.canManageUsers();
         return from user in AppServices.Get<UserAccount>()
            where viewAll || user.getId() == ClientSecurityContext.CurrentUser.getId()
            select new UserViewModel(user);
      }

      #endregion
   }
}
