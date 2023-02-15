using HR.Project.BL.Abstract;
using HR.Project.BL.Services;
using HR.Project.DAL.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace HR.Project.WebUI
{
	public class Program
	{
		public static void Main(string[] args)
		{



			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllersWithViews();
			// Add services to the container.
			builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
			builder.Services.AddScoped(typeof(IGenericService<>), typeof(BaseService<>));

            //builder.Services.AddDbContext<HRProjectContext>(options => options.UseSqlServer("Server=tcp:hr-project-hs6.database.windows.net,1433;Initial Catalog=HRProject;Persist Security Info=False;User ID=hrproject-master;Password=boostHS6;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
            builder.Services.AddDbContext<HRProjectContext>(options => options.UseSqlServer("Server = DESKTOP - VA3Q2CE\\YUNUS; Database = HRDB; uid = sa; TrustServerCertificate = True; Trusted_Connection = True;"));

            

            builder.Services.AddDbContext<HRProjectContext>(option =>
			{
				option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
			options =>
			{
				options.LoginPath = "/Account/Login";    // Yetki istenilen sayfalara girmek istediğimizde bizi yönlendireceği sayfayı belirliyoruz.
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
				  name: "areas",
				  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
				);
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");
			});

			app.Run();
		}
	}
}