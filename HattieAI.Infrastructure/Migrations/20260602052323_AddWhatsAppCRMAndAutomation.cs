using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HattieAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWhatsAppCRMAndAutomation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWebsiteChatbotEnabled",
                table: "Tenants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWhatsAppEnabled",
                table: "Tenants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Channel",
                table: "ChatSessions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "ChatSessions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WhatsAppAutomationRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TriggerKeyword = table.Column<string>(type: "text", nullable: false),
                    MatchType = table.Column<string>(type: "text", nullable: false),
                    ReplyText = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhatsAppAutomationRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WhatsAppConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PhoneNumberId = table.Column<string>(type: "text", nullable: false),
                    WabaId = table.Column<string>(type: "text", nullable: true),
                    AccessToken = table.Column<string>(type: "text", nullable: false),
                    VerifyToken = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ConnectedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhatsAppConfigs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WhatsAppAutomationRules");

            migrationBuilder.DropTable(
                name: "WhatsAppConfigs");

            migrationBuilder.DropColumn(
                name: "IsWebsiteChatbotEnabled",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "IsWhatsAppEnabled",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Channel",
                table: "ChatSessions");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "ChatSessions");
        }
    }
}
