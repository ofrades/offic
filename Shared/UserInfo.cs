namespace Shared {

	/// <summary>
	/// User Info Shared between Server-Side and Client-Side
	/// </summary>
	public class UserInfo {

		/// <summary>
		/// Check if user is authenticated
		/// </summary>
		/// <value></value>
		public bool IsAuthenticated { get; set; }

		/// <summary>
		/// User name
		/// </summary>
		/// <value></value>
		public string Name { get; set; }
	}
}