using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Windows;
using QuanLySach.Business;
using QuanLySach.Business.Interfaces;
using QuanLySach.Commands;
using QuanLySach.ViewModels;
using QuanLySach.DAL.EF;
using QuanLySach.DAL.EF.Interfaces;
using QuanLySach.DAL.Excel;
using QuanLySach.DAL.Excel.Interfaces;
using QuanLySach.DAL.Interfaces;
using QuanLySach.DAL.WindowRegistry;
using QuanLySach.DAL.WindowRegistry.Interfaces;
using QuanLySach.Settings;
using QuanLySach.DesignTimeDbContextFactory;

namespace QuanLySach
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(cfg =>
                {
                    cfg.SetBasePath(Directory.GetCurrentDirectory());
                    cfg.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((ctx, services) =>
                {
                    // Settings
                    services.AddSingleton<ApplicationSetting>();

                    // Commands
                    services.AddTransient<ImportProductCommand>();
                    services.AddTransient<OpenProductCommand>();
                    services.AddTransient<OpenShoppingCommand>();
                    services.AddTransient<OpenOrderListCommand>();
                    services.AddTransient<OpenSettingCommand>();

                    // ViewModels
                    services.AddSingleton<LoginViewModel>();
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<ProductViewModel>();
                    services.AddTransient<AddProductViewModel>();
                    services.AddTransient<ShoppingCartViewModel>();
                    services.AddTransient<OrderListViewModel>();
                    services.AddSingleton<SettingViewModel>();

                    // Logics
                    services.AddTransient<IAuthenticationLogic, AuthenticationLogic>();
                    services.AddTransient<IAccountLogic, AccountLogic>();
                    services.AddTransient<IUploadProductLogic, UploadProductLogic>();
                    services.AddTransient<IProductLogic, ProductLogic>();
                    services.AddTransient<ICategoryLogic, CategoryLogic>();
                    services.AddTransient<IOrderLogic, OrderLogic>();
                    services.AddTransient<IStatisticLogic, StatisticLogic>();

                    // Data Services
                    var conn = ctx.Configuration.GetConnectionString("QuanLyCafeDatabase");
                    services.AddDbContext<BookDbContext>(o =>
                        o.UseSqlServer(conn, sql => sql.MigrationsAssembly("QuanLySach.DAL.EF")));
                    services.AddSingleton(new BookDbContextFactory(options =>
                        options.UseSqlServer(conn)));

                    services.AddScoped(typeof(IDataService<>), typeof(GenericDataService<>));
                    services.AddScoped(typeof(NonQueryDataService<>));
                    services.AddScoped<IUserDataService, UserDataService>();
                    services.AddScoped<IBookDataService, BookDataService>();
                    services.AddScoped<ICategoryDataService, CategoryDataService>();
                    services.AddScoped<IOrderDataService, OrderDataService>();
                    services.AddScoped<ICustomerDataService, CustomerDataService>();

                    services.AddSingleton<ICredentialDataService, WindowRegistryDataService>();
                    services.AddSingleton<IApplicationSettingDataService, ApplicationSettingDataService>();
                    services.AddScoped<IExcelProductDataService, ExcelDataService>();

                    // Windows
                    services.AddTransient<LoginWindow>();
                    services.AddSingleton<MainWindow>();
                    services.AddTransient<ProductWindow>();
                    services.AddTransient<AddProductWindow>();
                    services.AddTransient<ShoppingCartWindow>();
                    services.AddTransient<OrderListWindow>();
                    services.AddTransient<SettingWindow>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            // Run EF Core migrations
            using (var scope = _host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<BookDbContext>();
                await db.Database.MigrateAsync();
            }

            await _host.Services.EnsureAdminAsync();

            // Show the login window
            var loginWindow = _host.Services.GetRequiredService<LoginWindow>();
            loginWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
