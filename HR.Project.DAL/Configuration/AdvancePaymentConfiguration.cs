using HR.Project.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Project.DAL.Configuration
{
    public class AdvancePaymentConfiguration : IEntityTypeConfiguration<AdvancePayment>
    {
        public void Configure(EntityTypeBuilder<AdvancePayment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RequestType);
            builder.Property(x => x.ReplyDate);
            builder.Property(x => x.ApprovalStatus);
            builder.Property(x => x.Currency);
            builder.Property(x => x.ModifiedDate);
            builder.Property(x => x.CreateDate);
            builder.Property(x => x.Description).HasMaxLength(250);

            builder.HasOne(x => x.User).WithMany(x => x.AdvancePayments).HasForeignKey(x => x.UserId);
        }
    }
}
