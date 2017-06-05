using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WhereItMatters.DataAccess;
using WhereItMatters.Core;
using System.Data.Entity;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Newtonsoft.Json;
using Braintree;

namespace WhereItMatters
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddScoped<DbContext>(_ => new DataContext());
            services.AddScoped<IRepository<Donation>, BaseRepository<Donation>>();
            services.AddScoped<DonationRequestRepository, DonationRequestRepository>();
            services.AddScoped<IRepository<Organisation>, BaseRepository<Organisation>>();
            services.AddScoped<IRepository<Mission>, BaseRepository<Mission>>();
            services.AddScoped<IRepository<GalleryItem>, BaseRepository<GalleryItem>>();
            services.AddScoped<LocalizationRepository, LocalizationRepository>();

            // Configure payment
            services.AddScoped<IBraintreeGateway>(_ => new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = "yddcm6yv73tp75pt",
                PublicKey = "drdydcycp5fv4rsy",
                PrivateKey = "0750c2f7f02f6f0b6a5dd85c0dd980b6"
            });

            ImageConfig.Url = "http://whereitmattersdev-admin.azurewebsites.net/images/";
            PdfConfig.Url = "http://whereitmattersdev-admin.azurewebsites.net/pdfs/";
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            PaymentConfig.Currency = "USD";
            PaymentConfig.CurrencyShort = "$";

            SetCulture(app);

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void SetCulture(IApplicationBuilder app)
        {
            var appCulture = new CultureInfo("en-US");
            appCulture.NumberFormat.CurrencyDecimalSeparator = ".";

            var supportedCultures = new List<CultureInfo>
            {
                appCulture
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(options);
        }
    }
}
