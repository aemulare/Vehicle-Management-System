//=================================================================================================
// Class ListController
// List controller
// Represents an abstract base class for all list controllers (view models).
//=================================================================================================
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using vms;

namespace Vms.ViewModels
{
   /// <summary>
   /// List controller
   /// Represents an abstract base class for all list controllers (view models).
   /// </summary>
   /// <typeparam name="TView">The actual view type.</typeparam>
   /// <typeparam name="TViewModel">The actual view model type for the list item.</typeparam>
   /// <typeparam name="T">The actual model type.</typeparam>
   public abstract class ListController<TView,TViewModel,T> : ViewModelBase
      where TViewModel : ItemViewModel<T>, new()
      where TView : Window, new()
      where T : PersistentEntity
   {
      #region Constructors.

      /// <summary>
      /// Default constructor.
      /// </summary>
      protected ListController()
      {
         Initialize();
      }

      #endregion

      #region Commands

      /// <summary>
      /// Creates a new item.
      /// </summary>
      public ICommand CreateItemCommand { get; private set; }

      /// <summary>
      /// Edit the selected item.
      /// </summary>
      public ICommand EditItemCommand { get; private set; }

      /// <summary>
      /// Views the selected item.
      /// </summary>
      public ICommand ViewItemCommand { get; private set; }

      /// <summary>
      /// Deletes the selected item.
      /// </summary>
      public ICommand DeleteItemCommand { get; private set; }

      #endregion

      #region Public properties

      /// <summary>
      /// A collection of available list items.
      /// </summary>
      public ObservableCollection<TViewModel> Items { get; private set; }

      /// <summary>
      /// Selected item from the list.
      /// </summary>
      public TViewModel SelectedItem { get; set; }

      #endregion

      #region Protected properties

      /// <summary>
      /// Determines whether a new item can be created.
      /// </summary>
      protected virtual bool CanCreateItem => true;

      /// <summary>
      /// Determines whether the selected item can be edited.
      /// </summary>
      protected virtual bool CanEditItem => SelectedItem != null;

      /// <summary>
      /// Determines whether the selected item can be viewed.
      /// </summary>
      protected virtual bool CanViewItem => SelectedItem != null;

      /// <summary>
      /// Determines whether the selected item can be deleted.
      /// </summary>
      protected virtual bool CanDeleteItem => SelectedItem != null;

      #endregion

      #region Protected methods

      /// <summary>
      /// Creates and returns a collection of view model items.
      /// </summary>
      /// <returns>A collection of view models.</returns>
      protected abstract IEnumerable<TViewModel> GetItems();



      /// <summary>
      /// Initializes WPF commands implemented by controller.
      /// </summary>
      protected virtual void InitializeCommands()
      {
         CreateItemCommand = new DelegateCommand(args => CreateItem(), args => CanCreateItem);
         EditItemCommand = new DelegateCommand(args => EditItem(), args => CanEditItem);
         ViewItemCommand = new DelegateCommand(args => ViewItem(), args => CanViewItem);
         DeleteItemCommand = new DelegateCommand(args => DeleteItem(), args => CanDeleteItem);
      }



      /// <summary>
      /// Creates a new item.
      /// </summary>
      protected virtual void CreateItem()
      {
         var viewModel = new TViewModel();
         if(ShowDialog(viewModel))
            Items.Add(viewModel);
      }



      /// <summary>
      /// Performs edit of the selected item.
      /// </summary>
      protected virtual void EditItem()
      {
         ShowDialog(SelectedItem);
      }



      /// <summary>
      /// Performs view of the selected item.
      /// </summary>
      protected virtual void ViewItem()
      {
         ShowDialog(SelectedItem, readOnly: true);
      }

      #endregion

      #region Private methods

      /// <summary>
      /// Initializes a controller.
      /// </summary>
      private void Initialize()
      {
         InitializeCommands();
         Items = new ObservableCollection<TViewModel>(GetItems());
      }



      /// <summary>
      /// Displays create/view/edit dialog view for the specified view model item.
      /// </summary>
      /// <returns>True, if the user performed save operation; otherwise, false.</returns>
      private static bool ShowDialog(TViewModel viewModel, bool readOnly=false)
      {
         viewModel.IsReadOnly = readOnly;
         var view = new TView
         {
            Owner = Application.Current.MainWindow,
            DataContext = viewModel
         };
         return view.ShowDialog() == true;
      }



      /// <summary>
      /// Deletes the selected item.
      /// </summary>
      private void DeleteItem()
      {
         var item = SelectedItem;
         var caption = $"Delete {item.ShortName}";
         if(MessageBox.Show($"Do you want to delete the {item.LongName}?", caption,
            MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            return;

         item.Delete();
         Items.Remove(item);

         MessageBox.Show($"The {item.LongName} has been deleted.", caption,
            MessageBoxButton.OK, MessageBoxImage.Information);
      }

      #endregion
   }
}
