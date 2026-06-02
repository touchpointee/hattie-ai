using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HattieAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantTokenUsage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "Tenants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MonthlyTokenLimit",
                table: "Tenants",
                type: "integer",
                nullable: false,
                defaultValue: 100000);

            migrationBuilder.AddColumn<int>(
                name: "MonthlyTokenUsage",
                table: "Tenants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenUsageMonthStartedAt",
                table: "Tenants",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "date_trunc('month', now())");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactPhone",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "MonthlyTokenLimit",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "MonthlyTokenUsage",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "TokenUsageMonthStartedAt",
                table: "Tenants");
        }
    }
}
