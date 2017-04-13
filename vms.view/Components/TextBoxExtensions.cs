//=================================================================================================
// Class TextBoxExtensions.
// TextBox extension methods and attached properties .
// Provides the set of extension methods of System.Windows.Controls.TextBox class.
//=================================================================================================
using System;
using System.Windows;
using System.Windows.Controls;

namespace Vms.Views
{
   /// <summary>
   /// TextBox extension methods.
   /// </summary>
   /// <remarks>
   /// Provides the set of extension methids of <see cref="TextBox"/> class.
   /// </remarks>
   public static class TextBoxExtensions
   {
      #region Public Static Extension Methods.

      /// <summary>
      /// Updates the binded source property (Text) of the specified TextBox control.
      /// </summary>
      /// <param name="control">TextBox control instance.</param>
      public static void UpdateSource(this TextBox control)
      {
         if(control == null)
            throw new ArgumentNullException(nameof(control));
         control.UpdateSource(TextBox.TextProperty);
      }

      #endregion


      #region Properties.

      /// <summary>
      /// Gets the Watermark property value.
      /// </summary>
      /// <param name="obj">Target textbox.</param>
      public static string GetWatermark(TextBox obj)
      {
         return (string)obj.GetValue(WatermarkProperty);
      }



      /// <summary>
      /// Sets the Watermark property value.
      /// </summary>
      /// <param name="obj">Target object.</param>
      /// <param name="value">New value.</param>
      public static void SetWatermark(TextBox obj, string value)
      {
         obj.SetValue(WatermarkProperty, value);
      }



      /// <summary>
      /// Identifies the Watermark attached property.
      /// </summary>
      public static readonly DependencyProperty WatermarkProperty = 
         DependencyProperty.RegisterAttached("Watermark", typeof(string), typeof(TextBoxExtensions));



      /// <summary>
      /// Gets the WatermarkEnabled property value.
      /// </summary>
      /// <param name="obj">Target textbox.</param>
      public static bool GetWatermarkEnabled(TextBox obj)
      {
         return (bool)obj.GetValue(WatermarkEnabledProperty);
      }



      /// <summary>
      /// Sets the WatermarkEnabled property value.
      /// </summary>
      /// <param name="obj">Target textbox.</param>
      /// <param name="value">New value.</param>
      public static void SetWatermarkEnabled(TextBox obj, bool value)
      {
         obj.SetValue(WatermarkEnabledProperty, value);
      }



      /// <summary>
      /// Identifies the WatermarkEnabled attached property.
      /// </summary>
      public static readonly DependencyProperty WatermarkEnabledProperty = 
         DependencyProperty.RegisterAttached("WatermarkEnabled", typeof(bool), typeof(TextBoxExtensions), new PropertyMetadata(false));



      /// <summary>
      /// Gets the OverlayText property value.
      /// </summary>
      /// <param name="obj">Target textbox.</param>
      public static bool GetOverlayText(TextBox obj)
      {
         return (bool)obj.GetValue(OverlayTextProperty);
      }



      /// <summary>
      /// Sets the OverlayText property value.
      /// </summary>
      /// <param name="obj">Target textbox</param>
      /// <param name="value">New value.</param>
      public static void SetOverlayText(TextBox obj, bool value)
      {
         obj.SetValue(OverlayTextProperty, value);
      }



      /// <summary>
      /// Identifies the OverlayText attached property.
      /// </summary>
      public static readonly DependencyProperty OverlayTextProperty =
         DependencyProperty.RegisterAttached("OverlayText", typeof(bool), typeof(TextBoxExtensions), new PropertyMetadata(false));

      #endregion

      /// <summary>
      /// Updates the source property of the specified WPF element.
      /// </summary>
      /// <param name="element">WPF framework element instance.</param>
      /// <param name="property">The specified dependency property to update source.</param>
      public static void UpdateSource(this TextBox element, DependencyProperty property)
      {
         if(element == null)
            throw new ArgumentNullException(nameof(element));
         if(property == null)
            throw new ArgumentNullException(nameof(property));

         var bindingExpression = element.GetBindingExpression(property);
         bindingExpression?.UpdateSource();
      }
   }
}
