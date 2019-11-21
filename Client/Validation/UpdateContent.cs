using System.ComponentModel.DataAnnotations;

namespace Client.Validation
{
	public class UpdateContent {

		[Required]
		public string content { get; set; }

        [Required]
		public string commitMessage { get; set; }
	}
}
