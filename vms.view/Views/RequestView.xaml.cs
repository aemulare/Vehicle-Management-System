using Vms.ViewModels;

namespace Vms.Views
{
   public partial class RequestView
   {
      public RequestView()
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
            var viewModel = (RequestViewModel)DataContext;
            viewModel.PerformValidate();
            if(!viewModel.HasErrors)
               DialogResult = true;
         };
      }
   }
}
