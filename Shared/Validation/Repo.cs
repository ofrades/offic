using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Validation {

	/// <summary>
	/// Validation Repo Class
	/// </summary>
	public class Repo {

		/// <summary>
		/// Validation Owner
		/// </summary>
		/// <value></value>
		[Required]
        public string owner { get; set; }

		/// <summary>
		/// Validation repoName
		/// </summary>
		/// <value></value>
		public string repoName { get; set; }
	}
}
