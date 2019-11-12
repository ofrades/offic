using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Server {

	/// <summary>
	/// Server Program
	/// </summary>
	public class Program {

		/// <summary>
		/// Server Program Main
		/// </summary>
		/// <param name="args"></param>
		public static void Main (string[] args) {
			BuildWebHost (args).Run ();
		}

		/// <summary>
		/// Server Program BuildWebHost
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static IWebHost BuildWebHost (string[] args) =>
			WebHost.CreateDefaultBuilder (args)
			.UseConfiguration (new ConfigurationBuilder ()
				.AddCommandLine (args)
				.Build ())
			.UseStartup<Startup> ()
			.Build ();
	}
}