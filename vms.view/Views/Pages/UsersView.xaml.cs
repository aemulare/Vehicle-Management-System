using System.Windows;

namespace Vms.Views
{
   public partial class UsersView
   {
      public UsersView()
      {
         InitializeComponent();
      }

        private void CreateNewUser(object sender, RoutedEventArgs e)
        {
            var window = new UsersView();
            //window.ShowDialog();
        }
    }
}
