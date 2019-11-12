using Microsoft.AspNetCore.Blazor.Hosting;

namespace Client {

	/// <summary>
	/// Client Program
	/// </summary>
	public class Program {

		/// <summary>
		/// Client Program Main
		/// </summary>
		/// <param name="args"></param>
		public static void Main (string[] args) {
			CreateHostBuilder (args).Build ().Run ();
		}

		/// <summary>
		/// Client Program CreateHostBuilder
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static IWebAssemblyHostBuilder CreateHostBuilder (string[] args) =>
			BlazorWebAssemblyHost.CreateDefaultBuilder ()
			.UseBlazorStartup<Startup> ();
	}
}