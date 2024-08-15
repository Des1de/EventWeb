using EventWeb.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventWeb.DataAccess.Configuration
{
    public abstract class BaseEntityConfiguration<T> :
        IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreatedAt)
                .IsRequired();

            builder.Property(e => e.ModifiedAt)
                .IsRequired();
        }
    }
}