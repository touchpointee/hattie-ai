using System;
using System.Threading;
using System.Threading.Tasks;
using HattieAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HattieAI.Infrastructure.Persistence
{
    public static class DefaultAdminAccount
    {
        public static readonly Guid Id = Guid.Parse("00000000-0000-0000-0000-000000000001");
        public const string Username = "admin";
        public const string Email = "admin@hattie.ai";
        public const string Password = "admin";
        public const string PasswordHash = "qRib0c+6Qgc6j+1FmRRHs6VyXhdElC5W4eY3EpP3TLkw3Du/";
        public const string Role = "Admin";

        public static async Task EnsureAsync(HattieDbContext dbContext, CancellationToken cancellationToken = default)
        {
            var admin = await dbContext.AppUsers
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(user => user.Username == Username, cancellationToken);

            if (admin == null)
            {
                dbContext.AppUsers.Add(new AppUser
                {
                    Id = Id,
                    Username = Username,
                    Email = Email,
                    PasswordHash = PasswordHash,
                    Role = Role,
                    TenantId = string.Empty,
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                });

                await dbContext.SaveChangesAsync(cancellationToken);
                return;
            }

            var changed = false;

            if (!PasswordSecurity.VerifyPassword(Password, admin.PasswordHash))
            {
                admin.PasswordHash = PasswordHash;
                changed = true;
            }

            if (admin.Email != Email)
            {
                admin.Email = Email;
                changed = true;
            }

            if (admin.Role != Role)
            {
                admin.Role = Role;
                changed = true;
            }

            if (admin.TenantId != string.Empty)
            {
                admin.TenantId = string.Empty;
                changed = true;
            }

            if (changed)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
