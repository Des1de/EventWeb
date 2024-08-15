using EventWeb.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventWeb.DataAccess.Configuration
{
    public class ParticipationConfiguration : BaseEntityConfiguration<Participation>
    {
        public override void Configure(EntityTypeBuilder<Participation> builder)
        {
            base.Configure(builder);

            builder.HasOne(e => e.Event)
                .WithMany(e => e.Participations)
                .HasForeignKey(e => e.EventId)
                .IsRequired();

            builder.HasOne(e => e.User)
                .WithMany(e => e.Participations)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.EventRegistrationDate)
                .IsRequired(); 
        }
    }
}