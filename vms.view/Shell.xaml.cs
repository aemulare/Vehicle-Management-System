using System;
using System.Windows;
using Vms.ViewModels;

namespace Vms.Views
{
   public partial class MainWindow : Window
   {
      public MainWindow()
      {
         InitializeComponent();
         Loaded += (sender, e) => SignIn();
      }




      /// <summary>
      /// Performs explicit client sign in operation.
      /// </summary>
      private void SignIn()
      {
         var signInView = new SignInView { Owner = Application.Current.MainWindow };
         if(signInView.ShowDialog() == true)
            this.lbUser.Text = ClientSecurityContext.CurrentUser.getPerson().getFullName();
         else
            Application.Current.Shutdown();
      }
   }
}
