using ExpensesControl.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensesControl.Data.Configurations;

public class FixedExpenseConfiguration : IEntityTypeConfiguration<FixedExpense>
{
    public void Configure(EntityTypeBuilder<FixedExpense> builder)
    {
        builder.HasKey(x => x.FixedExpenseId);

        builder.Property(x => x.Description)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2);

        builder.Property(x => x.IsPaid)
            .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}