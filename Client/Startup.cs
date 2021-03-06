using Blazorise;
using Blazorise.Bootstrap;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Client {

	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			services.AddAuthorizationCore();
			services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
			services
				.AddBlazorise()
				.AddBootstrapProviders();
			services.AddScoped<AppState>();
			services.AddScoped<ListState>();
			services.AddScoped<EditorState>();
		}

		public void Configure(IComponentsApplicationBuilder app) {
			app.Services
				.UseBootstrapProviders();
			app.AddComponent<App>("app");
		}
	}
}