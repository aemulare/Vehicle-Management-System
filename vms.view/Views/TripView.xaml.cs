using Vms.ViewModels;

namespace Vms.Views
{
   public partial class TripView
   {
      public TripView()
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
            var viewModel = (TripViewModel)DataContext;
            viewModel.PerformValidate();
            if(!viewModel.HasErrors)
               DialogResult = true;
         };
      }
   }
}
