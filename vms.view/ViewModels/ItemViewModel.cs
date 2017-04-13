//=================================================================================================
// Class ItemViewModel
// Item view model
// Represents a base abstract class for all item view models.
//=================================================================================================
using System.Windows;
using System.Windows.Input;
using vms;

namespace Vms.ViewModels
{
   /// <summary>
   /// Item view model
   /// Represents a base abstract class for all item view models.
   /// </summary>
   /// <typeparam name="T">The actual domain model type.</typeparam>
   public abstract class ItemViewModel<T> : ViewModelBase
      where T : PersistentEntity
   {
      #region Constructors

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="model">A model instance.</param>
      /// <param name="isNew">Determines whether the specified model is new.</param>
      protected ItemViewModel(T model, bool isNew=false)
      {
         Model = model;
         IsNew = isNew;
         SaveCommand = new DelegateCommand(args => Save(), args => IsEnabled && !HasErrors);
      }

      #endregion

      #region WPF commands

      /// <summary>
      /// Save a model instance into the database.
      /// </summary>
      public ICommand SaveCommand { get; private set; }

      #endregion

      #region Public properties.

      /// <summary>
      /// Determines whether the model is in read-only mode.
      /// </summary>
      public bool IsReadOnly { get; internal set; }

      /// <summary>
      /// Determines whether the model is enabled (not read-only).
      /// </summary>
      public bool IsEnabled => !IsReadOnly;

      #endregion

      #region Internal properties

      /// <summary>
      /// Displayed name (long form).
      /// </summary>
      internal abstract string LongName { get; }

      /// <summary>
      /// Displayed name (short form).
      /// </summary>
      internal abstract string ShortName { get; }

      #endregion

      #region Internal methods

      /// <summary>
      /// Deletes the given model instance.
      /// </summary>
      internal void Delete()
      {
         AppServices.Delete(Model);
      }

      #endregion

      #region Protected properties

      /// <summary>
      /// Model instance.
      /// </summary>
      protected T Model { get; set; }

      /// <summary>
      /// Determines whether the model instance is new (not saved in the database).
      /// </summary>
      protected bool IsNew { get; set; }

      #endregion

      #region Protected methods

      /// <summary>
      /// Performs save model operation.
      /// </summary>
      protected virtual void Save()
      {
         AppServices.Save(Model);
         Confirmation();
         IsNew = false;
      }



      /// <summary>
      /// Displays a confirmation message after save operation.
      /// </summary>
      protected virtual void Confirmation()
      {
         MessageBox.Show($"The {LongName} has been saved.", $"Save {ShortName}",
            MessageBoxButton.OK, MessageBoxImage.Information);
      }

      #endregion
   }
}
