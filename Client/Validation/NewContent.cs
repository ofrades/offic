using System.ComponentModel.DataAnnotations;

namespace Client.Validation
{
	public class NewContent {

        [Required]
		public string newContent { get; set; }
        
		[Required]
		public string commitMessage { get; set; }

        [Required]
		public string newFileName { get; set; }
        
		[Required]
		public string newFileExtension { get; set; }
    }
}
