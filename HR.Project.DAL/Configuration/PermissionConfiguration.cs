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
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Status).IsRequired(true);
            builder.Property(x=>x.Description).HasMaxLength(500).IsRequired();
            builder.Property(x=>x.CreateDate).IsRequired(true);
            builder.Property(x => x.CreateDate).IsRequired(true);
            builder.Property(x => x.PermissionStartDate).IsRequired(true);
            builder.Property(x => x.PermissionEndDate).IsRequired(true);
            builder.Property(x => x.ReplyDate).IsRequired(false);

            builder.HasOne(x => x.User).WithMany(x => x.Permissions).HasForeignKey(x => x.UserId);

        }
    }
}
