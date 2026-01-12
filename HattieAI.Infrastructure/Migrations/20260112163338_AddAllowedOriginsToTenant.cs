using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HattieAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAllowedOriginsToTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /* 
            // Column already exists from partial migration failure
            migrationBuilder.AddColumn<List<string>>(
                name: "AllowedOrigins",
                table: "Tenants",
                type: "text[]",
                nullable: false,
                defaultValue: new List<string>());
            */

            migrationBuilder.CreateTable(
                name: "KnowledgeChunks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Embedding = table.Column<float[]>(type: "real[]", nullable: true),
                    TenantId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeChunks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KnowledgeChunks_Tenants_TenantId1",
                        column: x => x.TenantId1,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeChunks_TenantId1",
                table: "KnowledgeChunks",
                column: "TenantId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KnowledgeChunks");

            migrationBuilder.DropColumn(
                name: "AllowedOrigins",
                table: "Tenants");
        }
    }
}
