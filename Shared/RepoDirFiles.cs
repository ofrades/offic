using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared {

	/// <summary>
	/// RepoDirFiles Class
	/// </summary>
	public class RepoDirFiles {

		/// <summary>
		/// Parameterless Constructor needed for deserialization
		/// </summary>
		public RepoDirFiles(){}

		/// <summary>
		/// Repo Constructor
		/// </summary>
		/// <param name="name"></param>
		public RepoDirFiles(string name){
			Name = name;
		}

		/// <summary>
		/// Repo File Name
		/// </summary>
		/// <value></value>
		public string Name { get; set; }
	}
}