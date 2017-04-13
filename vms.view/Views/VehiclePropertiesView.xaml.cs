using Vms.ViewModels;

namespace Vms.Views
{
   public partial class VehiclePropertiesView
   {
      public VehiclePropertiesView()
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
            var viewModel = (VehicleViewModel)DataContext;
            viewModel.PerformValidate();
            if(!viewModel.HasErrors)
               DialogResult = true;
         };
      }
   }
}
