using HR.Project.DAL.Configuration;
using HR.Project.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace HR.Project.DAL.Context
{
	public class HRProjectContext : DbContext
	{
		public HRProjectContext(DbContextOptions<HRProjectContext> options) : base(options)
		{


		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=localhost,1433;Database=master;User Id=sa;Password=Str0ngP@ssw0rd!;");

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserEntityConfig());
			modelBuilder.ApplyConfiguration(new AdvancePaymentConfiguration());
			modelBuilder.ApplyConfiguration(new RoleConfig());
			modelBuilder.ApplyConfiguration(new PermissionConfiguration());
			modelBuilder.ApplyConfiguration(new SpendingConfiguration());
			//modelBuilder.Entity<Role>().HasData(new Role
			//{				
			//	RoleName = "SuperAdmin"
			//});
			modelBuilder.Entity<UserEntity>().HasData(
				
				new UserEntity
				{				
					Address = "",
					BirthDate = DateTime.Now,
					BirthLocation = "",
					CreateDate = DateTime.Now,
					Phone = "",
					Department = "IT",
					Email = "yeteke68@gmial.com",
					FirstName = "Yunus Emre",
					LastName = "Teke",
					Gender = Entities.Enum.Gender.Man,
					Id = 1,
					IdentityNumber = "1234567890",
					ImageURL = @"C:\Users\Emre\Downloads\wp2013223-nissan-skyline-gt-r-r34-wallpapers.jpg",
					IsActıve = true,
					Job = "DEv",
					JobEnterDate = DateTime.Now,
					Password = "password",
					UserRoleId = 1
					

                });
		}



		private DbSet<UserEntity> Users { get; set; }
		private DbSet<AdvancePayment> AdvancePayments { get; set; }
		private DbSet<Role> Roles { get; set; }
		private DbSet<Permission> Permissions { get; set; }
		private DbSet<Spending> Spendigs { get; set; }

	}
}
