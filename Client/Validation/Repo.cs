using System;
using System.ComponentModel.DataAnnotations;

namespace Client.Validation
{
	public class Repo {

		[Required]
        public string owner { get; set; }

		public string repoName { get; set; }
	}
}
