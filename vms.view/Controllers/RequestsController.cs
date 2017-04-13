//=================================================================================================
// Class RequestsController
// Requests controller
// Implements presentation logic for the list of requests.
//=================================================================================================
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using vms;
using Vms.Views;

namespace Vms.ViewModels
{
   /// <summary>
   /// Requests controller
   /// Implements presentation logic for the list of requests.
   /// </summary>
   public class RequestsController : ListController<RequestView,RequestViewModel,Request>
   {
      #region Constructors

      /// <summary>
      /// Default constructor.
      /// </summary>
      public RequestsController()
      {
         ApproveRequestCommand = new DelegateCommand(args => SelectedItem.ApproveRequest(),
            args => SelectedItem != null && SelectedItem.CanApproveDeclineRequest);
         DeclineRequestCommand = new DelegateCommand(args => SelectedItem.DeclineRequest(),
            args => SelectedItem != null && SelectedItem.CanApproveDeclineRequest);
      }

      #endregion

      #region WPF commands

      /// <summary>
      /// Approves a request.
      /// </summary>
      public ICommand ApproveRequestCommand { get; private set; }

      /// <summary>
      /// Declines a request.
      /// </summary>
      public ICommand DeclineRequestCommand { get; private set; }

      #endregion

      #region Public properties

      /// <summary>
      /// Determines whether a current user can approve requests.
      /// </summary>
      public bool AllowApproveDecline => ClientSecurityContext.CurrentUser.getRole().canApproveRequests();

      #endregion

      #region Protected properties

      /// <summary>
      /// Determines whether the selected request can be deleted.
      /// </summary>
      protected override bool CanDeleteItem => base.CanDeleteItem && SelectedItem.Status == Request.RequestStatus.INITIAL
         && (ClientSecurityContext.CurrentUser.getRole().canDeleteRequests() || SelectedItem.IsBelongToCurrentUser);

      /// <summary>
      /// Determines whether the selected request can be edited.
      /// </summary>
      protected override bool CanEditItem => base.CanEditItem && SelectedItem.Status == Request.RequestStatus.INITIAL;

      #endregion

      #region Protected methods

      /// <summary>
      /// Creates and returns a collection of request view model items.
      /// </summary>
      /// <returns>A collection of request view models.</returns>
      protected override IEnumerable<RequestViewModel> GetItems()
      {
         var role = ClientSecurityContext.CurrentUser.getRole();
         var viewAll = role.canApproveRequests() || role.canCheckInVehicles() || role.canManageUsers();
         return from request in AppServices.Get<Request>()
            where viewAll || request.getRequestor().getId() == ClientSecurityContext.CurrentUser.getId()
            select new RequestViewModel(request);
      }

      #endregion
   }
}
