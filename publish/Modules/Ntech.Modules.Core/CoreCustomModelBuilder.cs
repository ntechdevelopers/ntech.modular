using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ntech.Core.Server;
using Ntech.Modules.Core.Models;

namespace Ntech.Modules.Core
{
    public class CoreCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Core_User");

            modelBuilder.Entity<Role>()
                .ToTable("Core_Role");

            modelBuilder.Entity<IdentityUserClaim<long>>(b =>
            {
                b.HasKey(uc => uc.Id);
                b.ToTable("Core_UserClaim");
            });

            modelBuilder.Entity<IdentityRoleClaim<long>>(b =>
            {
                b.HasKey(rc => rc.Id);
                b.ToTable("Core_RoleClaim");
            });

            modelBuilder.Entity<IdentityUserRole<long>>(b =>
            {
                b.HasKey(r => new { r.UserId, r.RoleId });
                b.ToTable("Core_UserRole");
            });

            modelBuilder.Entity<IdentityUserLogin<long>>(b =>
            {
                b.HasKey(rc => rc.UserId);
                b.ToTable("Core_UserLogin");
            });

            modelBuilder.Entity<IdentityUserToken<long>>(b =>
            {
                b.HasKey(rc => rc.UserId);
                b.ToTable("Core_UserToken");
            });
        }
    }
}
