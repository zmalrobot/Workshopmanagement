using AKSoftware.Localization.MultiLanguages;
using MudBlazor;
using MudBlazor.Services;
using System.Globalization;
using System.Reflection;
using Workshopmanagement.Components;
using Workshopmanagement.DatabaseManagment;
using Workshopmanagement.DatabaseManagment.DataObject;
using Workshopmanagement.Utility;
using Workshopmanagement.Utility.Themes;

namespace Workshopmanagement
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            //Dependecy            
            builder.Services.AddLanguageContainer(Assembly.GetExecutingAssembly(), CultureInfo.GetCultureInfo(Environment.GetEnvironmentVariable("WebClientCulture") ?? "it-IT")); //, CultureInfo.GetCultureInfo("it-IT")); //Nel caso non lo traduciamo ma lo vogliamo in Italiano
            builder.Services.AddScoped<IDbDependency, DbDependency>();
            builder.Services.AddScoped<ICamperTheme, VeniceCamperTheme>();
            builder.Services.AddSingleton<ThemeService>();
            builder.Services.AddSingleton<LogSettingsService>();
            builder.Services.AddScoped<IAppLogger, AppLogger>();
            builder.Services.AddMudServices();
            builder.Services.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;

                config.SnackbarConfiguration.PreventDuplicates = true;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });

            //builder.WebHost.UseStaticWebAssets();

            var app = builder.Build();

            // Initialize log level from CompanySettings at startup
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var db = scope.ServiceProvider.GetRequiredService<IDbDependency>();
                    var logSettings = app.Services.GetRequiredService<LogSettingsService>();
                    var settingsList = await db.GetDataAccessCompanySettings().FindAsync(new SearcherCompanySettings());
                    var companySettings = settingsList.FirstOrDefault();
                    if (companySettings != null)
                        logSettings.SetLogLevel(companySettings.LogLevel);
                }
                catch { /* DB may not be available at first startup; default level (Information) will be used */ }
            }

            // Ensure MongoDB indexes exist at startup
            await MongoDbIndexInitializer.EnsureIndexesAsync();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
