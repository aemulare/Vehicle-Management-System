namespace Vms.Views
{
   public partial class NewPassengerView
   {
      public NewPassengerView()
      {
         InitializeComponent();
         this.btOk.Click += (sender, e) => DialogResult = true;
      }
   }
}
