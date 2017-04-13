//=================================================================================================
// Class SignInView.
// Sign in view.
// Represents the WPF popup window used to enter user credentials for sign in operation.
//=================================================================================================
using System.Windows;
using Vms.ViewModels;

namespace Vms.Views
{
   /// <summary>
   /// Sign in view.
   /// </summary>
   /// <remarks>
   /// Represents the WPF popup window used to enter user credentials for sign in operation.
   /// </remarks>
   public partial class SignInView
   {
      #region Private Members.

      // The number of sign in attempts.
      private int attempts;

      #endregion

      #region Constructors.

      /// <summary>
      /// Default constructor.
      /// </summary>
      public SignInView()
      {
         InitializeComponent();

         Loaded += (sender, e) => this.edLogin.Focus();
         this.btCancel.Click += (sender, e) => DialogResult = false;
         this.btLogin.Click += (sender, e) => SignIn();
         this.edLogin.TextChanged += (sender, e) => CheckSignInAbility();
         this.edPwd.PasswordChanged += (sender, e) => CheckSignInAbility();
         CheckSignInAbility();
      }

      #endregion

      #region Private methods

      /// <summary>
      /// Performs sign in operation.
      /// </summary>
      private void SignIn()
      {
         if(ClientSecurityContext.SignIn(this.edLogin.Text.Trim(), this.edPwd.Password.Trim()))
         {
            DialogResult = true;
            return;
         }
         this.edPwd.Password = "";
         MessageBox.Show(this, "Authentication failed. Invalid user credentials or access is denied.",
            "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
         this.edPwd.Focus();
         if(++attempts >= 3)
            DialogResult = false;
      }



      /// <summary>
      /// Checks the ability of sign in operation.
      /// </summary>
      private void CheckSignInAbility()
      {
         this.btLogin.IsEnabled = !string.IsNullOrWhiteSpace(this.edLogin.Text) && !string.IsNullOrWhiteSpace(this.edPwd.Password);
      }

      #endregion
   }
}
