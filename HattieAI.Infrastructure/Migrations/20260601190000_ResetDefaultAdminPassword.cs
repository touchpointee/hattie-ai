using HattieAI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HattieAI.Infrastructure.Migrations
{
    [DbContext(typeof(HattieDbContext))]
    [Migration("20260601190000_ResetDefaultAdminPassword")]
    public partial class ResetDefaultAdminPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                UPDATE "AppUsers"
                SET "PasswordHash" = 'qRib0c+6Qgc6j+1FmRRHs6VyXhdElC5W4eY3EpP3TLkw3Du/'
                WHERE "Id" = '00000000-0000-0000-0000-000000000001';
                """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                UPDATE "AppUsers"
                SET "PasswordHash" = 'A3aFfidnSdLfNj3oJlJ7xsFoeuVjJFU+VYNn90KIunGCf56s'
                WHERE "Id" = '00000000-0000-0000-0000-000000000001';
                """);
        }
    }
}
