using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using CheckAppCore.Data;
using CheckAppCore.Providers;
using CheckAppCore.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure.Mobile.Server.Login;

namespace CheckAppCore
{
    public partial class Startup
    {
        const string TokenAudience = "https://checkapptest.azurewebsites.net/";
        const string TokenIssuer = "https://checkapptest.azurewebsites.net/";
        
        private SymmetricSecurityKey _key;

        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddDbContext<CheckAppContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Token", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes("Cookie")
                    .RequireAuthenticatedUser().Build());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CheckAppContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var secretKey = env.IsDevelopment() ? "mysupersecretkey!@1234" : Environment.GetEnvironmentVariable("WEBSITE_AUTH_SIGNING_KEY");
            _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));            

            var tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = _key,
                ValidAudience = env.IsDevelopment() ? "localhost" : TokenAudience,
                ValidateAudience = true,
                ValidIssuer = env.IsDevelopment() ? "localhost" : TokenIssuer,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            };

            // The secret key every token will be signed with.
            // Keep this safe on the server!
            var signingCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            app.UseSimpleTokenProvider(new TokenProviderOptions
            {
                Path = "/token",
                Audience = env.IsDevelopment() ? "localhost" : TokenAudience,
                Issuer = env.IsDevelopment() ? "localhost" : TokenIssuer,
                SigningCredentials = signingCredentials,
                IdentityResolver = GetIdentity
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = "Cookie",
                CookieName = "access_token",
                TicketDataFormat = new CustomJwtDataFormat(SecurityAlgorithms.HmacSha256, tokenValidationParameters)
            });

            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
            app.UseMvcWithDefaultRoute();

            DbInitializer.Initialize(context);
        }

        private Task<ClaimsIdentity> GetIdentity(CheckAppContext context, string username, string password, string oauth_id)
        {
            var userRepo = new UserRepository(context);
            var isAuth = userRepo.AuthenticateUser(username, password);
            var isFbAuth = userRepo.AuthenticateFBUser(oauth_id);
            
            if (isAuth || isFbAuth)
            {
                return Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }));
            }

            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
