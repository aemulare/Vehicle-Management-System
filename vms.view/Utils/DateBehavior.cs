//=================================================================================================
// Class DateBehavior.
// Date text field behavior.
// Processes the user input into the <see cref="TextBox"/> control matching it according
// Date format.
//=================================================================================================
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Vms.Views
{
	/// <summary>
	/// Date text field behavior.
	/// </summary>
	/// <remarks>
	/// Processes the user input into the <see cref="TextBox"/> control matching it according Date format.
	/// </remarks>
	public sealed class DateBehavior : Behavior<TextBox>
	{
		#region Dependency Properties.

		/// <summary>
		/// Date provider property definition.
		/// </summary>
		private static readonly DependencyProperty MaskProperty;

		#endregion

		#region Constructors.

		/// <summary>
		/// Constructor.
		/// </summary>
		static DateBehavior()
		{
			MaskProperty = DependencyProperty.Register("Mask", typeof(DateMask), typeof(DateBehavior));
		}

		#endregion

		#region Public Properties.

		/// <summary>
		/// Date mask provider.
		/// </summary>
		public DateMask Mask
		{
			get { return (DateMask)GetValue(MaskProperty); }
			set { SetValue(MaskProperty, value); }
		}

		#endregion

		#region Protected Methods.

		/// <summary>
		/// Called after the behavior is attached into the associated object.
		/// </summary>
		protected override void OnAttached()
		{
			base.OnAttached();

			AssociatedObject.PreviewKeyDown += HandlePreviewKeyDown;
			AssociatedObject.GotKeyboardFocus += SelectAllText;
			AssociatedObject.MouseDoubleClick += SelectAllText;
			AssociatedObject.PreviewMouseLeftButtonDown += SelectivelyIgnoreMouseButton;
			AssociatedObject.PreviewTextInput += HandlePreviewTextInput;
			DataObject.AddPastingHandler(AssociatedObject, HandlePaste);

			AddBindingNotification();
			Binding.AddTargetUpdatedHandler(AssociatedObject, HandleTargetUpdated);
		}



		/// <summary>
		/// Called when the behavior is been detached from its associated object but before it has actually occured.
		/// </summary>
		protected override void OnDetaching()
		{
			base.OnDetaching();

			AssociatedObject.PreviewKeyDown -= HandlePreviewKeyDown;
			AssociatedObject.GotKeyboardFocus -= SelectAllText;
			AssociatedObject.MouseDoubleClick -= SelectAllText;
			AssociatedObject.PreviewMouseLeftButtonDown -= SelectivelyIgnoreMouseButton;
			AssociatedObject.PreviewTextInput -= HandlePreviewTextInput;
			DataObject.RemovePastingHandler(AssociatedObject, HandlePaste);
			Binding.RemoveTargetUpdatedHandler(AssociatedObject, HandleTargetUpdated);
		}

		#endregion

		#region Private Event Handlers.

		private void HandleTargetUpdated(object sender, DataTransferEventArgs e)
		{
			if (Mask == null)
				return;

			Mask.Clear();
			Mask.ProcessText(AssociatedObject.Text);
		}



		/// <summary>
		/// Handles paste command into <see cref="TextBox"/> control.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event arguments.</param>
		private void HandlePaste(object sender, DataObjectPastingEventArgs e)
		{
			var isText = e.SourceDataObject.GetDataPresent(DataFormats.Text, true);
			if (!isText)
			{
				e.CancelCommand();
				e.Handled = true;
				return;
			}

			var text = (string)e.SourceDataObject.GetData(DataFormats.Text);
			if (!Mask.ProcessPaste(AssociatedObject.SelectionStart,
				AssociatedObject.SelectedText, text))
			{
				e.CancelCommand();
				e.Handled = true;
				return;
			}

			AssociatedObject.Text = Mask.Content;
			AssociatedObject.CaretIndex = Mask.Position;
			e.CancelCommand();
			e.Handled = true;
		}



		/// <summary>
		/// Handles <see cref="UIElement.PreviewTextInput"/> event and processes the user text input.
		/// </summary>
		/// <param name="sender">Event handler.</param>
		/// <param name="e">Event arguments.</param>
		private void HandlePreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (AssociatedObject.IsReadOnly || !AssociatedObject.IsEnabled)
			{
				e.Handled = true;
				return;
			}

			if (!Mask.ProcessInput(e.Text, AssociatedObject.SelectionStart, AssociatedObject.SelectedText))
			{
				e.Handled = true;
				return;
			}

			AssociatedObject.Text = Mask.Content;
			AssociatedObject.CaretIndex = Mask.Position;
			e.Handled = true;
		}



		/// <summary>
		/// Handles and processes TextBox.PreviewKeyDown event.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event arguments.</param>
		/// <remarks>
		/// Prevents space symbols entering into the associated control.
		/// </remarks>
		private void HandlePreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Space)
			{
				e.Handled = true;
				return;
			}
			if (e.Key == Key.Back)
			{
				if (!Mask.ProcessBackspace(AssociatedObject.SelectionStart, AssociatedObject.SelectedText))
				{
					e.Handled = true;
					return;
				}

				AssociatedObject.Text = Mask.Content;
				AssociatedObject.CaretIndex = Mask.Position;
				e.Handled = true;
			}
			if (e.Key == Key.Delete)
			{
				if (!Mask.ProcessDelete(AssociatedObject.SelectionStart, AssociatedObject.SelectedText))
				{
					e.Handled = true;
					return;
				}

				AssociatedObject.Text = Mask.Content;
				AssociatedObject.CaretIndex = Mask.Position;
				e.Handled = true;
			}
		}



		/// <summary>
		/// Selects all text in the TextBox control.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event arguments.</param>
		private void SelectAllText(object sender, RoutedEventArgs e)
		{
			AssociatedObject.SelectAll();
		}



		/// <summary>
		/// Handles <see cref="UIElement.PreviewMouseLeftButtonDown"/> event and
		/// returns focus into the associated object.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event arguments.</param>
		private void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
		{
			if (!AssociatedObject.IsKeyboardFocusWithin)
			{
				AssociatedObject.Focus();
				e.Handled = true;
			}
		}

		#endregion

		
		#region Private Methods.

		private void AddBindingNotification()
		{
			var bindingExpression = AssociatedObject.GetBindingExpression(TextBox.TextProperty);
			if (bindingExpression == null)
				return;

			var newBinding = CloneBinding(bindingExpression.ParentBinding);
			newBinding.NotifyOnTargetUpdated = true;

			AssociatedObject.SetBinding(TextBox.TextProperty, newBinding);
		}



		private Binding CloneBinding(Binding binding)
		{
			var result = new Binding
			{
				AsyncState = binding.AsyncState,
				BindingGroupName = binding.BindingGroupName,
				BindsDirectlyToSource = binding.BindsDirectlyToSource,
				Converter = binding.Converter,
				ConverterCulture = binding.ConverterCulture,
				ConverterParameter = binding.ConverterCulture,
				ElementName = binding.ElementName,
				FallbackValue = binding.FallbackValue,
				IsAsync = binding.IsAsync,
				Mode = binding.Mode,
				NotifyOnSourceUpdated = binding.NotifyOnSourceUpdated,
				NotifyOnTargetUpdated = binding.NotifyOnTargetUpdated,
				NotifyOnValidationError = binding.NotifyOnValidationError,
				Path = binding.Path,
				StringFormat = binding.StringFormat,
				TargetNullValue = binding.TargetNullValue,
				UpdateSourceExceptionFilter = binding.UpdateSourceExceptionFilter,
				UpdateSourceTrigger = binding.UpdateSourceTrigger,
				ValidatesOnDataErrors = binding.ValidatesOnDataErrors,
				ValidatesOnExceptions = binding.ValidatesOnExceptions,
				XPath = binding.XPath,
			};

			if (binding.RelativeSource != null)
				result.RelativeSource = binding.RelativeSource;

			foreach (var validationRule in binding.ValidationRules)
				result.ValidationRules.Add(validationRule);

			return result;

		}
		
		#endregion
	}
}
