using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared {

	/// <summary>
	/// Search Response
	/// </summary>
	public class SearchResponse {

		/// <summary>
		/// Search Response Items
		/// </summary>
		/// <value></value>
		public IList<Repo> Items { get; set; }

		/// <summary>
		/// Search Response Total Count
		/// </summary>
		/// <value></value>
		public int Total_Count { get; set; }
	}
}