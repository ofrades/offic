using Blazorise;
using Blazorise.Bootstrap;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace Client {

	/// <summary>
	/// Client Startup
	/// </summary>
	public class Startup {

		/// <summary>
		/// Client Startup ConfigureServices
		/// </summary>
		/// <param name="services"></param>
		public void ConfigureServices (IServiceCollection services) {
			services.AddAuthorizationCore ();
			services.AddScoped<AuthenticationStateProvider, 
			ServerAuthenticationStateProvider> ();
			services
				.AddBlazorise ()
				.AddBootstrapProviders ();
		}

		/// <summary>
		/// Client Startup Configure
		/// </summary>
		/// <param name="app"></param>
		public void Configure (IComponentsApplicationBuilder app) {
			app.Services
				.UseBootstrapProviders ();
			app.AddComponent<App> ("app");
		}
	}
}