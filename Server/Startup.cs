using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared;

namespace Server {

    /// <summary>
    /// Server Startup
    /// </summary>
	public class Startup {

        /// <summary>
        /// Server Startup Constructor
        /// </summary>
        /// <param name="configuration"></param>
		public Startup (IConfiguration configuration) {
			Configuration = configuration;
		}

        /// <summary>
        /// Server Startup Configuration
        /// </summary>
        /// <value></value>
		public IConfiguration Configuration { get; }
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        /// <summary>
        /// Server Startup Configure Services
        /// </summary>
        /// <param name="services"></param>
		public void ConfigureServices (IServiceCollection services) {
			services.AddControllers();
			services.AddAuthentication(options => {
					options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					
				})
				.AddCookie()
				.AddGitHub (githubOptions => {
					githubOptions.ClientId = Configuration["Authentication:GitHub:ClientId"];
					githubOptions.ClientSecret = Configuration["Authentication:GitHub:ClientSecret"];
					githubOptions.SaveTokens = true;
					githubOptions.TokenEndpoint = "https://github.com/login/oauth/access_token";
					githubOptions.CallbackPath = "/signin";
					githubOptions.ClaimsIssuer = "GitHub";
				});
			services.AddAuthorization();
			services.AddServerSideBlazor();
			services.AddTransient<GitReadme>(provider => {return new GitReadme();});
		}

		/// <summary>
		/// Server Startup Configure
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment ()) {
				app.UseDeveloperExceptionPage ();
				app.UseBlazorDebugging ();
			}
			app.UseStaticFiles ();
			app.UseRouting ();
			app.UseAuthentication ();
			app.UseAuthorization ();
			app.UseEndpoints (endpoints => {
				endpoints.MapControllers();
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToClientSideBlazor<Client.Startup> ("index.html");
			});
			app.UseClientSideBlazorFiles<Client.Startup> ();
			
		}
	}
}