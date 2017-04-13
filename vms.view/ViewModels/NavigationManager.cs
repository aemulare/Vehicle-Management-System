using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Vms.ViewModels
{
   public class NavigationManager : ViewModelBase
   {
      // The list of view models for separate navigation pages.
      private readonly IList<IPageNavigator> pageModels;
      // Current page index.
      private int pageIndex;



      public NavigationManager()
      {
         this.pageModels = new List<IPageNavigator>
         {
            new LandingController(this)
         };
         Application.Current.Navigated += HandleNavigated;
      }



      /// <summary>
      /// Current page view model.
      /// </summary>
      public IPageNavigator CurrentPageModel => this.pageModels[this.pageIndex];

      public IList<IPageNavigator> PageModels => this.pageModels;


      /// <summary>
      /// Determines whether the application can be navigated to the next page.
      /// </summary>
      internal bool CanNavigateNext => CurrentPageModel.CanNavigateNext;

      /// <summary>
      /// Determines whether the application can be navigated to the next page.
      /// </summary>
      internal bool CanNavigatePrev => this.pageIndex > 0;



      /// <summary>
      /// Navigates to the next page.
      /// </summary>
      internal void NavigateNext()
      {
         this.pageIndex++;
         RaisePropertyChanged(() => CurrentPageModel);
      }



      /// <summary>
      /// Navigates to the previous page.
      /// </summary>
      internal void NavigatePrev()
      {
         this.pageIndex--;
         RaisePropertyChanged(() => CurrentPageModel);
      }



      private void HandleNavigated(object sender, NavigationEventArgs args)
      {
         var page = (Page)args.Content;
         this.pageIndex = Convert.ToInt32(page.Tag, CultureInfo.InvariantCulture);
      }
   }
}
