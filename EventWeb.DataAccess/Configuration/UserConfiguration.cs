using EventWeb.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventWeb.DataAccess.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        private const int NAME_MAX_LENGTH = 40;
        private const int SURNAME_MAX_LENGTH = 40;
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(NAME_MAX_LENGTH);

            builder.Property(u => u.Surname)
                .IsRequired()
                .HasMaxLength(SURNAME_MAX_LENGTH);

        }
    }
}