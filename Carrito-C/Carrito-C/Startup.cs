using Carrito_C.Data;
using Carrito_C.Models;
using Microsoft.EntityFrameworkCore;

namespace Carrito_C
{
    public static class Startup
    {
        public static WebApplication InicializarApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);

            var app = builder.Build();
            Configure(app);

            return app;
        }
        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            //builder.Services.AddDbContext<CarritoCContext>(options => options.UseInMemoryDatabase("CarritoDb"));
            builder.Services.AddDbContext<CarritoCContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CarritoDbCS")));

            #region Identity
            builder.Services.AddIdentity<Persona, Rol>().AddEntityFrameworkStores<CarritoCContext>();
            #endregion

            builder.Services.AddControllersWithViews();
        }

        private static void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }

    }
}
