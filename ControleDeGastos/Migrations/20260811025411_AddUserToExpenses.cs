using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensesControl.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToExpenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "fixed_expenses",
                type: "text",
                nullable: false,
                defaultValue: "ee0a2fdd-67eb-443d-bc25-678e2cdf831c");

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "daily_expenses",
                type: "text",
                nullable: false,
                defaultValue: "ee0a2fdd-67eb-443d-bc25-678e2cdf831c");

            migrationBuilder.CreateIndex(
                name: "ix_fixed_expenses_user_id",
                table: "fixed_expenses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_daily_expenses_user_id",
                table: "daily_expenses",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_daily_expenses_users_user_id",
                table: "daily_expenses",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_fixed_expenses_users_user_id",
                table: "fixed_expenses",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_daily_expenses_users_user_id",
                table: "daily_expenses");

            migrationBuilder.DropForeignKey(
                name: "fk_fixed_expenses_users_user_id",
                table: "fixed_expenses");

            migrationBuilder.DropIndex(
                name: "ix_fixed_expenses_user_id",
                table: "fixed_expenses");

            migrationBuilder.DropIndex(
                name: "ix_daily_expenses_user_id",
                table: "daily_expenses");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "fixed_expenses");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "daily_expenses");
        }
    }
}
