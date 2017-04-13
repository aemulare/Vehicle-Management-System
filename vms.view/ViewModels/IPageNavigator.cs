namespace Vms.ViewModels
{
   /// <summary>
   /// 
   /// </summary>
   public interface IPageNavigator
   {
      /// <summary>
      /// 
      /// </summary>
      bool CanNavigateNext { get; }

      /// <summary>
      /// 
      /// </summary>
      IPageNavigator NextPage { get; }
   }
}
