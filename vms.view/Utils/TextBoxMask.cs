//=================================================================================================
// Class TextBoxMask.
// Text box mask.
// Provides base text mask functionality applied to TextBox WPF controls.
//=================================================================================================
using System.Linq;

namespace Vms.Views
{
	/// <summary>
	/// Text box mask.
	/// Provides base text mask functionality applied to TextBox WPF controls.
	/// </summary>
	public abstract class TextBoxMask
	{
		#region Protected Properties.

		/// <summary>
		/// A collection of mask elements.
		/// </summary>
		protected MaskElement[] Elements { get; set; }

		/// <summary>
		/// The current mask element index.
		/// </summary>
		protected int CurrentIndex { get; set; }

		#endregion

		#region Public Methods.

		/// <summary>
		/// Clears all mask elements.
		/// </summary>
		public void Clear()
		{
			foreach(var e in Elements)
				e.Content = null;
			CurrentIndex = 0;
		}



		/// <summary>
		/// Process a text symbols entered to the masked field.
		/// </summary>
		/// <param name="insertedText">Inserted text</param>
		/// <returns>The actual text content.</returns>
		public string ProcessText(string insertedText)
		{
			if(insertedText == null)
				return null;

			var text = Remove(insertedText, MaskSymbols);
			if(text.Length == 0)
				return "";

			foreach(var c in text)
			{
				ShiftRight();
				ProcessSymbol(c);
			}

			return Content;
		}

		#endregion

		#region Internal Properties.

		/// <summary>
		/// The current text content.
		/// </summary>
		internal string Content
		{
			get { return Elements.Aggregate("", (c, e) => c + e.Text); }
		}



		/// <summary>
		/// The current position for the newly entered symbol.
		/// </summary>
		internal int Position
		{
			get
			{
				var pos = 0;
				for(var i = 0; i < CurrentIndex; i++)
					pos += Elements[i].Positions;

				return pos;
			}
		}

		#endregion

		#region Internal Methods.

		/// <summary>
		/// Processes a user text input.
		/// </summary>
		/// <param name="text">The text entered by the user.</param>
		/// <param name="cursorPosition">The current cursor position in the text field.</param>
		/// <param name="selectedText">Selected text if any.</param>
		/// <returns>True, if the entered text is accepted; otherwise, false.</returns>
		internal bool ProcessInput(string text, int cursorPosition, string selectedText)
		{
			if(selectedText.Length > 0)
			{
				var toRemove = Remove(selectedText, MaskSymbols);
				if(toRemove.Length > 0)
				{
					for(var i = 0; i < toRemove.Length; i++)
					{
						AdjustCurrentPosition(cursorPosition);
						if(CurrentIndex < 0 || CurrentIndex >= Length)
							return false;

						ShiftLeftCurrent();
					}
				}
			}

			if(Elements.Last().Content != null)
				return false;
			if(text.Any(c => !char.IsDigit(c)))
				return false;

			AdjustCurrentPosition(cursorPosition);
			ShiftRight();

			return text.All(ProcessSymbol);
		}



		/// <summary>
		/// Processes clipboard paste operation.
		/// </summary>
		/// <param name="cursorPosition">The current cursor position.</param>
		/// <param name="selectedText">Selected text, if any.</param>
		/// <param name="insertedText">Inserted text.</param>
		/// <returns></returns>
		internal bool ProcessPaste(int cursorPosition, string selectedText, string insertedText)
		{
			if(insertedText == null)
				return false;
			var text = Remove(insertedText, MaskSymbols);
			if(text.Length == 0)
				return false;

			if(selectedText.Length > 0)
			{
				var toRemove = Remove(selectedText, MaskSymbols);
				if(toRemove.Length > 0)
				{
					for(var i = 0; i < toRemove.Length; i++)
					{
						AdjustCurrentPosition(cursorPosition);
						if(CurrentIndex < 0 || CurrentIndex >= Length)
							return false;

						ShiftLeftCurrent();
					}
				}
			}
			if(Elements.Last().Content != null)
				return false;

			foreach(var c in text)
			{
				ShiftRight();
				ProcessSymbol(c);
			}

			return true;
		}



