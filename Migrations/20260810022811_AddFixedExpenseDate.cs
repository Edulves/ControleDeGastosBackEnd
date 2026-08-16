using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensesControl.Migrations
{
    /// <inheritdoc />
    public partial class AddFixedExpenseDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "fixed_expense_date",
                table: "fixed_expenses",
                type: "date",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fixed_expense_date",
                table: "fixed_expenses");
        }
    }
}
