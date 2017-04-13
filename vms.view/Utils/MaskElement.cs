//=================================================================================================
// Class MaskElement.
// Mask element.
// Represents one symbol placeholder for entered value and the appropriate metadata.
//=================================================================================================
using System.Linq;

namespace Vms.Views
{
	/// <summary>
	/// Mask element.
	/// Represents one symbol placeholder for entered value and the appropriate metadata.
	/// </summary>
	public sealed class MaskElement
	{
		#region Constructors.

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="maskSymbols">Mask symbol associated with the element.</param>
		internal MaskElement(params char[] maskSymbols)
		{
			MaskSymbols = maskSymbols;
		}

		#endregion

		#region Internal Properties.

		/// <summary>
		/// Determines whether the mask element is empty.
		/// </summary>
		internal bool IsEmpty => Content == null;

		/// <summary>
		/// The character representing the element content if any.
		/// </summary>
		internal char? Content { get; set; }

		/// <summary>
		/// Mask symbols associated with the element.
		/// </summary>
		internal char[] MaskSymbols { get; }

		/// <summary>
		/// The number of positions taken by the element including content and mask symbol.
		/// </summary>
		internal int Positions => MaskSymbols.Length == 0 ? 1 : MaskSymbols.Length + 1;

		/// <summary>
		/// Text value representing by the element including mask symbol.
		/// </summary>
		internal string Text => Content != null
			? MaskSymbols.Aggregate("", (current, m) => current + m.ToString()) + Content : null;

		#endregion

		#region Internal Methods.

		/// <summary>
		/// Determines whether the specified symbol is accepted by the element.
		/// </summary>
		/// <param name="symbol">The specified symbol to check.</param>
		/// <returns>True, if the specified symbol can be accepted to input.</returns>
		internal bool IsSymbolAccepted(char symbol)
		{
			return char.IsDigit(symbol);
		}

		#endregion
	}
}