		/// <summary>
		/// Processes delete key pressed by user.
		/// </summary>
		/// <param name="cursorPosition">The current cursor position.</param>
		/// <param name="selectedText">Selected text, if any.</param>
		/// <returns></returns>
		internal bool ProcessDelete(int cursorPosition, string selectedText)
		{
			var count = 1;
			if(selectedText.Length > 0)
			{
				var text = Remove(selectedText, MaskSymbols);
				if(text.Length == 0)
					return false;

				count = text.Length;
			}
			for(var i = 0; i < count; i++)
			{
				AdjustCurrentPosition(cursorPosition);
				if(CurrentIndex < 0 || CurrentIndex >= Length)
					return false;

				ShiftLeftCurrent();
			}
			return true;
		}



		/// <summary>
		/// Processes backspace key pressed by user.
		/// </summary>
		/// <param name="cursorPosition">The current cursor position in the text field.</param>
		/// <param name="selectedText">Selected text, if any.</param>
		/// <returns></returns>
		internal bool ProcessBackspace(int cursorPosition, string selectedText)
		{
			if(selectedText.Length > 0)
				return ProcessDelete(cursorPosition, selectedText);
			if(cursorPosition <= 0)
				return false;

			AdjustCurrentPosition(cursorPosition);
			if(CurrentIndex <= 0 || CurrentIndex > Length)
				return false;

			ShiftLeft();
			CurrentIndex--;

			return true;
		}

		#endregion

		#region Protected Properties.

		/// <summary>
		/// The maximum total number of available symbols to be entered.
		/// </summary>
		protected int Length => Elements.Length;

		/// <summary>
		/// A collection of symbols available for the mask.
		/// </summary>
		protected abstract char[] MaskSymbols { get; }

		#endregion

		#region Protected Methods.

		/// <summary>
		/// Shifts all mask elements to one position right from the current position to the end of mask.
		/// </summary>
		protected void ShiftRight()
		{
			for(var i = Elements.Length - 1; i >= CurrentIndex && i > 0; i--)
				Elements[i].Content = Elements[i-1].Content;
		}



		/// <summary>
		/// Shifts all mask elements to one position left from the current position to the end of mask.
		/// The last mask element is set to null.
		/// </summary>
		/// <remarks>
		/// Simulates backspace button behavior.
		/// </remarks>
		protected void ShiftLeft()
		{
			for(var i = CurrentIndex; i < Elements.Length; i++)
				Elements[i-1].Content = Elements[i].Content;

			Elements[Elements.Length-1].Content = null;
		}



		/// <summary>
		/// Shifts all mask elements to one position left from the current position to the end of mask.
		/// The last mask element is set to null.
		/// </summary>
		/// <remarks>
		/// Simulates delete button behavior.
		/// </remarks>
		protected void ShiftLeftCurrent()
		{
			for(var i = CurrentIndex; i < Elements.Length - 1; i++)
				Elements[i].Content = Elements[i+1].Content;

			Elements[Elements.Length-1].Content = null;
		}



		/// <summary>
		/// Processes one entered symbol to the mask.
		/// </summary>
		/// <param name="symbol">A symbol to be processed.</param>
		/// <returns>True, if the entered symbol is accepted by the mask; otherwise, false.</returns>
		protected bool ProcessSymbol(char symbol)
		{
			if(CurrentIndex < 0 || CurrentIndex >= Length)
				return false;

			if(Elements[CurrentIndex].IsSymbolAccepted(symbol))
			{
				Elements[CurrentIndex++].Content = symbol;
				return true;
			}
			return false;
		}



		/// <summary>
		/// Adjusts a position of the current mask element.
		/// </summary>
		/// <param name="cursorPosition">The actual cursor position.</param>
		protected void AdjustCurrentPosition(int cursorPosition)
		{
			var content = Content;
			var pos = 0;
			for(var i = 0; i < cursorPosition; i++)
			{
				if(!MaskSymbols.Contains(content[i]))
					pos++;
			}
			CurrentIndex = pos;
		}

      #endregion

      #region Private methods

      private static string Remove(string source, char[] toRemove)
      {
         return string.IsNullOrEmpty(source) ? source :
            toRemove.Aggregate(source, (current, ch) => current.Replace(ch.ToString(), null));
      }

	   #endregion
   }
}
