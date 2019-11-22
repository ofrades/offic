using System.ComponentModel.DataAnnotations;

namespace Shared.Validation {

	/// <summary>
	/// Validation Update Content Class
	/// </summary>
	public class UpdateContent {

		/// <summary>
		/// Validation Update Content
		/// </summary>
		/// <value></value>
		[Required]
		public string content { get; set; }

		/// <summary>
		/// Validation Update Commit Message
		/// </summary>
		/// <value></value>
        [Required]
		public string commitMessage { get; set; }
	}
}
