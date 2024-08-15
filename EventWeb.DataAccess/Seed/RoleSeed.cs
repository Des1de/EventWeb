using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventWeb.DataAccess.Seed
{
    public class RoleSeed : IEntityTypeConfiguration<IdentityRole<Guid>>
    {
        private readonly string[] _roles = ["User", "Admin"];
        public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
        {
            var roles = new IdentityRole<Guid>[_roles.Length];
            for (int i = 0; i < _roles.Length; i++)
            {
                roles[i] = new IdentityRole<Guid>(_roles[i])
                {
                    Id = Guid.NewGuid(),
                    NormalizedName = _roles[i].ToUpper()
                };
            }

            builder.HasData(roles);
        }
    }
}