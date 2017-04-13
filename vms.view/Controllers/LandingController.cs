namespace Vms.ViewModels
{
   public class LandingController : ViewModelBase, IPageNavigator
   {
      #region Constructors.

      // Navigation hub instance.
      private readonly NavigationManager hub;


      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="hub">Navigation hub.</param>
      internal LandingController(NavigationManager hub)
      {
         this.hub = hub;
      }

      #endregion

      #region IPageNavigator implementation

      /// <summary>
      /// 
      /// </summary>
      public bool CanNavigateNext => true;

      /// <summary>
      /// 
      /// </summary>
      public IPageNavigator NextPage => null;

      #endregion

      #region Public Properties.

      /// <summary>
      /// Page URI value.
      /// </summary>
      public string PageUri => "/Views/Pages/LandingView.xaml";

      #endregion
   }
}
