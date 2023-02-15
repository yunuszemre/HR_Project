using HR.Project.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Project.DAL.Configuration
{
    public class SpendingConfiguration : IEntityTypeConfiguration<Spending>
    {

        public void Configure(EntityTypeBuilder<Spending> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SpendingDescription).HasMaxLength(500).IsRequired();
            builder.Property(x => x.SpendingAmount).IsRequired(true);
            builder.Property(x => x.SpendingCurrency).IsRequired(true);
            builder.Property(x => x.SpendingType).IsRequired(true);
            builder.Property(x => x.Status).IsRequired(true);
            builder.Property(x => x.CreateDate).IsRequired(true);
            builder.Property(x => x.ReplyDate).IsRequired(false);

            builder.HasOne(x => x.User).WithMany(x => x.Spendings).HasForeignKey(x => x.UserId);

        }

    }
}
