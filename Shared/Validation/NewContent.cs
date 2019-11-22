using System.ComponentModel.DataAnnotations;

namespace Shared.Validation {

	/// <summary>
	/// Validation New Content Class
	/// </summary>
	public class NewContent {

		/// <summary>
		/// Validation New Content
		/// </summary>
		/// <value></value>
        [Required]
		public string newContent { get; set; }
        
		/// <summary>
		/// Validation New Content Commit Message
		/// </summary>
		/// <value></value>
		[Required]
		public string commitMessage { get; set; }

		/// <summary>
		/// Validation New File Name
		/// </summary>
		/// <value></value>
        [Required]
		public string newFileName { get; set; }
        
		/// <summary>
		/// Validation New File Extension
		/// </summary>
		/// <value></value>
		[Required]
		public string newFileExtension { get; set; }
    }
}
