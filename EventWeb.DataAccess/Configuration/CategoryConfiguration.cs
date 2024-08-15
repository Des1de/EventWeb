using EventWeb.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventWeb.DataAccess.Configuration
{
    public class CategoryConfiguration : BaseEntityConfiguration<Category>
    {
        private const int NAME_MAX_LENGTH = 40; 

        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(NAME_MAX_LENGTH); 
        }
    }
}