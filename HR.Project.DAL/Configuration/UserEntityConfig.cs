using HR.Project.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Project.DAL.Configuration
{
    public class UserEntityConfig : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.FirstName).HasMaxLength(255);
            builder.Property(x=>x.SecondName).HasMaxLength(255);
            builder.Property(x=>x.LastName).HasMaxLength(255);
            builder.Property(x=>x.SecondLastName).HasMaxLength(255);
            builder.Property(x=>x.BirthDate);
            builder.Property(x=>x.BirthLocation).HasMaxLength(255);
            builder.Property(x=>x.ImageURL);
            builder.Property(x=>x.IdentityNumber).HasMaxLength(11);
            builder.Property(x=>x.JobEnterDate);
            builder.Property(x=>x.JobExitDate);
            builder.Property(x=>x.IsActıve);
            builder.Property(x=>x.Job).HasMaxLength(255);
            builder.Property(x=>x.Department).HasMaxLength(255);
            builder.Property(x=>x.Email).HasMaxLength(255);
            builder.Property(x=>x.Address).HasMaxLength(255);
            builder.Property(x=>x.Phone).HasMaxLength(255);
            builder.Property(x=>x.CreateDate);
            builder.Property(x=>x.ModifiedDate);
            builder.Property(x=>x.Password).HasMaxLength(255);

            builder.HasOne(x => x.UserRole).WithMany(x => x.Users).HasForeignKey(x => x.UserRoleId);
        }
    }
}
