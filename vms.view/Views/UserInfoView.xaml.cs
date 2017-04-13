using Vms.ViewModels;

namespace Vms.Views
{
   public partial class UserInfoView
   {
      public UserInfoView()
      {
         InitializeComponent();
         InitializeEvents();
      }



      /// <summary>
      /// Initializes view events.
      /// </summary>
      private void InitializeEvents()
      {
         this.btSave.Click += (sender, e) =>
         {
            var viewModel = (UserViewModel)DataContext;
            viewModel.PerformValidate();
            if(!viewModel.HasErrors)
               DialogResult = true;
         };
      }
   }
}
