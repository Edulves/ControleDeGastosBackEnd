using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensesControl.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserIdDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE daily_expenses
                ALTER COLUMN user_id DROP DEFAULT;
            """);

                    migrationBuilder.Sql("""
                ALTER TABLE fixed_expenses
                ALTER COLUMN user_id DROP DEFAULT;
            """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE daily_expenses
                ALTER COLUMN user_id
                SET DEFAULT 'ee0a2fdd-67eb-443d-bc25-678e2cdf831c';
            """);

                    migrationBuilder.Sql("""
                ALTER TABLE fixed_expenses
                ALTER COLUMN user_id
                SET DEFAULT 'ee0a2fdd-67eb-443d-bc25-678e2cdf831c';
            """);
        }
    }
}
