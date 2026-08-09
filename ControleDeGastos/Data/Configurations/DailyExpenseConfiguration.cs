using ExpensesControl.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensesControl.Data.Configurations;

public class DailyExpenseConfiguration : IEntityTypeConfiguration<DailyExpense>
{
    public void Configure(EntityTypeBuilder<DailyExpense> builder)
    {
        builder.ToTable("daily_expenses");

        builder.HasKey(x => x.DailyExpenseId);

        builder.Property(x => x.Amount)
            .HasPrecision(18, 2);

        builder.Property(x => x.Note)
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);

        builder.HasOne(x => x.TransactionCategory)
            .WithMany(x => x.DailyExpenses)
            .HasForeignKey(x => x.TransactionCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}