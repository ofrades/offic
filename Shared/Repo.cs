using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared {

	/// <summary>
	/// Repo Info
	/// </summary>
	public class Repo {

		/// <summary>
		/// Repo Description
		/// </summary>
		/// <value></value>
		public string Description { get; set; }

		/// <summary>
		/// Repo Name
		/// </summary>
		/// <value></value>
		public string Name { get; set; }

		/// <summary>
		/// Repo Full Name
		/// </summary>
		/// <value></value>
		public string Full_Name { get; set; }

		/// <summary>
		/// Repo Stars
		/// </summary>
		/// <value></value>
		public int Stargazers_Count { get; set; }
	}
}