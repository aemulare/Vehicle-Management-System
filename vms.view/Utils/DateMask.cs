//=================================================================================================
// Class DateMask.
// Date mask.
// Provides date mask functionality.
//=================================================================================================
namespace Vms.Views
{
	/// <summary>
	/// Date mask.
	/// Provides date mask functionality.
	/// </summary>
	public sealed class DateMask : TextBoxMask
	{
		#region Constructors.

		/// <summary>
		/// Default constructor.
		/// </summary>
		public DateMask()
		{
			Elements = new[]
			{
				new MaskElement(),
				new MaskElement(),
				new MaskElement('/'),
				new MaskElement(),
				new MaskElement('/'),
				new MaskElement(),
				new MaskElement(),
				new MaskElement()
			};
		}

		#endregion

		#region Protected Properties.

		/// <summary>
		/// A collection of symbols available for the mask.
		/// </summary>
		protected override char[] MaskSymbols => new[] { '/' };

		#endregion
	}
}
