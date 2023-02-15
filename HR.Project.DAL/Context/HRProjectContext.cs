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
			optionsBuilder.UseSqlServer("Server=DESKTOP-VA3Q2CE\\YUNUS; Database=HRDB; uid=sa; TrustServerCertificate=True;Trusted_Connection=True;");

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserEntityConfig());
			modelBuilder.ApplyConfiguration(new AdvancePaymentConfiguration());
			modelBuilder.ApplyConfiguration(new RoleConfig());
			modelBuilder.ApplyConfiguration(new PermissionConfiguration());
			modelBuilder.ApplyConfiguration(new SpendingConfiguration());
		}


		private DbSet<UserEntity> Users { get; set; }
		private DbSet<AdvancePayment> AdvancePayments { get; set; }
		private DbSet<Role> Roles { get; set; }
		private DbSet<Permission> Permissions { get; set; }
		private DbSet<Spending> Spendigs { get; set; }

	}
}
