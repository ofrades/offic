using System.Net.Http;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Server {

	/// <summary>
	/// Server Startup
	/// </summary>
	public class Startup {

		/// <summary>
		/// Server Startup Constructor
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration) {
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
		public void ConfigureServices(IServiceCollection services) {
			services.AddControllers();
			services.AddAuthentication(options => {
					options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				})
				.AddCookie()
				.AddGitHub(githubOptions => {
					// githubOptions.ClientId = Configuration["Authentication:GitHub:ClientId"];
					githubOptions.ClientId = System.Environment.GetEnvironmentVariable("ClientId");
					githubOptions.ClientSecret = System.Environment.GetEnvironmentVariable("ClientSecret");
					// githubOptions.ClientSecret = Configuration["Authentication:GitHub:ClientSecret"];
					githubOptions.SaveTokens = true;
					githubOptions.TokenEndpoint = "https://github.com/login/oauth/access_token";
					githubOptions.Scope.Add("public_repo");
					githubOptions.CallbackPath = "/signin";
					githubOptions.ClaimsIssuer = "GitHub";
				});

			services.AddSwaggerGen(options => {
				options.SwaggerDoc(name: "v1", info : new OpenApiInfo { Title = "Offic Service API Version 1", Version = "v1" });
			});
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<AuthorizeClient>();
			services.AddAutoMapper(typeof(Startup));
			services.AddScoped<HttpClient>();
			services.AddAuthorization();
			services.AddServerSideBlazor();
		}

		/// <summary>
		/// Server Startup Configure
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public void Configure(
			IApplicationBuilder app,
			IWebHostEnvironment env
		) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseBlazorDebugging();
			}
			app.UseSwagger();
			app.UseSwaggerUI(c => {
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToClientSideBlazor<Client.Startup>("index.html");
			});
			app.UseClientSideBlazorFiles<Client.Startup>();

		}
	}
}