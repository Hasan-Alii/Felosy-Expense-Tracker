using Felosy_Expense_Tracker.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Felosy_Expense_Tracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Dependency Injection
            builder.Services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            //Register Syncfusion<sup style="font-size:70%">&reg;</sup> license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NNaF5cXmBCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWXlfcXRQQ2ldU0ByXERWYUA=");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Transactions}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
