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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CheckAppContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var secretKey = Environment.GetEnvironmentVariable("WEBSITE_AUTH_SIGNING_KEY");
            _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));            

            var tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = _key,
                ValidAudience = TokenAudience,
                ValidIssuer = TokenIssuer,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            };

            //app.UseJwtBearerAuthentication(new JwtBearerOptions
            //{
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true,
            //    TokenValidationParameters = tokenValidationParameters
            //});

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = "Cookie",
                CookieName = "access_token",
                TicketDataFormat = new CustomJwtDataFormat(SecurityAlgorithms.HmacSha256, tokenValidationParameters)
            });

            // The secret key every token will be signed with.
            // Keep this safe on the server!
            var signingCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            app.UseSimpleTokenProvider(new TokenProviderOptions
            {
                Path = "/api/token",
                Audience = TokenAudience,
                Issuer = TokenIssuer,
                SigningCredentials = signingCredentials,
                IdentityResolver = GetIdentity,
                DbContext = context
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
            
            if (isAuth.Result || isFbAuth.Result)
            {
                return Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }));
            }

            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
