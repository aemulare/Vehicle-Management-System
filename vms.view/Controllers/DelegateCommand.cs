//=================================================================================================
// Class DelegateCommand.
// WPF Command.
// Allows to inject command logic via delegates passed into its constructor.
//===-=============================================================================================
using System;
using System.Windows.Input;

namespace Vms.ViewModels
{
   /// <summary>
   /// WPF Command.
   /// Allows to inject command logic via delegates passed into its constructor.
   /// </summary>
   public sealed class DelegateCommand : ICommand
   {
      #region Private members

      // Defines the method to be called when the command is invoked.
      private readonly Action<object> execute;
      // Defines the predicate determining whether the command can execute in its current state.
      private readonly Predicate<object> canExecute;

      #endregion

      #region Constructors

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="execute">Defines the method to be called when the command is invoked.</param>
      /// <param name="canExecute">Defines the predicate determining whether the command can execute in its current state.</param>
      public DelegateCommand(Action<object> execute, Predicate<object> canExecute=null)
      {
         if(execute == null)
            throw new ArgumentNullException(nameof(execute));
         this.execute = execute;
         this.canExecute = canExecute;
      }

      #endregion

      #region ICommand implementation

      /// <summary>
      /// Occurs when changes occur that affect whether or not the command should execute.
      /// </summary>
      public event EventHandler CanExecuteChanged
      {
         add { CommandManager.RequerySuggested += value; }
         remove { CommandManager.RequerySuggested -= value; }
      }



      /// <summary>
      /// The method to be called when the command is invoked.
      /// </summary>
      /// <param name="parameter">Data used by the command.</param>
      public void Execute(object parameter)
      {
         this.execute(parameter);
      }



      /// <summary>
      /// Determines whether the command can execute in its current state.
      /// </summary>
      /// <param name="parameter">Data used by the command.</param>
      /// <returns>True, if this command can be executed; otherwise, false.</returns>
      public bool CanExecute(object parameter)
      {
         return this.canExecute == null || this.canExecute(parameter);
      }

      #endregion
   }
}
