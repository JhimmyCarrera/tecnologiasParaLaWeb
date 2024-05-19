﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace WebUtn1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DBContext") ?? throw new InvalidOperationException("Connection string 'DBContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var supportedCultures = new[] { "en-US" };
            var supportedUICultures = new[] { "es-ES" };

            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedUICultures);

            var app = builder.Build();
            app.UseRequestLocalization(localizationOptions);

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
