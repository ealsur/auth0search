using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using auth0search.Models;
using auth0search.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace auth0search
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)

        {
            services.AddAuthentication(
                options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);
            services.Configure<Auth0Settings>(Configuration.GetSection("Auth0"));
           
           // Configure OIDC
            services.Configure<OpenIdConnectOptions>(options =>
            {
                // Specify Authentication Scheme
                options.AuthenticationScheme = "Auth0";

                // Set the authority to your Auth0 domain
                options.Authority = $"https://{Configuration["auth0:domain"]}";

                // Configure the Auth0 Client ID and Client Secret
                options.ClientId = Configuration["auth0:clientId"];
                options.ClientSecret = Configuration["auth0:clientSecret"];

                // Do not automatically authenticate and challenge
                options.AutomaticAuthenticate = false;
                options.AutomaticChallenge = false;

                // Set response type to code
                options.ResponseType = "code";

                // Set the callback path, so Auth0 will call back to http://localhost:5000/signin-auth0 
                // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard 
                options.CallbackPath = new PathString("/signin-auth0");

                // Configure the Claims Issuer to be Auth0
                options.ClaimsIssuer = "Auth0";
            });

            //Injecting Azure Search service
            services.AddSingleton<ISearchService>(new SearchService(Configuration["search:accountName"],Configuration["search:queryKey"] ));
            
            services.AddMvc();
            services.AddOptions();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<OpenIdConnectOptions> oidcOptions)
        {

            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });
            // Add the OIDC middleware
            app.UseOpenIdConnectAuthentication(oidcOptions.Value);       


            
            app.UseMvcWithDefaultRoute();
        }
    }
}
