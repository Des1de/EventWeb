using EventWeb.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventWeb.DataAccess.Configuration
{
    public class EventConfiguration : BaseEntityConfiguration<Event>
    {
        private const int NAME_MAX_LENGTH = 40; 
        private const int DESCRIPTION_MAX_LENGTH = 600; 
        private const int LOCATION_MAX_LENGTH = 50; 
        private const int IMAGE_URL_MAX_LENGTH = 200; 

        public override void Configure(EntityTypeBuilder<Event> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(NAME_MAX_LENGTH); 
            
            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(DESCRIPTION_MAX_LENGTH); 

            builder.HasOne(e => e.Category)
                .WithMany(e => e.Events)
                .HasForeignKey(e => e.CategoryId)
                .IsRequired(); 

            builder.HasMany(e => e.Participations)
                .WithOne(e => e.Event)
                .HasForeignKey(e => e.EventId)
                .IsRequired(); 

            builder.Property(e => e.Location)
                .HasMaxLength(LOCATION_MAX_LENGTH)
                .IsRequired();

            builder.Property(e => e.EventTime)
                .IsRequired(); 

            builder.Property(e => e.MaxParticipantsNumber)
                .IsRequired(); 

            builder.Property(e => e.ImageUrl)
                .HasMaxLength(IMAGE_URL_MAX_LENGTH)
                .IsRequired(); 
        }
    }
}