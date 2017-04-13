//=================================================================================================
// Class ViewModelBase
// View Model base abstract class.
// Represents the root class in the view models hierarchy. Provides base functionality for
// all view models.
//=================================================================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;

namespace Vms.ViewModels
{
   /// <summary>
   /// View Model base abstract class.
   /// Represents the root class in the view models hierarchy.
   /// </summary>
   public abstract class ViewModelBase : DependencyObject, INotifyPropertyChanged, INotifyDataErrorInfo
   {
      #region Private members

      // The dictionary containing validation errors for view model properties.
      private readonly IDictionary<string,IList<string>> validationErrors =
         new Dictionary<string,IList<string>>();

      #endregion

      #region INotifyPropertyChanged implementation

      /// <summary>
      /// Occurs when a property value changes.
      /// </summary>
      public event PropertyChangedEventHandler PropertyChanged;

      #endregion

      #region Public methods

      /// <summary>
      /// Performs validation.
      /// </summary>
      public virtual void PerformValidate()
      {
      }

      #endregion

      #region INotifyDataErrorInfo Implementation.

      /// <summary>
      /// Occurs when the validation errors have changed for a property or for the entire entity.
      /// </summary>
      public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;



      /// <summary>
      /// Determines whether the entity currently has validation errors.
      /// </summary>
      public bool HasErrors => this.validationErrors.Count > 0;



      /// <summary>
      /// Gets the validation errors for the specified property or for the entire entity.
      /// </summary>
      /// <param name="propertyName">The name of property to retrieve validation errors,
      /// or null or empty to retrieve entity-level errors.</param>
      /// <returns>The validation errors for the property or entity.</returns>
      public IEnumerable GetErrors(string propertyName)
      {
         if(propertyName != null)
            return this.validationErrors.ContainsKey(propertyName) ? this.validationErrors[propertyName] : new string[0];

         var errors = new List<string>();
         foreach(var err in this.validationErrors.Values)
            errors.AddRange(err);
         return errors;
      }

      #endregion

      #region Protected methods

      /// <summary>
      /// Raises the <see cref="PropertyChanged"/> event.
      /// </summary>
      /// <typeparam name="T">The type of the property that has a new value.</typeparam>
      /// <param name="propertyExpression">A lambda expression representing the property that has a new value.</param>
      protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
      {
         ClearValidationErrors(propertyExpression);
         var propertyName = GetPropertyName(propertyExpression);
         var handler = PropertyChanged;
         handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }



      /// <summary>
      /// Clears all validation errors for the specified property.
      /// </summary>
      protected void ClearValidationErrors<T>(Expression<Func<T>> propertyExpression)
      {
         var propertyName = GetPropertyName(propertyExpression);
         if(this.validationErrors.ContainsKey(propertyName))
         {
            this.validationErrors.Remove(propertyName);
            RaiseErrorsChanged(propertyExpression);
         }
      }



      /// <summary>
      /// Raises <see cref="ErrorsChanged"/> event.
      /// </summary>
      protected void RaiseErrorsChanged<T>(Expression<Func<T>> propertyExpression)
      {
         var propertyName = GetPropertyName(propertyExpression);
         var handler = ErrorsChanged;
         handler?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
      }



      /// <summary>
      /// Sets the new error into the collection of validation errors.
      /// </summary>
      protected void SetValidationError<T>(Expression<Func<T>> propertyExpression, string errorText)
      {
         if(string.IsNullOrWhiteSpace(errorText))
         {
            ClearValidationErrors(propertyExpression);
            return;
         }

         var propertyName = GetPropertyName(propertyExpression);
         if(!this.validationErrors.ContainsKey(propertyName))
            this.validationErrors.Add(propertyName, new List<string>());

         var errors = this.validationErrors[propertyName];
         if(!errors.Contains(errorText))
         {
            errors.Add(errorText.Trim());
            RaiseErrorsChanged(propertyExpression);
         }
      }

      #endregion

      #region Private methods

      /// <summary>
      /// Extracts the property name from the specified property expression in the case
      /// when the expresson member is a property.
      /// </summary>
      /// <typeparamref name="T">The object type containing the property specified in the expression.</typeparamref>
      /// <param name="propertyExpression">The property expression (e.g. p => p.PropertyName).</param>
      /// <returns>The name of the property.</returns>
      private static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
      {
         if(propertyExpression == null)
            throw new ArgumentNullException(nameof(propertyExpression));

         var memberExpression = propertyExpression.Body as MemberExpression;
         if(memberExpression == null)
            throw new ArgumentException(@"The specified expression is not a member access expression.", nameof(propertyExpression));

         var propertyInfo = memberExpression.Member as PropertyInfo;
         if(propertyInfo == null)
            throw new ArgumentException(@"The specified expression does not have a property.", nameof(propertyExpression));

         var getMethod = propertyInfo.GetGetMethod(true);
         if(getMethod.IsStatic)
            throw new ArgumentException(@"The property specified in the expression must not be static.", nameof(propertyExpression));

         return memberExpression.Member.Name;
      }

      #endregion
   }
}
